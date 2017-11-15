using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

/*
 * VisualDoSomething Application (VDTS) 
 *
 * Created by:   Matthias Reimann
 * Created time: October 2017 
 * 
 * Purpose     : [1] Manage and synchronize users for CmpE240 Diode Test System
 *               [2] Control and callibrate Diode Test System devices
 * 
 * Note        : Global values (constants, file names, etc)  are stored in vdstValues.cs
 * 
 * Issues      : [1]  Create custom dialog for errors to allow centering on form(s)
 *               [2]  Disallow disabling last admin account 
 *               [3]  Disallow disabling admin status on last admin account
 *               [4]  If Minimum/Maximum Motor Speed and Laser Power are changed, update for all users
 *               [5]  Change Help by adding help text to ToolTipText instead of Tag
 *               [6]  Set Motor control to callibrated values when Control form is loaded
 *               [7]  Set Laser Power to callibrated values when Control form is loaded
 *               [8]  Set Temp Callibrated value when Control form is loaded
 *               [9]  Enable temperating monitoring and alarm
 *               [10] Finish Control Form sync function/error indicator
 *               [11] Finish Sync form  
 */
 
namespace vdst_v1
{
    /*
    * VisualDoSomething Application (VDTS) 
    *
    * Created by:   Matthias Reimann
    * Created time: October 2017 
    * 
    * Purpose     : [1] Main screen for user management
    *
    * Issues      : [1] Try setting up arrays for common control elements to allow loops
    *               [2] Improve handling of values in tblAccounts    
    */
    
    public partial class frmMain : Form
    {
        public static User myCurrUser;// = new User();
        public static String strPassCode;
        public static List<String> lstLog = new List<string>();
        // Used for Help->View Help:
        public static SortedDictionary<String, String> tblHelp = new SortedDictionary<string, string>();
        // Used for Edit->Preferences (Values and Help Texts):
        public static SortedDictionary<String, String> tblDefaults = new SortedDictionary<string, string>();
        public static SortedDictionary<String, String> tblFieldInfo = new SortedDictionary<String, String>();
        // Used for storing account into: 
        public static SortedDictionary<string, string> tblAccounts = new SortedDictionary<string, string>();

        public static bool isNotLoading = false;
        public static bool isChanged = false;
        private string strPrevUser = "";
        private string strPrevValue = "";

        public frmMain()
        {
            bool isHelp = readKeyValueFile(vdstValues.APP_DATA_PATH + vdstValues.APP_FILE_HELP, tblHelp, vdstValues.KEY_VALUE_SEPARATOR, vdstValues.REJECT_EMPTY_VALUES);
            
            if (!readKeyValueFile(vdstValues.APP_DATA_PATH + vdstValues.APP_FILE_PREFERENCES_FIELDS, tblDefaults, vdstValues.KEY_VALUE_SEPARATOR, vdstValues.ALLOW_EMPTY_VALUE))
            {
                setDefaults(tblDefaults);
                writeKeyValueFile(vdstValues.APP_DATA_PATH + vdstValues.APP_FILE_PREFERENCES_FIELDS, tblDefaults, vdstValues.KEY_VALUE_SEPARATOR);
            }

            InitializeComponent();
            setFieldsToDefaults(tblDefaults, "MainSettings");

            //If no user account file exists, create first (admin) user
            if (!readKeyValueFile(vdstValues.APP_DATA_PATH + vdstValues.APP_FILE_ACCOUNTS, tblAccounts, vdstValues.KEY_VALUE_SEPARATOR, vdstValues.REJECT_EMPTY_VALUES))
            {
                string sAccountValues = "00,true,Admin,Admin";
                setNewAccountDefaults(ref sAccountValues);
                tblAccounts.Add("00", sAccountValues);
            }

            // Check if this is still required (moved to _show event?)
            if (isChanged)
            {
                saveChanges();
                isChanged = false;
            }

            // Show Help->View Help if no Help text file exists
            mnuMainHelpView.Visible = isHelp;
            mnuMainHelpSep1.Visible = isHelp;
        }

        #region Form Events
        private void frmMain_Shown(object sender, EventArgs e)
        {
            using (frmLogin frmTemp = new frmLogin())
            {

                if (frmTemp.ShowDialog(this) == DialogResult.Cancel)
                {
                    if (isChanged)
                    {
                        tblAccounts[myCurrUser.ID] = myCurrUser.getAccountValuesString();
                        saveChanges();
                    }
                    System.Environment.Exit(1);
                }
            }

            if (myCurrUser.RequiredPasswordChange)
            {
                using (frmPasscode frmTemp = new frmPasscode())
                {
                    frmTemp.lblPasscodeOld.Enabled = false;
                    frmTemp.txtPasscodeOld.Enabled = false;
                    frmTemp.sAdminPassword = "";
                    frmTemp.sCurrID = "";
                    frmTemp.txtPasscodeOld.Text = myCurrUser.ID + myCurrUser.Passcode;

                    if (frmTemp.ShowDialog(this) == DialogResult.Cancel)
                    {
                        Application.Exit();
                    }
                }
            }

            strPrevUser = myCurrUser.ID;

            // Set Admin fields
            if (myCurrUser.isAdmin) mnuMainFileSave.Text = "&Save User List";
            mnuMainEdit.Visible = myCurrUser.isAdmin;
            mnuMainControl.Enabled = !myCurrUser.isAdmin;
            mnuMainEditAdd.Enabled = myCurrUser.isAdmin;
            // Users Panel
            btnMainAddUser.Enabled = myCurrUser.isAdmin;
            txtMainSettingsAccountId.Visible = !myCurrUser.isAdmin;
            cmbMainSettingsAccountId.Visible = myCurrUser.isAdmin;
            chkMainSettingsAdmin.Visible = myCurrUser.isAdmin;
            chkMainSettingsAccountEnabled.Enabled = myCurrUser.isAdmin;
            chkMainSettingsAccountLocked.Enabled = myCurrUser.isAdmin;
            chkMainSettingsAccountMustChange.Enabled = myCurrUser.isAdmin;
            chkMainSettingsAccountAllowReuse.Enabled = myCurrUser.isAdmin;
            // Temperature
            chkMainSettingsTempEnabled.Enabled = myCurrUser.isAdmin;
            // Laser
            chkMainSettingsLaserEnabled.Enabled = myCurrUser.isAdmin;
            lblMainSettingsLaserRange.Enabled = myCurrUser.isAdmin;
            txtMainSettingsLaserRangeMin.Enabled = myCurrUser.isAdmin;
            lblMainSettingsLaserTo.Enabled = myCurrUser.isAdmin;
            txtMainSettingsLaserRangeMax.Enabled = myCurrUser.isAdmin;
            // Motor
            chkMainSettingsMotorEnabled.Enabled = myCurrUser.isAdmin;
            lblMainSettingsMotorRange.Enabled = myCurrUser.isAdmin;
            lblMainSettingsMotorTo.Enabled = myCurrUser.isAdmin;
            txtMainSettingsMotorRangeMin.Enabled = myCurrUser.isAdmin;
            txtMainSettingsMotorRangeMax.Enabled = myCurrUser.isAdmin;

            // Set user fields
            setFieldstoAccountValues(myCurrUser);

            setTableToAccountValues();

            // Enable executing of field events
            isNotLoading = true;

            // If password was changed, save changes
            if (isChanged) saveChanges();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            exitCheck();
        }
#endregion
        
