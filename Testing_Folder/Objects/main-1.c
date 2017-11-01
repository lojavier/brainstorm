#include <c8051f120.h>
#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <string.h>
#include <absacc.h>

#define     SYSTEM_CLOCK                24500000
#define 	BAUD_RATE		            115200
#define		RX_BUFFER_SIZE	            256
#define		TX_BUFFER_SIZE	            256

#define		TX_WAIT_LIMIT				100 * (SYSTEM_CLOCK / 1000000)		// Waiting limit for TI0 and TI1 flags to be set (10 microseconds max)
#define		RX_WAIT_LIMIT				100 * (SYSTEM_CLOCK / 1000000)		// Waiting limit for TI0 and TI1 flags to be set (10 microseconds max)

sfr16   	RCAP2   					= 0xCA;              				// Timer 2 capture/reload
sfr16   	TMR2    					= 0xCC;                 	        // Timer 2
sfr16   	RCAP3   					= 0xCA;                             // Timer 3 reload registers
sfr16   	TMR3    					= 0xCC;                             // Timer 3 counter registers
sfr16   	RCAP4   					= 0xCA;                             // Timer 3 reload registers
sfr16		TMR4						= 0xCC;								// Timer 4
sfr16 		DAC0    					= 0xD2;                 			// DAC0 data
sfr16 		DAC1    					= 0xD2;                 			// DAC1 data
sfr16 		ADC0    					= 0xBE;                 			// ADC0 data
sfr16		PCA0						= 0xF9;								// PCA register

#define 	INTERRUPT_Timer_0 			1
#define 	INTERRUPT_Timer_1 			3
#define 	INTERRUPT_UART_0 			4
#define 	INTERRUPT_Timer_2 			5
#define		INTERRUPT_SMB				7
#define		INTERRUPT_CPA				9
#define		INTERRUPT_COMP0_FALLING		10
#define 	INTERRUPT_COMP0_RISING		11
#define		INTERRUPT_COMP1_FALLING		12
#define		INTERRUPT_COMP1_RISING		13
#define 	INTERRUPT_Timer_3 			14
#define 	INTERRUPT_Timer_4 			16
#define 	INTERRUPT_ADC_0 			15
#define 	INTERRUPT_ADC_2 			18
#define		INTERRUPT_UART_1			20

//-------------------------------------------------------------------------------------------------------
// Global Declarations
//-------------------------------------------------------------------------------------------------------
unsigned char tsByte;

bit splashEnd = 0;
bit screenReset = 0;
bit ackFromScreen = 0;
bit tsCommandReceived = 0;
bit tsCommandTransmitted = 0;

unsigned char tsRxBuffer[RX_BUFFER_SIZE];
unsigned char tsTxBuffer[TX_BUFFER_SIZE];
unsigned char userCommand[RX_BUFFER_SIZE];

unsigned int tsRxIn;
unsigned int tsRxOut;
unsigned int tsTxIn;
unsigned int tsTxOut;

bit tsRxEmpty;
bit tsTxEmpty;
bit tsLastCharGone;

void systemClockInit(void)
{
    char SFRPAGE_SAVE = SFRPAGE;        										// Save Current SFR page
	int i = 0;

	SFRPAGE  = CONFIG_PAGE;

    OSCICN    = 0x83;

	SFRPAGE = SFRPAGE_SAVE;             										// Restore SFRPAGE
}

