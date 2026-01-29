namespace Grader_Test_APP_v2._0
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tab_all_components = new TabControl();
            tabPage_frimware = new TabPage();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            button_disconnect = new Button();
            label_message = new Label();
            label_device = new Label();
            label_port = new Label();
            label_baudrate = new Label();
            button_connect = new Button();
            button_refresh = new Button();
            comboBox_baudrate = new ComboBox();
            comboBox_port = new ComboBox();
            comboBox_device = new ComboBox();
            panel3 = new Panel();
            label_FwCalculatedCRC = new Label();
            label_fwHeaderCRC = new Label();
            label_fwLength = new Label();
            label_fwID = new Label();
            label_filetype = new Label();
            label_fwtype = new Label();
            label_BinStatus = new Label();
            label_binsize = new Label();
            label_binName = new Label();
            button_browse_file = new Button();
            panel2 = new Panel();
            button_OTA_mode = new Button();
            lable_dataPackets_Update = new Label();
            lable_progressBar_Percentage = new Label();
            button_upgrade = new Button();
            progressBar = new ProgressBar();
            tabPage_test_device = new TabPage();
            panel4 = new Panel();
            btnCustomCommand = new Button();
            button_ResetDevice = new Button();
            button_clearLogs = new Button();
            button_saveLogs = new Button();
            button_baidu = new Button();
            button_gallileo = new Button();
            button_glonass = new Button();
            button_base_config = new Button();
            button_rover_config = new Button();
            button_constellation_test = new Button();
            button_radio_test = new Button();
            button_gnss_test = new Button();
            rtbLogs = new RichTextBox();
            tab_all_components.SuspendLayout();
            tabPage_frimware.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            tabPage_test_device.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // tab_all_components
            // 
            tab_all_components.Appearance = TabAppearance.Buttons;
            tab_all_components.Controls.Add(tabPage_frimware);
            tab_all_components.Controls.Add(tabPage_test_device);
            tab_all_components.Dock = DockStyle.Fill;
            tab_all_components.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tab_all_components.Location = new Point(0, 0);
            tab_all_components.Multiline = true;
            tab_all_components.Name = "tab_all_components";
            tab_all_components.SelectedIndex = 0;
            tab_all_components.Size = new Size(998, 678);
            tab_all_components.TabIndex = 3;
            // 
            // tabPage_frimware
            // 
            tabPage_frimware.BackColor = Color.White;
            tabPage_frimware.Controls.Add(panel1);
            tabPage_frimware.Controls.Add(panel3);
            tabPage_frimware.Controls.Add(panel2);
            tabPage_frimware.Location = new Point(4, 29);
            tabPage_frimware.Name = "tabPage_frimware";
            tabPage_frimware.Padding = new Padding(3);
            tabPage_frimware.Size = new Size(990, 645);
            tabPage_frimware.TabIndex = 0;
            tabPage_frimware.Text = "Firmware Upgrade";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(34, 34, 34);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(button_disconnect);
            panel1.Controls.Add(label_message);
            panel1.Controls.Add(label_device);
            panel1.Controls.Add(label_port);
            panel1.Controls.Add(label_baudrate);
            panel1.Controls.Add(button_connect);
            panel1.Controls.Add(button_refresh);
            panel1.Controls.Add(comboBox_baudrate);
            panel1.Controls.Add(comboBox_port);
            panel1.Controls.Add(comboBox_device);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(350, 639);
            panel1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(79, 385);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(204, 46);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // button_disconnect
            // 
            button_disconnect.BackColor = Color.FromArgb(186, 26, 26);
            button_disconnect.BackgroundImageLayout = ImageLayout.None;
            button_disconnect.Dock = DockStyle.Bottom;
            button_disconnect.Enabled = false;
            button_disconnect.FlatStyle = FlatStyle.Popup;
            button_disconnect.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            button_disconnect.ForeColor = Color.Azure;
            button_disconnect.Location = new Point(0, 501);
            button_disconnect.Name = "button_disconnect";
            button_disconnect.Size = new Size(350, 46);
            button_disconnect.TabIndex = 6;
            button_disconnect.Text = "Disconnect";
            button_disconnect.UseVisualStyleBackColor = false;
            button_disconnect.Visible = false;
            button_disconnect.Click += button_disconnect_Click;
            // 
            // label_message
            // 
            label_message.BackColor = Color.FromArgb(186, 26, 26);
            label_message.BorderStyle = BorderStyle.Fixed3D;
            label_message.Dock = DockStyle.Top;
            label_message.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_message.ForeColor = Color.White;
            label_message.Location = new Point(0, 0);
            label_message.Name = "label_message";
            label_message.Size = new Size(350, 25);
            label_message.TabIndex = 5;
            label_message.Text = "Disconnected";
            // 
            // label_device
            // 
            label_device.AutoSize = true;
            label_device.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label_device.ForeColor = Color.White;
            label_device.Location = new Point(36, 129);
            label_device.Name = "label_device";
            label_device.Size = new Size(62, 20);
            label_device.TabIndex = 2;
            label_device.Text = "Device*";
            // 
            // label_port
            // 
            label_port.AutoSize = true;
            label_port.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label_port.ForeColor = Color.White;
            label_port.Location = new Point(36, 195);
            label_port.Name = "label_port";
            label_port.Size = new Size(44, 20);
            label_port.TabIndex = 3;
            label_port.Text = "Port*";
            // 
            // label_baudrate
            // 
            label_baudrate.AutoSize = true;
            label_baudrate.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label_baudrate.ForeColor = Color.White;
            label_baudrate.Location = new Point(36, 257);
            label_baudrate.Name = "label_baudrate";
            label_baudrate.Size = new Size(78, 20);
            label_baudrate.TabIndex = 4;
            label_baudrate.Text = "Baudrate*";
            // 
            // button_connect
            // 
            button_connect.BackColor = Color.FromArgb(46, 46, 40);
            button_connect.Dock = DockStyle.Bottom;
            button_connect.FlatStyle = FlatStyle.Popup;
            button_connect.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            button_connect.ForeColor = Color.Azure;
            button_connect.Location = new Point(0, 547);
            button_connect.Name = "button_connect";
            button_connect.Size = new Size(350, 46);
            button_connect.TabIndex = 3;
            button_connect.Text = "Connect";
            button_connect.UseVisualStyleBackColor = false;
            button_connect.Click += button_connect_Click;
            // 
            // button_refresh
            // 
            button_refresh.BackColor = Color.FromArgb(46, 46, 40);
            button_refresh.Dock = DockStyle.Bottom;
            button_refresh.FlatStyle = FlatStyle.Popup;
            button_refresh.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            button_refresh.ForeColor = Color.Azure;
            button_refresh.Location = new Point(0, 593);
            button_refresh.Name = "button_refresh";
            button_refresh.Size = new Size(350, 46);
            button_refresh.TabIndex = 4;
            button_refresh.Text = "Refresh";
            button_refresh.UseVisualStyleBackColor = false;
            button_refresh.Click += button_refresh_Click;
            // 
            // comboBox_baudrate
            // 
            comboBox_baudrate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            comboBox_baudrate.Font = new Font("Segoe UI", 11.25F);
            comboBox_baudrate.ForeColor = Color.Black;
            comboBox_baudrate.FormattingEnabled = true;
            comboBox_baudrate.Items.AddRange(new object[] { "4800", "9600", "19200", "38400", "57600", "115200" });
            comboBox_baudrate.Location = new Point(166, 251);
            comboBox_baudrate.Name = "comboBox_baudrate";
            comboBox_baudrate.Size = new Size(154, 28);
            comboBox_baudrate.TabIndex = 4;
            comboBox_baudrate.Text = "Select";
            // 
            // comboBox_port
            // 
            comboBox_port.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            comboBox_port.Font = new Font("Segoe UI", 11.25F);
            comboBox_port.ForeColor = Color.Black;
            comboBox_port.FormattingEnabled = true;
            comboBox_port.Location = new Point(166, 189);
            comboBox_port.Name = "comboBox_port";
            comboBox_port.Size = new Size(154, 28);
            comboBox_port.TabIndex = 3;
            comboBox_port.Text = "Select";
            comboBox_port.DropDown += comboBox_port_DropDown;
            // 
            // comboBox_device
            // 
            comboBox_device.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            comboBox_device.Font = new Font("Segoe UI", 11.25F);
            comboBox_device.ForeColor = Color.Black;
            comboBox_device.FormattingEnabled = true;
            comboBox_device.Items.AddRange(new object[] { "N200L-422", "N300A-BLE", "N300B-BLE", "N300L-BLE" });
            comboBox_device.Location = new Point(166, 124);
            comboBox_device.Name = "comboBox_device";
            comboBox_device.Size = new Size(154, 28);
            comboBox_device.TabIndex = 2;
            comboBox_device.Text = "Select";
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel3.BackColor = Color.FromArgb(34, 34, 34);
            panel3.Controls.Add(label_FwCalculatedCRC);
            panel3.Controls.Add(label_fwHeaderCRC);
            panel3.Controls.Add(label_fwLength);
            panel3.Controls.Add(label_fwID);
            panel3.Controls.Add(label_filetype);
            panel3.Controls.Add(label_fwtype);
            panel3.Controls.Add(label_BinStatus);
            panel3.Controls.Add(label_binsize);
            panel3.Controls.Add(label_binName);
            panel3.Controls.Add(button_browse_file);
            panel3.Font = new Font("Consolas", 11.25F);
            panel3.ForeColor = Color.White;
            panel3.Location = new Point(359, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(628, 311);
            panel3.TabIndex = 6;
            // 
            // label_FwCalculatedCRC
            // 
            label_FwCalculatedCRC.AutoSize = true;
            label_FwCalculatedCRC.Font = new Font("Consolas", 9.75F);
            label_FwCalculatedCRC.Location = new Point(32, 245);
            label_FwCalculatedCRC.Name = "label_FwCalculatedCRC";
            label_FwCalculatedCRC.Size = new Size(133, 15);
            label_FwCalculatedCRC.TabIndex = 10;
            label_FwCalculatedCRC.Text = "Fw Calculated CRC:";
            // 
            // label_fwHeaderCRC
            // 
            label_fwHeaderCRC.AutoSize = true;
            label_fwHeaderCRC.Font = new Font("Consolas", 9.75F);
            label_fwHeaderCRC.Location = new Point(32, 220);
            label_fwHeaderCRC.Name = "label_fwHeaderCRC";
            label_fwHeaderCRC.Size = new Size(105, 15);
            label_fwHeaderCRC.TabIndex = 9;
            label_fwHeaderCRC.Text = "Fw Header CRC:";
            // 
            // label_fwLength
            // 
            label_fwLength.AutoSize = true;
            label_fwLength.Font = new Font("Consolas", 9.75F);
            label_fwLength.Location = new Point(32, 195);
            label_fwLength.Name = "label_fwLength";
            label_fwLength.Size = new Size(77, 15);
            label_fwLength.TabIndex = 8;
            label_fwLength.Text = "Fw Length:";
            // 
            // label_fwID
            // 
            label_fwID.AutoSize = true;
            label_fwID.Font = new Font("Consolas", 9.75F);
            label_fwID.Location = new Point(32, 171);
            label_fwID.Name = "label_fwID";
            label_fwID.Size = new Size(42, 15);
            label_fwID.TabIndex = 7;
            label_fwID.Text = "FwID:";
            // 
            // label_filetype
            // 
            label_filetype.AutoSize = true;
            label_filetype.Font = new Font("Consolas", 9.75F);
            label_filetype.Location = new Point(32, 146);
            label_filetype.Name = "label_filetype";
            label_filetype.Size = new Size(70, 15);
            label_filetype.TabIndex = 6;
            label_filetype.Text = "FileType:";
            // 
            // label_fwtype
            // 
            label_fwtype.AutoSize = true;
            label_fwtype.Font = new Font("Consolas", 9.75F);
            label_fwtype.Location = new Point(32, 122);
            label_fwtype.Name = "label_fwtype";
            label_fwtype.Size = new Size(56, 15);
            label_fwtype.TabIndex = 5;
            label_fwtype.Text = "Fwtype:";
            // 
            // label_BinStatus
            // 
            label_BinStatus.AutoSize = true;
            label_BinStatus.Font = new Font("Consolas", 9.75F);
            label_BinStatus.ForeColor = Color.White;
            label_BinStatus.Location = new Point(32, 269);
            label_BinStatus.Name = "label_BinStatus";
            label_BinStatus.Size = new Size(56, 15);
            label_BinStatus.TabIndex = 4;
            label_BinStatus.Text = "Status:";
            // 
            // label_binsize
            // 
            label_binsize.AutoSize = true;
            label_binsize.Font = new Font("Consolas", 9.75F);
            label_binsize.ForeColor = Color.White;
            label_binsize.Location = new Point(32, 98);
            label_binsize.Name = "label_binsize";
            label_binsize.Size = new Size(42, 15);
            label_binsize.TabIndex = 2;
            label_binsize.Text = "Size:";
            // 
            // label_binName
            // 
            label_binName.AutoSize = true;
            label_binName.Font = new Font("Consolas", 9.75F);
            label_binName.ForeColor = Color.White;
            label_binName.Location = new Point(32, 75);
            label_binName.Name = "label_binName";
            label_binName.Size = new Size(42, 15);
            label_binName.TabIndex = 1;
            label_binName.Text = "Name:";
            // 
            // button_browse_file
            // 
            button_browse_file.Enabled = false;
            button_browse_file.FlatStyle = FlatStyle.System;
            button_browse_file.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_browse_file.Location = new Point(31, 27);
            button_browse_file.Name = "button_browse_file";
            button_browse_file.Size = new Size(85, 35);
            button_browse_file.TabIndex = 0;
            button_browse_file.Text = "Browse ";
            button_browse_file.UseVisualStyleBackColor = true;
            button_browse_file.Click += button_browse_file_Click;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.FromArgb(34, 34, 34);
            panel2.Controls.Add(button_OTA_mode);
            panel2.Controls.Add(lable_dataPackets_Update);
            panel2.Controls.Add(lable_progressBar_Percentage);
            panel2.Controls.Add(button_upgrade);
            panel2.Controls.Add(progressBar);
            panel2.Font = new Font("Microsoft Sans Serif", 11.25F);
            panel2.Location = new Point(359, 320);
            panel2.Name = "panel2";
            panel2.Size = new Size(628, 322);
            panel2.TabIndex = 5;
            // 
            // button_OTA_mode
            // 
            button_OTA_mode.BackColor = Color.FromArgb(46, 46, 40);
            button_OTA_mode.Enabled = false;
            button_OTA_mode.FlatStyle = FlatStyle.Popup;
            button_OTA_mode.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_OTA_mode.ForeColor = Color.Azure;
            button_OTA_mode.Location = new Point(17, 26);
            button_OTA_mode.Name = "button_OTA_mode";
            button_OTA_mode.Size = new Size(172, 39);
            button_OTA_mode.TabIndex = 8;
            button_OTA_mode.Text = "Enter FW Upgrade Mode";
            button_OTA_mode.UseVisualStyleBackColor = false;
            button_OTA_mode.Click += button_OTA_mode_Click;
            // 
            // lable_dataPackets_Update
            // 
            lable_dataPackets_Update.AutoSize = true;
            lable_dataPackets_Update.BackColor = Color.Transparent;
            lable_dataPackets_Update.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lable_dataPackets_Update.ForeColor = Color.White;
            lable_dataPackets_Update.Location = new Point(15, 153);
            lable_dataPackets_Update.Name = "lable_dataPackets_Update";
            lable_dataPackets_Update.Size = new Size(45, 20);
            lable_dataPackets_Update.TabIndex = 7;
            lable_dataPackets_Update.Text = "Idle";
            lable_dataPackets_Update.Visible = false;
            // 
            // lable_progressBar_Percentage
            // 
            lable_progressBar_Percentage.AutoSize = true;
            lable_progressBar_Percentage.BackColor = Color.Transparent;
            lable_progressBar_Percentage.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lable_progressBar_Percentage.ForeColor = Color.Transparent;
            lable_progressBar_Percentage.Location = new Point(580, 153);
            lable_progressBar_Percentage.Name = "lable_progressBar_Percentage";
            lable_progressBar_Percentage.Size = new Size(28, 21);
            lable_progressBar_Percentage.TabIndex = 6;
            lable_progressBar_Percentage.Text = "0%";
            lable_progressBar_Percentage.Visible = false;
            // 
            // button_upgrade
            // 
            button_upgrade.Anchor = AnchorStyles.Top;
            button_upgrade.BackColor = Color.FromArgb(46, 46, 40);
            button_upgrade.Enabled = false;
            button_upgrade.FlatStyle = FlatStyle.Popup;
            button_upgrade.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            button_upgrade.ForeColor = Color.Azure;
            button_upgrade.Location = new Point(246, 187);
            button_upgrade.Name = "button_upgrade";
            button_upgrade.Size = new Size(137, 39);
            button_upgrade.TabIndex = 2;
            button_upgrade.Text = "Upgrade";
            button_upgrade.UseVisualStyleBackColor = false;
            button_upgrade.Click += button_upgrade_Click;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.BackColor = Color.White;
            progressBar.Location = new Point(17, 118);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(595, 32);
            progressBar.TabIndex = 5;
            // 
            // tabPage_test_device
            // 
            tabPage_test_device.BackColor = Color.White;
            tabPage_test_device.Controls.Add(panel4);
            tabPage_test_device.Controls.Add(rtbLogs);
            tabPage_test_device.Location = new Point(4, 29);
            tabPage_test_device.Name = "tabPage_test_device";
            tabPage_test_device.Padding = new Padding(3);
            tabPage_test_device.Size = new Size(990, 645);
            tabPage_test_device.TabIndex = 1;
            tabPage_test_device.Text = "Test Device";
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel4.BackColor = Color.FromArgb(34, 34, 34);
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(btnCustomCommand);
            panel4.Controls.Add(button_ResetDevice);
            panel4.Controls.Add(button_clearLogs);
            panel4.Controls.Add(button_saveLogs);
            panel4.Controls.Add(button_baidu);
            panel4.Controls.Add(button_gallileo);
            panel4.Controls.Add(button_glonass);
            panel4.Controls.Add(button_base_config);
            panel4.Controls.Add(button_rover_config);
            panel4.Controls.Add(button_constellation_test);
            panel4.Controls.Add(button_radio_test);
            panel4.Controls.Add(button_gnss_test);
            panel4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            panel4.ForeColor = SystemColors.ControlText;
            panel4.Location = new Point(8, 7);
            panel4.Name = "panel4";
            panel4.Size = new Size(233, 628);
            panel4.TabIndex = 6;
            // 
            // btnCustomCommand
            // 
            btnCustomCommand.BackColor = Color.FromArgb(50, 50, 50);
            btnCustomCommand.FlatStyle = FlatStyle.Flat;
            btnCustomCommand.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnCustomCommand.ForeColor = Color.White;
            btnCustomCommand.Location = new Point(0, 240);
            btnCustomCommand.Name = "btnCustomCommand";
            btnCustomCommand.Size = new Size(231, 50);
            btnCustomCommand.TabIndex = 13;
            btnCustomCommand.Text = "Send Custom Command";
            btnCustomCommand.UseVisualStyleBackColor = false;
            btnCustomCommand.Click += btnCustomCommand_Click_1;
            // 
            // button_ResetDevice
            // 
            button_ResetDevice.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            button_ResetDevice.BackColor = Color.FromArgb(50, 50, 50);
            button_ResetDevice.FlatStyle = FlatStyle.Flat;
            button_ResetDevice.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            button_ResetDevice.ForeColor = Color.White;
            button_ResetDevice.Location = new Point(114, 581);
            button_ResetDevice.Name = "button_ResetDevice";
            button_ResetDevice.Size = new Size(117, 45);
            button_ResetDevice.TabIndex = 12;
            button_ResetDevice.Text = "Reset Device";
            button_ResetDevice.UseVisualStyleBackColor = false;
            button_ResetDevice.Click += button_ResetDevice_Click;
            // 
            // button_clearLogs
            // 
            button_clearLogs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            button_clearLogs.BackColor = Color.FromArgb(50, 50, 50);
            button_clearLogs.FlatStyle = FlatStyle.Flat;
            button_clearLogs.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            button_clearLogs.ForeColor = Color.White;
            button_clearLogs.Location = new Point(0, 581);
            button_clearLogs.Name = "button_clearLogs";
            button_clearLogs.Size = new Size(117, 45);
            button_clearLogs.TabIndex = 11;
            button_clearLogs.Text = "Clear Logs";
            button_clearLogs.UseVisualStyleBackColor = false;
            button_clearLogs.Click += button_clearLogs_Click;
            // 
            // button_saveLogs
            // 
            button_saveLogs.BackColor = Color.FromArgb(50, 50, 50);
            button_saveLogs.FlatStyle = FlatStyle.Flat;
            button_saveLogs.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            button_saveLogs.ForeColor = Color.White;
            button_saveLogs.Location = new Point(115, 195);
            button_saveLogs.Name = "button_saveLogs";
            button_saveLogs.Size = new Size(116, 45);
            button_saveLogs.TabIndex = 10;
            button_saveLogs.Text = "Save Logs";
            button_saveLogs.UseVisualStyleBackColor = false;
            button_saveLogs.Click += button_saveLogs_Click;
            // 
            // button_baidu
            // 
            button_baidu.BackColor = Color.FromArgb(50, 50, 50);
            button_baidu.FlatStyle = FlatStyle.Flat;
            button_baidu.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            button_baidu.ForeColor = Color.White;
            button_baidu.Location = new Point(0, 195);
            button_baidu.Name = "button_baidu";
            button_baidu.Size = new Size(115, 45);
            button_baidu.TabIndex = 9;
            button_baidu.Text = "Enable/Disable Beidou";
            button_baidu.UseVisualStyleBackColor = false;
            button_baidu.Click += button_beidou_Click;
            // 
            // button_gallileo
            // 
            button_gallileo.BackColor = Color.FromArgb(50, 50, 50);
            button_gallileo.FlatStyle = FlatStyle.Flat;
            button_gallileo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            button_gallileo.ForeColor = Color.White;
            button_gallileo.Location = new Point(0, 150);
            button_gallileo.Name = "button_gallileo";
            button_gallileo.Size = new Size(115, 45);
            button_gallileo.TabIndex = 8;
            button_gallileo.Text = "Enable/Disable Gallileo";
            button_gallileo.UseVisualStyleBackColor = false;
            button_gallileo.Click += button_gallileo_Click;
            // 
            // button_glonass
            // 
            button_glonass.BackColor = Color.FromArgb(50, 50, 50);
            button_glonass.FlatStyle = FlatStyle.Flat;
            button_glonass.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            button_glonass.ForeColor = Color.White;
            button_glonass.Location = new Point(115, 150);
            button_glonass.Name = "button_glonass";
            button_glonass.Size = new Size(116, 45);
            button_glonass.TabIndex = 7;
            button_glonass.Text = "Enable/Disable Glonass";
            button_glonass.UseVisualStyleBackColor = false;
            button_glonass.Click += button_glonass_Click;
            // 
            // button_base_config
            // 
            button_base_config.BackColor = Color.FromArgb(50, 50, 50);
            button_base_config.FlatStyle = FlatStyle.Flat;
            button_base_config.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            button_base_config.ForeColor = Color.White;
            button_base_config.Location = new Point(0, 290);
            button_base_config.Name = "button_base_config";
            button_base_config.Size = new Size(231, 50);
            button_base_config.TabIndex = 5;
            button_base_config.Text = "Configure Base";
            button_base_config.UseVisualStyleBackColor = false;
            button_base_config.Click += button_base_config_Click;
            // 
            // button_rover_config
            // 
            button_rover_config.BackColor = Color.FromArgb(50, 50, 50);
            button_rover_config.FlatStyle = FlatStyle.Flat;
            button_rover_config.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            button_rover_config.ForeColor = Color.White;
            button_rover_config.Location = new Point(0, 340);
            button_rover_config.Name = "button_rover_config";
            button_rover_config.Size = new Size(231, 50);
            button_rover_config.TabIndex = 1;
            button_rover_config.Text = "Configure Rover";
            button_rover_config.UseVisualStyleBackColor = false;
            button_rover_config.Click += button_rover_config_Click;
            // 
            // button_constellation_test
            // 
            button_constellation_test.BackColor = Color.FromArgb(50, 50, 50);
            button_constellation_test.FlatStyle = FlatStyle.Flat;
            button_constellation_test.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            button_constellation_test.ForeColor = Color.White;
            button_constellation_test.Location = new Point(0, 100);
            button_constellation_test.Name = "button_constellation_test";
            button_constellation_test.Size = new Size(231, 50);
            button_constellation_test.TabIndex = 4;
            button_constellation_test.Text = "Test Constellation";
            button_constellation_test.UseVisualStyleBackColor = false;
            button_constellation_test.Click += button_constellation_test_Click;
            // 
            // button_radio_test
            // 
            button_radio_test.BackColor = Color.FromArgb(50, 50, 50);
            button_radio_test.FlatStyle = FlatStyle.Flat;
            button_radio_test.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            button_radio_test.ForeColor = Color.White;
            button_radio_test.Location = new Point(0, 50);
            button_radio_test.Name = "button_radio_test";
            button_radio_test.Size = new Size(231, 50);
            button_radio_test.TabIndex = 2;
            button_radio_test.Text = "Test Radio";
            button_radio_test.UseVisualStyleBackColor = false;
            button_radio_test.Click += button_radio_test_Click;
            // 
            // button_gnss_test
            // 
            button_gnss_test.BackColor = Color.FromArgb(50, 50, 50);
            button_gnss_test.FlatStyle = FlatStyle.Flat;
            button_gnss_test.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            button_gnss_test.ForeColor = Color.White;
            button_gnss_test.Location = new Point(0, 0);
            button_gnss_test.Name = "button_gnss_test";
            button_gnss_test.Size = new Size(231, 50);
            button_gnss_test.TabIndex = 3;
            button_gnss_test.Text = "Test GNSS";
            button_gnss_test.UseVisualStyleBackColor = false;
            button_gnss_test.Click += btnGnssTest_Click;
            // 
            // rtbLogs
            // 
            rtbLogs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbLogs.BackColor = Color.FromArgb(17, 17, 17);
            rtbLogs.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbLogs.ForeColor = Color.FromArgb(19, 154, 14);
            rtbLogs.Location = new Point(251, 8);
            rtbLogs.Name = "rtbLogs";
            rtbLogs.ReadOnly = true;
            rtbLogs.Size = new Size(731, 627);
            rtbLogs.TabIndex = 6;
            rtbLogs.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(17, 17, 17);
            ClientSize = new Size(998, 678);
            Controls.Add(tab_all_components);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GNSS Test Tool v1.0";
            tab_all_components.ResumeLayout(false);
            tabPage_frimware.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tabPage_test_device.ResumeLayout(false);
            panel4.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private TabControl tab_all_components;
        private TabPage tabPage_frimware;
        private Panel panel1;
        private Button button_disconnect;
        private Label label_message;
        private Label label_device;
        private Label label_port;
        private Label label_baudrate;
        private Button button_connect;
        private Button button_refresh;
        private ComboBox comboBox_baudrate;
        private ComboBox comboBox_port;
        private ComboBox comboBox_device;
        private Panel panel3;
        private Label label_FwCalculatedCRC;
        private Label label_fwHeaderCRC;
        private Label label_fwLength;
        private Label label_fwID;
        private Label label_filetype;
        private Label label_fwtype;
        private Label label_BinStatus;
        private Label label_binsize;
        private Label label_binName;
        private Button button_browse_file;
        private Panel panel2;
        private Label lable_dataPackets_Update;
        private Label lable_progressBar_Percentage;
        private Button button_upgrade;
        private ProgressBar progressBar;
        private TabPage tabPage_test_device;
        private Button button_rover_config;
        private Button button_radio_test;
        private Button button_gnss_test;
        private Button button_constellation_test;
        private Button button_base_config;
        private Panel panel4;
        private RichTextBox rtbLogs;
        private Button button_OTA_mode;
        private PictureBox pictureBox1;
        private Button button_saveLogs;
        private Button button_baidu;
        private Button button_gallileo;
        private Button button_glonass;
        private Button button_clearLogs;
        private Button button_ResetDevice;
        private Button btnCustomCommand;
    }
}