        #region Get/Set Defaults and Field Values
        private void setDefaults(SortedDictionary<string, string> oData)
        {
            // To Do: Consider reading field defaults to populate this list 
            //        (May require changing use of tag for allowing negative values)             
            oData.Add("chkAccountEnabled", "true");
            oData.Add("chkAccountLocked", "false");
            oData.Add("chkAccountMustChange", "true");
            oData.Add("chkAccountAllowReuse", "false");
            oData.Add("cmbSyncPort", "COM1");
            oData.Add("cmbSyncBaud", "57600");
            oData.Add("chkSyncDuplex", "true");
            oData.Add("cmbSyncDataBits", "8");
            oData.Add("cmbSyncStopBits", "1");
            oData.Add("cmbSyncParity", "None");
            oData.Add("cmbSyncFlowControl", "None");
            oData.Add("cmbSyncTimeout", "1");
            oData.Add("chkTempEnabled", "true");
            oData.Add("cmbTempUnits", "Fahrenheit");//Celsius");
            oData.Add("txtTempAlarm1", "");
            oData.Add("txtTempAlarm2", "");
            oData.Add("txtTempAlarm3", "");
            oData.Add("txtTempAlarm4", "");
            oData.Add("txtTempAlarm5", "");
            oData.Add("txtTempAlarmInterval", "1000");
            oData.Add("txtTempAlarmDuration", "250");
            oData.Add("txtTempAlarmFrequency", "500");
            oData.Add("cmbTempAlarmVolume", "Medium");
            oData.Add("chkTempAlarmAudible", "true");
            oData.Add("chkLaserEnabled", "true");
            oData.Add("txtLaserRangeMin", "2");
            oData.Add("txtLaserRangeMax", "400");
            oData.Add("txtLaserPower1", "");
            oData.Add("txtLaserPower2", "");
            oData.Add("txtLaserPower3", "");
            oData.Add("txtLaserPower4", "");
            oData.Add("txtLaserPower5", "");
            oData.Add("txtLaserPulse", "1");
            oData.Add("chkMotorEnabled", "true");
            oData.Add("txtMotorRangeMin", "-2000");
            oData.Add("txtMotorRangeMax", "2000");
            oData.Add("txtMotorSpeed1", "");
            oData.Add("txtMotorSpeed2", "");
            oData.Add("txtMotorSpeed3", "");
            oData.Add("txtMotorSpeed4", "");
            oData.Add("txtMotorSpeed5", "");
            oData.Add("txtMotorPulse", "3");
        }

        private void setNewAccountDefaults(ref string sText)
        {
            // User Account
            addToString(ref sText, chkMainSettingsAccountEnabled.Checked, ',');
            addToString(ref sText, chkMainSettingsAccountLocked.Checked, ',');
            addToString(ref sText, chkMainSettingsAccountMustChange.Checked, ',');
            addToString(ref sText, chkMainSettingsAccountAllowReuse.Checked, ',');
            // Temperature
            addToString(ref sText, chkMainSettingsTempEnabled.Checked, ',');
            addToString(ref sText, txtMainSettingsTempAlarm1.Text, ',');
            addToString(ref sText, txtMainSettingsTempAlarm2.Text, ':');
            addToString(ref sText, txtMainSettingsTempAlarm3.Text, ':');
            addToString(ref sText, txtMainSettingsTempAlarm4.Text, ':');
            addToString(ref sText, txtMainSettingsTempAlarm5.Text, ':');
            addToString(ref sText, txtMainSettingsTempAlarmInterval.Text, ',');
            addToString(ref sText, txtMainSettingsTempAlarmDuration.Text, ',');
            addToString(ref sText, txtMainSettingsTempAlarmFrequency.Text, ',');
            addToString(ref sText, cmbMainSettingsTempAlarmVolume.Text, ',');
            // Laser
            addToString(ref sText, chkMainSettingsLaserEnabled.Checked, ',');
            addToString(ref sText, txtMainSettingsLaserPower1.Text, ',');
            addToString(ref sText, txtMainSettingsLaserPower2.Text, ':');
            addToString(ref sText, txtMainSettingsLaserPower3.Text, ':');
            addToString(ref sText, txtMainSettingsLaserPower4.Text, ':');
            addToString(ref sText, txtMainSettingsLaserPower5.Text, ':');
            addToString(ref sText, txtMainSettingsLaserPulse.Text, ',');
            addToString(ref sText, txtMainSettingsLaserRangeMin.Text, ',');
            addToString(ref sText, txtMainSettingsLaserRangeMax.Text, ':');
            // Motor                
            addToString(ref sText, chkMainSettingsMotorEnabled.Checked, ',');
            addToString(ref sText, txtMainSettingsMotorSpeed1.Text, ',');
            addToString(ref sText, txtMainSettingsMotorSpeed2.Text, ':');
            addToString(ref sText, txtMainSettingsMotorSpeed3.Text, ':');
            addToString(ref sText, txtMainSettingsMotorSpeed4.Text, ':');
            addToString(ref sText, txtMainSettingsMotorSpeed5.Text, ':');
            addToString(ref sText, txtMainSettingsMotorPulse.Text, ',');
            addToString(ref sText, txtMainSettingsMotorRangeMin.Text, ',');
            addToString(ref sText, txtMainSettingsMotorRangeMax.Text, ':');
        }

