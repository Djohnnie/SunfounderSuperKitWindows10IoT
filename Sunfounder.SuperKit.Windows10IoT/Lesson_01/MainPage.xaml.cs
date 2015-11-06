using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lesson_01
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const Int32 Gpio5 = 5;

        private GpioController _gpio;
        private GpioPin _pin;
        private Boolean _value;

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
            _pin = _gpio.OpenPin(Gpio5);
            _pin.SetDriveMode(GpioPinDriveMode.Output);
        }

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += Tick;
            _timer.Start();
        }

        private void Tick(Object sender, Object e)
        {
            _pin.Write(_value ? GpioPinValue.High : GpioPinValue.Low);
            _value = !_value;
        }
    }
}