void portInit(void)
{
	char SFRPAGE_SAVE = SFRPAGE;                                				// Save Current SFR page

   	SFRPAGE = CONFIG_PAGE;                                      				// Set SFR page

	XBR0 = 0x2F;																// Enable UART0, UART1, SPI0, SMB, CEX0 - CEX4
   	XBR1 = 0x01;
    XBR2 = 0xC4;																// Enable crossbar and disable weak pull-up

   	P0MDOUT = 0x01;                                            					// Set TX0 pin to push-pull
																				// TX0 = P0.0; RX0 = P0.1
	P0 = 0xFF;																	// Initialize port P0 latch
	P1 = 0xFF;																	// Initialize port P1 latch
	P2 = 0xFF;																	// Initialize port P2 latch
	P3 = 0xFF;																	// Initialize port P3 latch
	P4 = 0xFF;																	// Initialize port P4 latch
	P5 = 0xFF;																	// Initialize port P5 latch
	P6 = 0xFF;																	// Initialize port P6 latch
	P7 = 0xFF;																	// Initialize port P7 latch

    SFRPAGE = SFRPAGE_SAVE;                                      				// Restore SFR page
}

void enableInterrupts(void)
{
 	IE = 0x92;																	// Enable all interrupts + UART0 + Timer 0
}

void uart0Init(void)
{
   char SFRPAGE_SAVE;

   SFRPAGE_SAVE = SFRPAGE;                                      				// Preserve SFRPAGE

   SFRPAGE = TMR2_PAGE;
   TMR2CN = 0x00;                                               				// Stop timer. Timer 2 in 16-bit auto-reload up timer mode
   TMR2CF = 0x08;                                               				// SYSCLK is time base; no output; up count only
   RCAP2L = 0xF3;                                                               // Low byte
   RCAP2H = 0xFF;                                                               // High byte
   TMR2 = RCAP2;                                                                // Load 16 bit reload value into timer 2
   TMR2CN = 0x04;                                                               // Enable timer 2 (Start timer 2)

   SFRPAGE = UART0_PAGE;
   SCON0 = 0x50;                                                				// 8-bit variable baud rate; 9th bit ignored; RX enabled
   SSTA0 = 0x05;                                                				// Enable baud rate
                                                                				// Use timer 2 as RX and TX baud rate source
   IE = 0x90;                                                                   // Enable all interrupts and UART0 Interrupt

   SFRPAGE = SFRPAGE_SAVE;                                      				// Restore SFRPAGE
}

void disableWatchdog(void)
{
	WDTCN = 0xDE;                       				        				// Disable watchdog timer
    WDTCN = 0xAD;
}