        private void setFieldsToDefaults(SortedDictionary<string, string> oData, string strForm)
        {
            foreach (KeyValuePair<string, string> kvpItem in oData)
            {
                string strFieldType = kvpItem.Key.Substring(0, 3);
                string strName = kvpItem.Key.Substring(3);
                string strFieldName = strFieldType + strForm + strName;

                Control ctl = Controls.Find(strFieldName, true).FirstOrDefault();

                if (ctl != null)
                {
                    if (strFieldType == "chk")
                        ((CheckBox)ctl).Checked = (kvpItem.Value == "true");
                    else if (strFieldType == "chk")
                        ((CheckBox)ctl).Text = kvpItem.Value;
                    else if (strFieldType == "txt")
                        ((TextBox)ctl).Text = kvpItem.Value;

                }
                else
                {
                    if (strName.Substring(0, 4) != "Sync")
                        writeLog(vdstValues.APP_DATA_PATH + "Local.log", "Invalid config value {" + kvpItem.Key + "} -> {" + strFieldName + "}");
                }
            }
        }

        private void setFieldstoAccountValues(User aUser)
        {
            // User Account
            cmbMainSettingsAccountId.Text = aUser.ID;
            txtMainSettingsAccountId.Text = aUser.ID;
            txtMainSettingsAccountFirstName.Text = aUser.FirstName;
            txtMainSettingsAccountLastName.Text = aUser.LastName;
            chkMainSettingsAdmin.Checked = aUser.isAdmin;
            chkMainSettingsAccountEnabled.Checked = aUser.Enabled;
            chkMainSettingsAccountLocked.Checked = aUser.Locked;
            chkMainSettingsAccountMustChange.Checked = aUser.RequiredPasswordChange;
            chkMainSettingsAccountAllowReuse.Checked = aUser.AllowPasswordReuse;
            // Temperature
            chkMainSettingsTempEnabled.Checked = aUser.isTemperatureEnabled;
            txtMainSettingsTempAlarm1.Text = aUser.getAlarmTemperature(0);
            txtMainSettingsTempAlarm2.Text = aUser.getAlarmTemperature(1);
            txtMainSettingsTempAlarm3.Text = aUser.getAlarmTemperature(2);
            txtMainSettingsTempAlarm4.Text = aUser.getAlarmTemperature(3);
            txtMainSettingsTempAlarm5.Text = aUser.getAlarmTemperature(4);
            txtMainSettingsTempAlarmInterval.Text = aUser.AlarmInterval;
            txtMainSettingsTempAlarmDuration.Text = aUser.AlarmDuration;
            txtMainSettingsTempAlarmFrequency.Text = aUser.AlarmFrequency;
            cmbMainSettingsTempAlarmVolume.Text = aUser.AlarmVolume;
            // Laser
            chkMainSettingsLaserEnabled.Checked = aUser.isLaserEnabled;
            txtMainSettingsLaserPower1.Text = aUser.getLaserPower(0);
            txtMainSettingsLaserPower2.Text = aUser.getLaserPower(1);
            txtMainSettingsLaserPower3.Text = aUser.getLaserPower(2);
            txtMainSettingsLaserPower4.Text = aUser.getLaserPower(3);
            txtMainSettingsLaserPower5.Text = aUser.getLaserPower(4);
            txtMainSettingsLaserPulse.Text = aUser.LaserPulse;
            txtMainSettingsLaserRangeMin.Text = aUser.LaserPowerMin.ToString();
            txtMainSettingsLaserRangeMax.Text = aUser.LaserPowerMax.ToString();
            // Motor
            chkMainSettingsMotorEnabled.Checked = aUser.isMotorEnabled;
            txtMainSettingsMotorSpeed1.Text = aUser.getMotorSpeed(0);
            txtMainSettingsMotorSpeed2.Text = aUser.getMotorSpeed(1);
            txtMainSettingsMotorSpeed3.Text = aUser.getMotorSpeed(2);
            txtMainSettingsMotorSpeed4.Text = aUser.getMotorSpeed(3);
            txtMainSettingsMotorSpeed5.Text = aUser.getMotorSpeed(4);
            txtMainSettingsMotorPulse.Text = aUser.MotorPulse;
            txtMainSettingsMotorRangeMin.Text = aUser.MotorSpeedMin.ToString();
            txtMainSettingsMotorRangeMax.Text = aUser.MotorSpeedMax.ToString();
        }

        private void setTableToAccountValues()
        {
            if (myCurrUser.isAdmin)
            {
                foreach (KeyValuePair<string, string> kvpItem in tblAccounts)
                {
                    string[] sArray = kvpItem.Value.Split(',');
                    tblMainUsers.Rows.Add(kvpItem.Key, sArray[vdstValues.USER_LAST_NAME], sArray[vdstValues.USER_FIRST_NAME], sArray[vdstValues.USER_IS_ADMIN] == "true" ? true : false, sArray[vdstValues.USER_ENABLED] == "true" ? true : false, sArray[vdstValues.USER_LOCKED] == "true" ? true : false);
                    cmbMainSettingsAccountId.Items.Add((tblMainUsers.RowCount - 1).ToString("00"));
                    if (tblMainUsers.Rows.Count >= vdstValues.MAX_ACCOUNTS) break;
                }
            }
            else
            {
                tblMainUsers.Rows.Add(myCurrUser.ID, myCurrUser.LastName, myCurrUser.FirstName, myCurrUser.isAdmin, myCurrUser.Enabled, myCurrUser.Locked);
            }
        }

