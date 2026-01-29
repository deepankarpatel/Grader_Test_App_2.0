using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grader_Test_APP_v2._0
{
    public partial class CustomCommandForm : Form
    {
        public string EnteredCommand { get; private set; }

        public CustomCommandForm()
        {
            InitializeComponent();
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            if (txtCommand.Text == "")
            {
                MessageBox.Show("Please Enter Command"); 
                return;
            }
            else
            {
                EnteredCommand = txtCommand.Text.Trim();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}