void uart0Interrupt(void) interrupt INTERRUPT_UART_0 using 2
{
   	char SFRPAGE_SAVE = SFRPAGE;
	unsigned int i = 0;
	unsigned long txWaitCounter = 0;

	SFRPAGE = UART0_PAGE;

   	if(RI0 == 1)                                               					// There is a char in SBUF
   	{
     	RI0 = 0;                           			            				// Clear interrupt flag

     	tsByte = SBUF0;                      			            			// Read a character from UART

      	if(tsRxIn < RX_BUFFER_SIZE)												// If buffer size is within limit
      	{
         	if(tsByte != '\r')													// Check end of a command from touch screen
			{
				tsRxBuffer[tsRxIn] = tsByte;									// Store a character in software buffer
			 	tsRxIn++;														// Increment index
			}
         	else 																// If it is CR character, it marks end of command
		 	{
				if(tsRxBuffer[0] == '{')                                        // Splash screen indicator
				{
					if(tsRxBuffer[1] == 'c' && tsRxBuffer[2] == 'm' && tsRxBuffer[3] == 'p' && tsRxBuffer[4] == 'e' && tsRxBuffer[5] == '}')
					{
						splashEnd = 1;                                      	// Detect end of splash screen
						screenReset = 1;										// Screen was reset, so touch screen sends {babe\r}
					}
					else
					{
						splashEnd = 0;                                          // End of splash screen NOT detected
						screenReset = 0;
					}
				}
				else if(tsRxBuffer[0] == '(') 									// It is a command from touch screen controller
				{																// A command starts with '('
					for(i = 0; i < tsRxIn; i++)
					{
					 	userCommand[i] = tsRxBuffer[i];							// Copy to command array for later evaluation
					}
					ackFromScreen = 0;											// This is a command, NOT an ACK
					tsCommandReceived = 1;										// Set flag when a complete command is received
				}
				else if(tsRxBuffer[0] == 'l' && tsRxBuffer[1] == 'p' && tsRxBuffer[2] == '_') 									// It is a command from touch screen controller
				{																// A command starts with '('
					for(i = 0; i < tsRxIn; i++)
					{
					 	userCommand[i] = tsRxBuffer[i];							// Copy to command array for later evaluation
					}
					userCommand[tsRxIn]='\0';
					ackFromScreen = 0;											// This is a command, NOT an ACK
					tsCommandReceived = 1;										// Set flag when a complete command is received
				}
				else															// Not a command from touch screen controller
				{
					ackFromScreen = 1;											// Set a flag to indicate it is an ACK from screen
					tsCommandReceived = 0;										// No need to set flag because it is not a command
				}

				for(i = 0; i < tsRxIn; i++)
				{
				 	tsRxBuffer[i] = '\0';										// Delete all contents
				}

				tsRxOut = 0;													// Reset index Out
				tsRxIn = 0;														// Reset index In
			}
      	}
		else																	// Reset all indexes
		{
			while(tsRxOut < tsRxIn)
			{
				tsRxBuffer[tsRxOut] = '\0';
				tsRxOut++;
			}
			tsRxOut = 0;
			tsRxIn = 0;
			tsCommandReceived = 0;
		}
   }

   if(TI0 == 1)                   					            				// Check if one character is successfully sent out
   {
      	TI0 = 0;                           			            				// Clear interrupt flag

		if(tsTxEmpty == 0)														// TX buffer has something to send
		{
		 	SBUF0 = tsTxBuffer[tsTxOut];										// Send a character in TX buffer
			tsTxOut++;															// Move to next character

			while(TI0 == 0 && txWaitCounter++ < TX_WAIT_LIMIT);					// Wait until completion of transmission TI0 = 1
			if(txWaitCounter >= TX_WAIT_LIMIT)
			{
			 	TI0 = 1;														// TI0 is not set by hardware, set it by software
			}																	// When TI0 is set to 1, this ISR is executed again

			txWaitCounter = 0;													// Reset counter for next execution

			if(tsTxOut >= TX_BUFFER_SIZE)
			{
			 	tsTxOut = 0;													// Reset index to 0
			}

			if(tsTxOut == tsTxIn)												// If two indexes are equal
			{
			 	tsTxEmpty = 1;													// No more character in buffer. Empty
			}
		}
		else
		{
		 	tsLastCharGone = 1;													// Last character has gone. Buffer is empty
		}
   	}

	SFRPAGE = SFRPAGE_SAVE;                                 					// Restore SFR page detector
}

//-------------------------------------------------------------------------------------------------------
// Function Name: sendCommand
// Return Value: None
// Parmeters: s (a string to send)
// Function Description: This function sends a command from the touch screen
//-------------------------------------------------------------------------------------------------------
void sendCommand(const char * s)
{
	char SFRPAGE_SAVE = SFRPAGE;

	while(*s != '\0')															// Search for end of touch screen command in buffer
    {
      	if(tsTxEmpty == 1 || (tsTxOut != tsTxIn))								// Tx is empty or two indexes are not equal
		{
		 	tsTxBuffer[tsTxIn++] = *s;
			if(tsTxIn >= TX_BUFFER_SIZE)										// Check for limit
			{
			 	tsTxIn = 0;														// Reset if limit reached
			}

			if(tsTxEmpty == 1)													// If buffer is empty
			{
			 	tsTxEmpty = 0;													// Now buffer has at leat 1 character, set flag
			}
		}

		s++;																	// Point to next char to send out
    }

	if(tsLastCharGone == 1)														// All characters in buffer has sent out
	{
		tsLastCharGone = 0;														// Reset flag to indicate no char left in buffer
		SFRPAGE = UART0_PAGE;
		TI0 = 1;                                                        		// Set this flage to call ISR to send out one character
	}

	SFRPAGE = SFRPAGE_SAVE;                                      				// Restore SFRPAGE
}