        private void setAccountValuesToFields(User aUser)
        {
            // User Account
            aUser.FirstName = txtMainSettingsAccountFirstName.Text;
            aUser.LastName = txtMainSettingsAccountLastName.Text;
            aUser.isAdmin = chkMainSettingsAdmin.Checked;
            aUser.Enabled = chkMainSettingsAccountEnabled.Checked;
            aUser.Locked = chkMainSettingsAccountLocked.Checked;
            aUser.RequiredPasswordChange = chkMainSettingsAccountMustChange.Checked;
            aUser.AllowPasswordReuse = chkMainSettingsAccountAllowReuse.Checked;
            // Temperature
            aUser.isTemperatureEnabled = chkMainSettingsTempEnabled.Checked;
            aUser.setMotorSpeed(0, txtMainSettingsTempAlarm1.Text);
            aUser.setMotorSpeed(1, txtMainSettingsTempAlarm1.Text);
            aUser.setMotorSpeed(2, txtMainSettingsTempAlarm1.Text);
            aUser.setMotorSpeed(3, txtMainSettingsTempAlarm1.Text);
            aUser.setMotorSpeed(4, txtMainSettingsTempAlarm1.Text);
            aUser.AlarmInterval = txtMainSettingsTempAlarmInterval.Text;
            aUser.AlarmDuration = txtMainSettingsTempAlarmDuration.Text;
            aUser.AlarmFrequency = txtMainSettingsTempAlarmFrequency.Text;
            aUser.AlarmVolume = cmbMainSettingsTempAlarmVolume.Text;
            // Laser
            aUser.isLaserEnabled = chkMainSettingsLaserEnabled.Checked;
            aUser.setLaserPower(0, txtMainSettingsLaserPower1.Text);
            aUser.setLaserPower(1, txtMainSettingsLaserPower2.Text);
            aUser.setLaserPower(2, txtMainSettingsLaserPower3.Text);
            aUser.setLaserPower(3, txtMainSettingsLaserPower4.Text);
            aUser.setLaserPower(4, txtMainSettingsLaserPower5.Text);
            aUser.LaserPulse = txtMainSettingsLaserPulse.Text;
            aUser.setLaserPowerMin(txtMainSettingsLaserRangeMin.Text);
            aUser.setLaserPowerMax(txtMainSettingsLaserRangeMax.Text);
            // Motor
            aUser.isMotorEnabled = chkMainSettingsMotorEnabled.Checked;
            aUser.setMotorSpeed(0, txtMainSettingsMotorSpeed1.Text);
            aUser.setMotorSpeed(1, txtMainSettingsMotorSpeed2.Text);
            aUser.setMotorSpeed(2, txtMainSettingsMotorSpeed3.Text);
            aUser.setMotorSpeed(3, txtMainSettingsMotorSpeed4.Text);
            aUser.setMotorSpeed(4, txtMainSettingsMotorSpeed5.Text);
            aUser.MotorPulse = txtMainSettingsMotorPulse.Text;
            aUser.setMotorSpeedMin(txtMainSettingsMotorRangeMin.Text);
            aUser.setMotorSpeedMax(txtMainSettingsMotorRangeMax.Text);
        }
        #endregion

        #region Menu Functions

        #region File Menu
        private void mnuMainFileSave_Click(object sender, EventArgs e)
        {
            saveChanges();
        }

        private void mnuMainFileSync_Click(object sender, EventArgs e)
        {
            using (frmSync frmTemp = new frmSync())
            {
                frmTemp.ShowDialog(this);
            }
        }

        private void mnuMainFileLogout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void mnuMainFileExit_Click(object sender, EventArgs e)
        {
            //exitCheck(vdstValues.FORCE_CLOSE);
            Application.Exit();
        }
        #endregion

        #region Edit Menu
        // Edit Menu Options
        private void mnuMainEditAdd_Click(object sender, EventArgs e)
        {
            addUser();
        }

        private void mnuMainEditPrefsGeneral_Click(object sender, EventArgs e)
        {
            openPreferences(vdstValues.TAB_GENERAL);
        }

        private void mnuMainEditPrefsControls_Click(object sender, EventArgs e)
        {
            openPreferences(vdstValues.TAB_CONTROLS);
        }

        private void mnuMainEditPrefsControlsTemp_Click(object sender, EventArgs e)
        {
            openPreferences(vdstValues.TAB_CONTROLS + vdstValues.TAB_TEMP);
        }

        private void mnuMainEditPrefsControlsLaser_Click(object sender, EventArgs e)
        {
            openPreferences(vdstValues.TAB_CONTROLS + vdstValues.TAB_LASER);
        }

        private void mnuMainEditPrefsControlsMotor_Click(object sender, EventArgs e)
        {
            openPreferences(vdstValues.TAB_CONTROLS + vdstValues.TAB_MOTOR);
        }

        private void mnuMainEditPrefsHelp_Click(object sender, EventArgs e)
        {
            openPreferences(vdstValues.TAB_HELP);
        }

        private void openPreferences(int iPanel)
        {
            using (frmDefaults frmTemp = new frmDefaults())
            {
                if (iPanel >= 10)
                {
                    frmTemp.tabDefaultsControls.SelectedIndex = iPanel - 10;
                    iPanel /= 10;
                }
                readKeyValueFile(vdstValues.APP_DATA_PATH + vdstValues.APP_FILE_PREFERENCES_HELP, tblFieldInfo, vdstValues.KEY_VALUE_SEPARATOR, vdstValues.ALLOW_EMPTY_VALUE);
                frmTemp.tabDefaults.SelectedIndex = iPanel;

                if (frmTemp.ShowDialog(this) == DialogResult.OK)
                {
                    writeKeyValueFile(vdstValues.APP_DATA_PATH + vdstValues.APP_FILE_PREFERENCES_FIELDS, tblDefaults, vdstValues.KEY_VALUE_SEPARATOR);
                    writeKeyValueFile(vdstValues.APP_DATA_PATH + vdstValues.APP_FILE_PREFERENCES_HELP, tblFieldInfo, vdstValues.KEY_VALUE_SEPARATOR);
                }
            }
        }
        #endregion

        #region View Menu
        private void mnuMainViewLogLocal_Click(object sender, EventArgs e)
        {
            showLog("Local");
        }

        private void mnuMainViewLogDevice_Click(object sender, EventArgs e)
        {
            showLog("Device");
        }
        #endregion

        #region Control Menu
        private void mnuMainControlTemp_Click(object sender, EventArgs e)
        {
            openControl(vdstValues.TAB_TEMP);
        }

        private void mnuMainControlLaser_Click(object sender, EventArgs e)
        {
            openControl(vdstValues.TAB_LASER);
        }

        private void mnuMainControlMotor_Click(object sender, EventArgs e)
        {
            openControl(vdstValues.TAB_MOTOR);
        }

        private void openControl(int iPanel)
        {
            using (frmControl frmTemp = new frmControl())
            {
                frmTemp.tabControl.SelectedIndex = iPanel;
                frmTemp.setAlarmImage(chkMainSettingsTempAlarmAudible.Checked ? vdstValues.ALARM_AUDIBLE : vdstValues.ALARM_SILENT);

                frmTemp.ShowDialog(this);
            }
        }
        #endregion




        
        
        

        private void showLog(string strType)
        {
            readLog(vdstValues.APP_DATA_PATH + strType + ".log");

            using (frmLog frmTemp = new frmLog())
            {
                frmTemp.Text += " (" + strType + ")";
                frmTemp.Tag = strType;

                frmTemp.ShowDialog(this);
            }
        }

