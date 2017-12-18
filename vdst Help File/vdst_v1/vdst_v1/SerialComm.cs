using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace vdst_v1
{
    class SerialComm
    {
        private string Port;
        private int Speed;
        private bool isHalfDuplex;
        private int DataBits;
        private int StopBits;
        private string Parity;
        private string FlowControl;
        private int timeout;
        public string SyncType { get; set; } = "Synchronize All";
        public string Command { get; set; } = "";
        public string Response { get; } = "";
        public bool isConnected { get; set; } = false;
        public bool ConnectionFailed { get; set; } = false;
        private bool isStarted = false;
        private SerialPort myPort = null;

        public SerialComm(string sPort, string sSpeed, bool bDuplex, string sDataBits, string sStopBits, string sParity, string sFlowControl)
        {
            Port = sPort;
            Speed = toInt(sSpeed, 57600);
            isHalfDuplex = bDuplex;
            DataBits = toInt(sDataBits, 8);
            StopBits = toInt(sStopBits, 1);
            Parity = sParity;
            FlowControl = sFlowControl;
            isConnected = Connect();
        }

        public void Start(int iTimeout)
        {
            isStarted = true;
            timeout = iTimeout;
            ConnectionFailed = false;
            isConnected = Connect();
        }

        public void Stop()
        {
            myPort.Close();
            isStarted = false;
        }

        public void tick()
        {
            if (isStarted)
            {
                timeout -= 1;
                ConnectionFailed = (timeout <= 0);
            }
        }

        private int toInt(string sText, int iDefault)
        {
            int iValue;
            if (!Int32.TryParse(sText, out iValue)) iValue = iDefault;

            return iValue;
        }

        private bool Connect()
        {
            Parity myParity = System.IO.Ports.Parity.None;
            StopBits myStopBits = System.IO.Ports.StopBits.None;

            if (Parity == "Odd") myParity = System.IO.Ports.Parity.Odd;
            else if (Parity == "Even") myParity = System.IO.Ports.Parity.Even;
            else if (Parity == "Mark") myParity = System.IO.Ports.Parity.Mark;
            else if (Parity == "Space") myParity = System.IO.Ports.Parity.Space;

            if (StopBits == 1) myStopBits = System.IO.Ports.StopBits.One;
            else if (StopBits == 2) myStopBits = System.IO.Ports.StopBits.Two;

            myPort = new SerialPort(Port, Speed, myParity, DataBits, myStopBits);
            myPort.Open();
            
            return myPort.IsOpen;
        }

        public void write(string sText)
        {
            myPort.Write(sText);
        }
    }
}
