using System;

public class User
{
    public string ID { get; } = "00";
    public string Passcode { get; set; } = "00";
    public bool isAdmin { get; set; } = true;
    public string FirstName { get; set; } = "Admin";
    public string LastName { get; set; } = "Admin";
    public bool Enabled { get; set; } = true;
    public bool Locked { get; set; } = false;
    public bool RequiredPasswordChange { get; set; } = true;
    public bool AllowPasswordReuse { get; set; } = false;
    public bool isTemperatureEnabled { get; set; } = true;
    public string AlarmTemperatures { get; set; } = "::::";
    public string AlarmInterval { get; set; } = "1";
    public string AlarmDuration { get; set; } = "2";
    public string AlarmFrequency { get; set; } = "3";
    public string AlarmVolume { get; set; } = "4";
    public bool isLaserEnabled { get; set; } = true;
    public string LaserPowers { get; set; } = "::::";
    public string LaserPulse { get; set; }
    public string LaserPowerMinMax { get; set; } = "0:200";
    public bool isMotorEnabled { get; set; } = true;
    public string MotorSpeeds { get; set; } = "::::";
    public string MotorPulse { get; set; }
    public string MotorSpeedMinMax { get; set; } = "-1000:1000";
    public int TemperatureCallibration { get; set; } = 0;
    public string LaserCallibrationMinMax { get; set; } = "0:0";
    public string MotorCallibrationMinMax { get; set; } = "0:0:";

    public User()
    {
        ID = "00";
        Passcode = "00";
        isAdmin = true;
        FirstName = "Admin";
        LastName = "Admin";
    }

    public User(string strId, string[] arrAccountData)
    {
        //fix this so we have a list to parse!
        try
        {
            // User Account 
            ID = strId;
            Passcode = arrAccountData[0];
            isAdmin = (arrAccountData[1] == "true");
            FirstName = arrAccountData[2];
            LastName = arrAccountData[3];
            Enabled = (arrAccountData[4] == "true");
            Locked = (arrAccountData[5] == "true");
            RequiredPasswordChange = (arrAccountData[6] == "true");
            AllowPasswordReuse = (arrAccountData[7] == "true");
            //Temperature
            isTemperatureEnabled = (arrAccountData[8] == "true");
            AlarmTemperatures = arrAccountData[9];
            AlarmInterval = arrAccountData[10];
            AlarmDuration = arrAccountData[11];
            AlarmFrequency = arrAccountData[12];
            AlarmVolume = arrAccountData[13];
            // Laser
            isLaserEnabled = (arrAccountData[14] == "true");
            LaserPowers = arrAccountData[15];
            LaserPulse = arrAccountData[16];
            LaserPowerMinMax = arrAccountData[17];
            // Motor
            isMotorEnabled = (arrAccountData[18] == "true");
            MotorSpeeds = arrAccountData[19];
            MotorPulse = arrAccountData[20];
            MotorSpeedMinMax = arrAccountData[21];
            // Callibration
            TemperatureCallibration = toInt(arrAccountData[22],0);
            LaserCallibrationMinMax = arrAccountData[23];
            MotorCallibrationMinMax = arrAccountData[24];
        }
        catch (Exception) { }
    }

    public string getAlarmTemperature(int iIndex)
    {
        string[] sValues = AlarmTemperatures.Split(':');
        return (iIndex < sValues.Length)?sValues[iIndex]:"";      
    }

    public void setAlarmTemperature(int iIndex, string sText)
    {
        string[] sValues = AlarmTemperatures.Split(':');
        if (iIndex < sValues.Length) sValues[iIndex] = sText;
        AlarmTemperatures = string.Join(":", sValues);
    }

    public string getLaserPower(int iIndex)
    {
        string[] sValues = LaserPowers.Split(':');
        return (iIndex < sValues.Length) ? sValues[iIndex] : "";

    }

    public void setLaserPower(int iIndex, string sText)
    {
        string[] sValues = LaserPowers.Split(':');
        if (iIndex < sValues.Length) sValues[iIndex] = sText;
        LaserPowers = string.Join(":", sValues);
    }

    public string getMotorSpeed(int iIndex)
    {
        string[] sValues = MotorSpeeds.Split(':');
        return (iIndex < sValues.Length) ? sValues[iIndex] : "";

    }

    public void setMotorSpeed(int iIndex, string sText)
    {
        string[] sValues = MotorSpeeds.Split(':');
        if (iIndex < sValues.Length) sValues[iIndex] = sText;
        MotorSpeeds = string.Join(":", sValues);
    }

    public int LaserPowerMin
    {
        get
        {
            string[] sArray = LaserPowerMinMax.Split(':');
            return toInt(sArray[0], 0);
        }
        set
        {
            string[] sArray = LaserPowerMinMax.Split(':');
            LaserPowerMinMax = value.ToString() + ":" + sArray[1];
        }
    }

    public int LaserPowerMax
    {
        get
        {
            string[] sArray = LaserPowerMinMax.Split(':');
            return toInt(sArray[1], 0);
        }
        set
        {
            string[] sArray = LaserPowerMinMax.Split(':');
            LaserPowerMinMax = sArray[0] + ":" + value.ToString();
        }
    }

    public int MotorSpeedMin
    {
        get
        {
            string[] sArray = MotorSpeedMinMax.Split(':');
            return toInt(sArray[0],0);
        }
        set
        {
            string[] sArray = MotorSpeedMinMax.Split(':');
            MotorSpeedMinMax = value.ToString() + ":" + sArray[1];
        }
    }