// Control variables
unsigned int funcToRun = 0; // default 0

// Control functions
int get_function_code();
int passcode[3]={0};
void splash_page_load(); // function code == 11
void login_page_load(); // function code == 21
void login_attempts(); // function code == 22
void login_clear_stars(); // function code == 23
void login_disp_1_star(); // function code == 24
void login_disp_2_star(); // function code == 25
void login_disp_3_star(); // function code == 26
void login_disp_4_star(); // function code == 27
void locked_page_load(); // function code == 31
void update_wait_time(); // function code == 32
void login_clear_disp(); // function code == 33

void main()
{
	int i = 0;
    char str[64];

    disableWatchdog();
    systemClockInit();
	portInit();
	enableInterrupts();
	uart0Init();

    tsLastCharGone = 1;
    tsTxOut = tsTxIn = 0;
    tsTxEmpty = 1;

    // load default settings
		sprintf(str, "z\r");
    sendCommand(str);
    sprintf(str, "m load_bar_full\r");
    sendCommand(str);
	while(1)
	{
        if (tsCommandReceived) {
            switch (get_function_code()) {
                case 11: splash_page_load(); break;
                case 21: login_page_load(); break;
                case 22: login_attempts(); break;
                case 23: login_clear_stars(); break;
                case 24: login_disp_1_star(); break;
                case 25: login_disp_2_star(); break;
                case 26: login_disp_3_star(); break;
                case 27: login_disp_4_star(); break;
            }
        }
	}
}

// Splash Page begin ==========

// function code == 11
void splash_page_load() {
    funcToRun = 0;
}
// Splash page end ============

int get_function_code() {
    // TODO, receive commands from screen
		if(passcode[0]==0)
		{
			passcode[0]=userCommand[3]-'0';
			tsCommandReceived=0;
			return 24;
		}
		else if(passcode[1]==0)
		{
			passcode[1]=userCommand[3]-'0';
			tsCommandReceived=0;
			return 25;
		}
		else if(passcode[2]==0)
		{
			passcode[2]=userCommand[3]-'0';
			tsCommandReceived=0;
			return 26;
		}
		else if(passcode[3]==0)
		{
			passcode[3]=userCommand[3]-'0';
			tsCommandReceived=0;
			return 27;
		}
    return 0;
}

// Login Page begin ===========

// function code == 21
void login_page_load() {

}

// function code == 22
void login_attempts() {

}

// function code == 23
void login_clear_stars() {

}

// function code == 24
void login_disp_1_star() {
		char str[64];
    sprintf(str, "xi 15 184 127\r");
    sendCommand(str);
}

// function code == 25
void login_disp_2_star() {
		char str[64];
    sprintf(str, "xi 15 271 127\r");
    sendCommand(str);
}

// function code == 26
void login_disp_3_star() {
		char str[64];
    sprintf(str, "xi 15 358 127\r");
    sendCommand(str);
}

// function code == 27
void login_disp_4_star() {
		char str[64];
    sprintf(str, "xi 15 446 127\r");
    sendCommand(str);
		if(passcode[0]==1 && passcode[1]==2 && passcode[2]==3 && passcode[3]==4)
		{
			sprintf(str, "m main_page\r");
			sendCommand(str);
		}
		else
		{
			sprintf(str, "xi 40 0 0\r");
			sendCommand(str);
			sprintf(str, "w 2000\r");
			sendCommand(str);
			passcode[0]=0;
			passcode[1]=0;
			passcode[2]=0;
			passcode[3]=0;
			sprintf(str, "m display_login_page\r");
			sendCommand(str);
		}
}
// Login Page end =============

// Locked out Page begin ======

// function code == 31
void locked_page_load() {

}

// function code == 32
void update_wait_time() {

}

// function code == 33
void login_clear_disp() {

}
// Locked out Page end ========
