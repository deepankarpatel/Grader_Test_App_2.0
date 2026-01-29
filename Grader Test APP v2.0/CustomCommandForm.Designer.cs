namespace Grader_Test_APP_v2._0
{
    partial class CustomCommandForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSend = new Button();
            txtCommand = new TextBox();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.FromArgb(70, 70, 70);
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.ForeColor = Color.White;
            btnSend.Location = new Point(155, 87);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(115, 31);
            btnSend.TabIndex = 0;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click_1;
            // 
            // txtCommand
            // 
            txtCommand.Location = new Point(65, 40);
            txtCommand.Name = "txtCommand";
            txtCommand.Size = new Size(292, 23);
            txtCommand.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(70, 70, 70);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(355, 140);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 27);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click_1;
            // 
            // CustomCommandForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 46, 46);
            ClientSize = new Size(442, 179);
            Controls.Add(btnCancel);
            Controls.Add(txtCommand);
            Controls.Add(btnSend);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Name = "CustomCommandForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Enter Command";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSend;
        private TextBox txtCommand;
        private Button btnCancel;
    }
}