    public int MotorSpeedMax
    {
        get
        {
            string[] sArray = MotorSpeedMinMax.Split(':');
            return toInt(sArray[1],0);
        }
        set
        {
            string[] sArray = MotorSpeedMinMax.Split(':');
            MotorSpeedMinMax = sArray[0] + ":" + value.ToString();
        }
    }

    public int LaserCallibrationMin
    {
        get
        {
            string[] sArray = LaserCallibrationMinMax.Split(':');
            return toInt(sArray[0],0);
        }
        set
        {
            string[] sArray = LaserCallibrationMinMax.Split(':');
            LaserCallibrationMinMax = value.ToString() + ":" + sArray[1]; ;
        }
    }

    public int LaserCallibrationMax
    {
        get
        {
            string[] sArray = LaserCallibrationMinMax.Split(':');
            return toInt(sArray[1],0);
        }
        set
        {
            string[] sArray = LaserCallibrationMinMax.Split(':');
            LaserCallibrationMinMax = sArray[0] + ":" + value.ToString();
        }
    }

    public int MotorCallibrationMin
    {
        get
        {
            string[] sArray = MotorCallibrationMinMax.Split(':');
            return toInt(sArray[0],0);
        }
        set
        {
            string[] sArray = MotorCallibrationMinMax.Split(':');
            MotorCallibrationMinMax = value.ToString() + ":" + sArray[1];
        }
    }

    public int MotorCallibrationMax
    {
        get
        {
            string[] sArray = MotorCallibrationMinMax.Split(':');
            return toInt(sArray[1],0);
        }
        set
        {
            string[] sArray = LaserCallibrationMinMax.Split(':');
            LaserCallibrationMinMax = sArray[0] + ":" + value.ToString();
        }
    }

    public void setLaserPowerMin(string sText)
    {
            string[] sArray = LaserPowerMinMax.Split(':');
            LaserPowerMinMax = sText + ":" + sArray[1];
    }

    public void setLaserPowerMax(string sText)
    {
        string[] sArray = LaserPowerMinMax.Split(':');
        LaserPowerMinMax = sArray[0] + ":" + sText;
    }

    public void setMotorSpeedMin(string sText)
    {
        string[] sArray = MotorSpeedMinMax.Split(':');
        MotorSpeedMinMax = sText + ":" + sArray[1];
    }

    public void setMotorSpeedMax(string sText)
    {
        string[] sArray = MotorSpeedMinMax.Split(':');
        MotorSpeedMinMax = sArray[0] + ":" + sText;
    }

    public void setLaserCallibrationMin(string sText)
    {
        string[] sArray = LaserCallibrationMinMax.Split(':');
        LaserCallibrationMinMax = sText + ":" + sArray[1];
    }

    public void setLaserCallibrationMax(string sText)
    {
        string[] sArray = LaserCallibrationMinMax.Split(':');
        LaserCallibrationMinMax = sArray[0] + ":" + sText;
    }

    public void setMotorCallibrationMin(string sText)
    {
        string[] sArray = MotorCallibrationMinMax.Split(':');
           MotorCallibrationMinMax = sText + ":" + sArray[1];
    }

    public void setMotorCallibrationMax(string sText)
    {
        string[] sArray = LaserCallibrationMinMax.Split(':');
        LaserCallibrationMinMax = sArray[0] + ":" + sText;
    }
   
    public string getAccountValuesString()
    {
        string sText = Passcode;

        // User Account
        addToString(ref sText, isAdmin, ',');
        addToString(ref sText, FirstName, ',');
        addToString(ref sText, LastName, ',');
        addToString(ref sText, Enabled, ',');
        addToString(ref sText, Locked, ',');
        addToString(ref sText, RequiredPasswordChange, ',');
        addToString(ref sText, AllowPasswordReuse, ',');
        // Temperature
        addToString(ref sText, isTemperatureEnabled, ',');
        addToString(ref sText, AlarmTemperatures, ',');
        addToString(ref sText, AlarmInterval, ',');
        addToString(ref sText, AlarmDuration, ',');
        addToString(ref sText, AlarmFrequency, ',');
        addToString(ref sText, AlarmVolume, ',');
        // Laser
        addToString(ref sText, isLaserEnabled, ',');
        addToString(ref sText, LaserPowers, ',');
        addToString(ref sText, LaserPulse, ',');
        addToString(ref sText, LaserPowerMinMax, ',');
        // Motor                
        addToString(ref sText, isMotorEnabled, ',');
        addToString(ref sText, MotorSpeeds, ',');
        addToString(ref sText, MotorPulse, ',');
        addToString(ref sText, MotorSpeedMinMax, ',');
        addToString(ref sText, TemperatureCallibration, ',');
        addToString(ref sText, LaserCallibrationMinMax, ',');
        addToString(ref sText, MotorCallibrationMinMax, ',');

        return sText;
    }

    private void addToString(ref string sText, string sNew, char cSeparator)
    {
        sText += cSeparator + sNew;
    }

    private void addToString(ref string sText, bool bNew, char cSeparator)
    {
        sText += cSeparator + (bNew ? "true" : "false");
    }

    private void addToString(ref string sText, int iNew, char cSeparator)
    {
        sText += cSeparator + iNew.ToString();
    }

    private int toInt(string sText, int iDefault)
    {
        int iValue;
        if (!Int32.TryParse(sText, out iValue)) iValue = iDefault;

        return iValue;
    }
}
