using System.IO.Ports;
using System.IO;
using System.Text;
using System.Runtime.CompilerServices;


namespace Grader_Test_APP_v2._0
{
    public partial class Form1 : Form
    {

        // Serial port object
        private SerialPort serialport1 = new SerialPort();


        // ================= Test Device TAB veriables =================
        //==============================================================

        private readonly StringBuilder _rxBuffer = new StringBuilder();

        // Flags to track test state
        //private bool _deviceStatusReceived = false;
        //private bool _radioStatusReceived = false;
        //private bool _constellationStausRecieved = false;
        //private bool _configBaseStatusReceived = false;
        //private bool _configRoverStatusReceived = false;
        //private bool _TestRunning = false;
        private bool _galileoEnabled = false;
        private bool _glonassEnabled = false;
        private bool _beidouEnabled = false;

        // log level for logging purpose in device test tab
        public enum LogLevel
        {
            INFO,
            WARNING,
            ERROR
        }

        // Active tests
        private enum ActiveTest
        {
            None,
            GNSS,
            ReadUid,
            Radio,
            Constellation,
            ConfigBase,
            ConfigRover,
            gallilio,
            glonass,
            beidou,
            Reset,
            Custom,
            FirmwareUpgrade
        }

        private ActiveTest _currentTest = ActiveTest.None;

        // Device info variables
        private string _deviceUid = "";
        private string _deviceImei = "";
        private string _deviceModel = "";
        private string _deviceProductId = "";

        // ================= Test Device TAB veriables =================
        //==============================================================

        // Firmware upgrade variables
        private byte[] firmwareData;
        //private int _lastLoggedFwPercent = -1;

        // Pause/Resume event for firmware upgrade
        private ManualResetEventSlim _fwPauseEvent = new ManualResetEventSlim(true);
        private volatile bool _fwIsPaused = false;
        private volatile bool _fwIsUpgrading = false;

        // Cancel support for firmware upgrade
        private volatile bool _fwCancelRequested = false;

        // Track firmware progress log position
        private int _fwProgressLogStart = -1;
        private int _fwProgressLogLength = 0;

        // Firmware constants
        const byte FW_TYPE_APP = 0xAA;
        const byte FW_FILE_BIN = 0x11;

        // CRC16 table for checksum calculation
        static readonly ushort[] crc16_table = new ushort[]
    {
    0x0000, 0x1021, 0x2042, 0x3063, 0x4084, 0x50A5, 0x60C6, 0x70E7,
    0x8108, 0x9129, 0xA14A, 0xB16B, 0xC18C, 0xD1AD, 0xE1CE, 0xF1EF,
    0x1231, 0x0210, 0x3273, 0x2252, 0x52B5, 0x4294, 0x72F7, 0x62D6,
    0x9339, 0x8318, 0xB37B, 0xA35A, 0xD3BD, 0xC39C, 0xF3FF, 0xE3DE,
    0x2462, 0x3443, 0x0420, 0x1401, 0x64E6, 0x74C7, 0x44A4, 0x5485,
    0xA56A, 0xB54B, 0x8528, 0x9509, 0xE5EE, 0xF5CF, 0xC5AC, 0xD58D,
    0x3653, 0x2672, 0x1611, 0x0630, 0x76D7, 0x66F6, 0x5695, 0x46B4,
    0xB75B, 0xA77A, 0x9719, 0x8738, 0xF7DF, 0xE7FE, 0xD79D, 0xC7BC,
    0x48C4, 0x58E5, 0x6886, 0x78A7, 0x0840, 0x1861, 0x2802, 0x3823,
    0xC9CC, 0xD9ED, 0xE98E, 0xF9AF, 0x8948, 0x9969, 0xA90A, 0xB92B,
    0x5AF5, 0x4AD4, 0x7AB7, 0x6A96, 0x1A71, 0x0A50, 0x3A33, 0x2A12,
    0xDBFD, 0xCBDC, 0xFBBF, 0xEB9E, 0x9B79, 0x8B58, 0xBB3B, 0xAB1A,
    0x6CA6, 0x7C87, 0x4CE4, 0x5CC5, 0x2C22, 0x3C03, 0x0C60, 0x1C41,
    0xEDAE, 0xFD8F, 0xCDEC, 0xDDCD, 0xAD2A, 0xBD0B, 0x8D68, 0x9D49,
    0x7E97, 0x6EB6, 0x5ED5, 0x4EF4, 0x3E13, 0x2E32, 0x1E51, 0x0E70,
    0xFF9F, 0xEFBE, 0xDFDD, 0xCFFC, 0xBF1B, 0xAF3A, 0x9F59, 0x8F78,
    0x9188, 0x81A9, 0xB1CA, 0xA1EB, 0xD10C, 0xC12D, 0xF14E, 0xE16F,
    0x1080, 0x00A1, 0x30C2, 0x20E3, 0x5004, 0x4025, 0x7046, 0x6067,
    0x83B9, 0x9398, 0xA3FB, 0xB3DA, 0xC33D, 0xD31C, 0xE37F, 0xF35E,
    0x02B1, 0x1290, 0x22F3, 0x32D2, 0x4235, 0x5214, 0x6277, 0x7256,
    0xB5EA, 0xA5CB, 0x95A8, 0x8589, 0xF56E, 0xE54F, 0xD52C, 0xC50D,
    0x34E2, 0x24C3, 0x14A0, 0x0481, 0x7466, 0x6447, 0x5424, 0x4405,
    0xA7DB, 0xB7FA, 0x8799, 0x97B8, 0xE75F, 0xF77E, 0xC71D, 0xD73C,
    0x26D3, 0x36F2, 0x0691, 0x16B0, 0x6657, 0x7676, 0x4615, 0x5634,
    0xD94C, 0xC96D, 0xF90E, 0xE92F, 0x99C8, 0x89E9, 0xB98A, 0xA9AB,
    0x5844, 0x4865, 0x7806, 0x6827, 0x18C0, 0x08E1, 0x3882, 0x28A3,
    0xCB7D, 0xDB5C, 0xEB3F, 0xFB1E, 0x8BF9, 0x9BD8, 0xABBB, 0xBB9A,
    0x4A75, 0x5A54, 0x6A37, 0x7A16, 0x0AF1, 0x1AD0, 0x2AB3, 0x3A92,
    0xFD2E, 0xED0F, 0xDD6C, 0xCD4D, 0xBDAA, 0xAD8B, 0x9DE8, 0x8DC9,
    0x7C26, 0x6C07, 0x5C64, 0x4C45, 0x3CA2, 0x2C83, 0x1CE0, 0x0CC1,
    0xEF1F, 0xFF3E, 0xCF5D, 0xDF7C, 0xAF9B, 0xBFBA, 0x8FD9, 0x9FF8,
    0x6E17, 0x7E36, 0x4E55, 0x5E74, 0x2E93, 0x3EB2, 0x0ED1, 0x1EF0
    };
        public Form1()
        {
            InitializeComponent();
            getAvailablePorts();
        }

