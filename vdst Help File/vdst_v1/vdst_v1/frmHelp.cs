using System;
using System.Collections;
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
        public partial class frmHelp : Form
    {
        

        public frmHelp()
        {
            InitializeComponent();
            

            foreach (var varData in frmMain.tblHelp)
            {
                txtHelpList.Items.Add(varData.Key);
            }

            if (txtHelpList.Items.Count > 0)
            {
                txtHelpList.SelectedIndex = 0;
            }
        }

        private void frmHelp_Load(object sender, EventArgs e)
        {
            
        }

        private void txtHelpSearch_TextChanged(object sender, EventArgs e)
        {
            txtHelpList.Items.Clear();

            foreach (var varData in frmMain.tblHelp)
            {
                if (txtHelpSearch.Text.Length <= varData.Key.Length && varData.Key.Substring(0, txtHelpSearch.Text.Length).ToUpper() == txtHelpSearch.Text.ToUpper())
                {
                    txtHelpList.Items.Add(varData.Key);
                }
            }
            if (txtHelpList.Items.Count > 0)
            {
                txtHelpList.SelectedIndex = 0;
            }
        }

        private void txtHelpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHelpText.Text = frmMain.tblHelp[txtHelpList.SelectedItem.ToString()];
        }

    }
}
