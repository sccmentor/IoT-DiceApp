using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
// DiceApp by Paul Winstanley 20.10.15

namespace DiceApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int ONELED_PIN = 26;
        private const int TWOLED_PIN = 13;
        private const int THREELED_PIN = 6;
        private const int FOURLED_PIN = 5;
        private const int FIVELED_PIN = 22;
        private const int SIXLED_PIN = 27;
        private GpioPin onepin;
        private GpioPin twopin;
        private GpioPin threepin;
        private GpioPin fourpin;
        private GpioPin fivepin;
        private GpioPin sixpin;
        private int LEDResult = 0;
        private DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();
            Unloaded += MainPage_Unloaded;
            InitGPIO();
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                onepin = null;
                twopin = null;
                threepin = null;
                fourpin = null;
                fivepin = null;
                sixpin = null;
                GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }
            onepin = gpio.OpenPin(ONELED_PIN);
            twopin = gpio.OpenPin(TWOLED_PIN);
            threepin = gpio.OpenPin(THREELED_PIN);
            fourpin = gpio.OpenPin(FOURLED_PIN);
            fivepin = gpio.OpenPin(FIVELED_PIN);
            sixpin = gpio.OpenPin(SIXLED_PIN);

            //Turn off all LEDs and set to GPIO pins to output
            onepin.Write(GpioPinValue.Low);
            onepin.SetDriveMode(GpioPinDriveMode.Output);
            twopin.Write(GpioPinValue.Low);
            twopin.SetDriveMode(GpioPinDriveMode.Output);
            threepin.Write(GpioPinValue.Low);
            threepin.SetDriveMode(GpioPinDriveMode.Output);
            fourpin.Write(GpioPinValue.Low);
            fourpin.SetDriveMode(GpioPinDriveMode.Output);
            fivepin.Write(GpioPinValue.Low);
            fivepin.SetDriveMode(GpioPinDriveMode.Output);
            sixpin.Write(GpioPinValue.Low);
            sixpin.SetDriveMode(GpioPinDriveMode.Output);
        }

        private void MainPage_Unloaded(object sender, object args)
        {
            // Cleanup
            onepin.Dispose();
            twopin.Dispose();
            threepin.Dispose();
            fourpin.Dispose();
            fivepin.Dispose();
            sixpin.Dispose();
        }

        private void rollMeButton_Click(object sender, RoutedEventArgs e)
        {
            // Turn off all LEDs between throws
            onepin.Write(GpioPinValue.Low);
            twopin.Write(GpioPinValue.Low);
            threepin.Write(GpioPinValue.Low);
            fourpin.Write(GpioPinValue.Low);
            fivepin.Write(GpioPinValue.Low);
            sixpin.Write(GpioPinValue.Low);

            //Generate a random number for the roll of the dice
            Random random = new Random();
            int randomNumber = random.Next(1, 7);
            LEDResult = randomNumber;
            string randomResult = randomNumber.ToString();
            textBlock.Text = randomResult;

            //Start timer for 1 second to keep LEDs turned off
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            timer.Stop();

            //Output the dice roll to the LEDs
            if (LEDResult == 1)
            {
                // Turn on 1 LED
               // textBlock.Text = randomResult;
                onepin.Write(GpioPinValue.High);

            }
            else if
                (LEDResult == 2)
            {
                // Turn on 2 LEDs

                onepin.Write(GpioPinValue.High);
                twopin.Write(GpioPinValue.High);

            }
            else if
                (LEDResult == 3)
            {
                // Turn on 3 LEDs

                onepin.Write(GpioPinValue.High);
                twopin.Write(GpioPinValue.High);
                threepin.Write(GpioPinValue.High);

            }
            else if
                (LEDResult == 4)
            {
                // Turn on 4 LEDs

                onepin.Write(GpioPinValue.High);
                twopin.Write(GpioPinValue.High);
                threepin.Write(GpioPinValue.High);
                fourpin.Write(GpioPinValue.High);

            }
            else if
               (LEDResult == 5)
            {
                // Turn on 5 LEDs

                onepin.Write(GpioPinValue.High);
                twopin.Write(GpioPinValue.High);
                threepin.Write(GpioPinValue.High);
                fourpin.Write(GpioPinValue.High);
                fivepin.Write(GpioPinValue.High);

            }
            else if
             (LEDResult == 6)
            {
                // Turn on 6 LEDs

                onepin.Write(GpioPinValue.High);
                twopin.Write(GpioPinValue.High);
                threepin.Write(GpioPinValue.High);
                fourpin.Write(GpioPinValue.High);
                fivepin.Write(GpioPinValue.High);
                sixpin.Write(GpioPinValue.High);
            }

        }

        private void resultTextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
