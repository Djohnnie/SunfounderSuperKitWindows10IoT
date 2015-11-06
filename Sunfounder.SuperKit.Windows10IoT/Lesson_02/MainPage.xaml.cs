using System;
using Windows.Devices.Gpio;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lesson_02
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const Int32 Gpio5 = 5;
        private const Int32 Gpio6 = 6;

        private GpioController _gpio;
        private GpioPin _ledPin;
        private GpioPin _buttonPin;

        public MainPage()
        {
            InitializeComponent();
            InitializeGpio();
        }

        private void InitializeGpio()
        {
            _gpio = GpioController.GetDefault();
            _ledPin = _gpio.OpenPin(Gpio5);
            _ledPin.SetDriveMode(GpioPinDriveMode.Output);
            _buttonPin = _gpio.OpenPin(Gpio6);
            _buttonPin.SetDriveMode(GpioPinDriveMode.InputPullUp);
            _buttonPin.DebounceTimeout = TimeSpan.FromMilliseconds(50);
            _buttonPin.ValueChanged += buttonPin_ValueChanged;
        }

        private void buttonPin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            _ledPin.Write(args.Edge == GpioPinEdge.FallingEdge ? GpioPinValue.Low : GpioPinValue.High);
        }
    }
}