        //calculate crc 
        static ushort CalculateCRC16(byte[] data)
        {
            ushort crc = 0xFFFF;

            foreach (byte b in data)
            {
                int index = ((crc >> 8) ^ b) & 0xFF;
                crc = (ushort)((crc << 8) ^ crc16_table[index]);
            }

            return crc;
        }

        //function to get available ports in the comboBox
        void getAvailablePorts()
        {
            String[] ports = SerialPort.GetPortNames();
            comboBox_port.Items.Clear();
            comboBox_port.Items.AddRange(ports);
        }

        //refresh port function
        private void refresh_port()
        {
            string previousSelection = comboBox_port.SelectedItem?.ToString();

            comboBox_port.Items.Clear();
            comboBox_port.Items.Add("Select");

            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);

            comboBox_port.Items.AddRange(ports);

            if (!string.IsNullOrEmpty(previousSelection) &&
                comboBox_port.Items.Contains(previousSelection))
            {
                comboBox_port.SelectedItem = previousSelection;
            }
            else
            {
                comboBox_port.SelectedIndex = 0;
            }
        }

        //handeles the UI, Do firmware updating in the background...
        void UI(Action action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }

        // for software reset, Reset to start, close port, reset flags and progress bar
        void reset()
        {
            if (serialport1.IsOpen)
                serialport1.Close();

            // Update UI elements on the main thread
            UI(() =>
            {
                label_message.Text = "Device Disconnected";
                label_message.ForeColor = Color.White;
                label_message.BackColor = ColorTranslator.FromHtml("#BA1A1A");
                button_gallileo.BackColor = Color.FromArgb(50, 50, 50);
                button_glonass.BackColor = Color.FromArgb(50, 50, 50);
                button_baidu.BackColor = Color.FromArgb(50, 50, 50);

                // Reset UI elements
                button_disconnect.Enabled = false;
                button_disconnect.Visible = false;
                button_connect.Enabled = true;
                button_connect.Visible = true;
                button_upgrade.Enabled = false;
                button_refresh.Enabled = true;
                button_browse_file.Enabled = false;
                button_OTA_mode.Enabled = false;
                comboBox_port.Enabled = true;
                comboBox_baudrate.Enabled = true;
                comboBox_device.Enabled = true;
                lable_dataPackets_Update.Visible = false;
                lable_progressBar_Percentage.Visible = false;
                progressBar.Value = 0;

                // Reset file info state
                label_binName.Text = "Name:";
                label_binsize.Text = "Size:";
                label_fwtype.Text = "Fwtype:";
                label_filetype.Text = "FileType:";
                label_fwID.Text = "FwID:";
                label_fwLength.Text = "Fw Length:";
                label_fwHeaderCRC.Text = "Fw Header CRC:";
                label_FwCalculatedCRC.Text = "Fw Calculated CRC:";
                label_BinStatus.Text = "Status:";
                label_BinStatus.BackColor = Color.FromArgb(34, 34, 34);

                AppendLog("Device disconnected", LogLevel.WARNING);   // log
            });
        }

        // OTA mode button on click
        private void button_OTA_mode_Click(object sender, EventArgs e)
        {

            //Sending command to device To enter in OTA Mode
            String command = "#42,1,0*07\r\n";
            serialport1.Write(command);

        }

        // Pause/Resume button on click
        private void btn_pauseResume_Click(object sender, EventArgs e)
        {
            if (!_fwIsUpgrading)
                return;

            if (!_fwIsPaused)
            {
                _fwPauseEvent.Reset();   // BLOCK firmware thread
                AppendLog($"Firmware Upgrade Pause at {lable_progressBar_Percentage.Text}", LogLevel.WARNING);
                _fwIsPaused = true;
                btn_pauseResume.Text = "Resume";
                label_message.Text = "Firmware Upgrade Paused";
                label_message.BackColor = Color.Yellow;
                label_message.ForeColor = Color.Black;
            }
            else
            {
                _fwPauseEvent.Set();     // UNBLOCK firmware thread
                _fwIsPaused = false;
                AppendLog("Firmware Upgrade Resume", LogLevel.INFO);
                btn_pauseResume.Text = "Pause";
                label_message.Text = "Firmware Upgrade Resumed";
                label_message.ForeColor = Color.White;
                label_message.BackColor = Color.FromArgb(56, 106, 31);
            }
        }

        // Cancel button on click
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (!_fwIsUpgrading)
                return;

            _fwCancelRequested = true;

            // Make sure firmware thread is not stuck in pause
            _fwPauseEvent.Set();

            label_message.Text = "Firmware upgrade canceled";
            label_message.BackColor = ColorTranslator.FromHtml("#BA1A1A");

            MessageBox.Show(
                "Firmware upgrade canceled Please Restart the device.",
                "",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // Reset UI and state
            progressBar.Value = 0;
            lable_progressBar_Percentage.Text = "0%";
            lable_dataPackets_Update.Text = "idle";

            // Reset UI elements
            button_OTA_mode.Enabled = false;
            button_gnss_test.Enabled = true;
            button_radio_test.Enabled = true;
            button_constellation_test.Enabled = true;
            button_base_config.Enabled = true;
            button_rover_config.Enabled = true;
            button_gallileo.Enabled = true;
            button_glonass.Enabled = true;
            button_baidu.Enabled = true;
            button_saveLogs.Enabled = true;
            btnCustomCommand.Enabled = true;
            btn_clearLogs.Enabled = true;
            btn_ResetDevice.Enabled = true;
            btn_pauseResume.Enabled = false;
            btn_cancel.Enabled = false;
            button_disconnect.Enabled = true;

            // Reset firmware upgrade state
            label_binName.Text = "Name:";
            label_binsize.Text = "Size:";
            label_fwtype.Text = "Fwtype:";
            label_filetype.Text = "FileType:";
            label_fwID.Text = "FwID:";
            label_fwLength.Text = "Fw Length:";
            label_fwHeaderCRC.Text = "Fw Header CRC:";
            label_FwCalculatedCRC.Text = "Fw Calculated CRC:";
            label_BinStatus.Text = "Status:";
            label_BinStatus.BackColor = Color.FromArgb(34, 34, 34);
        }

        // Button refresh on click
        private void button_refresh_Click(object sender, EventArgs e)
        {
            UI(() =>
            {
                refresh_port();
                comboBox_port.Text = "Select";
                comboBox_baudrate.Text = "Select";
                comboBox_device.Text = "Select";
                label_message.Text = "Port Refreshed";
                label_message.BackColor = Color.Yellow;
                label_message.ForeColor = Color.Black;
                progressBar.Value = 0;
            });
        }

        // Port comboBox drop down event
        private void comboBox_port_DropDown(object sender, EventArgs e)
        {
            refresh_port();
        }

