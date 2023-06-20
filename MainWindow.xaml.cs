using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Bluetooth_connector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BluetoothDeviceInfo[] devices;

        private BluetoothClient client;
        public MainWindow()
        {
            InitializeComponent();
            if (BluetoothRadio.IsSupported)
            {
                client = new BluetoothClient();
                devices = new BluetoothDeviceInfo[0];
            }
            else
            {
                MessageBox.Show("Not Supported");
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("searching for available devices...");
            devices = client.DiscoverDevicesInRange();
            if (devices.Length == 0)
            {
                MessageBox.Show("no devices found");
            }
            foreach (BluetoothDeviceInfo d in devices)
            {
                devicesComboBox.Items.Add(d.DeviceName);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(devicesComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("select  device first");
                return;
            }
            else if(devices.Length == 0)
            {
                MessageBox.Show("search for devices first");
                return;
            }
            try
            {
                client.Connect(devices[devicesComboBox.SelectedIndex].DeviceAddress, BluetoothService.PhonebookAccess);
                MessageBox.Show("Device connected successfully");
            }
            catch (Exception) { }
        }
    }
}
