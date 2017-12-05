#include "main.h"

//-------------------------------------------------------------------------------------------------------
// Global Declarations
//-------------------------------------------------------------------------------------------------------
int	SETTINGS_DATE_X	=163;
int	SETTINGS_DATE_Y	=393;
char SETTINGS_DATE_FG[7] = "00FF00";
char SETTINGS_DATE_BG[7] = "000000";
char SETTINGS_TIME_FG[7] = "00FF00";


unsigned char tsByte;

bit splashEnd = 0;
bit screenReset = 0;
bit ackFromScreen = 0;
bit tsCommandReceived = 0;
bit tsCommandTransmitted = 0;
bit SMB_RW;                                                 					// Software flag to indicate Read or Write

unsigned char sharedDataRx[SHARED_DATA_MAX];
unsigned char sharedDataTx[SHARED_DATA_MAX];
unsigned char eepromTx[EEPROM_TX_BUFFER];
unsigned char eepromRx[EEPROM_RX_BUFFER];
unsigned char eepromPageTx[EEPROM_PAGE_SIZE];

unsigned int pageCount;
unsigned int bytesExtra;
unsigned int eepromPageCounter;
unsigned int startAddr;
unsigned char slaveAddr;                                       					// Target SMBus slave address
unsigned char eepromDataByte;

unsigned int numBytesRD;
unsigned int numBytesWR;
unsigned char slaveWriteDone;
unsigned char slaveReadDone;
unsigned char eepromWriteDone;
unsigned char eepromReaddone;
unsigned char rtcWriteDone;
unsigned char rtcReadDone;

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

bit screenChanged;
unsigned char screen;
unsigned char lastScreen;

const char code * Font[] = {/*0*/	"m10B", 
							/*1*/	"m12B",
							/*2*/	"m14B",
							/*3*/	"m16B",
							/*4*/	"m20B",
							/*5*/	"m24B",
							/*6*/	"m32B",
							/*7*/	"m48",
							/*8*/	"m64"};

bit SMB_BUSY = 0;                                               				// Set to claim the bus, clear to free
bit SMB_RW;                                                                     // Software flag to indicate Read or Write

unsigned int startAddr;
unsigned char slaveAddr;                                       					// Target SMBus slave address

unsigned int numBytesRD;
unsigned int numBytesWR;

unsigned char slaveWriteDone;
unsigned char slaveReadDone;
                            
unsigned char roomTemp1;
unsigned char roomTemp2;
unsigned char roomTemp3;
                            
unsigned char seconds;
unsigned char minutes;
unsigned char hours;
unsigned char hours24;
unsigned char amPm;
unsigned char day;
unsigned char date;
unsigned char month;
unsigned char year;
unsigned char century;
unsigned char timeMode;
unsigned char currentIndex = 0;
unsigned char minuteIndex = 0;
unsigned char hourIndex = 0;

unsigned char currentDate;
unsigned char currentMonth;
unsigned char currentYear;

unsigned char realTimeClockItems;

unsigned char adjustedSeconds;
unsigned char adjustedMinutes;
unsigned char adjustedHours;
unsigned char adjustedAmPm;
unsigned char adjustedDay;
unsigned char adjustedDate;
unsigned char adjustedMonth;
unsigned char adjustedYear;
unsigned char adjustedCentury;
unsigned char adjustedTimeMode;

bit monthUpdated;
bit dateUpdated;
bit yearUpdated;
bit hoursUpdated;
bit minutesUpdated;
bit secondsUpdated;
bit amPmUpdated;
bit timeUpdated;
bit monthDateYearUpdated;

const char code * dayOfWeek[] = 	{
									 /*0*/	"SUN",
									 /*1*/	"MON", 
									 /*2*/	"TUE",
									 /*3*/	"WED", 
									 /*4*/	"THU",
									 /*5*/	"FRI", 
									 /*6*/	"SAT"};	

const char code * monthOfYear[] = 	{/*0*/	"NON",
									 /*1*/	"JAN",
									 /*2*/	"FEB", 
									 /*3*/	"MAR",
									 /*4*/	"APR", 
									 /*5*/	"MAY",
									 /*6*/	"JUN", 
									 /*7*/	"JUL",
									 /*8*/	"AUG",
									 /*9*/	"SEP",
									 /*10*/	"OCT", 
									 /*11*/	"NOV",
									 /*12*/	"DEC"};

const char code * clockSetupMsg[] = {/*0*/	"SET DATE AND TIME      ",
									/*1*/	"WRITING DATA... WAIT!  ",
									/*2*/	"DATA SUCCESSFULLY SAVED",
									/*3*/	"ERROR                  "};

//-------------------------------------------------------------------------------------------------------
// System Configurations
//-------------------------------------------------------------------------------------------------------
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
	P1MDOUT = 0x01;																// Set TX1 pin to push-pull, P1.0
																				// TX1 = P1.0; RX1 = P1.1
	P3MDOUT = 0xCA;																// P3.0, P3.2, P3.4, P3.5: open drain; P3.1, P3.3, P3.6, P3.7: push pull
	
	P4MDOUT = 0x0F;																// P4.0, P4.1, P4.2, P4.3: push pull
																				
	P5MDOUT = 0x04;																// P5.0 open drain; P5.1 Open drain; P5.2 Push pull
	
	P6MDOUT = 0x00;
																																							
	P7MDOUT = 0x80;																// Set P7.7 push-pull (smb error line)
	
	P0 = 0xFF;																	// Initialize port P0 latch
	P1 = 0xFF;																	// Initialize port P1 latch
	P2 = 0xFF;																	// Initialize port P2 latch
	P3 = 0xFF;																	// Initialize port P3 latch
	P4 = 0xFF;																	// Initialize port P4 latch
	P5 = 0xFF;																	// Initialize port P5 latch
	P6 = 0xFF;																	// Initialize port P6 latch
	P7 = 0xFF;																	// Initialize port P7 latch

	RHW = 0;																	// Pull low SMB error line

    SFRPAGE = SFRPAGE_SAVE;                                      				// Restore SFR page
}

