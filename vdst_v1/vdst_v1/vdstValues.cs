using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vdst_v1
{
    public static class vdstValues
    {
        internal const string APP_NAME = "VDST";
        internal const string APP_FULL_NAME = "Visual DoSomethingApp";
        internal const string APP_VERSION = "1.0.0";
        internal static string APP_DATA_PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + vdstValues.APP_NAME + "\\";
        internal static string APP_FILE_ACCOUNTS = "Accounts.dat";
        internal static string APP_FILE_HELP = "Help.txt";
        internal static string APP_FILE_LOG = "Local.log";
        internal static string APP_FILE_PREFERENCES_FIELDS = "Defaults.cfg";
        internal static string APP_FILE_PREFERENCES_HELP = "FieldInfo.cfg";
        internal const string KEY_VALUE_SEPARATOR = ":::";
        internal const string CSV_SEPARATOR = ",";
        internal const string ARRAY_SEPARATOR = ":";
        internal const int MAX_ACCOUNTS = 3;
        internal const bool ALLOW_EMPTY_VALUE = true;
        internal const bool REJECT_EMPTY_VALUES = false;
        internal const bool FORCE_CLOSE = true;
        internal const int PASSCODE_LENGTH = 4;
        internal const int PASSCODE_TRIES = 3;
        internal const int TAB_GENERAL = 0;
        internal const int TAB_CONTROLS = 10;
        internal const int TAB_HELP = 2;
        internal const int TAB_TEMP = 0;
        internal const int TAB_LASER = 1;
        internal const int TAB_MOTOR = 2;
        internal const int USER_PASSCODE = 0;
        internal const int USER_IS_ADMIN = 1;
        internal const int USER_FIRST_NAME = 2;
        internal const int USER_LAST_NAME = 3;
        internal const int USER_ENABLED = 4;
        internal const int USER_LOCKED = 5;
        internal const short ALARM_DISABLED = 0;
        internal const short ALARM_SILENT = 1;
        internal const short ALARM_SILENT_ON = 2;
        internal const short ALARM_AUDIBLE = 3;
        internal const short ALARM_AUDIBLE_ON = 4;
        internal const short IMAGE_CONNECTION_ON = 5;
        internal const short IMAGE_CONNECTION_OFF = 6;
        internal const bool NO_REFRESH = false;
        internal const bool REFRESH = true;
        internal const short TABLE_BUTTON_RESET = 6;
        internal const short TABLE_BUTTON_PASSWORD = 7;
        internal const int GRID_LAST_NAME = 1;
        internal const int GRID_FIRST_NAME = 2;
        internal const int GRID_IS_ADMIN = 3;
        internal const int GRID_ENABLED = 4;
        internal const int GRID_LOCKED = 5;
    }
}