        // Help Menu Options
        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmHelp frmTemp = new frmHelp())
            {
                frmTemp.ShowDialog(this);
            }
        }

        private void mnuMainHelpAbout_Click(object sender, EventArgs e)
        {
            using (frmAbout frmTemp = new frmAbout())
            {
                frmTemp.ShowDialog(this);
            }
        }

        #endregion

        #region Buttons
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            addUser();
        }

        private void addUser()
        {
            int rowCount = tblMainUsers.Rows.Count;
            writeLog(vdstValues.APP_DATA_PATH + vdstValues.APP_FILE_LOG, "Added User");

            if (rowCount < vdstValues.MAX_ACCOUNTS)
            {
                string sAccountValues = "00,false,,";
                setNewAccountDefaults(ref sAccountValues);
                tblAccounts.Add(rowCount.ToString("00"), sAccountValues);

                tblMainUsers.Rows.Add(rowCount.ToString("00"));
                cmbMainSettingsAccountId.Items.Add(rowCount.ToString("00"));
                rowCount++;
                isChanged = true;
                mnuMainFileSave.Enabled = true;
                isChanged = true;
            }
            else
            {
                MessageBox.Show(this, "Maximum user number reached.", "VDST", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }
        }

        private void btnMainSettingsAccountChangePassword_Click(object sender, EventArgs e)
        {
            using (frmPasscode frmTemp = new frmPasscode())
            {
                if (myCurrUser.isAdmin)
                {
                    frmTemp.Text = frmTemp.Text + " [User: " + cmbMainSettingsAccountId.Text + "]";
                    frmTemp.lblPasscodeOld.Text = "Admin Passcode:";
                    frmTemp.sAdminPassword = myCurrUser.ID + myCurrUser.Passcode;
                    frmTemp.sCurrID = cmbMainSettingsAccountId.Text;
                }
                else
                {
                    frmTemp.lblPasscodeOld.Text = "Old Passcode:";
                    frmTemp.sAdminPassword = ""; ;
                    frmTemp.sCurrID = myCurrUser.ID;

                }


                if (frmTemp.ShowDialog(this) == DialogResult.Cancel)
                {
                    Application.Exit();
                }
            }
        }

        private void btnMainSettingsMotorDefaults_Click(object sender, EventArgs e)
        {
            restoreMotorDefaults(vdstValues.REFRESH);
        }



        private void btnMainSettingsLaserDefaults_Click(object sender, EventArgs e)
        {
            restoreLaserDefaults(vdstValues.REFRESH);
        }

        private void btnMainSettingsTempDefaults_Click(object sender, EventArgs e)
        {
            restoreTemperatureDefaults(vdstValues.REFRESH);
        }

        private void btnMainSettingsAccountDefaults_Click(object sender, EventArgs e)
        {
            restoreTemperatureDefaults(vdstValues.NO_REFRESH);
            restoreLaserDefaults(vdstValues.NO_REFRESH);
            restoreMotorDefaults(vdstValues.REFRESH);
        }
        #endregion

        #region File I/O
        private bool readKeyValueFile(String strFileName, SortedDictionary<string, string> oData, string strSeparator, bool allowEmptyValue)
        {
            bool success = true;

            if (oData.Count < 1)
            {
                try
                {
                    using (StreamReader reader = new StreamReader(strFileName))
                    {
                        String strLine;
                        while ((strLine = reader.ReadLine()) != null)
                        {
                            String[] arrKeyValuePair = Regex.Split(strLine, strSeparator);
                            String strKey = arrKeyValuePair[0].Trim();
                            String strValue = arrKeyValuePair[0].Trim();
                            if (strKey != "" && (allowEmptyValue || strValue != ""))
                            {
                                oData.Add(arrKeyValuePair[0], arrKeyValuePair[1]);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    success = false;
                }
            }
            return success && (oData.Count > 0);
        }

        private bool writeKeyValueFile(String strFileName, SortedDictionary<string, string> oData, string strSeparator)
        {
            bool success = oData.Count > 0;
            bool isNotFirstEntry = false;

            if (success)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(strFileName))
                    {
                        foreach (KeyValuePair<string, string> kvpItem in oData)
                        {
                            if (isNotFirstEntry) writer.Write(Environment.NewLine);
                            writer.Write("{0}{1}{2}", kvpItem.Key, vdstValues.KEY_VALUE_SEPARATOR, kvpItem.Value);
                            isNotFirstEntry = true;
                        }
                    }
                }
                catch (Exception)
                {
                    success = false;
                }
            }

            return success;

        }

        public static void writeLog(String strFileName, String strMessage)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(strFileName))
                {
                    writer.WriteLine("{0}.{1} [{2}]: {3}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), "00", strMessage);
                }
            }
            catch (Exception) { }
        }

        private bool readLog(String strFileName)
        {
            bool success = true;
            lstLog.Clear();

            try
            {
                using (StreamReader reader = new StreamReader(strFileName))
                {
                    String strLine;
                    while ((strLine = reader.ReadLine()) != null)
                    {
                        lstLog.Add(strLine);
                    }
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        private bool saveChanges()
        {
            setAccountValuesToFields(myCurrUser);
            tblAccounts[myCurrUser.ID] = myCurrUser.getAccountValuesString();

            if (writeKeyValueFile(vdstValues.APP_DATA_PATH + vdstValues.APP_FILE_ACCOUNTS, tblAccounts, vdstValues.KEY_VALUE_SEPARATOR))
            {
                mnuMainFileSave.Enabled = false;
                isChanged = false;

                return true;
            }

            return false;
        }
        #endregion

        #region Field Events

        #region Temperature Alarm Temperature Fields
        private void txtMainSettingsTempAlarm1_TextChanged(object sender, EventArgs e)
        {
            txtMainSettingsTempAlarm2.Enabled = (txtMainSettingsTempAlarm1.Text != "");
            txtMainSettingsTempAlarm3.Enabled = (txtMainSettingsTempAlarm1.Text != "" && txtMainSettingsTempAlarm2.Text != "");
            txtMainSettingsTempAlarm4.Enabled = (txtMainSettingsTempAlarm1.Text != "" && txtMainSettingsTempAlarm2.Text != "" && txtMainSettingsTempAlarm3.Text != "");
            txtMainSettingsTempAlarm5.Enabled = (txtMainSettingsTempAlarm1.Text != "" && txtMainSettingsTempAlarm2.Text != "" && txtMainSettingsTempAlarm3.Text != "" && txtMainSettingsTempAlarm4.Text != "");
        }

        private void txtMainSettingsTempAlarm2_TextChanged(object sender, EventArgs e)
        {
            txtMainSettingsTempAlarm3.Enabled = (txtMainSettingsTempAlarm2.Text != "");
            txtMainSettingsTempAlarm4.Enabled = (txtMainSettingsTempAlarm2.Text != "" && txtMainSettingsTempAlarm3.Text != "");
            txtMainSettingsTempAlarm5.Enabled = (txtMainSettingsTempAlarm2.Text != "" && txtMainSettingsTempAlarm3.Text != "" && txtMainSettingsTempAlarm4.Text != "");
        }

        private void txtMainSettingsTempAlarm3_TextChanged(object sender, EventArgs e)
        {
            txtMainSettingsTempAlarm4.Enabled = (txtMainSettingsTempAlarm3.Text != "");
            txtMainSettingsTempAlarm5.Enabled = (txtMainSettingsTempAlarm3.Text != "" && txtMainSettingsTempAlarm4.Text != "");
        }

        private void txtMainSettingsTempAlarm4_TextChanged(object sender, EventArgs e)
        {
            txtMainSettingsTempAlarm5.Enabled = (txtMainSettingsTempAlarm4.Text != "");
        }
        #endregion

        #region Laser Power Fields
        private void txtMainSettingsLaserPower1_TextChanged(object sender, EventArgs e)
        {
            txtMainSettingsLaserPower2.Enabled = (txtMainSettingsLaserPower1.Text != "");
            txtMainSettingsLaserPower3.Enabled = (txtMainSettingsLaserPower1.Text != "" && txtMainSettingsLaserPower2.Text != "");
            txtMainSettingsLaserPower4.Enabled = (txtMainSettingsLaserPower1.Text != "" && txtMainSettingsLaserPower2.Text != "" && txtMainSettingsLaserPower3.Text != "");
            txtMainSettingsLaserPower5.Enabled = (txtMainSettingsLaserPower1.Text != "" && txtMainSettingsLaserPower2.Text != "" && txtMainSettingsLaserPower3.Text != "" && txtMainSettingsLaserPower4.Text != "");
        }

        private void txtMainSettingsLaserPower2_TextChanged(object sender, EventArgs e)
        {
            txtMainSettingsLaserPower3.Enabled = (txtMainSettingsLaserPower2.Text != "");
            txtMainSettingsLaserPower4.Enabled = (txtMainSettingsLaserPower2.Text != "" && txtMainSettingsLaserPower3.Text != "");
            txtMainSettingsLaserPower5.Enabled = (txtMainSettingsLaserPower2.Text != "" && txtMainSettingsLaserPower3.Text != "" && txtMainSettingsLaserPower4.Text != "");
        }

        private void txtMainSettingsLaserPower3_TextChanged(object sender, EventArgs e)
        {
            txtMainSettingsLaserPower4.Enabled = (txtMainSettingsLaserPower3.Text != "");
            txtMainSettingsLaserPower5.Enabled = (txtMainSettingsLaserPower3.Text != "" && txtMainSettingsLaserPower4.Text != "");
        }

        private void txtMainSettingsLaserPower4_TextChanged(object sender, EventArgs e)
        {
            txtMainSettingsLaserPower5.Enabled = (txtMainSettingsLaserPower4.Text != "");
        }
        #endregion

        #region Motor Speed Fields
        private void txtMainSettingsMotorSpeed1_TextChanged(object sender, EventArgs e)
        {
            txtMainSettingsMotorSpeed2.Enabled = (txtMainSettingsMotorSpeed1.Text != "");
            txtMainSettingsMotorSpeed3.Enabled = (txtMainSettingsMotorSpeed1.Text != "" && txtMainSettingsMotorSpeed2.Text != "");
            txtMainSettingsMotorSpeed4.Enabled = (txtMainSettingsMotorSpeed1.Text != "" && txtMainSettingsMotorSpeed2.Text != "" && txtMainSettingsMotorSpeed3.Text != "");
            txtMainSettingsMotorSpeed5.Enabled = (txtMainSettingsMotorSpeed1.Text != "" && txtMainSettingsMotorSpeed2.Text != "" && txtMainSettingsMotorSpeed3.Text != "" && txtMainSettingsMotorSpeed4.Text != "");
        }

        private void txtMainSettingsMotorSpeed2_TextChanged(object sender, EventArgs e)
        {
            txtMainSettingsMotorSpeed3.Enabled = (txtMainSettingsMotorSpeed2.Text != "");
            txtMainSettingsMotorSpeed4.Enabled = (txtMainSettingsMotorSpeed2.Text != "" && txtMainSettingsMotorSpeed3.Text != "");
            txtMainSettingsMotorSpeed5.Enabled = (txtMainSettingsMotorSpeed2.Text != "" && txtMainSettingsMotorSpeed3.Text != "" && txtMainSettingsMotorSpeed4.Text != "");
        }

        private void txtMainSettingsMotorSpeed3_TextChanged(object sender, EventArgs e)
        {
            txtMainSettingsMotorSpeed4.Enabled = (txtMainSettingsMotorSpeed3.Text != "");
            txtMainSettingsMotorSpeed5.Enabled = (txtMainSettingsMotorSpeed3.Text != "" && txtMainSettingsMotorSpeed4.Text != "");
        }

        private void txtMainSettingsMotorSpeed4_TextChanged(object sender, EventArgs e)
        {
            txtMainSettingsMotorSpeed5.Enabled = (txtMainSettingsMotorSpeed4.Text != "");
        }
        #endregion

        private void txtField_GotFocus(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;
            strPrevValue = ((TextBox)ctl).Text;

        }

        private void txtField_LostFocus(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;
            if (((TextBox)ctl).Text != strPrevValue)
            {
                isChanged = true;
                mnuMainFileSave.Enabled = true;
            }
        }

        private void txtControlNumeric_GotFocus(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;
            strPrevValue = ((TextBox)ctl).Text;
        }

        private void txtControlNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control ctl = (Control)sender;

            bool isNotLeadingMinus = (e.KeyChar != 45 || (e.KeyChar == 45 && ((TextBox)ctl).SelectionStart > 0)); //ctl.Text.Length > 0;
            e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 11 && isNotLeadingMinus;
        }

        private void txtControlNumeric_LostFocus(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;
            String strName = ctl.Name;
            int iValue;

            if (((TextBox)ctl).Text != strPrevValue)
            {
                isChanged = true;
                mnuMainFileSave.Enabled = true;
            }

            // If the tag is NULL, it does not contain a note 'Allow Negative'
            if (!Int32.TryParse(ctl.Text, out iValue) || (ctl.Tag == null && iValue < 0))
                ctl.Text = strPrevValue;
            else
                ctl.Text = iValue.ToString();

            checkFieldAgainstMinMax(strName);
        }

        private void txtControlNumericPair_GotFocus(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;
            string strName = ctl.Name;
            strPrevValue = ((TextBox)ctl).Text;

            if (tmrMainValueSwitch.Enabled && strName.IndexOf(tmrMainValueSwitch.Tag.ToString()) > -1)
            {
                tmrMainValueSwitch.Stop();
            }
        }

        private void txtControlNumericPair_LostFocus(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;
            string strName = ctl.Name;
            int iValue;

            tmrMainValueSwitch.Tag = strName.Substring(0, strName.Length - 3);
            tmrMainValueSwitch.Start();

            // If the tag is NULL, it does not contain a note 'Allow Negative'
            if (!Int32.TryParse(ctl.Text, out iValue) || (ctl.Tag == null && iValue < 0))
                ctl.Text = strPrevValue;
            else
                ctl.Text = iValue.ToString();
        }

        private void txtMainSettingsAccountFirstName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tblMainUsers.Rows[Int32.Parse(txtMainSettingsAccountId.Text)].Cells[vdstValues.GRID_FIRST_NAME].Value = txtMainSettingsAccountFirstName.Text;
            }
            catch (Exception) { }
        }

        private void txtMainSettingsAccountName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar < 65 || e.KeyChar > 90) && (e.KeyChar < 97 || e.KeyChar > 122) && e.KeyChar != 45 && e.KeyChar != 32 && e.KeyChar != 8 && e.KeyChar != 11;
        }

        private void txtMainSettingsAccountLastName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tblMainUsers.Rows[Int32.Parse(txtMainSettingsAccountId.Text)].Cells[vdstValues.GRID_LAST_NAME].Value = txtMainSettingsAccountLastName.Text;
            }
            catch (Exception) { }

        }

        private void chkMainSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (isNotLoading)
            {
                isChanged = true;
                mnuMainFileSave.Enabled = true;
            }
        }

        private void cmbMainSettingsAccountId_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSelected = cmbMainSettingsAccountId.Text;
            int iSelected = Int32.Parse(sSelected);
            User aUser = new User(strPrevUser, tblAccounts[sSelected].Split(','));
            setAccountValuesToFields(aUser);
            tblAccounts[aUser.ID] = aUser.getAccountValuesString();
            strPrevUser = sSelected;

            txtMainSettingsAccountId.Text = sSelected;
            tblMainUsers.CurrentRow.Selected = false;
            tblMainUsers.Rows[iSelected].Selected = true;
            tblMainUsers.CurrentCell = tblMainUsers.Rows[iSelected].Cells[0];
            aUser = new User(sSelected, tblAccounts[sSelected].Split(','));
            setFieldstoAccountValues(aUser);
        }

        private void tblMainUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var ctl = (DataGridView)sender;
            string sSelected = e.RowIndex.ToString("00");

            if (sSelected != cmbMainSettingsAccountId.Text)
            {
                cmbMainSettingsAccountId.Text = sSelected;
                txtMainSettingsAccountId.Text = sSelected;

                User aUser = new User(strPrevUser, tblAccounts[sSelected].Split(','));
                setAccountValuesToFields(aUser);
                tblAccounts[aUser.ID] = aUser.getAccountValuesString();
                strPrevUser = sSelected;

                aUser = new User(sSelected, tblAccounts[sSelected].Split(','));
                setFieldstoAccountValues(aUser);
            }

            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (ctl.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    if (e.ColumnIndex == vdstValues.TABLE_BUTTON_RESET)
                    {
                        restoreTemperatureDefaults(vdstValues.NO_REFRESH);
                        restoreLaserDefaults(vdstValues.NO_REFRESH);
                        restoreMotorDefaults(vdstValues.REFRESH);
                    }
                    else if (e.ColumnIndex == vdstValues.TABLE_BUTTON_PASSWORD)
                    {
                        using (frmPasscode frmTemp = new frmPasscode())
                        {
                            if (myCurrUser.isAdmin)
                            {
                                frmTemp.Text = frmTemp.Text + " [User: " + cmbMainSettingsAccountId.Text + "]";
                                frmTemp.lblPasscodeOld.Text = "Admin Passcode:";
                                frmTemp.sAdminPassword = myCurrUser.ID + myCurrUser.Passcode;
                                frmTemp.sCurrID = cmbMainSettingsAccountId.Text;
                            }
                            else
                            {
                                frmTemp.lblPasscodeOld.Text = "Old Passcode:";
                                frmTemp.sAdminPassword = "";
                                frmTemp.sCurrID = myCurrUser.ID;
                            }
                            if (frmTemp.ShowDialog(this) == DialogResult.OK) isChanged = true;
                        }
                    }
                }
                else if (ctl.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    bool newChecked = !(bool)ctl.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    ctl.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = newChecked;

                    if (e.ColumnIndex == vdstValues.GRID_IS_ADMIN)
                        chkMainSettingsAdmin.Checked = newChecked;
                    else if (e.ColumnIndex == vdstValues.GRID_ENABLED)
                        chkMainSettingsAccountEnabled.Checked = newChecked;
                    else if (e.ColumnIndex == vdstValues.GRID_LOCKED)
                        chkMainSettingsAccountLocked.Checked = newChecked;

                    isChanged = true;
                    mnuMainFileSave.Enabled = true;
                }
                else if (ctl.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn)
                {
                    if (e.ColumnIndex == vdstValues.GRID_FIRST_NAME)
                        strPrevValue = txtMainSettingsAccountFirstName.Text;
                    if (e.ColumnIndex == vdstValues.GRID_LAST_NAME)
                        strPrevValue = txtMainSettingsAccountLastName.Text;
                }
            }
        }

        private void tblMainUsers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var ctl = (DataGridView)sender;

            if (e.ColumnIndex >= 0 && ctl.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn && e.RowIndex >= 0)
            {
                string sText = ctl.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (sText != strPrevValue)
                {
                    if (e.ColumnIndex == vdstValues.GRID_FIRST_NAME)
                        txtMainSettingsAccountFirstName.Text = sText;
                    else if (e.ColumnIndex == vdstValues.GRID_LAST_NAME)
                        txtMainSettingsAccountFirstName.Text = ctl.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                        isChanged = true;
                        mnuMainFileSave.Enabled = true;
                }
            }
        }
        
        // Sets a status (help) text when the mouse moves over a field. Uses TAG value for text.
        private void ctlWithHelpText_MouseEnter(object sender, System.EventArgs e)
        {
            Control ctl = (Control)sender;

            tmrMainHelp.Stop();
            statusMainHelp.Text = ctl.Tag.ToString();
        }

        // Clears field-specific help after timer delay
        private void ctlWithHelpText_MouseLeave(object sender, System.EventArgs e)
        {
            if (tmrMainHelp.Enabled == false)
            {
                tmrMainHelp.Start();
            }
        }
        #endregion

        #region Helper Functions
        private void restoreTemperatureDefaults(bool isRefresh)
        {
            string sCurrID = myCurrUser.isAdmin ? cmbMainSettingsAccountId.Text : txtMainSettingsAccountId.Text;
            User aUser = new User(sCurrID, tblAccounts[sCurrID].Split(','));

            try { aUser.AlarmTemperatures = tblDefaults["txtTempAlarm1"] + tblDefaults["txtTempAlarm2"] + tblDefaults["txtTempAlarm3"] + tblDefaults["txtTempAlarm4"] + tblDefaults["txtTempAlarm5"]; } catch (Exception) { }
            try { aUser.AlarmDuration = tblDefaults["txtTempAlarmDuration"]; } catch (Exception) { };
            try { aUser.AlarmFrequency = tblDefaults["txtTempAlarmFrequency"]; } catch (Exception) { };
            try { aUser.AlarmInterval = tblDefaults["txtTempAlarmInterval"]; } catch (Exception) { };
            try { aUser.AlarmVolume = tblDefaults["cmbTempAlarmVolume"]; } catch (Exception) { };
            tblAccounts[sCurrID] = aUser.getAccountValuesString();
            if (isRefresh) setFieldstoAccountValues(aUser);
            isChanged = true;
            mnuMainFileSave.Enabled = true;
        }

        private void restoreLaserDefaults(bool isRefresh)
        {
            string sCurrID = myCurrUser.isAdmin ? cmbMainSettingsAccountId.Text : txtMainSettingsAccountId.Text;
            User aUser = new User(sCurrID, tblAccounts[sCurrID].Split(','));

            try { aUser.LaserPowers = tblDefaults["txtLaserPower1"] + tblDefaults["txtLaserPower2"] + tblDefaults["txtLaserPower3"] + tblDefaults["txtLaserPower4"] + tblDefaults["txtLaserPower5"]; } catch (Exception) { }
            try { aUser.LaserPulse = tblDefaults["txtLaserPulse"]; } catch (Exception) { };
            try { aUser.LaserPowerMinMax = tblDefaults["txtLaserRangeMin"] + ":" + tblDefaults["txtLaserRangeMax"]; } catch (Exception) { };
            tblAccounts[sCurrID] = aUser.getAccountValuesString();
            if (isRefresh) setFieldstoAccountValues(aUser);
            isChanged = true;
            mnuMainFileSave.Enabled = true;
        }

        private void restoreMotorDefaults(bool isRefresh)
        {
            string sCurrID = myCurrUser.isAdmin ? cmbMainSettingsAccountId.Text : txtMainSettingsAccountId.Text;
            User aUser = new User(sCurrID, tblAccounts[sCurrID].Split(','));

            try { aUser.MotorSpeeds = tblDefaults["txtMotorSpeed1"] + tblDefaults["txtMotorSpeed2"] + tblDefaults["txtMotorSpeed3"] + tblDefaults["txtMotorSpeed4"] + tblDefaults["txtMotorSpeed5"]; } catch (Exception) { }
            try { aUser.MotorPulse = tblDefaults["txtMotorPuls"]; } catch (Exception) { };
            try { aUser.MotorSpeedMinMax = tblDefaults["txtMotorRangeMin"] + ":" + tblDefaults["txtMotorRangeMax"]; } catch (Exception) { };
            tblAccounts[sCurrID] = aUser.getAccountValuesString();
            if (isRefresh)
            {
                setFieldstoAccountValues(aUser);
                isChanged = true;
                mnuMainFileSave.Enabled = true;
            }
        }

        private void checkFieldAgainstMinMax(string strField)
        {
            // Get rid of 'PowerX' at the end of the field name
            string strPrefix = strField.Substring(0, strField.Length - 6);

            TextBox txtMin = Controls.Find(strPrefix + "RangeMin", true).FirstOrDefault() as TextBox;
            TextBox txtMax = Controls.Find(strPrefix + "RangeMax", true).FirstOrDefault() as TextBox;
            TextBox txtVal = Controls.Find(strField, true).FirstOrDefault() as TextBox;

            int iMinValue;
            int iMaxValue;
            int iValue;

            if (txtMin != null && txtMax != null && Int32.TryParse(txtVal.Text, out iValue))
            {
                if (Int32.TryParse(txtMin.Text, out iMinValue) && iValue < iMinValue)
                    txtVal.Text = txtMin.Text;
                else if (Int32.TryParse(txtMax.Text, out iMaxValue) && iValue > iMaxValue)
                    txtVal.Text = txtMax.Text;
            }
        }

        // Add a separator and a string to an existing string
        public void addToString(ref string sText, string sNew, char cSeparator)
        {
            sText += cSeparator + sNew;
        }

        // Add a separator and a boolean (as string) to an existing string
        public void addToString(ref string sText, bool bNew, char cSeparator)
        {
            sText += cSeparator + (bNew ? "true" : "false");
        }

        private void exitCheck()
        {
            if (isChanged)
            {
                if (MessageBox.Show("Save pending user updates?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    saveChanges();
                }
            }

            // Force close if File->Exit was used. Not required for 'X'
            //if (forceClose) Application.Exit();
        }
        #endregion

        #region Timers
        private void tmrMainValueSwitch_Tick(object sender, EventArgs e)
        {
            tmrMainValueSwitch.Stop();

            int iMinValue;
            int iMaxValue;
            string strPrefix = tmrMainValueSwitch.Tag.ToString();
            TextBox txtMin = Controls.Find(strPrefix + "Min", true).FirstOrDefault() as TextBox;
            TextBox txtMax = Controls.Find(strPrefix + "Max", true).FirstOrDefault() as TextBox;

            if (Int32.TryParse(txtMin.Text, out iMinValue) && Int32.TryParse(txtMax.Text, out iMaxValue))
            {
                if (iMinValue > iMaxValue)
                {
                    txtMin.Text = iMaxValue.ToString();
                    txtMax.Text = iMinValue.ToString();
                }
            }
        }

        private void tmrMainHelp_Tick(object sender, EventArgs e)
        {
            tmrMainHelp.Stop();
            statusMainHelp.Text = "";
        }


























        #endregion

    }
}