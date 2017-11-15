using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vdst_v1
{
    public partial class frmControl : Form
    {
        private int iCallibrationMin = 0;
        private int iCallibrationMax = 0;
        private int iOldValue = 0;
        private int iCurrLaserPulse = 0;
        private int iCurrMotorPulse = 0;
        private string strPrevValue = "";
        private static SerialComm conn;

        public frmControl()
        {
            InitializeComponent();
            //setFieldsToDefaults(frmMain.tblDefaults, "Control");
            setFieldsToUserValues(frmMain.myCurrUser);
        }

        private void setFieldsToUserValues(User aUser)
        {
            // Temperature
            txtControlTempAlarm1.Text = aUser.getAlarmTemperature(0);
            txtControlTempAlarm2.Text = aUser.getAlarmTemperature(1);
            txtControlTempAlarm3.Text = aUser.getAlarmTemperature(2);
            txtControlTempAlarm4.Text = aUser.getAlarmTemperature(3);
            txtControlTempAlarm5.Text = aUser.getAlarmTemperature(4);
            txtControlTemp.Text = aUser.TemperatureCallibration.ToString();
            // Laser
            txtControlLaserPower1.Text = aUser.getLaserPower(0);
            txtControlLaserPower2.Text = aUser.getLaserPower(1);
            txtControlLaserPower3.Text = aUser.getLaserPower(2);
            txtControlLaserPower4.Text = aUser.getLaserPower(3);
            txtControlLaserPower5.Text = aUser.getLaserPower(4);
            txtControlLaserPulse.Text = aUser.LaserPulse;
            barControlLaserIntensity.Minimum = aUser.LaserCallibrationMin;
            barControlLaserIntensity.Maximum = aUser.LaserCallibrationMax;
            // Motor
            txtControlMotorSpeed1.Text = aUser.getMotorSpeed(0);
            txtControlMotorSpeed2.Text = aUser.getMotorSpeed(1);
            txtControlMotorSpeed3.Text = aUser.getMotorSpeed(2);
            txtControlMotorSpeed4.Text = aUser.getMotorSpeed(3);
            txtControlMotorSpeed5.Text = aUser.getMotorSpeed(4);
            txtControlMotorPulse.Text = aUser.MotorPulse;
            barControlMotorSpeed.Minimum = aUser.MotorCallibrationMin;
            barControlMotorSpeed.Maximum = aUser.MotorCallibrationMax;
        }

        private void setFieldsToDefaults(SortedDictionary<string, string> oData, string strForm)
        {
            //ToDo: Move this duplicate function to a shared code library

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
                        ((ComboBox)ctl).Text = kvpItem.Value;
                    else if (strFieldType == "txt")
                        ((TextBox)ctl).Text = kvpItem.Value;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrControlTemp.Stop();
            stopLaserTimer();
            stopMotorTimer();
        }

        private void txtControlNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control ctl = (Control)sender;

            bool isNotLeadingMinus = (e.KeyChar != 45 || (e.KeyChar == 45 && ((TextBox)ctl).SelectionStart > 0)); //ctl.Text.Length > 0;
            e.Handled = !Char.IsNumber(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 11 && isNotLeadingMinus;
        }

        private void txtControlNumeric_GotFocus(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;
            strPrevValue = ctl.Text;
        }

        private void txtControlNumeric_LostFocus(object sender, EventArgs e)
        {
            Control ctl = (Control)sender;
            String strName = ctl.Name;
            int iValue;

            // If the tag is NULL, it does not contain a note 'Allow Negative'
            if (!Int32.TryParse(ctl.Text, out iValue) || (ctl.Tag == null && iValue < 0))
            {
                ctl.Text = strPrevValue;
            }
            else
            {
                ctl.Text = iValue.ToString();
            }

            if (strName == "txtControlTemp")
            {
                txtControlTemp.Visible = false;
                lblControlTempHelp.Visible = false;
                imgControlTempBell.Visible = true;

                tmrControlTemp.Start();
            }
            else
            {
                checkFieldAgainstMinMax(strName);
            }
        }

        private void checkFieldAgainstMinMax(string strField)
        {
            // Get rid of prexis ('txt')Number at the end of the field name
            string strPrefix = strField.Substring(3, strField.Length - 4);
           
            TrackBar barVal = Controls.Find("bar"+strPrefix, true).FirstOrDefault() as TrackBar;
                TextBox txtVal = Controls.Find(strField, true).FirstOrDefault() as TextBox;

                int iMinValue = barVal.Minimum;
                int iMaxValue = barVal.Maximum;
                int iValue;

                if (Int32.TryParse(txtVal.Text, out iValue))
                {
                    if (iValue < iMinValue)
                        txtVal.Text = iMinValue.ToString();
                    else if (iValue > iMaxValue)
                        txtVal.Text = iMaxValue.ToString();
                }
        }

        public void setAlarmImage(short iAlarmType)
        {
            imgControlTempBell.BackgroundImage = ControlTempImageList.Images[iAlarmType];
            imgControlTempBell.Tag = iAlarmType;
        }

        private void imgControlTempBell_Click(object sender, EventArgs e)
        {
            short iAlarmType = 0;
            if (Int16.TryParse(imgControlTempBell.Tag.ToString(), out iAlarmType)) 
            {
                iAlarmType++;
                if (iAlarmType == 3 || iAlarmType == 5) iAlarmType = 0;
            }
            
            setAlarmImage(iAlarmType);
        }

        private void btnControlConnect_Click(object sender, EventArgs e)
        {
            using (frmSync frmTemp = new frmSync())
            {
                stopLaserTimer();

                frmTemp.ShowDialog(this);
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnControlCalibrate_Click(object sender, EventArgs e)
        {
            btnControlCalibrate.Enabled = false;
            btnControlCom.Enabled = false;

            if (tabControl.SelectedIndex == vdstValues.TAB_TEMP)
            {
                tmrControlTemp.Stop();

                short iAlarmType = 0;

                if (Int16.TryParse(imgControlTempBell.Tag.ToString(), out iAlarmType))
                {
                    if (iAlarmType > 2) iAlarmType -= 2;
                }
                imgControlTempBell.Visible = false;
                setAlarmImage(iAlarmType);

                txtControlTemp.Text = lblControlTemp.Text;
                txtControlTemp.Visible = true;
                txtControlTemp.SelectAll();
                txtControlTemp.Focus();
                lblControlTempHelp.Visible = true;                
            }
            else if (tabControl.SelectedIndex == vdstValues.TAB_LASER)
            {
                stopLaserTimer();

                iOldValue = barControlLaserIntensity.Value;
                lblControlLaserIntensity.Visible = false;
                btnControlLaserPulse.Visible = false;
                imgControlLaserStatus.Visible = false;
                imgControlLaserCalibrateLeft.Visible = true;
                lblControlLaserHelp.Visible = true;
            }
            else
            {
                iOldValue = barControlMotorSpeed.Value;
                lblControlMotorSpeedLeft.Visible = false;
                lblControlMotorSpeedRight.Visible = false;
                btnControlMotorPulse.Visible = false;
                imgControlMotorStatus.Visible = false;
                imgControlMotorCalibrateLeft.Visible = true;
                lblControlMotorHelp.Visible = true;
            }
        }

        private void txtControlTemp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtControlTemp.Visible = false;
                lblControlTempHelp.Visible = false;
                imgControlTempBell.Visible = true;

                // SAVE CALLIBRATION VALUE [TEMP]

                tmrControlTemp.Start();
            }
        }

        //private void txtControlTemp_LostFocus(object sender, EventArgs e)
        //{
         //   txtControlTemp.Visible = false;
          //  lblControlTempHelp.Visible = false;
         //   imgControlTempBell.Visible = true;
         //
//            tmrControlTemp.Start();
       // }

        private void lblControlLaserHelp_Click(object sender, EventArgs e)
        {

        }

        private void imgControlLaserCalibrateLeft_Click(object sender, EventArgs e)
        {
            String strTemp = lblControlLaserHelp.Text;

            iCallibrationMin = barControlLaserIntensity.Value;
            imgControlLaserCalibrateLeft.Visible = false;
            imgControlLaserCalibrateRight.Visible = true;
            lblControlLaserHelp.Text = lblControlLaserHelp.Tag.ToString();
            lblControlLaserHelp.Tag = strTemp;
        }

        private void imgControlLaserCalibrateRight_Click(object sender, EventArgs e)
        {
            String strTemp = lblControlLaserHelp.Text;
            iCallibrationMax = barControlLaserIntensity.Value;

            imgControlLaserCalibrateRight.Visible = false;

            if (iCallibrationMax <= iCallibrationMin)
            {
                MessageBox.Show("Discarding changes. Maximum value is not higher than the minimum value.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                barControlLaserIntensity.Value = iOldValue;
            }
            else
            {
                // SAVE CALLIBRATION VALUE [LASER]
                barControlLaserIntensity.Value = 0;
            }
                        
            lblControlLaserIntensity.Visible = true;
            imgControlLaserStatus.Visible = true;
            lblControlLaserHelp.Visible = false;
            btnControlLaserPulse.Visible = true;
            lblControlLaserHelp.Text = lblControlLaserHelp.Tag.ToString();
            lblControlLaserHelp.Tag = strTemp;
            barControlLaserIntensity.Value = 0;
            btnControlCalibrate.Enabled = true;
            btnControlCom.Enabled = true;
        }

        private void lblControlLaserIntensityLeft_Click(object sender, EventArgs e)
        {

        }

        private void lblControlTempHelp_Click(object sender, EventArgs e)
        {

        }

        private void barControlLaserIntensity_Scroll(object sender, EventArgs e)
        {
            
        }

        private void barControlLaserIntensity_ValueChanged(object sender, EventArgs e)
        {
            if (!imgControlLaserCalibrateLeft.Visible && !imgControlLaserCalibrateRight.Visible)
            {
                int iImage = 7;
                if (barControlLaserIntensity.Value > 0) iImage = 8;

                lblControlLaserIntensity.Text = barControlLaserIntensity.Value.ToString();
                imgControlLaserStatus.BackgroundImage = ControlTempImageList.Images[iImage];
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnControlCalibrate.Enabled = true;
            btnControlCom.Enabled = true;

            if (imgControlLaserCalibrateLeft.Visible)
            {
                imgControlLaserCalibrateLeft.Visible = false;
                imgControlLaserStatus.Visible = true;
                lblControlLaserIntensity.Visible = true;
                lblControlLaserHelp.Visible = false;
                btnControlLaserPulse.Visible = true;
            }
            else if (imgControlLaserCalibrateRight.Visible)
            {
                String strTemp = lblControlLaserHelp.Text;
                lblControlLaserHelp.Text = lblControlLaserHelp.Tag.ToString();
                lblControlLaserHelp.Tag = strTemp;
                imgControlLaserCalibrateRight.Visible = false;
                imgControlLaserStatus.Visible = true;
                lblControlLaserIntensity.Visible = true;
                lblControlLaserHelp.Visible = false;
                btnControlLaserPulse.Visible = true;
                barControlLaserIntensity.Value = iOldValue;
            }
            else if (imgControlMotorCalibrateLeft.Visible)
            {
                imgControlMotorCalibrateLeft.Visible = false;
                imgControlMotorStatus.Visible = true;
                lblControlMotorHelp.Visible = false;
                btnControlMotorPulse.Visible = true;
            }
            else if (imgControlMotorCalibrateRight.Visible)
            {
                String strTemp = lblControlMotorHelp.Text;
                lblControlMotorHelp.Text = lblControlMotorHelp.Tag.ToString();
                lblControlMotorHelp.Tag = strTemp;
                imgControlMotorCalibrateRight.Visible = false;
                imgControlMotorStatus.Visible = true;
                lblControlMotorHelp.Visible = false;
                btnControlMotorPulse.Visible = true;
                barControlMotorSpeed.Value = iOldValue;
            }
        }

        private void imgControlLaserStatus_Click(object sender, EventArgs e)
        {
            barControlLaserIntensity.Value = 0;            
        }

        private void barControlMotorSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (!imgControlMotorCalibrateLeft.Visible && !imgControlMotorCalibrateRight.Visible)
            {
                int iImage = 8;
                if (barControlMotorSpeed.Value == 0) iImage = 7;

                imgControlMotorStatus.BackgroundImage = ControlTempImageList.Images[iImage];

                if (barControlMotorSpeed.Value > 0)
                {
                    lblControlMotorSpeedRight.Text = barControlMotorSpeed.Value.ToString();
                    lblControlMotorSpeedRight.Visible = true;
                    lblControlMotorSpeedLeft.Visible = false;
                }
                else if (barControlMotorSpeed.Value < 0)
                {
                    barControlLaserIntensity.Value = 0;
                    lblControlMotorSpeedLeft.Text = barControlMotorSpeed.Value.ToString();
                    lblControlMotorSpeedLeft.Visible = true;
                    lblControlMotorSpeedRight.Visible = false;
                }
                else
                {
                    barControlMotorSpeed.Value = 0;
                    lblControlMotorSpeedLeft.Visible = false;
                    lblControlMotorSpeedRight.Visible = false;
                }
            }
        }

        private void imgControlMotorStatus_Click(object sender, EventArgs e)
        {
            barControlMotorSpeed.Value = 0;
        }

        private void imgControlMotorCalibrateLeft_Click(object sender, EventArgs e)
        {
            String strTemp = lblControlLaserHelp.Text;

            iCallibrationMin = barControlMotorSpeed.Value;
            imgControlMotorCalibrateLeft.Visible = false;
            imgControlMotorCalibrateRight.Visible = true;
            lblControlMotorHelp.Text = lblControlMotorHelp.Tag.ToString();
            lblControlMotorHelp.Tag = strTemp;
        }

        private void imgControlMotorCalibrateRight_Click(object sender, EventArgs e)
        {
            String strTemp = lblControlMotorHelp.Text;
            iCallibrationMax = barControlMotorSpeed.Value;

            imgControlMotorCalibrateRight.Visible = false;

            if (iCallibrationMin >= 0 || iCallibrationMax <= iCallibrationMin)
            {
                MessageBox.Show("Discarding changes. New values are outside of valid bounds.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                barControlMotorSpeed.Value = iOldValue;
            }
            else
            {
                // SAVE CALLIBRATION VALUE [MOTOR]
                barControlMotorSpeed.Value = 0;
            }

            lblControlMotorSpeedLeft.Visible = true;
            imgControlMotorStatus.Visible = true;
            lblControlMotorHelp.Visible = false;
            btnControlMotorPulse.Visible = true;
            lblControlMotorHelp.Text = lblControlLaserHelp.Tag.ToString();
            lblControlMotorHelp.Tag = strTemp;
            btnControlCalibrate.Enabled = true;
            btnControlCom.Enabled = true;
        }

        private void txtControlTempAlarm1_TextChanged(object sender, EventArgs e)
        {
            txtControlTempAlarm2.Enabled = (txtControlTempAlarm1.Text != "");
            txtControlTempAlarm3.Enabled = (txtControlTempAlarm1.Text != "" && txtControlTempAlarm2.Text != "");
            txtControlTempAlarm4.Enabled = (txtControlTempAlarm1.Text != "" && txtControlTempAlarm2.Text != "" && txtControlTempAlarm3.Text != "");
            txtControlTempAlarm5.Enabled = (txtControlTempAlarm1.Text != "" && txtControlTempAlarm2.Text != "" && txtControlTempAlarm3.Text != "" && txtControlTempAlarm4.Text != "");
            imgControlTempBell.Visible = (txtControlTempAlarm1.Text != "");
        }
        
        private void txtControlTempAlarm2_TextChanged(object sender, EventArgs e)
        {
            txtControlTempAlarm3.Enabled = (txtControlTempAlarm2.Text != "");
            txtControlTempAlarm4.Enabled = (txtControlTempAlarm2.Text != "" && txtControlTempAlarm3.Text != "");
            txtControlTempAlarm5.Enabled = (txtControlTempAlarm2.Text != "" && txtControlTempAlarm3.Text != "" && txtControlTempAlarm4.Text != "");
        }

        private void txtControlTempAlarm3_TextChanged(object sender, EventArgs e)
        {
            txtControlTempAlarm4.Enabled = (txtControlTempAlarm3.Text != "");
            txtControlTempAlarm5.Enabled = (txtControlTempAlarm3.Text != "" && txtControlTempAlarm4.Text != "");
        }

        private void txtControlTempAlarm4_TextChanged(object sender, EventArgs e)
        {
            txtControlTempAlarm5.Enabled = (txtControlTempAlarm4.Text != "");
        }

        private void txtControlLaserPower1_TextChanged(object sender, EventArgs e)
        {
            stopLaserTimer();

            txtControlLaserPower2.Enabled = (txtControlLaserPower1.Text != "");
            txtControlLaserPower3.Enabled = (txtControlLaserPower1.Text != "" && txtControlLaserPower2.Text != "");
            txtControlLaserPower4.Enabled = (txtControlLaserPower1.Text != "" && txtControlLaserPower2.Text != "" && txtControlLaserPower3.Text != "");
            txtControlLaserPower5.Enabled = (txtControlLaserPower1.Text != "" && txtControlLaserPower2.Text != "" && txtControlLaserPower3.Text != "" && txtControlLaserPower4.Text != "");
            lblControlLaserPulse.Visible = (txtControlLaserPower1.Text != "");
            txtControlLaserPulse.Visible = (txtControlLaserPower1.Text != "");
            btnControlLaserPulse.Visible = (txtControlLaserPower1.Text != "");
        }

        private void txtControlLaserPower2_TextChanged(object sender, EventArgs e)
        {
            stopLaserTimer();

            txtControlLaserPower3.Enabled = (txtControlLaserPower2.Text != "");
            txtControlLaserPower4.Enabled = (txtControlLaserPower2.Text != "" && txtControlLaserPower3.Text != "");
            txtControlLaserPower5.Enabled = (txtControlLaserPower2.Text != "" && txtControlLaserPower3.Text != "" && txtControlLaserPower4.Text != "");
        }

        private void txtControlLaserPower3_TextChanged(object sender, EventArgs e)
        {
            stopLaserTimer();

            txtControlLaserPower4.Enabled = (txtControlLaserPower3.Text != "");
            txtControlLaserPower5.Enabled = (txtControlLaserPower3.Text != "" && txtControlLaserPower4.Text != "");
        }

        private void txtControlLaserPower4_TextChanged(object sender, EventArgs e)
        {
            stopLaserTimer();

            txtControlLaserPower5.Enabled = (txtControlLaserPower4.Text != "");
        }

        private void btnControlLaserPulse_Click(object sender, EventArgs e)
        {
            if (imgControlConnectStatus.Tag != "Connected")
            {
                string strDetails = (imgControlConnectStatus.Tag == "Disconnected" ? "Click connection symbol to connect." : "Configure COM port.");
                string strTitle = (imgControlConnectStatus.Tag == "Disconnected" ? "Disconnected" : "Connection Failed");

                MessageBox.Show(strDetails, this.Text + " (" + strTitle + ")", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (btnControlLaserPulse.Text == "Start")
            {
                btnControlLaserPulse.Text = "Stop";
                tmrControlLaser.Interval = Int32.Parse(txtControlLaserPulse.Text);
                imgControlLaserStatus.Visible = false;
                iCurrLaserPulse = 0;
                tmrControlLaser.Start();
            }
            else
            {
                stopLaserTimer();
            }
        }

        private void tmrControlLaser_Tick(object sender, EventArgs e)
        {
            TextBox txtTemp = Controls.Find("txtControlLaserIntensity" + iCurrLaserPulse, true).FirstOrDefault() as TextBox;

            if (txtTemp != null) txtTemp.BackColor = SystemColors.Window;

            iCurrLaserPulse++;

            txtTemp = Controls.Find("txtControlLaserIntensity" + iCurrLaserPulse, true).FirstOrDefault() as TextBox;
            if (txtTemp == null || !txtTemp.Enabled || txtTemp.Text == "")
            {
                iCurrLaserPulse = 1;
                txtTemp = Controls.Find("txtControlLaserIntensity" + iCurrLaserPulse, true).FirstOrDefault() as TextBox;
            }

            txtTemp.BackColor = Color.Red;
            barControlLaserIntensity.Value = Int32.Parse(txtTemp.Text);
        }


        private void txtControlLaserPulse_TextChanged(object sender, EventArgs e)
        {
            stopLaserTimer();
        }

        private void stopLaserTimer()
        {
            TextBox txtTemp = Controls.Find("txtControlLaserIntensity" + iCurrLaserPulse, true).FirstOrDefault() as TextBox;

            tmrControlLaser.Stop();
            if (txtTemp != null) txtTemp.BackColor = SystemColors.Window;
            btnControlLaserPulse.Enabled = txtControlLaserPulse.Text != "";
            btnControlLaserPulse.Text = "Start";
            barControlLaserIntensity.Value = 0;
            imgControlLaserStatus.Visible = true;
        }

        private void txtControlMotorSpeed1_TextChanged(object sender, EventArgs e)
        {
            stopMotorTimer();

            txtControlMotorSpeed2.Enabled = (txtControlMotorSpeed1.Text != "");
            txtControlMotorSpeed3.Enabled = (txtControlMotorSpeed1.Text != "" && txtControlMotorSpeed2.Text != "");
            txtControlMotorSpeed4.Enabled = (txtControlMotorSpeed1.Text != "" && txtControlMotorSpeed2.Text != "" && txtControlMotorSpeed3.Text != "");
            txtControlMotorSpeed5.Enabled = (txtControlMotorSpeed1.Text != "" && txtControlMotorSpeed2.Text != "" && txtControlMotorSpeed3.Text != "" && txtControlMotorSpeed4.Text != "");
            lblControlMotorPulse.Visible = (txtControlMotorSpeed1.Text != "");
            txtControlMotorPulse.Visible = (txtControlMotorSpeed1.Text != "");
            btnControlMotorPulse.Visible = (txtControlMotorSpeed1.Text != "");
        }

        private void txtControlMotorSpeed2_TextChanged(object sender, EventArgs e)
        {
            stopMotorTimer();

            txtControlMotorSpeed3.Enabled = (txtControlMotorSpeed2.Text != "");
            txtControlMotorSpeed4.Enabled = (txtControlMotorSpeed2.Text != "" && txtControlMotorSpeed3.Text != "");
            txtControlMotorSpeed5.Enabled = (txtControlMotorSpeed2.Text != "" && txtControlMotorSpeed3.Text != "" && txtControlMotorSpeed4.Text != "");
        }

        private void txtControlMotorSpeed3_TextChanged(object sender, EventArgs e)
        {
            stopMotorTimer();

            txtControlMotorSpeed4.Enabled = (txtControlMotorSpeed3.Text != "");
            txtControlMotorSpeed5.Enabled = (txtControlMotorSpeed3.Text != "" && txtControlMotorSpeed4.Text != "");
        }

        private void txtControlMotorSpeed4_TextChanged(object sender, EventArgs e)
        {
            stopMotorTimer();

            txtControlMotorSpeed5.Enabled = (txtControlMotorSpeed4.Text != "");
        }

        private void txtControlMotorSpeed5_TextChanged(object sender, EventArgs e)
        {
            stopMotorTimer();
        }

        private void txtControlMotorPulse_TextChanged(object sender, EventArgs e)
        {
            stopMotorTimer();
        }

        private void txtControlLaserIntensity5_TextChanged(object sender, EventArgs e)
        {
            stopLaserTimer();
        }

        private void btnControlMotorPulse_Click(object sender, EventArgs e)
        {
            if (btnControlMotorPulse.Text == "Start")
            {
                btnControlMotorPulse.Text = "Stop";
                tmrControlMotor.Interval = Int32.Parse(txtControlMotorPulse.Text);
                imgControlMotorStatus.Visible = false;
                iCurrMotorPulse = 0;
                tmrControlMotor.Start();
            }
            else
            {
                stopMotorTimer();
            }
        }

        private void stopMotorTimer()
        {
            TextBox txtTemp = Controls.Find("txtControlMotorSpeed" + iCurrMotorPulse, true).FirstOrDefault() as TextBox;

            tmrControlMotor.Stop();
            if (txtTemp != null) txtTemp.BackColor = SystemColors.Window;
            btnControlMotorPulse.Enabled = txtControlMotorPulse.Text != "";
            btnControlMotorPulse.Text = "Start";
            barControlMotorSpeed.Value = 0;
            imgControlMotorStatus.Visible = true;
        }

        private void tmrControlMotor_Tick(object sender, EventArgs e)
        {
            TextBox txtTemp = Controls.Find("txtControlMotorSpeed" + iCurrMotorPulse, true).FirstOrDefault() as TextBox;

            if (txtTemp != null) txtTemp.BackColor = SystemColors.Window;

            iCurrMotorPulse++;

            txtTemp = Controls.Find("txtControlMotorSpeed" + iCurrMotorPulse, true).FirstOrDefault() as TextBox;
            if (txtTemp == null || !txtTemp.Enabled || txtTemp.Text == "")
            {
                iCurrMotorPulse = 1;
                txtTemp = Controls.Find("txtControlMotorSpeed" + iCurrMotorPulse, true).FirstOrDefault() as TextBox;
            }

            txtTemp.BackColor = Color.Red;
            barControlMotorSpeed.Value = Int32.Parse(txtTemp.Text);
        }

        private void tabControlMotorPage_Click(object sender, EventArgs e)
        {

        }

        private void frmControl_Load(object sender, EventArgs e)
        {
            tmrControlTemp.Start();
        }

        private void btnControlTempType_Click(object sender, EventArgs e)
        {
            if (btnControlTempType.Text == "°C")
                btnControlTempType.Text = "°F";
            else
                btnControlTempType.Text = "°C";
        }

        private void btnControlOK_Click(object sender, EventArgs e)
        {

        }

        private void imgControlConnectStatus_Click(object sender, EventArgs e)
        {
            tmrSyncError.Stop();

             if (tmrSync.Enabled)
            {
                conn.Stop();
                tmrSync.Stop();
                tmrControlTemp.Stop();
                tmrControlLaser.Stop();
                tmrControlMotor.Stop();
                imgControlConnectStatus.Tag = "Disonnected";
                imgControlConnectStatus.BackgroundImage = ControlTempImageList.Images[vdstValues.IMAGE_CONNECTION_OFF];
            }
            else { 
                try
                {
                    conn = new SerialComm(frmMain.tblDefaults["cmbSyncPort"], frmMain.tblDefaults["cmbSyncBaud"], frmMain.tblDefaults["chkSyncDuplex"] == "true", frmMain.tblDefaults["cmbSyncDataBits"], frmMain.tblDefaults["cmbSyncStopBits"], frmMain.tblDefaults["cmbSyncParity"], frmMain.tblDefaults["cmbSyncFlowControl"]);
                    conn.Start(Int32.Parse(frmMain.tblDefaults["cmbSyncTimeout"]) * (1000/tmrSync.Interval));
                    tmrSync.Start();
                }
                catch (Exception)
                {
                    frmMain.writeLog(vdstValues.APP_DATA_PATH + vdstValues.APP_FILE_LOG, "Missing or invalid connection config parameter");
                    tmrSyncError.Start();
                }
            }
        }

        private void tmrControlTemp_Tick(object sender, EventArgs e)
        {
            //Console.Beep(frmMain.myCurrUser.AlarmFrequency.tofrequency, dur);
        }

        private void tmrSyncError_Tick(object sender, EventArgs e)
        {
            imgControlConnectStatus.Visible = !imgControlConnectStatus.Visible;
        }

        private void tmrSync_Tick(object sender, EventArgs e)
        {            
            if (tmrSync.Interval == 5000)
            {
                tmrSync.Interval = 250;
                tmrSync.Stop();
                tmrSyncError.Stop();
                imgControlConnectStatus.Visible = true;
            }
            else {
                 
                conn.tick();

                if (conn.ConnectionFailed)
                {
                    tmrSync.Interval = 5000;
                    imgControlConnectStatus.Tag = "Disconnected";
                    imgControlConnectStatus.BackgroundImage = ControlTempImageList.Images[vdstValues.IMAGE_CONNECTION_OFF];
                    tmrSyncError.Start();
                }
                else if (conn.isConnected)
                {
                    tmrSync.Stop();
                    imgControlConnectStatus.Tag = "Connected";
                    imgControlConnectStatus.BackgroundImage = ControlTempImageList.Images[vdstValues.IMAGE_CONNECTION_ON];
                    tmrSyncError.Start();
                }
                else
                {
                    // Blink connection on/off while trying to connect
                    if (imgControlConnectStatus.Tag.ToString() == "Disconnected")
                    {
                        imgControlConnectStatus.Tag = "Connected";
                        imgControlConnectStatus.BackgroundImage = ControlTempImageList.Images[vdstValues.IMAGE_CONNECTION_ON];
                    }
                    else
                    {
                        imgControlConnectStatus.Tag = "Disconnected";
                        imgControlConnectStatus.BackgroundImage = ControlTempImageList.Images[vdstValues.IMAGE_CONNECTION_OFF];
                    }
                }
            }
        }
    }
}
