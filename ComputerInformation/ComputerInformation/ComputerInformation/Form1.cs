using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Net.NetworkInformation;


namespace ComputerInformation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadCPU();
            loadMotherBoard();
            loadRAM();
            loadStorage();
            loadGraphicCard();
            loadDevices();
            loadSoftware();
            loadNetwork();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {

        }

        private void loadCPU()
        {
            FlowLayoutPanel p = new FlowLayoutPanel();
            p.Size = new Size(300, 800);
            var cpu = new ManagementObjectSearcher("select * from Win32_Processor").Get().Cast<ManagementObject>().First();
            CPUtab.Controls.Add(p);

            Label l1 = new Label();
            Label l2 = new Label();
            Label l3 = new Label();
            Label l4 = new Label();
            Label l5 = new Label();
            Label l6 = new Label();
            Label l7 = new Label();
            Label l8 = new Label();
            Label l15 = new Label();
            Label l16 = new Label();
            Label l17 = new Label();
            Label l18 = new Label();
            Label l19 = new Label();
            Label l20 = new Label();
            Label l21 = new Label();
            Label l22 = new Label();
            Label l23 = new Label();
            Label l24 = new Label();
            Label l25 = new Label();
            Label l26 = new Label();

            l1.Text = " ProcessorId : ";
            l2.Text = (string)cpu["ProcessorId"];
            p.Controls.Add(l1);
            p.Controls.Add(l2);

            l3.Text = " SocketDesignation : ";
            l4.Text = (string)cpu["SocketDesignation"];
            p.Controls.Add(l3);
            p.Controls.Add(l4);

            l5.Text = " Name : ";
            l6.Text = (string)cpu["Name"];
            p.Controls.Add(l5);
            p.Controls.Add(l6);

            l7.Text = " Caption : ";
            l8.Text = (string)cpu["Caption"];
            p.Controls.Add(l7);
            p.Controls.Add(l8);

            l15.Text = " MaxClockSpeed : ";
            uint u = (uint)cpu["MaxClockSpeed"];
            l16.Text = u.ToString();
            p.Controls.Add(l15);
            p.Controls.Add(l16);

            l17.Text = " ExtClock : ";
            u = (uint)cpu["ExtClock"];
            l18.Text = u.ToString();
            p.Controls.Add(l17);
            p.Controls.Add(l18);

            l19.Text = " L2CacheSize : ";
            u = (uint)cpu["L2CacheSize"];
            l20.Text = u.ToString();
            p.Controls.Add(l19);
            p.Controls.Add(l20);

            l21.Text = " L3CacheSize : ";
            u = (uint)cpu["L3CacheSize"];
            l22.Text = u.ToString();
            p.Controls.Add(l21);
            p.Controls.Add(l22);

            l23.Text = " NumberOfCores : ";
            u = (uint)cpu["NumberOfCores"];
            l24.Text = u.ToString();
            p.Controls.Add(l23);
            p.Controls.Add(l24);

            l25.Text = " LogicalProcessors : ";
            u = (uint)cpu["NumberOfLogicalProcessors"];
            l26.Text = u.ToString();
            p.Controls.Add(l25);
            p.Controls.Add(l26);

        }
        private void loadMotherBoard()
        {



            SelectQuery Sq = new SelectQuery("Win32_MotherboardDevice");
            ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(Sq);
            ManagementObjectCollection osDetailsCollection = objOSDetails.Get();
            StringBuilder sb = new StringBuilder();

            foreach (ManagementObject mo in osDetailsCollection)
            {
                sb.AppendLine(string.Format("Caption: {0}", (string)mo["Caption"]));
                sb.AppendLine(string.Format("Availability: {0}", mo["Availability"].ToString()));
                sb.AppendLine(string.Format("InstallDate: {0}", Convert.ToDateTime(mo["InstallDate"]).ToString()));
                sb.AppendLine(string.Format("CreationClassName : {0}", (string)mo["CreationClassName"]));
                sb.AppendLine(string.Format("Description: {0}", (string)mo["Description"]));
                sb.AppendLine(string.Format("DeviceID : {0}", (string)mo["DeviceID"]));
                sb.AppendLine(string.Format("ErrorCleared: {0}", (string)mo["ErrorCleared"]));
                sb.AppendLine(string.Format("ErrorDescription : {0}", (string)mo["ErrorDescription"]));
                sb.AppendLine(string.Format("PrimaryBusType : {0}", (string)mo["PrimaryBusType"]));
                sb.AppendLine(string.Format("RevisionNumber : {0}", (string)mo["RevisionNumber"]));
                sb.AppendLine(string.Format("LastErrorCode : {0}", (string)mo["LastErrorCode"]));
                sb.AppendLine(string.Format("Name : {0}", (string)mo["Name"]));
                sb.AppendLine(string.Format("SecondaryBusType : {0}", (string)mo["SecondaryBusType"]));
                sb.AppendLine(string.Format("PNPDeviceID: {0}", (string)mo["PNPDeviceID"]));
                sb.AppendLine(string.Format("PowerManagementSupported : {0}", mo["PowerManagementSupported"]).ToString());
                sb.AppendLine(string.Format("Status : {0}", (string)mo["Status"]));
                sb.AppendLine(string.Format("SystemCreationClassName : {0}", (string)mo["SystemCreationClassName"]));
                sb.AppendLine(string.Format("SystemName: {0}", (string)mo["SystemName"]));
            }

            string mbInfo = String.Empty;
            ManagementScope scope = new ManagementScope("\\\\" + Environment.MachineName + "\\root\\cimv2");
            scope.Connect();
            ManagementObject wmiClass = new ManagementObject(scope, new ManagementPath("Win32_BaseBoard.Tag=\"Base Board\""), new ObjectGetOptions());

            sb.AppendLine("----------------------");
            sb.AppendLine("More Random Information : ");

            foreach (PropertyData propData in wmiClass.Properties)
            {
                if (propData.Name == "SerialNumber")
                    mbInfo = String.Format("{0,-25}{1}", propData.Name, Convert.ToString(propData.Value));

                if (propData.Value != null)
                {
                    sb.AppendLine(propData.Value.ToString());
                }
            }
            richTextBox1.Text = sb.ToString();
        }

        private void loadRAM()
        {
            StringBuilder sb = new StringBuilder();

            ManagementObjectSearcher Search = new ManagementObjectSearcher("Select * From Win32_ComputerSystem");

            foreach (ManagementObject Mobject in Search.Get())
            {

                double Ram_Bytes = (Convert.ToDouble(Mobject["TotalPhysicalMemory"]));
                sb.AppendLine(string.Format("RAM Size in Bytes: {0}", Ram_Bytes));
                sb.AppendLine(string.Format("RAM Size in Kilo Bytes: {0}", Ram_Bytes / 1024));
                sb.AppendLine(string.Format("RAM Size in Mega Bytes: {0}", Ram_Bytes / 1048576));
                sb.AppendLine(string.Format("RAM Size in Giga Bytes: {0}", Ram_Bytes / 1073741824));
            }

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_OperatingSystem");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    double free = Double.Parse(queryObj["FreePhysicalMemory"].ToString());
                    double total = Double.Parse(queryObj["TotalVisibleMemorySize"].ToString());
                    sb.AppendLine(string.Format("Percentage used: {0}%", Math.Round(((total - free) / total * 100), 2)));
                    Console.Read();
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine(value: e.Message);
            }

            richTextBox2.Text = sb.ToString();

        }

        private void loadStorage()
        {
            StringBuilder sb = new StringBuilder();

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                sb.AppendLine(string.Format("Drive {0}", d.Name));
                sb.AppendLine(string.Format("  Drive type: {0}", d.DriveType));
                if (d.IsReady == true)
                {
                    sb.AppendLine(string.Format("  Volume label: {0}", d.VolumeLabel));
                    sb.AppendLine(string.Format("  File system: {0}", d.DriveFormat));
                    sb.AppendLine(string.Format(
                        "  Available space to current user:{0, 15} bytes",
                        d.AvailableFreeSpace));

                    sb.AppendLine(string.Format(
                        "  Total available space:          {0, 15} bytes",
                        d.TotalFreeSpace));

                    sb.AppendLine(string.Format(
                        "  Total size of drive:            {0, 15} bytes ",
                        d.TotalSize));
                }
            }

            richTextBox3.Text = sb.ToString();

        }
        private void loadGraphicCard()
        {

            StringBuilder sb = new StringBuilder();
            ManagementObjectSearcher objvide = new ManagementObjectSearcher("select * from Win32_VideoController");

            foreach (ManagementObject obj in objvide.Get())
            {
                sb.AppendLine(string.Format("Name  -  " + obj["Name"]));
                sb.AppendLine(string.Format("DeviceID  -  " + obj["DeviceID"]));
                sb.AppendLine(string.Format("AdapterRAM  -  " + obj["AdapterRAM"]));
                sb.AppendLine(string.Format("AdapterDACType  -  " + obj["AdapterDACType"]));
                sb.AppendLine(string.Format("Monochrome  -  " + obj["Monochrome"]));
                sb.AppendLine(string.Format("InstalledDisplayDrivers  -  " + obj["InstalledDisplayDrivers"]));
                sb.AppendLine(string.Format("DriverVersion  -  " + obj["DriverVersion"]));
                sb.AppendLine(string.Format("VideoProcessor  -  " + obj["VideoProcessor"]));
                sb.AppendLine(string.Format("VideoArchitecture  -  " + obj["VideoArchitecture"]));
                sb.AppendLine(string.Format("VideoMemoryType  -  " + obj["VideoMemoryType"]));
            }
            richTextBox4.Text = sb.ToString();
        }


        private void loadSoftware()
        {

            var wmi =
    new ManagementObjectSearcher("select * from Win32_OperatingSystem").Get().Cast<ManagementObject>().First();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format((string)wmi["Caption"]).Trim());

            sb.AppendLine("Version ");
            sb.AppendLine(string.Format((string)wmi["Version"]));
            uint i = (uint)wmi["MaxNumberOfProcesses"];
            ulong l = (ulong)wmi["MaxProcessMemorySize"];
            sb.AppendLine("MaxNumberOfProcesses ");
            sb.AppendLine(i.ToString());
            sb.AppendLine("MaxProcessMemorySize ");
            sb.AppendLine(l.ToString());
            sb.AppendLine("OSArchitecture ");
            sb.AppendLine(string.Format((string)wmi["OSArchitecture"]));
            sb.AppendLine("SerialNumber ");
            sb.AppendLine(string.Format((string)wmi["SerialNumber"]));
            sb.AppendLine("BuildNumber ");
            sb.AppendLine(string.Format(((string)wmi["BuildNumber"])));

            richTextBox6.Text = sb.ToString();

        }

        private void loadDevices()
        {

            StringBuilder sb = new StringBuilder();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPSignedDriver");

            sb.AppendLine("Installed Devices:");
            sb.AppendLine(" ");
            foreach (ManagementObject obj in searcher.Get())
            {
                //Device name 
                try
                {
                    string s = $"{obj.GetPropertyValue("DeviceName")?.ToString() ?? "Unknown"}";
                    if (s != null)
                    {
                        sb.AppendLine(s);
                    }
                }
                catch  { }
            }
            richTextBox5.Text = sb.ToString();
        }

        private void loadNetwork()
        {
            StringBuilder sb = new StringBuilder();
            
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            sb.AppendLine(string.Format("Interface information for {0}.{1}     ",
                    computerProperties.HostName, computerProperties.DomainName));
            if (nics == null || nics.Length < 1)
            {
                sb.AppendLine("  No network interfaces found.");
                return;
            }

            sb.AppendLine(string.Format("  Number of interfaces .................... : {0}", nics.Length));
            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sb.AppendLine();
                sb.AppendLine(adapter.Description);
                sb.AppendLine(String.Empty.PadLeft(adapter.Description.Length, '='));
                sb.AppendLine(string.Format("  Interface type .......................... : {0}", adapter.NetworkInterfaceType));
                sb.AppendLine(string.Format("  Physical Address ........................ : {0}",
                           adapter.GetPhysicalAddress().ToString()));
                sb.AppendLine(string.Format("  Operational status ...................... : {0}",
                    adapter.OperationalStatus));
                string versions = "";

                // Create a display string for the supported IP versions. 
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    versions = "IPv4";
                }
                if (adapter.Supports(NetworkInterfaceComponent.IPv6))
                {
                    if (versions.Length > 0)
                    {
                        versions += " ";
                    }
                    versions += "IPv6";
                }
                sb.AppendLine(string.Format("  IP version .............................. : {0}", versions));
                sb.AppendLine(string.Format(properties.ToString()));

                // The following information is not useful for loopback adapters. 
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                {
                    continue;
                }
                sb.AppendLine(string.Format("  DNS suffix .............................. : {0}",
                    properties.DnsSuffix));

                string label;
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    IPv4InterfaceProperties ipv4 = properties.GetIPv4Properties();
                    sb.AppendLine(string.Format("  MTU...................................... : {0}", ipv4.Mtu));
                    if (ipv4.UsesWins)
                    {

                        IPAddressCollection winsServers = properties.WinsServersAddresses;
                        if (winsServers.Count > 0)
                        {
                            label = "  WINS Servers ............................ :";
                            sb.AppendLine(string.Format(label, winsServers));
                        }
                    }
                }

                sb.AppendLine(string.Format("  DNS enabled ............................. : {0}",
                    properties.IsDnsEnabled));
                sb.AppendLine(string.Format("  Dynamically configured DNS .............. : {0}",
                    properties.IsDynamicDnsEnabled));
                sb.AppendLine(string.Format("  Receive Only ............................ : {0}",
                    adapter.IsReceiveOnly));
                sb.AppendLine(string.Format("  Multicast ............................... : {0}",
                    adapter.SupportsMulticast));

                sb.AppendLine();

                richTextBox7.Text = sb.ToString();
            }
        }
    }
}


    
