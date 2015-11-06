using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lesson_03
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Int32[] _gpios = {5, 6, 13, 4, 26, 12, 25, 16};

        private GpioController _gpio;
        private GpioPin[] _ledPins;

        private DispatcherTimer _timer;

        public MainPage()
        {
            InitializeComponent();
            InitializeGpio();
            InitializeTimer();
        }

        private void InitializeGpio()
        {
            _gpio = GpioController.GetDefault();
            _ledPins = new GpioPin[8];

            for (int i = 0; i < 8; i++)
            {
                _ledPins[i] = _gpio.OpenPin(_gpios[i]);
                _ledPins[i].SetDriveMode(GpioPinDriveMode.Output);
                _ledPins[i].Write(GpioPinValue.High);
            }
        }

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(2000);
            _timer.Tick += Tick;
            _timer.Start();
        }

        private async void Tick(Object sender, Object e)
        {
            for (int i = 0; i < 8; i++)
            {
                _ledPins[i].Write(GpioPinValue.Low);
                await Task.Delay(100);
            }
            for (int i = 7; i > -1; i--)
            {
                _ledPins[i].Write(GpioPinValue.High);
                await Task.Delay(100);
            }
        }
    }
}