using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace SH.IOTLIb
{
    public class HCSR04:IDisposable
    {
        private int sensorTrig;
        private int sensorEcho;
        private GpioPin pinTrig;
        private GpioPin pinEcho;
        Stopwatch time = new Stopwatch();
        private int i = 1000;
        private bool isWork = true;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="trig">Trig Pin</param>
        /// <param name="echo">Echo Pin</param>
        public HCSR04(int trig, int echo)
        {
            sensorTrig = trig;
            sensorEcho = echo;
        }

        /// <summary
        /// Initialize the sensor
        /// </summary>
        public async Task Initialize()
        {
            GpioController gpio = await GpioController.GetDefaultAsync();
            //  GpioController gpio = GpioController.GetDefault();
            if (gpio == null)
            {
                return;
            }
            pinTrig = gpio.OpenPin(sensorTrig);
            pinEcho = gpio.OpenPin(sensorEcho);
            pinTrig.SetDriveMode(GpioPinDriveMode.Output);
            pinEcho.SetDriveMode(GpioPinDriveMode.Input);
            pinTrig.Write(GpioPinValue.Low);
        }
        /// <summary>
        /// Read data from the sensor
        /// </summary>
        /// <returns>A double type distance data</returns>
        public async Task<double> ReadAsync()
        {
            double result;
            isWork = true;
            pinTrig.Write(GpioPinValue.High);
            await Task.Delay(10);
            pinTrig.Write(GpioPinValue.Low);
            while (pinEcho.Read() == GpioPinValue.Low)
            {
                i--;
                if (i<0)
                {
                    i = 1000;
                    isWork = false;
                    break;
                }
            }
            if (!isWork)
            {
                return -0;
            }
            time.Restart();
            while (pinEcho.Read() == GpioPinValue.High)
            {
                i--;
                if (i < 0)
                {
                    i = 1000;
                    isWork = false;
                    break;
                }
            }
            time.Stop();
            if (!isWork)
            {
                return -0;
            }
            result = (time.Elapsed.TotalSeconds * 34000) / 2;
            return result;
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            pinTrig.Dispose();
            pinEcho.Dispose();
        }

    }
}