        // Port Connect button on click
        private void button_connect_Click(object sender, EventArgs e)
        {
            try
            {

                if (comboBox_device.Text == "Select")
                {
                    label_message.Text = "Please Select Device*";
                    label_message.BackColor = ColorTranslator.FromHtml("#BA1A1A");
                    label_message.ForeColor = Color.White;
                    return;
                }
                if (comboBox_port.Text == "Select")
                {
                    label_message.Text = "Please Select Port*";
                    label_message.BackColor = ColorTranslator.FromHtml("#BA1A1A");
                    label_message.ForeColor = Color.White;
                    return;
                }
                if (comboBox_baudrate.Text == "Select")
                {
                    label_message.Text = "Please Select baudrate*";
                    label_message.BackColor = ColorTranslator.FromHtml("#BA1A1A");
                    label_message.ForeColor = Color.White;
                    return;
                }

                else
                {
                    // Opening PORT
                    serialport1.PortName = comboBox_port.Text;
                    serialport1.BaudRate = Convert.ToInt32(comboBox_baudrate.Text);
                    serialport1.DataBits = 8;
                    serialport1.Parity = Parity.None;
                    serialport1.StopBits = StopBits.One;
                    serialport1.Handshake = Handshake.None;
                    serialport1.ReadTimeout = 1000;
                    serialport1.WriteTimeout = 1000;

                    serialport1.Open();
                    AttachRxHandler(); // attach data received handler
                    rtbLogs.Clear();

                    // Set current test to Read UID
                    _currentTest = ActiveTest.ReadUid;
                    ReadUid();
                    AppendLog("Reading Device UID...", LogLevel.INFO);

                    // Update UI elements on the main thread
                    UI(() =>
                    {
                        label_message.Text = "Device Connected";
                        label_message.ForeColor = Color.White;
                        label_message.BackColor = Color.FromArgb(56, 106, 31);

                        button_connect.Enabled = false;
                        button_connect.Visible = false;
                        button_disconnect.Enabled = true;
                        button_disconnect.Visible = true;
                        button_refresh.Enabled = false;
                        button_browse_file.Enabled = true;
                        comboBox_port.Enabled = false;
                        comboBox_baudrate.Enabled = false;
                        comboBox_device.Enabled = false;
                    });
                }
            }

            //Exception handling for port connection
            catch (UnauthorizedAccessException)
            {
                label_message.Text = "Unauthrized Access";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateDeviceInfoUI()
        {
            if (rtb_deviceInfo.InvokeRequired)
            {
                rtb_deviceInfo.Invoke(new Action(UpdateDeviceInfoUI));
                return;
            }

            rtb_deviceInfo.Clear();

            rtb_deviceInfo.AppendText($" Model: {_deviceModel}, ");
            rtb_deviceInfo.AppendText($"UID: {_deviceUid}, ");
            rtb_deviceInfo.AppendText($"Product ID: {_deviceProductId}");
        }

        //button port disconnect on click
        private void button_disconnect_Click(object sender, EventArgs e)
        {
            rtb_deviceInfo.Clear();
            reset();
        }
        //browse file button on click
        private void button_browse_file_Click(object sender, EventArgs e)
        {
            firmwareData = LoadFirmwareFromFile();

            button_upgrade.Enabled = firmwareData != null;
        }

        //send firmware function
        private bool send_firmware()
        {
            try
            {
                // Ensure firmware is running (not paused)
                _fwPauseEvent.Set();
                _fwIsPaused = false;

                // Reset progress log tracking
                _fwProgressLogStart = -1;
                _fwProgressLogLength = 0;

                // Clear cancel
                _fwCancelRequested = false;

                AppendLog("FIRMWARE UPGRADE STARTED", LogLevel.INFO); // log

                // Load firmware data
                if (firmwareData == null) return false;

                // Firmware parameters
                ushort fwId = 0x0001;

                byte[] firmwarePayload = firmwareData;   // FULL BIN
                uint fwLength = (uint)firmwarePayload.Length;

                uint actualLength = (uint)firmwareData.Length;

                if (fwLength != actualLength)
                {
                    UI(() => MessageBox.Show("Firmware length mismatch"));
                    return false;
                }

                // progress bar update
                UpdateStatus("Starting firmware update...", 0);
                progressBar.Value = 0;

                // Set current test to Firmware Upgrade
                _currentTest = ActiveTest.FirmwareUpgrade;

                // Detach data received handler during firmware upgrade
                serialport1.DataReceived -= SerialPort_DataReceived;
                serialport1.DiscardInBuffer();
                serialport1.DiscardOutBuffer();

                // START PACKET
                byte[] START_PACKET = { 0x24, 0x01, 0x01, 0x00, 0x00, 0x02, 0x23 };
                serialport1.DiscardInBuffer();

                //progress bar update
                UpdateStatus("Sending START packet...", 0);
                Application.DoEvents();

                AppendLog("Sending START packet", LogLevel.INFO); // log

                // Send START packet
                serialport1.Write(START_PACKET, 0, START_PACKET.Length);

                if (!WaitForUpdateResponse(5000))
                {
                    UI(() => MessageBox.Show("START failed"));
                    AppendLog("START packet failed (No ACK)", LogLevel.ERROR); //log
                    reset();
                    return false;
                }

                // HEADER PACKET
                ushort fwChk16 = CalculateCRC16(firmwareData);

                // payload = FW_TYPE + FILE_TYPE + FW_ID + FW_LENGTH + FW_CHK16
                List<byte> payload = new List<byte>();
                payload.Add(FW_TYPE_APP);        // 0xAA
                payload.Add(FW_FILE_BIN);        // 0x11
                payload.Add((byte)(fwId >> 8));
                payload.Add((byte)(fwId & 0xFF));
                payload.Add((byte)(fwLength >> 24));
                payload.Add((byte)(fwLength >> 16));
                payload.Add((byte)(fwLength >> 8));
                payload.Add((byte)(fwLength));
                payload.Add((byte)(fwChk16 >> 8));
                payload.Add((byte)(fwChk16 & 0xFF));

                byte len = (byte)payload.Count; // MUST be 10

                // checksum = CMD + LEN + sum(payload)
                ushort checksum = 0;
                checksum += 0x02;
                checksum += len;
                foreach (byte b in payload)
                    checksum += b;

                // Construct HEADER packet
                List<byte> headerPacket = new List<byte>();
                headerPacket.Add(0x24);          // STX
                headerPacket.Add(0x02);          // CMD
                headerPacket.Add(len);           // LEN (1 byte)
                headerPacket.AddRange(payload);  // DATA
                headerPacket.Add((byte)(checksum >> 8));
                headerPacket.Add((byte)(checksum & 0xFF));
                headerPacket.Add(0x23);          // ETX

                // progress bar update
                UpdateStatus("Sending firmware header...", 0);
                Application.DoEvents();
                AppendLog("Sending HEADER packet", LogLevel.INFO); // log

                // Send HEADER packet
                serialport1.Write(headerPacket.ToArray(), 0, headerPacket.Count);

                if (!WaitForUpdateResponse())
                {
                    UI(() => MessageBox.Show("HEADER failed"));
                    AppendLog("HEADER packet failed (No ACK)", LogLevel.ERROR); // log
                    reset();
                    return false;
                }

                // DATA PACKETS
                int offset = 0;
                int CHUNK = 128;

                while (offset < firmwareData.Length)
                {
                    // PAUSE GATE (SAFE PAUSE POINT)
                    _fwPauseEvent.Wait();
                    if (_fwCancelRequested)
                        throw new OperationCanceledException("Firmware upgrade cancelled by user");

                    // Determine chunk size
                    int size = Math.Min(CHUNK, firmwareData.Length - offset);

                    // payload = CMD + LEN + DATA
                    List<byte> dataPayload = new List<byte>();
                    dataPayload.Add(0x03);          // CMD_DATA
                    dataPayload.Add((byte)size);    // LEN
                    dataPayload.AddRange(firmwareData.Skip(offset).Take(size));

                    // Calculate checksum
                    ushort dataChecksum = 0;
                    foreach (byte b in dataPayload)
                        dataChecksum += b;

                    // Construct DATA packet
                    List<byte> dataPacket = new List<byte>();
                    dataPacket.Add(0x24);           // STX
                    dataPacket.AddRange(dataPayload);
                    dataPacket.Add((byte)(dataChecksum >> 8));
                    dataPacket.Add((byte)(dataChecksum & 0xFF));
                    dataPacket.Add(0x23);           // ETX

                    try
                    {
                        if (!serialport1.IsOpen)
                            throw new IOException("Serial port closed");

                        serialport1.Write(dataPacket.ToArray(), 0, dataPacket.Count);

                        // Wait for ACK
                        if (!WaitForUpdateResponse())
                            throw new IOException("No ACK received");
                    }
                    catch (Exception)
                    {
                        UI(() =>
                        {
                            MessageBox.Show(
                                "Data Packet failure.\n\nPlease Restart the device and try again.",
                                "Device Disconnected Or Power off",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        });
                        AppendLog($"Firmware data sent: {offset}%", LogLevel.INFO); //log
                        reset();
                        return false;
                    }

                    offset += size;

                    // Update progress every 1%
                    UI(() =>
                    {
                        int percent = (int)(offset * 100 / firmwareData.Length);

                        UpdateStatus($"Sending firmware data... ({offset}/{firmwareData.Length})", percent);

                        if (!_fwIsPaused)
                        {
                            UpdateFirmwareProgressLog(percent);
                        }
                    });

                    Thread.Sleep(50);
                }

                //PAUSE GATE (SAFE PAUSE POINT)
                if (_fwCancelRequested)
                    throw new OperationCanceledException("Firmware upgrade cancelled by user");

                //progress bar update
                UpdateStatus("Finalizing update...", 100);
                Application.DoEvents();

                // END PACKET and UPDTATE PACKET
                byte[] END_PACKET = { 0x24, 0x04, 0x01, 0x00, 0x00, 0x05, 0x23 };
                byte[] UPDATE_PACKET = { 0x24, 0x05, 0x01, 0x00, 0x00, 0x06, 0x23 };

                AppendLog("Sending END packet", LogLevel.INFO); // log

                // Send END packet
                serialport1.Write(END_PACKET, 0, END_PACKET.Length);
                if (!WaitForUpdateResponse())
                {
                    UI(() => MessageBox.Show("END failed"));
                    AppendLog("END packet failed (No ACK)", LogLevel.ERROR);
                    reset();
                    return false;
                }

                AppendLog("Sending UPDATE packet", LogLevel.INFO); // log
                AppendLog("Finalizing Update...", LogLevel.INFO); // log

                // Send UPDATE packet
                serialport1.Write(UPDATE_PACKET, 0, UPDATE_PACKET.Length);
                if (!WaitForUpdateResponse(40000))
                {
                    UI(() => MessageBox.Show("UPDATE failed"));
                    AppendLog("UPDATE packet failed (Device did not reboot)", LogLevel.ERROR); // log
                    reset();
                    return false;
                }

                //progress bar update
                UpdateStatus("Update complete", 100);
                Application.DoEvents();

                UI(() =>
                {
                    // Final UI update
                    lable_dataPackets_Update.Text = "Upgrade successful";
                    lable_progressBar_Percentage.Text = "100%";
                    MessageBox.Show(
                        "Firmware upgrade completed successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                });

                AppendLog("FIRMWARE UPGRADE COMPLETED SUCCESSFULLY", LogLevel.INFO); // log

                // Confirming the update is done or no test is active
                _currentTest = ActiveTest.None;

                serialport1.DiscardInBuffer();
                serialport1.DiscardOutBuffer();

                AttachRxHandler(); // re-attach data received handler  
                return true;
            }
            catch (OperationCanceledException)
            {
                AppendLog("Firmware upgrade cancelled by user", LogLevel.WARNING);

                UI(() =>
                {
                    label_message.Text = "Firmware Upgrade Cancelled";
                });

                _currentTest = ActiveTest.None;
                AttachRxHandler();

                return false;
            }
            catch (Exception ex)
            {
                AppendLog($"Firmware upgrade error: {ex.Message}", LogLevel.ERROR);
                reset();
                return false;
            }
        }

        // function to update firmware progress log
        private void UpdateFirmwareProgressLog(int percent)
        {
            if (rtbLogs.InvokeRequired)
            {
                rtbLogs.Invoke(new Action(() => UpdateFirmwareProgressLog(percent)));
                return;
            }

            string line = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] INFO: Sending firmware data:{percent}%\n";

            // First time → append new line
            if (_fwProgressLogStart < 0)
            {
                _fwProgressLogStart = rtbLogs.TextLength;
                _fwProgressLogLength = line.Length;

                rtbLogs.SelectionStart = _fwProgressLogStart;
                rtbLogs.SelectionLength = 0;
                rtbLogs.SelectionColor = Color.FromArgb(19, 154, 14); // INFO green
                rtbLogs.AppendText(line);
            }
            else
            {
                // Overwrite existing progress line
                rtbLogs.SelectionStart = _fwProgressLogStart;
                rtbLogs.SelectionLength = _fwProgressLogLength;
                rtbLogs.SelectionColor = Color.FromArgb(19, 154, 14); // INFO green
                rtbLogs.SelectedText = line;

                _fwProgressLogLength = line.Length;
            }

            rtbLogs.ScrollToCaret();
        }

        //upgrade button on click
        private async void button_upgrade_Click(object sender, EventArgs e)
        {

            _fwIsUpgrading = true;
            _fwIsPaused = false;
            _fwCancelRequested = false;
            _fwPauseEvent.Set();

            btn_pauseResume.Text = "Pause";

            button_upgrade.Enabled = false;
            button_disconnect.Enabled = false;
            button_browse_file.Enabled = false;
            button_gnss_test.Enabled = false;
            button_radio_test.Enabled = false;
            button_constellation_test.Enabled = false;
            button_base_config.Enabled = false;
            button_rover_config.Enabled = false;
            button_gallileo.Enabled = false;
            button_glonass.Enabled = false;
            button_baidu.Enabled = false;
            button_saveLogs.Enabled = false;
            btnCustomCommand.Enabled = false;
            btn_clearLogs.Enabled = false;
            btn_ResetDevice.Enabled = false;
            btn_pauseResume.Enabled = true;
            btn_cancel.Enabled = true;
            lable_dataPackets_Update.Visible = true;
            lable_progressBar_Percentage.Visible = true;


            // Start firmware update in background
            bool updateSuccess = false;

            try
            {
                // Run send_firmware in a background task
                updateSuccess = await Task.Run(() => send_firmware());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (updateSuccess)
            {
                button_disconnect.Enabled = true;
                button_OTA_mode.Enabled = false;
                button_gnss_test.Enabled = true;
                button_radio_test.Enabled = true;
                button_constellation_test.Enabled = true;
                button_base_config.Enabled = true;
                button_rover_config.Enabled = true;
                button_gallileo.Enabled = true;
                button_glonass.Enabled = true;
                button_baidu.Enabled = true;
                button_saveLogs.Enabled = true;
                btnCustomCommand.Enabled = true;
                btn_clearLogs.Enabled = true;
                btn_ResetDevice.Enabled = true;
                btn_pauseResume.Enabled = false;
                btn_cancel.Enabled = false;

                _fwIsUpgrading = false;
                _fwIsPaused = false;
                _fwPauseEvent.Set();

                btn_pauseResume.Text = "Start";
            }
        }

        //Function for waiting response from device
        private bool WaitForUpdateResponse(int timeoutMs = 5000)
        {
            serialport1.ReadTimeout = timeoutMs;

            try
            {
                // Read response packet
                byte[] resp = new byte[7];
                int read = 0;

                // Read until full packet received
                while (read < 7)
                {
                    if (!serialport1.IsOpen)
                        throw new IOException("Serial port closed");

                    read += serialport1.Read(resp, read, 7 - read);
                }

                // resp format: 24 CMD LEN DATA CHK_H CHK_L 23
                if (resp[0] != 0x24 || resp[6] != 0x23)
                    return false;

                // Check ACK/NACK
                byte ack = resp[3];

                // Log received response
                Console.WriteLine(
                    $"RX → {BitConverter.ToString(resp)}"
                );

                return ack == 0x00;   // ACK
            }
            catch (TimeoutException)
            {
                return false;
            }
            catch (OperationCanceledException)
            {
                HandlePortDisconnect();
                return false;
            }
            catch (IOException)
            {
                HandlePortDisconnect();
                return false;
            }
            catch (InvalidOperationException)
            {
                HandlePortDisconnect();
                return false;
            }
        }

        //function for if port is disconnected while updating firmware
        private void HandlePortDisconnect()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(HandlePortDisconnect));
                return;
            }

            UI(() => MessageBox.Show(
                "Serial port closed\n\nPlease restart the device and try again.",
                "Device disconnected.",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            ));

            reset();
        }

        //Loading firmware bin file from file explorer...
        private byte[] LoadFirmwareFromFile()
        {
            // Open file dialog to select firmware file
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select Firmware BIN File";
                ofd.Filter = "Firmware Files (*.bin)|*.bin";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() != DialogResult.OK)
                    return null;

                try
                {
                    // Read firmware file
                    byte[] firmware = File.ReadAllBytes(ofd.FileName);
                    FileInfo info = new FileInfo(ofd.FileName);

                    if (firmware.Length == 0)
                    {
                        UI(() => MessageBox.Show("Empty firmware file"));
                        return null;
                    }
                    // Calculate firmware parameters
                    uint fwLength = (uint)firmware.Length;
                    ushort calculatedCrc = CalculateCRC16(firmware);

                    byte fwType = FW_TYPE_APP;     // from constants
                    byte fileType = FW_FILE_BIN;  // from constants
                    ushort fwId = 0x0001;          // from app/versioning

                    UI(() =>
                    {
                        // Update UI with firmware info
                        label_binName.Text = $"Name: {info.Name}";
                        label_binsize.Text = $"Size: {fwLength / 1024.0:N2} kB";

                        label_fwtype.Text = $"Fwtype: 0x{fwType:X2}";
                        label_filetype.Text = $"Filetype: 0x{fileType:X2}";
                        label_fwID.Text = $"FwID: 0x{fwId:X4}";
                        label_fwLength.Text = $"Fw Length: {fwLength} bytes";
                        label_fwHeaderCRC.Text = $"Fw Header CRC: N/A";
                        label_FwCalculatedCRC.Text = $"Fw Calculated CRC: 0x{calculatedCrc:X4}";

                        label_BinStatus.Text = "Status: Firmware Loaded Enter Upgrade Mode to Upgrade firmware";
                        label_BinStatus.BackColor = ColorTranslator.FromHtml("#2E7D32");
                        label_BinStatus.ForeColor = Color.White;

                        button_OTA_mode.Enabled = true;
                    });

                    return firmware;
                }
                catch (Exception ex)
                {
                    UI(() => MessageBox.Show($"Failed to load firmware:\n{ex.Message}"));
                    return null;
                }
            }
        }

        //for showing the percentage and data packets transfer on the progress bar
        private void UpdateStatus(string operation, int percent)
        {
            UI(() =>
            {
                // Update status labels and progress bar
                lable_dataPackets_Update.Text = operation;
                lable_progressBar_Percentage.Text = percent + "%";
                progressBar.Value = percent;
            });
        }
        //=========================================================
        //================ Start of GNSS Test TAB =================
        //=========================================================

        // Test GNSS button on click
        private void btnGnssTest_Click(object sender, EventArgs e)
        {
            if (!EnsurePortOpen()) return;
            StopAllTests();
            AttachRxHandler();
            // rtbLogs.Clear();
            //_deviceStatusReceived = false;
            _currentTest = ActiveTest.GNSS;

            AppendLog("GNSS TEST STARTED", LogLevel.INFO);
            //LogConnectionInfo();
            SendGnssCommand();
        }

        // Test Radio button on click
        private void button_radio_test_Click(object sender, EventArgs e)
        {
            if (!EnsurePortOpen()) return;

            StopAllTests();
            AttachRxHandler();
            // rtbLogs.Clear();
            // _radioStatusReceived = false;
            _currentTest = ActiveTest.Radio;

            AppendLog("RADIO TEST STARTED", LogLevel.INFO);
            // LogConnectionInfo();
            SendRadioCommand();
        }

        // Constellation Test button on click
        private void button_constellation_test_Click(object sender, EventArgs e)
        {
            if (!EnsurePortOpen()) return;
            StopAllTests();
            AttachRxHandler();
            //  rtbLogs.Clear();
            //_constellationStausRecieved = false;
            _currentTest = ActiveTest.Constellation;

            AppendLog("CONSTELLATION TEST STARTED", LogLevel.INFO);
            // LogConnectionInfo();
            SendConstellationCommand();
        }

        // Base Config button on click
        private void button_base_config_Click(object sender, EventArgs e)
        {
            if (!EnsurePortOpen()) return;
            StopAllTests();
            AttachRxHandler();
            //_configBaseStatusReceived = false;
            _currentTest = ActiveTest.ConfigBase;
            ConfigureBaseCommand();
        }

        // Rover Config button on click
        private void button_rover_config_Click(object sender, EventArgs e)
        {
            if (!EnsurePortOpen()) return;
            StopAllTests();
            AttachRxHandler();
            //_configRoverStatusReceived = false;
            _currentTest = ActiveTest.ConfigRover;
            ConfigureRoverCommand();
        }

        // Galileo Enable/Disable button on click
        private void button_gallileo_Click(object sender, EventArgs e)
        {
            if (!EnsurePortOpen()) return;

            StopAllTests();
            AttachRxHandler();
            _currentTest = ActiveTest.gallilio;

            if (_galileoEnabled)
            {
                DisableGallileoCommand();
                button_gallileo.BackColor = Color.FromArgb(186, 26, 26);
            }
            else
            {
                EnableGallileoCommand();
                button_gallileo.BackColor = Color.FromArgb(56, 106, 31);
            }
        }

        // Glonass Enable/Disable button on click
        private void button_glonass_Click(object sender, EventArgs e)
        {
            if (!EnsurePortOpen()) return;

            StopAllTests();
            AttachRxHandler();
            _currentTest = ActiveTest.glonass;

            if (_glonassEnabled)
            {
                DisableGlonasssCommand();
                button_glonass.BackColor = Color.FromArgb(186, 26, 26);
            }
            else
            {
                EnableGlonassCommand();
                button_glonass.BackColor = Color.FromArgb(56, 106, 31);
            }
        }

        // Beidou Enable/Disable button on click
        private void button_beidou_Click(object sender, EventArgs e)
        {
            if (!EnsurePortOpen()) return;

            StopAllTests();
            AttachRxHandler();
            _currentTest = ActiveTest.beidou;

            if (_beidouEnabled)
            {
                DisableBeidouCommand();
                button_baidu.BackColor = Color.FromArgb(186, 26, 26);
            }
            else
            {
                EnableBeidouCommand();
                button_baidu.BackColor = Color.FromArgb(56, 106, 31);
            }

        }

        // Clear Logs button on click
        private void button_clearLogs_Click(object sender, EventArgs e)
        {
            rtbLogs.Clear();
        }

        private void button_ResetDevice_Click(object sender, EventArgs e)
        {
            if (!EnsurePortOpen()) return;
            StopAllTests();
            AttachRxHandler();
            _currentTest = ActiveTest.Reset;
            ResetCommand();
        }

        // Make sure that the port is open before sending commands
        private bool EnsurePortOpen()
        {
            if (!serialport1.IsOpen)
            {
                AppendLog("Serial port not open", LogLevel.ERROR);
                return false;
            }
            return true;
        }

        // Device Commands
        private void ResetCommand()
        {
            SendCommand("#1,0*2D\r\n", "Device Reset CMD sent Please wait...");
        }

        private void SendGnssCommand()
        {
            SendCommand("#6,14,0,1,45,0,0,31,0,0,1,1,0,0,0,0*1D\r\n", "CMD 6 sent Please wait...");
            //SendCommand("#6,1,0*37\r\n", "CMD 6 sent Please wait...");
        }

        private void SendRadioCommand()
        {
            SendCommand("#9,4,4340125,TRIMMK3-19200,L,2*4A\r\n", "CMD 9 sent Please wait...");
            //SendCommand("#9,24,4340125,TRIMMK3-9600,L,2*4D\r\n", "CMD 9 sent Please wait..."); 
        }

        private void SendConstellationCommand()
        {
            SendCommand("#12,0*1F\r\n", "CMD 20 sent Please wait...");
        }

        private void ConfigureBaseCommand()
        {
            SendCommand("#2,8,0,1,15,0,0,0,1,1000*23\r\n", "CMD 2 sent Please wait...");
        }

        private void ConfigureRoverCommand()
        {
            SendCommand("#3,3,1,15,1*04\r\n", "CMD 3 sent Please Wait...");
        }

        private void EnableGallileoCommand()
        {
            SendCommand("#15,2,GALILEO,1*6E\r\n", "CMD Enable Galileo sent Please wait...");
        }

        private void DisableGallileoCommand()
        {
            SendCommand("#15,2,GALILEO,0*6F\r\n", "CMD Disable Galileo sent Please wait...");
        }

        private void EnableGlonassCommand()
        {
            SendCommand("#15,2,GLONASS,1*60\r\n", "CMD Enable Glonass sent Please wait...");
        }

        private void DisableGlonasssCommand()
        {
            SendCommand("#15,2,GLONASS,0*61\r\n", "CMD Disable Glonass sent Please wait...");
        }

        private void EnableBeidouCommand()
        {
            SendCommand("#15,2,BEIDOU,1*3B\r\n", "CMD Enable Beidou sent Please wait...");
        }

        private void DisableBeidouCommand()
        {
            SendCommand("#15,2,BEIDOU,0*3A\r\n", "CMD Disable Beidou sent Please wait...");
        }

        private void ReadUid()
        {
            SendCommand("#36,0*19\r\n", "CMD 36 sent");
        }

        // send command helper function
        private void SendCommand(string cmd, string logText)
        {
            try
            {
                serialport1.DiscardInBuffer();
                serialport1.DiscardOutBuffer();
                serialport1.Write(cmd);
                AppendLog(logText, LogLevel.INFO);
            }
            catch (Exception ex)
            {
                AppendLog("Send failed: " + ex.Message, LogLevel.ERROR);
            }
        }

        // Attach data received handler
        private void AttachRxHandler()
        {
            serialport1.DataReceived -= SerialPort_DataReceived;
            serialport1.DataReceived += SerialPort_DataReceived;
        }

        // Data received event handler
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialport1.ReadExisting();

                if (_currentTest == ActiveTest.FirmwareUpgrade)
                    return; // drain only, do not parse

                _rxBuffer.Append(data);
                ProcessRxBuffer();
            }
            catch { }
        }

        // Rx buffer processing function
        private void ProcessRxBuffer()
        {
            string buffer = _rxBuffer.ToString();
            int idx;

            while ((idx = buffer.IndexOf("\r\n")) >= 0 ||
                   (idx = buffer.IndexOf("\n")) >= 0)
            {
                string packet = buffer.Substring(0, idx).Trim();
                buffer = buffer.Substring(idx).TrimStart('\r', '\n');

                if (packet.StartsWith("#") && packet.Contains("*"))
                {
                    ParseDevicePacket(packet);
                }
            }

            _rxBuffer.Clear();
            _rxBuffer.Append(buffer);
        }

        // Device packet parsing function
        private void ParseDevicePacket(string packet)
        {
            try
            {
                if (_currentTest == ActiveTest.None)
                {
                    return;
                }
                // Log received packet
                AppendLog("RX: " + packet, LogLevel.INFO);

                // Handle Custom command response
                if (_currentTest == ActiveTest.Custom)
                {
                    _currentTest = ActiveTest.None;
                    serialport1.DiscardInBuffer();

                    AppendLog("Command response received", LogLevel.INFO);
                    return;
                }

                string[] parts = packet.Substring(1).Split('*');
                string[] f = parts[0].Split(',');

                int cmd = int.Parse(f[0]);

                switch (cmd)
                {
                    case 6: // GNSS Status
                        if (_currentTest == ActiveTest.GNSS) //&& !_deviceStatusReceived)
                        {
                            ShowDeviceStatus(f);
                            //_deviceStatusReceived = true;
                            _currentTest = ActiveTest.None; // stop GNSS test
                            AppendLog("GNSS STATUS RECEIVED", LogLevel.INFO);
                        }
                        break;

                    case 9: // Radio Status
                        if (_currentTest == ActiveTest.Radio) //&& !_radioStatusReceived)
                        {
                            ShowRadioStatus(f);
                            //_radioStatusReceived = true;
                            _currentTest = ActiveTest.None; // stop Radio test
                            AppendLog("RADIO STATUS RECEIVED", LogLevel.INFO);
                        }
                        break;

                    case 12: // Constellation Status
                        if (_currentTest == ActiveTest.Constellation) // && !_constellationStausRecieved)
                        {
                            ShowConstellationStatus(f);
                            //_constellationStausRecieved = true;
                            _currentTest = ActiveTest.None; // stop Constellation test
                            AppendLog("CONSTELLATION STATUS RECEIVED", LogLevel.INFO);
                        }
                        break;

                    case 2: // Config Base ACK
                        //_configBaseStatusReceived = true;
                        _currentTest = ActiveTest.None; // stop Config Base test
                        AppendLog("BASE CONFIGURATION ACK RECEIVED", LogLevel.INFO);
                        break;

                    case 3: // Config Rover ACK
                        //_configBaseStatusReceived = true;
                        _currentTest = ActiveTest.None; // stop config Rover test
                        AppendLog("Rover CONFIGURATION ACK RECEIVED", LogLevel.INFO);
                        break;

                    case 15:
                        {
                            // ACK for constellation enable/disable
                            switch (_currentTest)
                            {
                                case ActiveTest.gallilio:
                                    _galileoEnabled = !_galileoEnabled;
                                    AppendLog("Galileo ACK RECEIVED", LogLevel.INFO);
                                    break;

                                case ActiveTest.glonass:
                                    _glonassEnabled = !_glonassEnabled;
                                    AppendLog("Glonass ACK RECEIVED", LogLevel.INFO);
                                    break;

                                case ActiveTest.beidou:
                                    _beidouEnabled = !_beidouEnabled;
                                    AppendLog("Beidou ACK RECEIVED", LogLevel.INFO);
                                    break;
                            }

                            _currentTest = ActiveTest.None;
                        }
                        break;

                    case 36:  // Read UID Response
                        {
                            if (_currentTest != ActiveTest.ReadUid)
                                break;

                            if (f.Length < 6)
                            {
                                AppendLog("Incomplete CMD-36 packet", LogLevel.ERROR);
                                break;
                            }


                            _deviceUid = f[2];
                            _deviceImei = f[3];
                            _deviceModel = f[4];
                            _deviceProductId = f[5];

                            UpdateDeviceInfoUI();
                            LogConnectionInfo();

                            _currentTest = ActiveTest.None;
                            break;
                        }
                    case 1:  // Device Reset ACK
                        {
                            _currentTest = ActiveTest.None;
                            AppendLog("Device Reset ACK RECEIVED", LogLevel.INFO);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                AppendLog("Packet parse error: " + ex.Message, LogLevel.ERROR);
            }
        }

        // display GNSS device status on log
        private void ShowDeviceStatus(string[] f)
        {
            // Expect: CMD + LEN + 14 payload fields = 16
            if (f.Length < 16)
            {
                AppendLog("Incomplete CMD-6 packet", LogLevel.ERROR);
                return;
            }

            try
            {
                int mode = int.Parse(f[2]);
                int fix = int.Parse(f[3]);
                int satCount = int.Parse(f[4]);
                int com = int.Parse(f[5]);
                int isStatic = int.Parse(f[6]);
                int battery = int.Parse(f[7]);
                int extPower = int.Parse(f[8]);
                int rtcmLen = int.Parse(f[9]);
                int satAvail = int.Parse(f[10]);
                int simAvail = int.Parse(f[11]);
                int net4g = int.Parse(f[12]);
                int server = int.Parse(f[13]);
                int runtime = int.Parse(f[14]);
                int usbDrive = int.Parse(f[15]);

                AppendLog("===== DEVICE STATUS =====", LogLevel.INFO);

                AppendLog($"Mode                 : {(mode == 0 ? "Base" : "Rover")}", LogLevel.INFO);

                AppendLog($"Fix Type             : {(fix == 4 ? "FIX" : fix == 5 ? "FLOAT" : "INVALID")}",
                          fix < 4 ? LogLevel.WARNING : LogLevel.INFO);

                AppendLog($"Satellite Count      : {satCount}", LogLevel.INFO);
                AppendLog($"COM                  : {com}", LogLevel.INFO);
                AppendLog($"Static Mode          : {(isStatic == 1 ? "YES" : "NO")}", LogLevel.INFO);

                AppendLog($"Battery              : {battery}%",
                          battery < 20 ? LogLevel.WARNING : LogLevel.INFO);

                AppendLog($"External Power       : {(extPower == 1 ? "Connected" : "Not Connected")}",
                          extPower == 1 ? LogLevel.INFO : LogLevel.WARNING);

                AppendLog($"RTCM Data Length/sec : {rtcmLen}", LogLevel.INFO);

                AppendLog($"Satellite Available  : {(satAvail == 1 ? "YES" : "NO")}",
                          satAvail == 1 ? LogLevel.INFO : LogLevel.WARNING);

                AppendLog($"SIM Available        : {(simAvail == 1 ? "YES" : "NO")}",
                          simAvail == 1 ? LogLevel.INFO : LogLevel.WARNING);

                AppendLog($"4G Network Available : {(net4g == 1 ? "YES" : "NO")}",
                          net4g == 1 ? LogLevel.INFO : LogLevel.WARNING);

                AppendLog($"Server Connected     : {(server == 1 ? "YES" : "NO")}",
                          server == 1 ? LogLevel.INFO : LogLevel.WARNING);

                AppendLog($"Estimated Runtime    : {runtime} minutes", LogLevel.INFO);

                AppendLog($"USB Drive Status     : {(usbDrive == 0 ? "DISCONNECTED" :
                                                    usbDrive == 1 ? "PLUGGED" : "READY")}",
                          usbDrive == 2 ? LogLevel.INFO : LogLevel.WARNING);

                AppendLog("=========================", LogLevel.INFO);
            }
            catch
            {
                AppendLog("Device status parse error", LogLevel.ERROR);
            }
        }

        // display Radio status on log
        private void ShowRadioStatus(string[] f)
        {
            int len = int.Parse(f[1]);

            AppendLog("===== RADIO STATUS =====", LogLevel.INFO);

            // Short response (ACK / minimal status)
            if (len == 1)
            {
                AppendLog("Radio status ACK received", LogLevel.INFO);
                AppendLog("========================", LogLevel.INFO);
                return;
            }

            // Full response: LEN = 4
            if (len == 4 && f.Length >= 6)
            {
                long frequency = long.Parse(f[2]);
                string protocol = f[3];
                string txPower = f[4];
                int direction = int.Parse(f[5]);

                AppendLog($"Frequency : {frequency} Hz", LogLevel.INFO);
                AppendLog($"Protocol : {protocol}", LogLevel.INFO);
                AppendLog($"TX Power : {txPower}", LogLevel.INFO);

                string dirText = direction switch
                {
                    0 => "RX Only",
                    1 => "TX Only",
                    2 => "TX / RX",
                    _ => "Unknown"
                };

                AppendLog($"Direction : {dirText}", LogLevel.INFO);
                AppendLog("========================", LogLevel.INFO);
                return;
            }

            // Anything else
            AppendLog($"Unknown CMD-9 format (LEN={len})", LogLevel.WARNING);
            AppendLog("========================", LogLevel.INFO);
        }

        //display constellation status on log
        private void ShowConstellationStatus(string[] f)
        {
            // CMD(12), LEN(3), GAL, GLO, BDS
            if (f.Length < 5)
            {
                AppendLog("Incomplete CMD-12 Constellation Status packet", LogLevel.ERROR);
                return;
            }

            if (f[1] != "3")
            {
                AppendLog($"Unexpected CMD-12 length: {f[1]}", LogLevel.WARNING);
                return;
            }
            // Parse constellation statuses
            bool galileo = f[2] == "";
            bool glonass = f[3] == "";
            bool beidou = f[4] == "";

            _galileoEnabled = galileo;
            _glonassEnabled = glonass;
            _beidouEnabled = beidou;

            AppendLog("===== CONSTELLATION STATUS (CMD 12) =====", LogLevel.INFO);
            AppendLog($"GALILEO : {(galileo ? "ENABLED" : "DISABLED")}", LogLevel.INFO);
            AppendLog($"GLONASS : {(glonass ? "ENABLED" : "DISABLED")}", LogLevel.INFO);
            AppendLog($"BEIDOU  : {(beidou ? "ENABLED" : "DISABLED")}", LogLevel.INFO);
            AppendLog("========================================", LogLevel.INFO);
        }

        // log connection info function
        private void LogConnectionInfo()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(LogConnectionInfo));
                return;
            }

            AppendLog("====== CONNECTION INFO =======", LogLevel.INFO);

            AppendLog($"Device     : {comboBox_device.Text}", LogLevel.INFO);
            AppendLog($"Port       : {comboBox_port.Text}", LogLevel.INFO);
            AppendLog($"Baudrate   : {comboBox_baudrate.Text}", LogLevel.INFO);


            // show device info
            AppendLog($"UID        : {(string.IsNullOrWhiteSpace(_deviceUid) ? "Not Read" : _deviceUid)}",
                string.IsNullOrWhiteSpace(_deviceUid) ? LogLevel.WARNING : LogLevel.INFO);

            AppendLog($"IMEI       : {(string.IsNullOrWhiteSpace(_deviceImei) ? "Not Read" : _deviceImei)}",
                string.IsNullOrWhiteSpace(_deviceImei) ? LogLevel.WARNING : LogLevel.INFO);

            AppendLog($"Model      : {(string.IsNullOrWhiteSpace(_deviceModel) ? "Not Read" : _deviceModel)}",
                string.IsNullOrWhiteSpace(_deviceModel) ? LogLevel.WARNING : LogLevel.INFO);

            AppendLog($"Product ID : {(string.IsNullOrWhiteSpace(_deviceProductId) ? "Not Read" : _deviceProductId)}",
                string.IsNullOrWhiteSpace(_deviceProductId) ? LogLevel.WARNING : LogLevel.INFO);

            AppendLog("==============================", LogLevel.INFO);
        }


        // stop all tests function, stop all test before starting a new one.
        private void StopAllTests()
        {
            _currentTest = ActiveTest.None;
            //_deviceStatusReceived = false;
            //_radioStatusReceived = false;

            // ensure clean RX state
            serialport1.DataReceived -= SerialPort_DataReceived;
            serialport1.DiscardInBuffer();
        }

        // append log function with log level colors
        private void AppendLog(string msg, LogLevel level)
        {
            if (rtbLogs.InvokeRequired)
            {
                rtbLogs.Invoke(new Action(() => AppendLog(msg, level)));
                return;
            }

            Color c = level switch
            {
                LogLevel.INFO => Color.FromArgb(19, 154, 14),
                LogLevel.WARNING => Color.Orange,
                LogLevel.ERROR => Color.Red,
                _ => Color.White
            };

            // Move caret to end safely
            rtbLogs.SelectionStart = rtbLogs.TextLength;
            rtbLogs.SelectionLength = 0;   // 🔴 IMPORTANT: prevent overwrite
            rtbLogs.SelectionColor = c;

            rtbLogs.AppendText($"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] {level}: {msg}\n");

            // Reset color to default to prevent color bleed
            rtbLogs.SelectionColor = rtbLogs.ForeColor;

            rtbLogs.ScrollToCaret();
        }


        // Save logs to file button on click
        private void button_saveLogs_Click(object sender, EventArgs e)
        {
            SaveLogsToFile();
        }

        // save logs to file function
        private void SaveLogsToFile()
        {
            try
            {
                string folder = Path.Combine(Application.StartupPath, "Logs");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName = $"{_deviceModel}_{_deviceUid}_{DateTime.Now:ddMMyyyy_HHmmss}.txt";
                string fullPath = Path.Combine(folder, fileName);

                File.WriteAllText(fullPath, rtbLogs.Text);

                AppendLog($"Logs saved: {fullPath}", LogLevel.INFO);
            }
            catch (Exception ex)
            {
                AppendLog("Log save failed: " + ex.Message, LogLevel.ERROR);
            }
        }

        private void btnCustomCommand_Click_1(object sender, EventArgs e)
        {
            if (!EnsurePortOpen()) return;

            using (var dlg = new CustomCommandForm())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(dlg.EnteredCommand))
                    {
                        SendUserCommand(dlg.EnteredCommand, autoChecksum: true);
                    }
                }
            }
        }

        //Custom User Command
        private void SendUserCommand(string rawCmd, bool autoChecksum = true)
        {

            try
            {
                // Block during firmware upgrade
                if (_currentTest == ActiveTest.FirmwareUpgrade)
                {
                    AppendLog("Cannot send custom command during firmware upgrade", LogLevel.WARNING);
                    return;
                }

                StopAllTests();
                _currentTest = ActiveTest.Custom;
                AttachRxHandler();

                string fullCmd = rawCmd.Trim();

                // Auto checksum for #... protocol
                if (autoChecksum && fullCmd.StartsWith("#") && !fullCmd.Contains("*"))
                {
                    fullCmd = BuildCommandWithChecksum(fullCmd);
                }
                else if (!fullCmd.EndsWith("\r\n"))
                {
                    fullCmd += "\r\n";
                }

                serialport1.DiscardInBuffer();
                serialport1.DiscardOutBuffer();
                serialport1.Write(fullCmd);

                AppendLog("USER TX: " + fullCmd.Trim(), LogLevel.INFO);
            }
            catch (Exception ex)
            {
                AppendLog("User command send failed: " + ex.Message, LogLevel.ERROR);
            }

        }

        private string BuildCommandWithChecksum(string body)
        {
            int cs = 0;

            for (int i = 1; i < body.Length; i++)
            {
                cs ^= body[i];
            }

            return $"{body}*{cs:X2}\r\n";
        }
    }
}