void enableInterrupts(void)
{
 	IE = 0x92;																	// Enable all interrupts + UART0 + Timer 0
	EIE2 |= 0x01;                                           					// Enable Timer 3 interrupt
	EIE2 |= 0x40;																// Enable UART1 interrupt
	EIE1 |= 0x0A;																// Enable SMBus interrupt
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
				else if(tsRxBuffer[0] == 'l' && tsRxBuffer[1] == 'o' && tsRxBuffer[2] == 'a') 									// It is a command from touch screen controller
				{																// A command starts with '('
					for(i = 0; i < tsRxIn; i++)
					{
					 	userCommand[i] = tsRxBuffer[i];							// Copy to command array for later evaluation
					}
					userCommand[tsRxIn]='\0';
					ackFromScreen = 0;											// This is a command, NOT an ACK
					tsCommandReceived = 1;										// Set flag when a complete command is received
				}
				else if(tsRxBuffer[0] == 's' && tsRxBuffer[1] == 't' && tsRxBuffer[2] == 'a') 									// It is a command from touch screen controller
				{																// A command starts with '('
					for(i = 0; i < tsRxIn; i++)
					{
					 	userCommand[i] = tsRxBuffer[i];							// Copy to command array for later evaluation
					}
					userCommand[tsRxIn]='\0';
					ackFromScreen = 0;											// This is a command, NOT an ACK
					tsCommandReceived = 1;										// Set flag when a complete command is received
				}
				else if(tsRxBuffer[0] == 'g' && tsRxBuffer[1] == 'e' && tsRxBuffer[2] == 't') 									// It is a command from touch screen controller
				{																// A command starts with '('
					for(i = 0; i < tsRxIn; i++)
					{
					 	userCommand[i] = tsRxBuffer[i];							// Copy to command array for later evaluation
					}
					userCommand[tsRxIn]='\0';
					ackFromScreen = 0;											// This is a command, NOT an ACK
					tsCommandReceived = 1;										// Set flag when a complete command is received
				}
				else if(tsRxBuffer[0] == 'l' && tsRxBuffer[1] == '2' && tsRxBuffer[2] == '4') 									// It is a command from touch screen controller
				{																// A command starts with '('
					for(i = 0; i < tsRxIn; i++)
					{
					 	userCommand[i] = tsRxBuffer[i];							// Copy to command array for later evaluation
					}
					userCommand[tsRxIn]='\0';
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
				else if(tsRxBuffer[0] == 'e' && tsRxBuffer[1] == 'n' && tsRxBuffer[2] == 'd') 									// It is a command from touch screen controller
				{																// A command starts with '('
					for(i = 0; i < tsRxIn; i++)
					{
					 	userCommand[i] = tsRxBuffer[i];							// Copy to command array for later evaluation
					}
					userCommand[tsRxIn]='\0';
					ackFromScreen = 0;											// This is a command, NOT an ACK
					tsCommandReceived = 1;										// Set flag when a complete command is received
				}
				else if(tsRxBuffer[0] == 'm' && tsRxBuffer[1] == 'p' && tsRxBuffer[2] == '_') 									// It is a command from touch screen controller
				{																// A command starts with '('
					for(i = 0; i < tsRxIn; i++)
					{
					 	userCommand[i] = tsRxBuffer[i];							// Copy to command array for later evaluation
					}
					userCommand[tsRxIn]='\0';
					ackFromScreen = 0;											// This is a command, NOT an ACK
					tsCommandReceived = 1;										// Set flag when a complete command is received
				}
				else if(tsRxBuffer[0] == 'n' && tsRxBuffer[1] == 's' && tsRxBuffer[2] == '_') 									// It is a command from touch screen controller
				{																// A command starts with '('
					for(i = 0; i < tsRxIn; i++)
					{
					 	userCommand[i] = tsRxBuffer[i];							// Copy to command array for later evaluation
					}
					userCommand[tsRxIn]='\0';
					ackFromScreen = 0;											// This is a command, NOT an ACK
					tsCommandReceived = 1;										// Set flag when a complete command is received
				}
				else if(tsRxBuffer[0] == 'd' && tsRxBuffer[1] == 't' && tsRxBuffer[2] == '_') 									// It is a command from touch screen controller
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
//--------------------------------------------------------------------------------------------------------------------------
signed char bdata slaveFlags1;														// Declare a set of 16 flags on slave
sbit waterPumpErr				= 	slaveFlags1^0;										// Starting byte address = 0x2E
sbit airFlowSensorErr			=	slaveFlags1^1;										// bit address = 0x70
sbit pressureSensorErr			=	slaveFlags1^2;
sbit roomTempSensorErr			=	slaveFlags1^3;
sbit waterInTempSensorErr		=	slaveFlags1^4;
sbit waterOutTempSensorErr		=	slaveFlags1^5;
sbit slaveRestarted				=	slaveFlags1^6;
sbit slaveWatchdogReset			=	slaveFlags1^7;

unsigned char bdata slaveFlags0;	
sbit lowWaterPressure 			= 	slaveFlags0^0;
sbit hiRoomTemp					=	slaveFlags0^1;										// If powerErr and this flag set, power is over 20%. Otherwise, power is under 20%
sbit hiWaterInTemp				=	slaveFlags0^2;										// Set when power has error (hi or low). loHiPowerErr indicates hi or low
sbit hiWaterOutTemp				=	slaveFlags0^3;
sbit hiLaserTemp     			=	slaveFlags0^4;
sbit lowAirFlow					=	slaveFlags0^5;
sbit reserved6					=	slaveFlags0^6;
sbit reserved7					=	slaveFlags0^7;


void checkSlaveFlags(void)
{	
	slaveFlags1 = readOneByteFromSlave(SLAVE_FLAGS_1);
	slaveFlags0 = readOneByteFromSlave(SLAVE_FLAGS_0);
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

//-------------------------------------------------------------------------------------------------------
// Function Name: displayText
// Return Value: None 
// Parmeters: fg, bg, size, message, x, y
// Function Description: This function displays a text on the touch screen
//-------------------------------------------------------------------------------------------------------
void displayText(const char* fg, const char* bg, const unsigned char size, const char* message, const unsigned int x, const unsigned int y)
{
	char str[TS_BUFFER_SIZE];													// String
		
	sprintf(str, "S %s %s\r", fg, bg);											// Set forground and background color
	sendCommand(str);														
	sprintf(str, "f %s\r", Font[size]);											// Set text font
	sendCommand(str);														
	sprintf(str, "t \"%s\" %u %u\r", message, x, y); 							// Display text
	sendCommand(str);														
}

//-------------------------------------------------------------------------------------------------------
// Function Name: showBitmap
// Return Value: None 
// Parmeters: index, x, y (bitmap index and coordinates)
// Function Description: This function displays a bitmap image
//-------------------------------------------------------------------------------------------------------
void showBitmap(const unsigned int index, const unsigned int x, const unsigned int y)
{
	char str[TS_BUFFER_SIZE];

	sprintf(str, "xi %u %u %u\r", index, x, y);									// Bitmap index
	sendCommand(str);
}

//-------------------------------------------------------------------------------------------------------
// Function Name: changeScreen
// Return Value: None 
// Parmeters: screenIndex (macro number)
// Function Description: This function switches to the new screen
//-------------------------------------------------------------------------------------------------------
void changeScreen(const unsigned char screenIndex)
{
	callMacro(screenIndex);														// Change screen		
}

//-------------------------------------------------------------------------------------------------------
// Function Name: callMacro
// Return Value: None 
// Parmeters: macroNumber (macro number in the macro file)
// Function Description: This function calls a macro
//-------------------------------------------------------------------------------------------------------
void callMacro(const unsigned int macroNumber)
{
	char str[TS_BUFFER_SIZE];

	sprintf(str, "m %u\r", macroNumber);										// Execute macro number
	sendCommand(str);
}

//-------------------------------------------------------------------------------------------------------
// Function Name: scanUserInput
// Return Value: None 
// Parmeters: None
// Function Description: This function processes commands from the touch screen
//-------------------------------------------------------------------------------------------------------
void scanUserInput(void)
{	
	int i = 0;

	if(screen == MAIN_PAGE)														// Main screen
	{		 
		if(userCommand[0] == '(')												// Check for an actual command followed by this '(' character
		{
			switch (userCommand[1])			   									// Scan a command type
			{
				case '1':														// Main page
					changeScreen(MAIN_PAGE);									// Stay in main page if main button is pressed again
					break;
				case '2':														// Settings page
					changeScreen(SETTINGS_PAGE);
					break;
				case '3':					   									// Service page
					changeScreen(SERVICE_PAGE);
					break;
				case 'A':
					// Call a function here	or do something here			
					break;
				case 'B':
					// Call a function here	or do something here
					break;
				case 'C':
					// Call a function here	or do something here
					break;	
				default:														// Other options
					break;
				}
		}
		else																	// Not a command, empty buffer with null char
		{
			i = 0;
			while(userCommand[i] != '\0')
			{
				userCommand[i] = '\0';
				i++;
			}
		}	
	}
	else if(screen == SETTINGS_PAGE)											// Settings page
	{
		if(userCommand[0] == '(')
		{
			switch (userCommand[1])
			{
				case '1':
					changeScreen(MAIN_PAGE);
					break;
				case '2':
					changeScreen(SETTINGS_PAGE);
					break;
				case '3':
					changeScreen(SERVICE_PAGE);
					break;
				case 'A':
					// Call a function here	or do something here
					break;
				case 'B':
				   	// Call a function here	or do something here
					break;
				case 'C':
				   	// Call a function here	or do something here
					break;
				default:
					break;
			}
		}
		else																	// Not a command, empty buffer with null
		{
			i = 0;
			while(userCommand[i] != '\0')
			{
				userCommand[i] = '\0';
				i++;
			}
		}				
	}
	else if(screen == SERVICE_PAGE)
	{
		if(userCommand[0] == '(')
		{
			switch (userCommand[1])
			{
				case '1':
					changeScreen(MAIN_PAGE);
					break;
				case '2':
					changeScreen(SETTINGS_PAGE);
					break;
				case '3':
					changeScreen(SERVICE_PAGE);
					break;
				case 'A':
					// Call a function here	or do something here
				case 'B':
					// Call a function here	or do something here
				case 'C':
					// Call a function here	or do something here
				default:
					break;
			}
		}
		else 																	// Not a command, empty buffer with null
		{														   	
			i = 0;
			while(userCommand[i] != '\0')
			{
				userCommand[i] = '\0';
				i++;
			}
		}				
	}	
	else
	{

	}

	i = 0;
	while(userCommand[i] != '\0')
	{
		userCommand[i] = '\0';													// Delete all contents in array
		i++;
	}
}

//-------------------------------------------------------------------------------------------------------
// Function Name: smbInit
// Return Value: None 
// Parmeters: None
// Function Description: This function initializes the SMB bus 
//-------------------------------------------------------------------------------------------------------
void smbInit(void)
{
   	int i;
	unsigned long pollingCounter = 0;
	char SFRPAGE_SAVE = SFRPAGE;

   	SFRPAGE = SMB0_PAGE;
	while(SDA == 0 && pollingCounter++ < SMB_POLLING_LIMIT)							// If slave is holding SDA low because of error or reset
   	{
      	SCL = 0;                                                					// Drive the clock low
      	for(i = 0; i < 255; i++);                               					// Hold the clock low
      	SCL = 1;                                                					// Release the clock
      	while(SCL == 0 && pollingCounter++ < SMB_POLLING_LIMIT);					// Wait for open-drain
      	for(i = 0; i < 10; i++);                                					// Hold the clock high
   	}
			
	SMB0CN = 0x07;                                          						// Assert Acknowledge low (AA bit = 1b);
                                                            						// Enable SMBus Free timeout detect;
	SMB0CR = 267 - (SYSTEM_CLOCK / (8 * SMB_FREQUENCY));							// Derived approximation from the Tlow and Thigh equations
																	
   	SMB0CN |= 0x40;                                         						// Enable SMBus;

   	SFRPAGE = SFRPAGE_SAVE;                                 						// Restore SFR page detector
	
	SMB_BUSY = 0;
                                                                                    // Release SMB
    slaveWriteDone = FALSE;
	slaveReadDone = FALSE;
	eepromWriteDone = FALSE;
	eepromReadDone = FALSE;
	rtcWriteDone = FALSE;
	rtcReadDone = FALSE;
}

//-------------------------------------------------------------------------------------------------------
// Function Name: timer3Init
// Return Value: None 
// Parmeters: None
// Function Description: This function nitializes timer 3 which is used to time out the SMB if errors occur
//-------------------------------------------------------------------------------------------------------
void timer3Init (void)
{
   	char SFRPAGE_SAVE = SFRPAGE;        

   	SFRPAGE = TMR3_PAGE;

   	TMR3CN = 0x00;                                          						// Timer 3 in timer mode
																					// Timer 3 auto reload
   	TMR3CF = 0x00;                                          						// Timer 3 prescaler = 12

   	RCAP3 = -(SYSTEM_CLOCK / 12 / 40);                           					// Timer 3 overflows after 25 ms
   	TMR3 = RCAP3;                                           					

   	TR3 = 1;                                                						// Start Timer 3

   	SFRPAGE = SFRPAGE_SAVE;                                 						// Restore SFR page
}

//-------------------------------------------------------------------------------------------------------
// Function Name: timer3ISR
// Return Value: None 
// Parmeters: None
// Function Description: This function is timer 3 ISR which is used to reset the SMB bus if the clock line is held for too long
//-------------------------------------------------------------------------------------------------------
void timer3ISR(void) interrupt INTERRUPT_Timer_3
{
   	char SFRPAGE_SAVE = SFRPAGE;                             						// Save Current SFR page

   	SFRPAGE = SMB0_PAGE;
   	SMB0CN &= ~0x40;                                         						// Disable SMBus
   	SMB0CN |= 0x40;                                          						// Re-enable SMBus

   	SFRPAGE = SFRPAGE_SAVE;                                  						// Switch back to the Timer3 SFRPAGE
   	TF3 = 0;                                                 						// Clear Timer3 interrupt-pending flag
   	SMB_BUSY = 0;                                            						// Free bus
   
   	SFRPAGE = SFRPAGE_SAVE;                                 						// Restore SFR page detector
}

//-------------------------------------------------------------------------------------------------------
// Function Name: writeOneByteToSlave
// Return Value: None 
// Parmeters: target, startAddr, content
// Function Description: This function writes one to the slave microprocessor
//-------------------------------------------------------------------------------------------------------
void writeOneByteToSlave(unsigned char startAddr, unsigned char content)
{   		
	sharedDataTx[startAddr] = content;
	smbWrite(MCU_SLAVE_ADDR, startAddr, 1);
}

//-------------------------------------------------------------------------------------------------------
// Function Name: readOneByteFromSlave
// Return Value: long 
// Parmeters: startAddr, bytes
// Function Description: This function reads one from the slave microprocessor
//-------------------------------------------------------------------------------------------------------
unsigned char readOneByteFromSlave(unsigned char startAddr)
{
	smbRead(MCU_SLAVE_ADDR, startAddr, 1);
	return sharedDataRx[startAddr];																								
}

//-------------------------------------------------------------------------------------------------------
// Function Name: writeBytesToRealTimeClock
// Return Value: None 
// Parmeters: target, startAddr, bytes
// Function Description: This function writes data byte to the real time clock DS3232
//-------------------------------------------------------------------------------------------------------
void writeBytesToRealTimeClock(unsigned char startAddr, unsigned char numBytes)
{
	smbWrite(REAL_TIME_CLOCK_ADDR, startAddr, numBytes);
}

//-------------------------------------------------------------------------------------------------------
// Function Name: readBytesFromRealTimeClock
// Return Value: None 
// Parmeters: target, startAddr, bytes
// Function Description: This function reads data byte from the real time clock DS3232
//-------------------------------------------------------------------------------------------------------
void readBytesFromRealTimeClock(unsigned char startAddr, unsigned char numBytes)
{
	smbRead(REAL_TIME_CLOCK_ADDR, startAddr, numBytes);
}

//-------------------------------------------------------------------------------------------------------
// Function Name: smbRead
// Return Value: unsigned char * 
// Parmeters: target, startAddr, bytes
// Function Description: This function reads from SM bus
//-------------------------------------------------------------------------------------------------------
void smbRead(unsigned char deviceId, unsigned int location, unsigned int bytes)
{
	char SFRPAGE_SAVE = SFRPAGE;

	SFRPAGE = SMB0_PAGE;
	
	while(BUSY || SMB_BUSY);				                                        // Wait for free SMB

	SFRPAGE = SFRPAGE_SAVE;
		
    switch(deviceId)
    {
        case MCU_SLAVE_ADDR:
        case REAL_TIME_CLOCK_ADDR:
        case EEPROM_ADDR:
            smbWrite(deviceId, location, 0);									    // Write address before reading
            break;
        default:
            break;	
    }

    SFRPAGE = SMB0_PAGE;
		
    while(BUSY || SMB_BUSY);
    slaveAddr = deviceId;                                        			        // Address of MCU slave
    startAddr = location;													        // Starting address to read from slave
    numBytesRD = bytes;														        // Number of bytes to read
    
    SMB_BUSY = 1;                                           				        // Claim SMBus (set to busy)
    SMB_RW = 1;                                             				        // Mark this transfer as a READ
    STA = 1;

	while(BUSY || SMB_BUSY);		                                                // Wait for SMB
		
	SFRPAGE = SFRPAGE_SAVE;
			
    switch(deviceId)
    {
        case MCU_SLAVE_ADDR:
            while(slaveReadDone == 0);		                                        // Wait until slave write completed
            break;
        case REAL_TIME_CLOCK_ADDR:
			while(rtcReadDone == 0);		                                        // Wait until real time clock write completed or timeout occurs
			break;
        case EEPROM_ADDR:
            while(eepromReadDone == 0);		                                        // Wait until EEPROM write completed
            break;
        default:
            break;	
    }	 
}

//-------------------------------------------------------------------------------------------------------
// Function Name: smbWrite
// Return Value: unsigned char * 
// Parmeters: target, startAddr, bytes
// Function Description: This function reads to SM bus
//-------------------------------------------------------------------------------------------------------
void smbWrite(unsigned char deviceId, unsigned int location, unsigned int bytes)
{
	unsigned char i = 0;
	unsigned int pageWrittenDelay = 0;
	char SFRPAGE_SAVE = SFRPAGE;
		
	SFRPAGE = SMB0_PAGE;
	
	while(BUSY || SMB_BUSY);				                                        // Wait for SMB to be free
    slaveAddr = deviceId;                                        				    // Address of MCU slave board
    startAddr = location;														    // Starting address to write to slave

    switch(deviceId)
    {
        case MCU_SLAVE_ADDR:													    // Pass through
        case DEVICE_DUMP_ADDR:													    // Pass through
            numBytesWR = bytes;													    // Number of bytes to read
            SMB_BUSY = 1;                                           			    // Claim SMBus (set to busy)
            SMB_RW = 0;                                             			    // Mark this transfer as a WRITE
            STA = 1;                                                			    // Start transfer
            while(slaveWriteDone == 0);	                                            // Wait until SRAM write completed or timeout occurs
            break;
        case REAL_TIME_CLOCK_ADDR:
            numBytesWR = bytes;													    // Number of bytes to read
            SMB_BUSY = 1;                                           			    // Claim SMBus (set to busy)
            SMB_RW = 0;                                             			    // Mark this transfer as a WRITE
            STA = 1;                                                			    // Start transfer
            while(rtcWriteDone == 0);	                                            // Wait until SRAM write completed or timeout occurs
            break;
        default:
            break;		
	}

	SFRPAGE = SFRPAGE_SAVE;															// Restore SFR page
}

//-------------------------------------------------------------------------------------------------------
// Function Name: smbISR
// Return Value: None 
// Parmeters: None
// Function Description: 
// SMBus Interrupt Service Routine (ISR)
// Anytime the SDA is pulled low by the master, this ISR will be called. For example, if STA = 1,
// this ISR is called and SMB0STA = SMB_START = SMB_REPEAT_START. These cases are executed within the switch statement.
//-------------------------------------------------------------------------------------------------------
void smbISR (void) interrupt INTERRUPT_SMB using 2
{
   	bit FAIL = 0;                                            						// Used by the ISR to flag failed transfers
   	static unsigned int TxCounter;													// Initialize counter
   	static unsigned int RxCounter;													// Initialize counter
	static unsigned int slaveCount = 0;
	static unsigned int realTimeClockCount = 0;
	static unsigned int eepromCount = 0;
	static unsigned char eepromAddrDone;
	
	switch (SMB0STA >> 3)															// Check SMB bus status
   	{
//-------------------------------------------------------------------------------------------------------
// Master Transmitter/Receiver: START condition transmitted. Load SMB0DAT with slave device address
//-------------------------------------------------------------------------------------------------------
      	case SMB_START:																// Master initiates a transfer

//-------------------------------------------------------------------------------------------------------
// Master Transmitter/Receiver: repeated START condition transmitted. Load SMB0DAT with slave device address
//-------------------------------------------------------------------------------------------------------
      	case SMB_REPEAT_START:
		 	SMB0DAT = slaveAddr;                                  					// Load address of the slave.
         	SMB0DAT &= 0xFE;                                   						// Clear the LSB of the address for the R/W bit
         	SMB0DAT |= SMB_RW;                                 						// Load R/W bit (Read = 1; Write = 0)
			STA = 0;                                           						// Manually clear STA bit

         	RxCounter = 0;                                     						// Reset the counter
         	TxCounter = 0;                                     						// Reset the counter
			eepromAddrDone = CLEAR;													// For 2 byte EEPROM address	
         	
			break;

//-------------------------------------------------------------------------------------------------------
// Master Transmitter: Slave address + WRITE transmitted.  ACK received. For a READ: N/A
// For a WRITE: Send the first data byte to the slave
//-------------------------------------------------------------------------------------------------------
      	case SMB_ADDR_W_TX_ACK_RX:
			if(slaveAddr == MCU_SLAVE_ADDR)
			{
				if(startAddr == DEVICE_DUMP_ADDR)									// Dump device address to check slave presence only
				{
					STO = 1;														// Stop this transfer
					SMB_BUSY = 0;													// Releas SMB
				}
				else
				{
					SMB0DAT = startAddr;											// Send 1 byte address to slave
					slaveWriteDone = 0;												// Mark start of slave write
				}
			}
            else if(slaveAddr == REAL_TIME_CLOCK_ADDR)
			{
				SMB0DAT = startAddr;												// Point to byte address to write on real time clock
				rtcWriteDone = 0;													// Mark start of rtc write
			}
            else{}
         	break;

//-------------------------------------------------------------------------------------------------------
// Master Transmitter: Slave address + WRITE transmitted.  NACK received. Restart the transfer
//-------------------------------------------------------------------------------------------------------
      	case SMB_ADDR_W_TX_NACK_RX:
			if(slaveAddr == MCU_SLAVE_ADDR)
			{
				if(slaveCount < MAX_NACK_RETRY)
				{
				 	slaveCount++;													// Increment number of attempts when NACK is received
					STA = 1;														// Restart a new transfer
				}
				else
				{
					slaveCount = 0;													// Reset this counter to keep retry seeking slave response
					slaveWriteDone = 1;
					STO = 1;
					SMB_BUSY = 0;
					FAIL = 1;
				}	
			}
            else if(slaveAddr == REAL_TIME_CLOCK_ADDR)
			{
				if(realTimeClockCount < MAX_NACK_RETRY)
				{
				 	realTimeClockCount++;											// Increment number of attempts when NACK is received
					STA = 1;														// Restart a new transfer
				}
				else
				{
					realTimeClockCount = 0;
					rtcWriteDone = 1;
					STO = 1;
					SMB_BUSY = 0;
					FAIL = 1;
				}	
			}
			else if(slaveAddr == EEPROM_ADDR)
			{
				if(eepromCount < MAX_NACK_RETRY)
				{
				 	eepromCount++;													// Increment number of attempts when NACK is received
					STA = 1;														// Restart a new transfer
				}
				else
				{
					eepromCount = 0;
					eepromWriteDone = 1;
					STO = 1;
					SMB_BUSY = 0;
					FAIL = 1;
				}	
			}
			else
			{}

         	break;

//-------------------------------------------------------------------------------------------------------
//Master Transmitter: Data byte transmitted.  ACK received. For a READ: N/A
//For a WRITE: Send all data.  After the last data byte, send the stop bit
//-------------------------------------------------------------------------------------------------------
      	case SMB_DATA_TX_ACK_RX:
         	if(slaveAddr == MCU_SLAVE_ADDR)
			{
				if(TxCounter < numBytesWR)
         		{
					SMB0DAT = sharedDataTx[startAddr + TxCounter];					// Send data byte
            		TxCounter++;
         		}
         		else
         		{
            		STO = 1;                                        				// Set STO to terminate transfer												
            		SMB_BUSY = 0;                                   				// And free SMBus interface
					slaveWriteDone = 1;												// Mark end of slave write
         		}
			}
            else if(slaveAddr == REAL_TIME_CLOCK_ADDR)
			{
				if(TxCounter < numBytesWR)
         		{
					SMB0DAT = sharedDataTx[startAddr + TxCounter];					// Send data byte
            		TxCounter++;
         		}
         		else
         		{
            		STO = 1;                                        				// Set STO to terminate transfer												
            		SMB_BUSY = 0;                                   				// And free SMBus interface
					rtcWriteDone = 1;												// Mark end of slave write
         		}
			}
         	break;

//-------------------------------------------------------------------------------------------------------
// Master Transmitter: Data byte transmitted.  NACK received. Restart the transfer
//-------------------------------------------------------------------------------------------------------
      	case SMB_DATA_TX_NACK_RX:
			if(slaveAddr == MCU_SLAVE_ADDR)
			{
				if(slaveCount < MAX_NACK_RETRY)
				{
				 	slaveCount++;													// Increment number of attempts when NACK is received
					STA = 1;														// Restart a new transfer
				}
				else
				{
					slaveCount = 0;
					STO = 1;
					SMB_BUSY = 0;
					FAIL = 1;
				}	
			}
            else if(slaveAddr == REAL_TIME_CLOCK_ADDR)
			{
				if(realTimeClockCount < MAX_NACK_RETRY)
				{
				 	realTimeClockCount++;											// Increment number of attempts when NACK is received
					STA = 1;														// Restart a new transfer
				}
				else
				{
					realTimeClockCount = 0;
					STO = 1;
					SMB_BUSY = 0;
					FAIL = 1;
				}	
			}
			else if(slaveAddr == EEPROM_ADDR)
			{
				if(eepromCount < MAX_NACK_RETRY)
				{
				 	eepromCount++;													// Increment number of attempts when NACK is received
					STA = 1;														// Restart a new transfer
				}
				else
				{
					eepromCount = 0;
					STO = 1;
					SMB_BUSY = 0;
					FAIL = 1;
				}	
			}
			else
			{}
         	break;

//-------------------------------------------------------------------------------------------------------
// Master Receiver: Slave address + READ transmitted.  ACK received. 
// For a READ: check if this is a one-byte transfer. if so, set the NACK after the data byte
// is received to end the transfer. if not, set the ACK and receive the other data bytes
//-------------------------------------------------------------------------------------------------------
      	case SMB_ADDR_R_TX_ACK_RX:
         	if(numBytesRD == 1)														// If there is one byte to transfer, send a NACK and go to
         	{																		// SMB_DATA_RX_NACK_TX case to accept data from slave
            	AA = 0;                                         					// Clear AA flag before data byte is received
                                                            						// send NACK signal to slave after byte is received
         	}
         	else
         	{
            	AA = 1;                                         					// More than one byte in this transfer,
                                                            						// send ACK after byte is received
         	}
         	break;

//-------------------------------------------------------------------------------------------------------
// Master Receiver: Slave address + READ transmitted.  NACK received. Restart the transfer
//-------------------------------------------------------------------------------------------------------
      	case SMB_ADDR_R_TX_NACK_RX:
			if(slaveAddr == MCU_SLAVE_ADDR)
			{
				if(slaveCount < MAX_NACK_RETRY)
				{
				 	slaveCount++;													// Increment number of attempts when NACK is received
					STA = 1;														// Restart transfer after receiving a NACK
				}
				else
				{
					slaveCount = 0;													// Reset counter
					STO = 1;
					SMB_BUSY = 0;
					FAIL = 1;
				}	
			}
            else if(slaveAddr == REAL_TIME_CLOCK_ADDR)
			{
				if(realTimeClockCount < MAX_NACK_RETRY)
				{
				 	realTimeClockCount++;											// Increment number of attempts when NACK is received
					STA = 1;														// Restart a new transfer
				}
				else
				{
					realTimeClockCount = 0;
					STO = 1;
					SMB_BUSY = 0;
					FAIL = 1;
				}	
			}
			else if(slaveAddr == EEPROM_ADDR)
			{
				if(eepromCount < MAX_NACK_RETRY)
				{
				 	eepromCount++;													// Increment number of attempts when NACK is received
					STA = 1;														// Restart a new transfer
				}
				else
				{
					eepromCount = 0;
					STO = 1;
					SMB_BUSY = 0;
					FAIL = 1;
				}	
			}
			else
			{}
         	break;

//-------------------------------------------------------------------------------------------------------
// Master Receiver: Data byte received. If AA flag was set beforehand, then ACK transmitted.
// For a READ: receive each byte from the slave.  if this is the last byte, send a NACK and set the STOP bit
//-------------------------------------------------------------------------------------------------------
      	case SMB_DATA_RX_ACK_TX:
			
			if(slaveAddr == MCU_SLAVE_ADDR)
			{
				if (RxCounter < numBytesRD)
         		{
					sharedDataRx[startAddr + RxCounter] = SMB0DAT;
            		AA = 1;                                         				// Send ACK to indicate byte received
            		RxCounter++;                                    				// Increment the byte counter
					slaveReadDone = 0;												// Mark start of slave read
         		}
         		else
         		{
            		AA = 0;                                         				// Send NACK to indicate last byte is received
					slaveReadDone = 1;												// Mark end of slave read
         		}
			}
            else if(slaveAddr == REAL_TIME_CLOCK_ADDR)
			{
				if(RxCounter < numBytesRD)
				{
					sharedDataRx[startAddr + RxCounter] = SMB0DAT;					// RTC
					rtcReadDone = 0;
					
					AA = 1;															// Send ACK to indicate byte received
					RxCounter++;													// Increment the byte counter
				}
				else
				{
					rtcReadDone = 1;
					AA = 0;															// Send NACK to indicate last byte is received
				}
			}
			else if(slaveAddr == EEPROM_ADDR)
			{
				if(RxCounter < numBytesRD)
				{
					eepromDataByte = eepromRx[RxCounter] = SMB0DAT;
					AA = 1;															// Send ACK to indicate byte received
					RxCounter++;													// Increment the byte counter
					eepromReadDone = 0;												// Mark start of fram read
				}
				else
				{
				 	AA = 0;															// Send NACK to indicate last byte is received
					eepromReadDone = 1;												// Mark end of fram read
				}
			}
         	break;

//-------------------------------------------------------------------------------------------------------
// Master Receiver: Data byte received. If AA flag was cleared, then NACK transmitted.
// For a READ: Read operation has completed.  Read data register and send STOP
//-------------------------------------------------------------------------------------------------------
      	case SMB_DATA_RX_NACK_TX:
		 	if(slaveAddr == MCU_SLAVE_ADDR)
			{
				sharedDataRx[startAddr + RxCounter] = SMB0DAT;
         		STO = 1;															// Stop transfer
         		SMB_BUSY = 0;														// Release SMB
         		AA = 1;
				slaveReadDone = 1;													// Mark end of slave read																// Set AA for next transfer                                          				
			}
            else if(slaveAddr == REAL_TIME_CLOCK_ADDR)
			{
				sharedDataRx[startAddr + RxCounter] = SMB0DAT;						// RTC
				rtcReadDone = 1;

				STO = 1;															// Stop transfer
				SMB_BUSY = 0;														// Release SMB
				AA = 1;																// Send ACK to indicate byte received
			}
			else if(slaveAddr == EEPROM_ADDR)
			{
				eepromRx[RxCounter] = SMB0DAT;
				STO = 1;															// Stop transfer
				SMB_BUSY = 0;														// Release SMB
				AA = 1;
				eepromReadDone = 1;													// Mark end of eeprom read      
			}
         	break;

//-------------------------------------------------------------------------------------------------------
// Master Transmitter: Arbitration lost
//-------------------------------------------------------------------------------------------------------
      	case SMB_ARBITRATION_LOST:
         	FAIL = 1;                                          						// Indicate failed transfer
                                                            						// and handle at end of ISR
         	break;

//-------------------------------------------------------------------------------------------------------
// All other status codes invalid.  Reset communication
//-------------------------------------------------------------------------------------------------------
      	default:
         	FAIL = 1;
         	break;
   }
//-------------------------------------------------------------------------------------------------------
// If all failed, reset everything
//-------------------------------------------------------------------------------------------------------
   	if(FAIL)                                                						// If the transfer failed,
   	{
      	SMB0CN &= ~0x40;                                      						// Reset communication
      	SMB0CN |= 0x40;
      	STA = 0;
      	STO = 0;
      	AA = 0;

      	SMB_BUSY = 0;                                         						// Free SMBus

      	FAIL = 0;
																					// Set to finish all pending processes
		slaveWriteDone = 1;															// Mark end of slave write
		slaveReadDone = 1;															// Mark end of slave read
        rtcWriteDone = 1;
		rtcReadDone = 1;
		eepromWriteDone = 1;														// Mark end of eeprom write
		eepromReadDone = 1;															// Mark end of eeprom read
	}

	SI = 0;                                                  						// Clear interrupt flag
}

//-------------------------------------------------------------------------------------------------------
// Function Name: setClockControl
// Return Value: None 
// Parmeters: None
// Function Description: This function sets values to the real time clock control register
// Check DS3232 documentation for more details of this this control register
//-------------------------------------------------------------------------------------------------------
void setClockControl(void)
{
	sharedDataTx[RTC_START_ADDR + RTC_CONTROL] = 0x04;
	sharedDataTx[RTC_START_ADDR + RTC_CONTROL_STATUS] = 0x30; 
	writeBytesToRealTimeClock(RTC_START_ADDR + RTC_CONTROL, 2);							// Set control registers (2 byte)
}
								  
//-------------------------------------------------------------------------------------------------------
// Function Name: setClock
// Return Value: None 
// Parmeters: None
// Function Description: This function sets the new values to the real time clock
//-------------------------------------------------------------------------------------------------------
void setClock(void)
{	
	unsigned char hoursAux;
	
	sharedDataTx[RTC_START_ADDR + SECONDS] = convertDecimalToBCD(seconds);				// Load all data from PC to array before writing
	sharedDataTx[RTC_START_ADDR + MINUTES] = convertDecimalToBCD(minutes);				// All data: seconds,.... are already converted to BCD
	sharedDataTx[RTC_START_ADDR + DAY] = convertDecimalToBCD(day);
	sharedDataTx[RTC_START_ADDR + DATE] = convertDecimalToBCD(date);
	sharedDataTx[RTC_START_ADDR + MONTH] = convertDecimalToBCD(month);
	sharedDataTx[RTC_START_ADDR + YEAR] = convertDecimalToBCD(year);
	
	hoursAux = convertDecimalToBCD(hours);												// Get updated hours from user on screen

	hoursAux = hoursAux | 0x40;															// Set 12/24 bit --> 12 hour mode
	
	if(amPm == 'P')
	{
		 hoursAux = hoursAux | 0x60;													// Set AM/PM bit --> PM mode
	}
	else if(amPm == 'A')
	{
		 hoursAux = hoursAux & 0x5F;													// Clear AM/PM bit --> AM mode
	}
	
	sharedDataTx[RTC_START_ADDR + HOURS] = hoursAux;
	
	writeBytesToRealTimeClock(RTC_START_ADDR, 7);                                       // Write 7 bytes to RTC

	//resetAllDisplayCounters();															// Update new values on screen
}			

//-------------------------------------------------------------------------------------------------------
// Function Name: getClockData()
// Return Value: None 
// Parmeters: None
// Function Description: This function gets the new values from the real time clock
//-------------------------------------------------------------------------------------------------------
void getClockData()
{
	static unsigned char previousMonth = 0;
	static unsigned char previousDate = 0;
	static unsigned char previousYear = 0;
	static unsigned char previousHours = 0;
	static unsigned char previousMinutes = 0;
	static unsigned char previousSeconds = 0;
	
	unsigned int currentIndex = 0;
	unsigned char tempHours;
	
	readBytesFromRealTimeClock(RTC_START_ADDR, 7);										// Get date and time (7 bytes)

	seconds = convertBCDToDecimal(sharedDataRx[RTC_START_ADDR + SECONDS]);				// Convert BCD to decimal for seconds (1 byte)
	minutes = convertBCDToDecimal(sharedDataRx[RTC_START_ADDR + MINUTES]);				// Convert BCD to decimal for minutes (1 byte)

	tempHours = sharedDataRx[RTC_START_ADDR + HOURS];									// Get BCD without conversion and check AM/PM

	if(tempHours & 0x40)																// 12/24 bit (bit 6) is set --> 12 hour mode
	{
	 	timeMode = TWELVE_HR_MODE;

		if(tempHours & 0x20)															// PM/AM bit (bit 5) is set --> PM
		{
		 	amPm = 'P';
		}
		else
		{
		    amPm = 'A';
		}
	}
	else
	{
	 	timeMode = TWENTY_FOUR_HR_MODE;
	}

	hours = convertBCDToDecimal(tempHours & 0x1F);										// Get hours (bit 4 to bit 0 only. Ignore others)

	if(hours == 12)
	{
	 	if(amPm == 'A')
		{
			hours24 = 0;
		}
		else
		{
		 	hours24 = hours;
		}	
	}
	else
	{
		if(amPm == 'P')
		{
			hours24 = hours + 12;														// Twenty four hour format if PM
		}
		else
		{
		 	hours24 = hours;															// Twenty four hour format if AM
		}
	}

	day = convertBCDToDecimal(sharedDataRx[RTC_START_ADDR + DAY]);						// Convert BCD to decimal for day (1 byte)
	date = convertBCDToDecimal(sharedDataRx[RTC_START_ADDR + DATE]);					// Convert BCD to decimal for date (1 byte)
	month = convertBCDToDecimal(sharedDataRx[RTC_START_ADDR + MONTH]);					// Convert BCD to decimal for month (1 byte)
	year = convertBCDToDecimal(sharedDataRx[RTC_START_ADDR + YEAR]);					// Convert BCD to decimal for year (1 byte)

	if(month != previousMonth || date != previousDate || year != previousYear)
	{
		monthDateYearUpdated = SET; 	
	}
	if(hours != previousHours || minutes != previousMinutes || seconds != previousSeconds)
	{
	 	timeUpdated = SET;
	}

	if(amPm == 'P')
	{
	 	currentIndex = ((12 + hours) * 60) + minutes;
	}
	else if(amPm == 'A')
	{
	 	if(hours == 12)
		{
		 	hours = 0;
		}
		
		currentIndex = (hours * 60) + minutes;
	}
	else
	{}

	minuteIndex = currentIndex % 60;													// Get minute index from 0 - 59
	hourIndex = currentIndex / 60;														// Get hour index from 0 - 23

	previousDate = date;
	previousMonth = month;
	previousYear = year;
	previousHours = hours;
	previousMinutes = minutes;
	previousSeconds = seconds;
}

//-------------------------------------------------------------------------------------------------------
// Function Name: moveDateTimeUp
// Return Value: None 
// Parmeters: None
// Function Description: This function sets date and time of the clock from the touch screen by pressing the up button
//-------------------------------------------------------------------------------------------------------
void moveDateTimeUp(void)
{
 	char str[SPRINTF_SIZE];
	
	if(realTimeClockItems == MONTH_ADJUST)
	{
		adjustedMonth++;
		monthUpdated = SET;

		if(adjustedMonth > 12)
		{
		 	adjustedMonth = 1;															// Roll over
		}

		sprintf(str, "%s ", monthOfYear[adjustedMonth]);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == DATE_ADJUST)
	{
		adjustedDate++;
		dateUpdated = SET;

		if(adjustedDate > 31)
		{
		 	adjustedDate = 1;															// Roll over
		}

		sprintf(str, "%bu   ", adjustedDate);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == YEAR_ADJUST)
	{
		adjustedYear++;
		yearUpdated = SET;

		if(adjustedYear > 99)
		{
		 	adjustedYear = 99;
		}

		sprintf(str, "20%02bu", adjustedYear);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == HOUR_ADJUST)
	{
		adjustedHours++;
		hoursUpdated = SET;

		if(adjustedHours > 12)
		{
		 	adjustedHours = 1;															// Roll over
		}

		sprintf(str, "%bu   ", adjustedHours);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == MINUTE_ADJUST)
	{
		adjustedMinutes++;
		minutesUpdated = SET;

		if(adjustedMinutes > 59)
		{
		 	adjustedMinutes = 0;
		}

		sprintf(str, "%bu   ", adjustedMinutes);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == AMPM_ADJUST)
	{
		if(adjustedAmPm == 'P')
		{
		 	adjustedAmPm = 'A';
		}
		else if(adjustedAmPm == 'A')
		{
		 	adjustedAmPm = 'P';
		}
		else
		{}

		amPmUpdated = SET;

		sprintf(str, "%cM  ", adjustedAmPm);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else
	{}
}

//-------------------------------------------------------------------------------------------------------
// Function Name: moveDateTimeDown
// Return Value: None 
// Parmeters: None
// Function Description: This function sets date and time of the clock from the touch screen by pressing the down button
//-------------------------------------------------------------------------------------------------------
void moveDateTimeDown(void)
{
	char str[SPRINTF_SIZE];

	if(realTimeClockItems == MONTH_ADJUST)
	{
		adjustedMonth--;
	   	monthUpdated = SET;

		if(adjustedMonth < 1)
		{
		 	adjustedMonth = 12;															// Roll over to hour 12
		}
		sprintf(str, "%s ", monthOfYear[adjustedMonth]);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == DATE_ADJUST)
	{
		adjustedDate--;
		dateUpdated = SET;

		if(adjustedDate < 1)
		{
		 	adjustedDate = 31;															// Roll over to day 31
		}

		sprintf(str, "%bu   ", adjustedDate);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == YEAR_ADJUST)
	{
		if(adjustedYear > 0)
		{
			adjustedYear--;																// Check if it is greater than 0 before decrementing												
		}																				// to avoid a negative number
		else
		{
			adjustedYear = 0;															// Roll over to 99
		}
		
		yearUpdated = SET;

		sprintf(str, "20%02bu", adjustedYear);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == HOUR_ADJUST)
	{
		if(adjustedHours > 0)
		{
			adjustedHours--;															// Check if it is greater than 0 before decrementing
		}																				// to avoid a negative number
		else
		{
		 	adjustedHours = 12;
		}

		hoursUpdated = SET;

		sprintf(str, "%bu   ", adjustedHours);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == MINUTE_ADJUST)
	{
		if(adjustedMinutes > 0)
		{
			adjustedMinutes--;															// Check if it is greater than 0 before decrementing
		}																				// to avoid a negative number
		else
		{
		 	adjustedMinutes = 59;
		}

		minutesUpdated = SET;

		sprintf(str, "%bu   ", adjustedMinutes);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == AMPM_ADJUST)
	{
		if(adjustedAmPm == 'P')
		{
		 	adjustedAmPm = 'A';
		}
		else if(adjustedAmPm == 'A')
		{
		 	adjustedAmPm = 'P';
		}
		else
		{}

		amPmUpdated = SET;

		sprintf(str, "%cM  ", adjustedAmPm);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else
	{}
}

//-------------------------------------------------------------------------------------------------------
// Function Name: enterDateTime
// Return Value: None 
// Parmeters: None
// Function Description: This function confirms the date or time set by the user on the touch screen
//-------------------------------------------------------------------------------------------------------
void enterDateTime(void)
{
 	char str[SPRINTF_SIZE];
	
	realTimeClockItems++;																// Move to next item
	
	if(realTimeClockItems == MONTH_ADJUST)
	{
		monthUpdated = SET;
		displayText(TITLE_DISPLAY_FG, TITLE_DISPLAY_BG, TITLE_DISPLAY_FONT, "SET MONTH ", TITLE_DISPLAY_X, TITLE_DISPLAY_Y);
		sprintf(str, "%s ", monthOfYear[adjustedMonth]);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == DATE_ADJUST)
	{
		dateUpdated = SET;
		displayText(TITLE_DISPLAY_FG, TITLE_DISPLAY_BG, TITLE_DISPLAY_FONT, "SET DATE  ", TITLE_DISPLAY_X, TITLE_DISPLAY_Y);
		sprintf(str, "%bu   ", adjustedDate);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == YEAR_ADJUST)
	{
		yearUpdated = SET;
		displayText(TITLE_DISPLAY_FG, TITLE_DISPLAY_BG, TITLE_DISPLAY_FONT, "SET YEAR  ", TITLE_DISPLAY_X, TITLE_DISPLAY_Y);
		sprintf(str, "20%02bu", adjustedYear);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == HOUR_ADJUST)
	{
		hoursUpdated = SET;
		displayText(TITLE_DISPLAY_FG, TITLE_DISPLAY_BG, TITLE_DISPLAY_FONT, "SET HOUR  ", TITLE_DISPLAY_X, TITLE_DISPLAY_Y);
		sprintf(str, "%bu   ", adjustedHours);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == MINUTE_ADJUST)
	{
		minutesUpdated = SET;
		amPmUpdated = SET;																// Set it anyway because user usually does not set at the end
		displayText(TITLE_DISPLAY_FG, TITLE_DISPLAY_BG, TITLE_DISPLAY_FONT, "SET MINUTE", TITLE_DISPLAY_X, TITLE_DISPLAY_Y);
		sprintf(str, "%bu   ", adjustedMinutes);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == AMPM_ADJUST)
	{
		amPmUpdated = SET;																// Set it anyway because user usually does not set at the end
		displayText(TITLE_DISPLAY_FG, TITLE_DISPLAY_BG, TITLE_DISPLAY_FONT, "SET AM/PM ", TITLE_DISPLAY_X, TITLE_DISPLAY_Y);
		sprintf(str, "%cM  ", adjustedAmPm);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == DONE_ADJUST)
	{
		amPmUpdated = SET;																// Set it if user explicitly press enter button
		displayText(TITLE_DISPLAY_FG, TITLE_DISPLAY_BG, TITLE_DISPLAY_FONT, "PRESS DONE", TITLE_DISPLAY_X, TITLE_DISPLAY_Y);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, "----", VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else if(realTimeClockItems == ROLL_OVER_ADJUST)
	{
		realTimeClockItems = MONTH_ADJUST;												// Set it for next round of real time clock items
		monthUpdated = SET;
		displayText(TITLE_DISPLAY_FG, TITLE_DISPLAY_BG, TITLE_DISPLAY_FONT, "SET MONTH ", TITLE_DISPLAY_X, TITLE_DISPLAY_Y);
		sprintf(str, "%s ", monthOfYear[adjustedMonth]);
		displayText(VALUE_DISPLAY_FG, VALUE_DISPLAY_BG, VALUE_DISPLAY_FONT, str, VALUE_DISPLAY_X, VALUE_DISPLAY_Y);
	}
	else
	{}
}

//-------------------------------------------------------------------------------------------------------
// Function Name: setClockOnScreen
// Return Value: None 
// Parmeters: None
// Function Description: This function sets the real time clock on the touch screen
//-------------------------------------------------------------------------------------------------------
void setClockOnScreen(void)
{		
	unsigned char hoursAux;
	
	if(monthUpdated == SET)
	{
		sharedDataTx[RTC_START_ADDR + MONTH] = convertDecimalToBCD(adjustedMonth);		// Get updated month from user on screen
		writeBytesToRealTimeClock(RTC_START_ADDR + MONTH, 1);							// Set month only (1 byte)
		monthUpdated = CLEAR;															//Clear flag after writing to real time clock													
	}
	
	if(dateUpdated == SET)
	{
		sharedDataTx[RTC_START_ADDR + DATE] = convertDecimalToBCD(adjustedDate);		// Get updated date from user on screen
		writeBytesToRealTimeClock(RTC_START_ADDR + DATE, 1);							// Set date only (1 byte)
		dateUpdated = CLEAR;
	}

	if(yearUpdated == SET)
	{
		sharedDataTx[RTC_START_ADDR + YEAR] = convertDecimalToBCD(adjustedYear);		// Get updated year from user on screen
		writeBytesToRealTimeClock(RTC_START_ADDR + YEAR, 1);							// Set year only (1 byte)
		yearUpdated = CLEAR;
	}

	if(hoursUpdated == SET || amPmUpdated == SET)
	{
		hoursAux = convertDecimalToBCD(adjustedHours);									// Get updated hours from user on screen
		
		if(amPmUpdated == SET)
		{
			amPm = adjustedAmPm;														// Set updated amPm from user on screen
		
			hoursAux = hoursAux | 0x40;													// Set 12/24 bit --> 12 hour mode
			
			if(adjustedAmPm == 'P')
			{																	
			 	hoursAux = hoursAux | 0x60;												// Set AM/PM bit --> PM mode
			}
			else if(adjustedAmPm == 'A')
			{																	
			 	hoursAux = hoursAux & 0x5F;												// Clear AM/PM bit --> AM mode
			}
			else
			{}

			amPmUpdated = CLEAR;
		}
		
		sharedDataTx[RTC_START_ADDR + HOURS] = hoursAux;
		writeBytesToRealTimeClock(RTC_START_ADDR + HOURS, 1);							// Set hours only (1 byte)
		hoursUpdated = CLEAR; 
	}

	if(minutesUpdated == SET)
	{
		sharedDataTx[RTC_START_ADDR + MINUTES] = convertDecimalToBCD(adjustedMinutes);	// Get updated minutes from user on screen
		writeBytesToRealTimeClock(RTC_START_ADDR + MINUTES, 1);							// Set minutes only (1 byte)
		minutesUpdated = CLEAR;
	}

	realTimeClockItems = MONTH_ADJUST;													// Set starting item = Month for adjustment

	getClockData();																	    // Get current month, date, year set by user

	//clockSetupDisplayRepeat = 0;														// Display clock updates on screen 																		// System goes back to main page					
}

//-------------------------------------------------------------------------------------------------------
// Function Name: resetClock
// Return Value: None 
// Parmeters: None
// Function Description: This function resets the real time clock to 0:00:00 1/1/2000
//-------------------------------------------------------------------------------------------------------
void resetClock(void)
{
	seconds = 0;
	minutes = 0;
	hours = 0;
	amPm = 'P';
	day = 1;
	date = 1;
	month = 1;
	year = 0;
	century = 0;
	timeMode = 1;

	setClock();															                // Set real time clock
}

//-------------------------------------------------------------------------------------------------------
// Function Name: convertDecimalToBCD
// Return Value: bcd 
// Parmeters: decimal
// Function Description: This function converts a decimal number to a BCD when writing the new value to the real time clock
//-------------------------------------------------------------------------------------------------------
unsigned char convertDecimalToBCD(unsigned char decimal)
{
 	unsigned char bcd;

	bcd = (decimal / 10) << 4;															// Get upper 4 bits
	bcd = bcd | (decimal % 10);															// Get a BCD

	return bcd;
}

//-------------------------------------------------------------------------------------------------------
// Function Name: convertBCDToDecimal
// Return Value: decimal value 
// Parmeters: bcd
// Function Description: This function converts a BCD to a decimal number when reading the current value from the real time clock
//-------------------------------------------------------------------------------------------------------
unsigned char convertBCDToDecimal(unsigned char bcd)
{
 	unsigned char decimal;

	decimal = ((bcd >> 4) * 10) + (bcd & 0x0F);											// Combine upper and lower nibbles to get
																						// 8 bit number
	return decimal;
}

//-------------------------------------------------------------------------------------------------------
// Function Name: displayClock
// Return Value: None 
// Parmeters: None
// Function Description: This function displays clock data on the touch screen
// The clock format is MM/DD/YYYY hour/minute/second
//-------------------------------------------------------------------------------------------------------
void displayClock(void)
{
 	char str[SPRINTF_SIZE];
	
	//if(screen == MAIN_PAGE)
	//{
        getClockData();

        sprintf(str, "%s %02bu, 20%02bu %s", monthOfYear[month], date, year, dayOfWeek[day]);
        displayText(SETTINGS_DATE_FG, SETTINGS_DATE_BG, SETTINGS_DATE_FONT, str, SETTINGS_DATE_X, SETTINGS_DATE_Y);
        monthDateYearUpdated = CLEAR;
        
        sprintf(str, "%2bu : %02bu : %02bu %cM ", hours, minutes, seconds, amPm);
        displayText(SETTINGS_TIME_FG, SETTINGS_TIME_BG, SETTINGS_TIME_FONT, str, SETTINGS_TIME_X, SETTINGS_TIME_Y);
	//}
	/*else if(screen == CLOCK_SETUP_PAGE)
	{
		if(buttonPressed == CLEAR)
		{
			getClockData();
	
			sprintf(str, "%s %02bu, 20%02bu", monthOfYear[month], date, year);
			displayText(DATE_DISPLAY_FG, DATE_DISPLAY_BG, DATE_DISPLAY_FONT, str, DATE_DISPLAY_X, DATE_DISPLAY_Y);
			monthDateYearUpdated = CLEAR;

			sprintf(str, "%2bu:%02bu:%02bu %cM ", hours, minutes, seconds, amPm);
			displayText(TIME_DISPLAY_FG, TIME_DISPLAY_BG, TIME_DISPLAY_FONT, str, TIME_DISPLAY_X, TIME_DISPLAY_Y);
			timeUpdated = CLEAR;
		}
	}*/
}

//-------------------------------------------------------------------------------------------------------
// Main
//-------------------------------------------------------------------------------------------------------
// Control functions
void bar_load(); // function code == 11
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
void main_page_load();	// function code == 40
void temp_page_load();	// function code == 41
void motor_page_load();	// function code == 42
void laser_page_load();	// function code == 43
void setting_page_load();	// function code == 44
void c_to_f();	// function code == 50
void f_to_c();	// function code == 51
void add_point();	// function code == 52
void game_start();	// function code == 53
void brightness_setting();	// function code == 54
void time_setting();	// function code == 55
void set_clock();	// function code == 56
void contrast_setting();	// function code == 57

int get_function_code();
int passcode[4]={-1};
int is_locked_out=0;
int attempts=5;
int is_in_temp_page=0;
int is_in_motor_page=0;
int is_in_game=0;
int is_in_laser_page=0;
int is_in_main_page=0;
int is_in_setting_page=0;
int is_in_c=1;
int _delay=0;
char userID[64];
int stones=20;
int duration=2000;
int point=0;
int brightness=120;
int c=20;
int set_time=0;
double dayweek;
int select=0;
int clock_value;
int contrast=255;
int xyz=0;
	
void main()
{
		int i = 0;
    char str[SPRINTF_SIZE];
	  int testCounter = 0;
	
    disableWatchdog();
    systemClockInit();
		portInit();
		enableInterrupts();
		uart0Init();
    smbInit();
    timer3Init(); 
  
    tsLastCharGone = 1;
    tsTxOut = tsTxIn = 0;
    tsTxEmpty = 1;
	
	
		day=0;
    contrast_setting();
	
	while(1)
	{
		//------------------clock display in main page------------------
				if(_delay%10000==0 && (is_in_main_page || is_in_temp_page))
				{
        getClockData();   
        sprintf(str, "%2bu:%02bu%cM", hours, minutes, amPm);
        displayText("000000", "F7F9F8", 2, str, 277, 0);
				sprintf(str, "20%02bu/%02bu/%02bu", year, month, date);
        displayText("000000", "F7F9F8", 2, str, 265, 20);
        monthDateYearUpdated = CLEAR;
				}
				else if(_delay%10000==0 && is_in_motor_page)
				{
        getClockData();   
        sprintf(str, "%2bu:%02bu%cM", hours, minutes, amPm);
        displayText("000000", "FFFFFF", 2, str, 190, -4);
        monthDateYearUpdated = CLEAR;
				}
		//------------------time display------------------
				if(is_in_setting_page && _delay%10000==0)
				{
					displayClock();															// Display clock data on the screen
					switch(select)
					{
						case 0: break;
						case 1: sprintf(str, "xi 16 163 393\r"); sendCommand(str); break;
						case 2: sprintf(str, "xi 16 237 393\r"); sendCommand(str); break;
						case 3: sprintf(str, "xi 17 318 393\r"); sendCommand(str); break;
						case 4: sprintf(str, "xi 16 163 437\r"); sendCommand(str); break;
						case 5: sprintf(str, "xi 16 258 437\r"); sendCommand(str); break;
						case 6: sprintf(str, "xi 16 362 437\r"); sendCommand(str); break;
						case 7: break;
						default: break;
					}
		//			sprintf(str, "%s", amPm);
		//			displayText("000000", "FFFFFF", 4, str, 123, 0);
		/*		if(!set_time)
				{
					
					seconds=0;
					minutes=23;
					hours=4;
					amPm='A';
					date=27;
					month=11;
					year=17;
					century=20;
					timeMode=1;
					if(month==1 || month==2)								day=(date+1+2*(month+12)+3*((month+12)+1)/5+(year-1)+(year-1)/4-(year-1)/100+(year-1)/400)%7;
					else																		day=(date+1+2*month+3*(month+1)/5+year+year/4-year/100+year/400)%7;
					setClock();
					set_time=1;
				}*/
				}
		//-----------------game display-------------------
				if(is_in_motor_page && _delay%10000==0)
				{
						roomTemp1 = readOneByteFromSlave(ROOM_TEMP_1);
						c=roomTemp1;
						if(is_in_c)
						{
							sprintf(str, "body°C:%d", c);
							displayText("000000", "FFFFFF", 2, str, 365,-4);
						}
						else
						{
							sprintf(str, "body°F:%d", c*9/5+32);
							displayText("000000", "FFFFFF", 2, str, 365,-4);							
						}
						if(is_in_game)
						{
							if(point<10)
							{
								sprintf(str, "%d", point);
								displayText("000000", "FF0000", 6, str, 305,20);
							}
							else
							{
								sprintf(str, "%d", point);
								displayText("000000", "FF0000", 6, str, 290,20);
							}
						}

				}
				if(is_in_game && _delay%40000==0)
				{	
						if(stones<=20 && stones>15)	
						{
							if(c<20)	duration=700;
							else			duration=700+((c-20)*130);
							sprintf(str, "STAGE 1");
							displayText("000000", "FFFFFF", 4, str, 260,80);
						}
						else if(stones<=15 && stones>10)	
						{
							if(c<20)	duration=500;
							else			duration=500+((c-20)*100);
							sprintf(str, "STAGE 2");
							displayText("000000", "FFFFFF", 4, str, 260,80);
						}
						else if(stones<=10 && stones>5)	
						{
							if(c<20)	duration=300;
							else			duration=300+((c-20)*70);
							sprintf(str, "STAGE 3");
							displayText("000000", "FFFFFF", 4, str, 260,80);
						}
						else if(stones<=5 && stones>0)		
						{
							if(c<20)	duration=100;
							else			duration=100+((c-20)*40);
							sprintf(str, "STAGE 4");
							displayText("000000", "FFFFFF", 4, str, 260,80);
						}
						if(stones<=0)	
						{
							is_in_game=0;
							if(point<10)
							{
								getClockData();
								sprintf(str, "m lose %2bu %02bu %cM\r",  hours, minutes, amPm);
								sendCommand(str);
							}
							else
							{
								sprintf(str, "m win %s\r", userID);
								sendCommand(str);
							}
						}
						else
						{
							int number=(rand() % 10) +1;
							switch (number)
							{
							case 1:
							sprintf(str, "m stone_display 133 118 %d\r", duration);
							sendCommand(str);
							break;
							case 2:
							sprintf(str, "m stone_display 118 159 %d\r", duration);
							sendCommand(str);
							break;
							case 3:
							sprintf(str, "m stone_display 124 202 %d\r", duration);
							sendCommand(str);
							break;
							case 4:
							sprintf(str, "m stone_display 130 263 %d\r", duration);
							sendCommand(str);	
							break;
							case 5:
							sprintf(str, "m stone_display 150 300 %d\r", duration);
							sendCommand(str);
							break;
							case 6:
							sprintf(str, "m stone_display 474 120 %d\r", duration);
							sendCommand(str);
							break;
							case 7:
							sprintf(str, "m stone_display 487 159 %d\r", duration);
							sendCommand(str);
							break;
							case 8:
							sprintf(str, "m stone_display 483 204 %d\r", duration);
							sendCommand(str);	
							break;
							case 9:
							sprintf(str, "m stone_display 480 266 %d\r", duration);
							sendCommand(str);
							break;
							case 10:
							sprintf(str, "m stone_display 452 306 %d\r", duration);
							sendCommand(str);	
							break;							
							default:
							break;
							}
							stones--;
						}
				}
		//-----------------text animation display-------------------
				if(is_in_laser_page && _delay%2000==0)
				{	
						sprintf(str, "m text_display\r");
						sendCommand(str);
				}
		//-----------------temperature display-------------------	
				if(is_in_temp_page && _delay%20000==0)
				{	
					checkSlaveFlags();
					/*
					//----------------for testing only-------------
					sprintf(str, "flag:%c", slaveFlags1);
					displayText("000000", "F7F9F8", 2, str, 100,0);
					//---------------------------------------------
					*/
					if(is_in_c)	//C
					{
						// if temp1 & temp2 on
						if(slaveFlags1=='f')
						{
							//Left
							roomTemp1 = readOneByteFromSlave(ROOM_TEMP_1);
							sprintf(str, " %-5bu", roomTemp1);
							displayText("000000", "8D8989", 6, str, 208, 160);				
							//Right
							roomTemp2 = readOneByteFromSlave(ROOM_TEMP_2);
							sprintf(str, " %-5bu", roomTemp2);
							displayText("000000", "8D8989", 6, str, 338, 160);	
						}
						// if only temp1 on
						else if (slaveFlags1=='v')
						{
							//Left
							roomTemp1 = readOneByteFromSlave(ROOM_TEMP_1);
							sprintf(str, " %-5bu", roomTemp1);
							displayText("000000", "8D8989", 6, str, 208, 160);				
							//Right
							roomTemp2 = readOneByteFromSlave(ROOM_TEMP_2);
							sprintf(str, " ERR", roomTemp2);
							displayText("000000", "8D8989", 6, str, 338, 160);	
						}
						// if only temp2 on
						else if (slaveFlags1=='n')
						{
							//Left
							roomTemp1 = readOneByteFromSlave(ROOM_TEMP_1);
							sprintf(str, " ERR", roomTemp1);
							displayText("000000", "8D8989", 6, str, 208, 160);				
							//Right
							roomTemp2 = readOneByteFromSlave(ROOM_TEMP_2);
							sprintf(str, " %-5bu", roomTemp2);
							displayText("000000", "8D8989", 6, str, 338, 160);	
						}
						// both temp1 & temp2 off
						else
						{
							//Left
							roomTemp1 = readOneByteFromSlave(ROOM_TEMP_1);
							sprintf(str, " ERR", roomTemp1);
							displayText("000000", "8D8989", 6, str, 208, 160);				
							//Right
							roomTemp2 = readOneByteFromSlave(ROOM_TEMP_2);
							sprintf(str, " ERR", roomTemp2);
							displayText("000000", "8D8989", 6, str, 338, 160);	
						}
					}
					else	//F
					{
						// if temp1 & temp2 on
						if(slaveFlags1=='f')
						{
							//Left
							roomTemp1 = readOneByteFromSlave(ROOM_TEMP_1);
							sprintf(str, " %-5bu", roomTemp1*9/5+32);
							displayText("000000", "8D8989", 6, str, 208, 160);				
							//Right
							roomTemp2 = readOneByteFromSlave(ROOM_TEMP_2);
							sprintf(str, " %-5bu", roomTemp2*9/5+32);
							displayText("000000", "8D8989", 6, str, 338, 160);	
						}
						// if only temp1 on
						else if (slaveFlags1=='v')
						{
							//Left
							roomTemp1 = readOneByteFromSlave(ROOM_TEMP_1);
							sprintf(str, " %-5bu", roomTemp1*9/5+32);
							displayText("000000", "8D8989", 6, str, 208, 160);				
							//Right
							roomTemp2 = readOneByteFromSlave(ROOM_TEMP_2);
							sprintf(str, " ERR", roomTemp2*9/5+32);
							displayText("000000", "8D8989", 6, str, 338, 160);	
						}
						// if only temp2 on
						else if (slaveFlags1=='n')
						{
							//Left
							roomTemp1 = readOneByteFromSlave(ROOM_TEMP_1);
							sprintf(str, " ERR", roomTemp1*9/5+32);
							displayText("000000", "8D8989", 6, str, 208, 160);				
							//Right
							roomTemp2 = readOneByteFromSlave(ROOM_TEMP_2);
							sprintf(str, " %-5bu", roomTemp2*9/5+32);
							displayText("000000", "8D8989", 6, str, 338, 160);	
						}
						// both temp1 & temp2 off
						else
						{
							//Left
							roomTemp1 = readOneByteFromSlave(ROOM_TEMP_1);
							sprintf(str, " ERR", roomTemp1*9/5+32);
							displayText("000000", "8D8989", 6, str, 208, 160);				
							//Right
							roomTemp2 = readOneByteFromSlave(ROOM_TEMP_2);
							sprintf(str, " ERR", roomTemp2*9/5+32);
							displayText("000000", "8D8989", 6, str, 338, 160);	
						}
					}
				}
		//----------------------------------------------------------
        if (tsCommandReceived) {
            switch (get_function_code()) {
                case 11: bar_load(); break;
                case 21: login_page_load(); break;
                case 22: login_attempts(); break;
                case 23: login_clear_stars(); break;
                case 24: login_disp_1_star(); break;
                case 25: login_disp_2_star(); break;
                case 26: login_disp_3_star(); break;
                case 27: login_disp_4_star(); break;
								case 40: main_page_load(); break;
							  case 41: temp_page_load(); break;
								case 42: motor_page_load(); break;
								case 43: laser_page_load(); break;
								case 44: setting_page_load(); break;
								case 50: c_to_f(); break;
							  case 51: f_to_c(); break;
								case 52: add_point(); break;
								case 53: game_start(); break;
								case 54: brightness_setting(); break;
								case 55: time_setting(); break;
								case 56: set_clock(); break;
								case 57: contrast_setting(); break;
								default: break;
            }
        }
				_delay++;
	}
}

// Splash Page begin ==========

// function code == 11
void bar_load() {
		char str[64];
    sprintf(str, "m load_bar_full 220 275 100 03\r");
    sendCommand(str);
}
// Splash page end ============

int get_function_code() {
    // TODO, receive commands from screen
		if(userCommand[0]=='l' && userCommand[1]=='o' && userCommand[2]=='a')
		{
			is_in_temp_page=0;
			is_in_motor_page=0;
			is_in_game=0;
			is_in_laser_page=0;
			is_in_setting_page=0;
			is_in_main_page=0;
			is_in_c=1;
			attempts==5;
			is_locked_out=0;
			select=0;
			tsCommandReceived=0;
			return 11;
		}
		else if(userCommand[0]=='g' && userCommand[1]=='e' && userCommand[2]=='t' && userCommand[3]=='_')
		{
			tsCommandReceived=0;
			return 52;
		}
		else if(userCommand[0]=='s' && userCommand[1]=='t' && userCommand[2]=='a' && userCommand[3]=='r')
		{
			tsCommandReceived=0;
			return 53;
		}
		else if(userCommand[0]=='l' && userCommand[1]=='2' && userCommand[2]=='4' && userCommand[3]=='1')
		{
			if			(userCommand[6]=='\0')	brightness=userCommand[5]-'0';
			else if	(userCommand[7]=='\0')	brightness=(userCommand[5]-'0')*10 + userCommand[6]-'0';
			else														brightness=(userCommand[5]-'0')*100 +(userCommand[6]-'0')*10 + userCommand[7]-'0';
			tsCommandReceived=0;
			return 54;
		}
		else if(userCommand[0]=='l' && userCommand[1]=='2' && userCommand[2]=='4' && userCommand[3]=='3')
		{
			if			(userCommand[6]=='\0')	clock_value=userCommand[5]-'0';
			else 														clock_value=(userCommand[5]-'0')*10 + userCommand[6]-'0';
			tsCommandReceived=0;
			return 56;
		}
		else if(userCommand[0]=='l' && userCommand[1]=='2' && userCommand[2]=='4' && userCommand[3]=='2')
		{
			if			(userCommand[6]=='\0')	contrast=userCommand[5]-'0';
			else if	(userCommand[7]=='\0')	contrast=(userCommand[5]-'0')*10 + userCommand[6]-'0';
			else														contrast=(userCommand[5]-'0')*100 +(userCommand[6]-'0')*10 + userCommand[7]-'0';
			tsCommandReceived=0;
			return 57;
		}
		else if(userCommand[0]=='d' && userCommand[1]=='t' && userCommand[2]=='_')
		{
			switch (userCommand[3])
			{
					case 'M': select=1; break;
          case 'd': select=2; break;
          case 'y': select=3; break;
          case 'h': select=4; break;
          case 'm': select=5; break;
          case 's': select=6; break;
					case 'a': select=7; break;
					default: break;
      }
			tsCommandReceived=0;
			return 55;
		}
		else if(userCommand[0]=='e' && userCommand[1]=='n' && userCommand[2]=='d' && userCommand[3]=='l')
		{
			tsCommandReceived=0;
			return 21;
		}
		else if(userCommand[0]=='m' && userCommand[1]=='p' && userCommand[2]=='_' && userCommand[3]=='l' && userCommand[4]=='o')
		{
			tsCommandReceived=0;
			return 23;
		}
		else if(passcode[0]==-1)
		{
			passcode[0]=userCommand[3]-'0';
			tsCommandReceived=0;
			return 24;
		}
		else if(passcode[1]==-1)
		{
			passcode[1]=userCommand[3]-'0';
			tsCommandReceived=0;
			return 25;
		}
		else if(passcode[2]==-1)
		{
			passcode[2]=userCommand[3]-'0';
			tsCommandReceived=0;
			return 26;
		}
		else if(passcode[3]==-1)
		{
			passcode[3]=userCommand[3]-'0';
			tsCommandReceived=0;
			return 27;
		}
		else if(userCommand[0]=='m' && userCommand[1]=='p'&& userCommand[2]=='_' && userCommand[3]=='m' && userCommand[4]=='a')
		{
			tsCommandReceived=0;
			return 40;
		}
		else if(userCommand[0]=='m' && userCommand[1]=='p'&& userCommand[2]=='_' && userCommand[3]=='t')
		{
			tsCommandReceived=0;
			return 41;
		}
		else if(userCommand[0]=='m' && userCommand[1]=='p'&& userCommand[2]=='_' && userCommand[3]=='m' && userCommand[4]=='o')
		{
			tsCommandReceived=0;
			return 42;
		}
		else if(userCommand[0]=='m' && userCommand[1]=='p'&& userCommand[2]=='_' && userCommand[3]=='l' && userCommand[4]=='a')
		{
			tsCommandReceived=0;
			return 43;
		}
		else if(userCommand[0]=='m' && userCommand[1]=='p'&& userCommand[2]=='_' && userCommand[3]=='s' && userCommand[4]=='e')
		{
			tsCommandReceived=0;
			return 44;
		}
				else if(userCommand[0]=='n' && userCommand[1]=='s'&& userCommand[2]=='_' && userCommand[3]=='f')
		{
			tsCommandReceived=0;
			return 50;
		}
				else if(userCommand[0]=='n' && userCommand[1]=='s'&& userCommand[2]=='_' && userCommand[3]=='c')
		{
			tsCommandReceived=0;
			return 51;
		}
    return 0;
}

// Login Page begin ===========

// function code == 21
void login_page_load() {
			char str[64];
			sprintf(userID, "");
			passcode[0]=-1;
			passcode[1]=-1;
			passcode[2]=-1;
			passcode[3]=-1;
			is_locked_out=0;
			is_in_main_page=0;
			sprintf(str, "m display_login_page\r");
			sendCommand(str);
			sprintf(str, "bd 5 0 0 1 \"\" 0 0 37 37\r");
			sendCommand(str);
			sprintf(str, "xm 5 power_off_button\r");
			sendCommand(str);
}

// function code == 22
void login_attempts() {

}

// function code == 23
void login_clear_stars() {
			char str[64];
			attempts=5;
			sprintf(userID, "");
			passcode[0]=-1;
			passcode[1]=-1;
			passcode[2]=-1;
			passcode[3]=-1;
			is_locked_out=0;
			is_in_main_page=0;
			sprintf(str, "m display_login_page\r");
			sendCommand(str);
			sprintf(str, "bd 5 0 0 1 \"\" 0 0 37 37\r");
			sendCommand(str);
			sprintf(str, "xm 5 power_off_button\r");
			sendCommand(str);
}

// function code == 24
void login_disp_1_star() {
		char str[64];
    sprintf(str, "m display_asterik_1\r");
    sendCommand(str);
}

// function code == 25
void login_disp_2_star() {
		char str[64];
    sprintf(str, "m display_asterik_2\r");
    sendCommand(str);
}

// function code == 26
void login_disp_3_star() {
		char str[64];
    sprintf(str, "m display_asterik_3\r");
    sendCommand(str);
}

// function code == 27
void login_disp_4_star() {
		char str[64];
    sprintf(str, "m display_asterik_4\r");
    sendCommand(str);
		if(is_locked_out==0 && passcode[0]==1 && passcode[1]==2 && passcode[2]==3 && passcode[3]==4)
		{
			sprintf(userID, "J Lin");
			sprintf(str, "m set_uid %s\r", userID);
			sendCommand(str);
			sprintf(str, "m display_main_page\r");
			sendCommand(str);
			is_in_main_page=1;
		}
		else if(is_locked_out==0 && passcode[0]==9 && passcode[1]==9 && passcode[2]==9 && passcode[3]==9)
		{
			sprintf(userID, "S-H Yang");
			sprintf(str, "m set_uid %s\r", userID);
			sendCommand(str);
			sprintf(str, "m display_main_page\r");
			sendCommand(str);
			is_in_main_page=1;
		}
		else if(is_locked_out==0 && passcode[0]==1 && passcode[1]==1 && passcode[2]==1 && passcode[3]==1)
		{
			sprintf(userID, "Y Li");
			sprintf(str, "m set_uid %s\r", userID);
			sendCommand(str);
			sprintf(str, "m display_main_page\r");
			sendCommand(str);
			is_in_main_page=1;
		}
		else if(is_locked_out==1 && passcode[0]==6 && passcode[1]==7 && passcode[2]==8 && passcode[3]==9)
		{
			sprintf(userID, "C Davila");
			sprintf(str, "m set_uid %s\r", userID);
			sendCommand(str);
			sprintf(str, "m display_main_page\r");
			sendCommand(str);
			is_in_main_page=1;
		}
		else
		{
			sprintf(str, "xi 40 0 0\r");
			sendCommand(str);
			sprintf(str, "xc all\r");
			sendCommand(str);
			sprintf(str, "w 1000\r");
			sendCommand(str);
			passcode[0]=-1;
			passcode[1]=-1;
			passcode[2]=-1;
			passcode[3]=-1;
			attempts--;
			if(attempts<=0)
			{
				is_locked_out=1;
				sprintf(str, "m display_locked_page\r");
				sendCommand(str);
			}
			else
			{
				sprintf(str, "m display_login_attempts_left %d\r", attempts);
				sendCommand(str);
				sprintf(str, "bd 5 0 0 1 \"\" 0 0 37 37\r");
				sendCommand(str);
				sprintf(str, "xm 5 power_off_button\r");
				sendCommand(str);
			}
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

void main_page_load()
{
			char str[64];
			is_in_temp_page=0;
			is_in_motor_page=0;
			is_in_game=0;
			is_in_laser_page=0;
			is_in_setting_page=0;
			select=0;
			sprintf(str, "m display_main_page\r");
			sendCommand(str);
			is_in_main_page=1;
}
// finction code == 40

void temp_page_load()
{
			char str[64];
			is_in_main_page=0;
			sprintf(str, "m display_temp_page\r");
			sendCommand(str);
			is_in_temp_page=1;
			if(is_in_c==0)
			{
				sprintf(str, "m temp_unit_f\r");
				sendCommand(str);
			}
			else
			{
				sprintf(str, "m temp_unit_c\r");
				sendCommand(str);
			}				
}
// finction code == 41

void motor_page_load()
{
			char str[64];
			is_in_main_page=0;
			point=0;
			stones=20;
			sprintf(str, "m display_game_page\r");
			sendCommand(str);
			is_in_motor_page=1;
}
// finction code == 42

void laser_page_load()
{
			char str[64];
			is_in_main_page=0;
			sprintf(str, "m display_laser_page\r");
			sendCommand(str);
			is_in_laser_page=1;

}
// finction code == 43

void setting_page_load()
{
			char str[64];
			is_in_main_page=0;
			sprintf(str, "m display_settings_screen %d %d\r", brightness, contrast);
			sendCommand(str);
			is_in_setting_page=1;
}
// finction code == 44

void c_to_f()
{
	is_in_c=0;
}
// finction code == 50

void f_to_c()
{
	is_in_c=1;
}
// finction code == 51

void add_point()
{
	point++;
}
// finction code == 52

void game_start()
{
	point=0;
	stones=20;
	is_in_game=1;
}
// finction code == 53

void brightness_setting()
{
	char str[64];
	sprintf(str, "m adjust_brightness %d\r", brightness);
	sendCommand(str);
/*	
	if(brightness<10)
	{
		sprintf(str, "  %d", brightness);
		displayText("000000", "F7F9F8", 3, str, 305,118);
	}
	else if(brightness<100)
	{
		sprintf(str, " %d", brightness);
		displayText("000000", "F7F9F8", 3, str, 305,118);
	}
	else
	{
		sprintf(str, "%d", brightness);
		displayText("000000", "F7F9F8", 3, str, 305,118);
	}
*/
}
// finction code == 54

void time_setting()
{
	char str[64];
	switch(select)
	{
			case 0: break;
			case 1: sprintf(str, "m display_date_time_slider %02bu 12 1\r", month); sendCommand(str); break;
			case 2: if(month==1 || month==3 ||month==5 || month==7 ||month==8 || month==10 || month==12)
							{
								sprintf(str, "m display_date_time_slider %02bu 31 1\r", date); sendCommand(str); break;
							}
							else if(month==4 || month==6 ||month==9 || month==11)
							{
								sprintf(str, "m display_date_time_slider %02bu 30 1\r", date); sendCommand(str); break;
							}
							else if(month==2 && year%4==0)
							{
								sprintf(str, "m display_date_time_slider %02bu 29 1\r", date); sendCommand(str); break;
							}
							else
							{
								sprintf(str, "m display_date_time_slider %02bu 28 1\r", date); sendCommand(str); break;
							}
			case 3: sprintf(str, "m display_date_time_slider %02bu 99 0\r", year); sendCommand(str); break;
			case 4: sprintf(str, "m display_date_time_slider %02bu 12 1\r", hours); sendCommand(str); break;
			case 5: sprintf(str, "m display_date_time_slider %02bu 59 0\r", minutes); sendCommand(str); break;
			case 6: sprintf(str, "m display_date_time_slider %02bu 59 0\r", seconds); sendCommand(str); break;
			case 7: if(amPm=='P')
							{
								amPm='A'; 
							}
							else
							{
								amPm='P'; 
							}
							setClock(); 
							break;
			default: break;
	}

}
// finction code == 55

void set_clock()
{
	char str[64];
	switch(select)
	{
			case 0: break;
			case 1: month=clock_value; break;
			case 2: date=clock_value; break;
			case 3: year=clock_value; break;
			case 4: hours=clock_value; break;
			case 5: minutes=clock_value; break;
			case 6: seconds=clock_value; break;
			case 7: break;
			default: break;
	}
	if(month==1 || month==2)								day=(date+1+2*(month+12)+3*((month+12)+1)/5+(year-1)+(year-1)/4-(year-1)/100+(year-1)/400)%7;
	else																		day=(date+1+2*month+3*(month+1)/5+year+year/4-year/100+year/400)%7;
	setClock();
}
// finction code == 56

void contrast_setting()
{
	char str[64];
	if(contrast<16)
	{
		sprintf(SETTINGS_DATE_FG, "000%X00", contrast);
		sprintf(SETTINGS_TIME_FG, "000%X00", contrast);
		sprintf(SETTINGS_DATE_BG, "%X%X%X", 255-contrast, 255-contrast, 255-contrast);
	}
	else if(contrast>239)
	{
		sprintf(SETTINGS_DATE_FG, "00%X00", contrast);
		sprintf(SETTINGS_TIME_FG, "00%X00", contrast);
		sprintf(SETTINGS_DATE_BG, "0%X0%X0%X", 255-contrast, 255-contrast, 255-contrast);
	}
	else
	{
		sprintf(SETTINGS_DATE_FG, "00%X00", contrast);
		sprintf(SETTINGS_TIME_FG, "00%X00", contrast);
		sprintf(SETTINGS_DATE_BG, "%X%X%X", 255-contrast, 255-contrast, 255-contrast);
	}
}
// finction code == 57