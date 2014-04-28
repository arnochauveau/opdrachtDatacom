using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Drawing;
using System.Windows.Threading;
using System.Globalization;

namespace cameraApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer tmr = new DispatcherTimer();
        private DispatcherTimer tmrInactivity = new DispatcherTimer();
        private Double[] arrpos = new Double[4];
        private bool isBussy = false;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            initTimer();
        }

        private void initTimer()
        {
            tmr.Interval = TimeSpan.FromMilliseconds(5);
            tmr.Tick += tmr_Tick;
            tmr.Start();

            tmrInactivity.Interval = TimeSpan.FromMinutes(5);
            tmrInactivity.Tick += tmrInactivity_Tick;
            tmrInactivity.Start();
        }

        void tmrInactivity_Tick(object sender, EventArgs e)
        {
            if (isBussy)
            {
                //start auto watch
            }
            else
            {
                //stop auto watch
            }
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            updateImg();
        }

        private void updateImg()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://172.23.49.1/axis-cgi/jpg/image.cgi");
            req.Credentials = new NetworkCredential("student", "nmct");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            Stream receiveStream = resp.GetResponseStream();

            BitmapImage im = new BitmapImage();
            im.BeginInit();
            im.StreamSource = receiveStream;
            im.EndInit();

            img.Source = im;

            HttpWebRequest req2 = (HttpWebRequest)HttpWebRequest.Create("http://172.23.49.1/axis-cgi/com/ptz.cgi?query=position");

            req2.Credentials = new NetworkCredential("student", "nmct");

            req2.GetResponse();
            HttpWebResponse resp2 = (HttpWebResponse)req2.GetResponse();
            Stream r = resp2.GetResponseStream();
            StreamReader readStream = new StreamReader(r);

            for (int i = 0; i < 4; i++)
            {
                String[] arrTest = readStream.ReadLine().Split('=');

                //arrpos[i] = Convert.ToDouble(readStream.ReadLine().Split('=')[1]);
                CultureInfo ciClone = null;
                double value = 0;
                try
                {
                    ciClone = (CultureInfo)CultureInfo.InvariantCulture.Clone();
                    ciClone.NumberFormat.NumberDecimalSeparator = ",";
                    ciClone.NumberFormat.NumberGroupSeparator = ".";
                    value = Double.Parse(arrTest[1], ciClone);
                    Console.WriteLine("Waarde van " + arrTest[0] + " is: " + value);
                    arrpos[i] = value;
                }
                catch (Exception e)
                {
                    Console.WriteLine("foutje alweer");
                }

            }
            pan.Value = arrpos[0];
            tilt.Value = arrpos[1];
            zoom1.Value = arrpos[2];
            focus.Value = arrpos[3];

            readStream.Close();
            r.Close();
            req2.Abort();
            resp2.Dispose();
        }

        private void autoWatch()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://172.23.49.1/axis-cgi/com/ptz.cgi?ContinuousPan=true");
            req.Credentials = new NetworkCredential("student", "nmct");
            req.GetResponse();
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            req.Abort();
            resp.Dispose();


        }

        #region Commands

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://172.23.49.1/axis-cgi/com/ptz.cgi?move=right");
            req.Credentials = new NetworkCredential("student", "nmct");
            req.GetResponse();
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            req.Abort();
            resp.Dispose();
            isBussy = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://172.23.49.1/axis-cgi/com/ptz.cgi?move=left");
            req.Credentials = new NetworkCredential("student", "nmct");
            req.GetResponse();
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            req.Abort();
            resp.Dispose();
            isBussy = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://172.23.49.1/axis-cgi/com/ptz.cgi?move=up");
            req.Credentials = new NetworkCredential("student", "nmct");
            req.GetResponse();
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            req.Abort();
            resp.Dispose();
            isBussy = true;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://172.23.49.1/axis-cgi/com/ptz.cgi?move=down");
            req.Credentials = new NetworkCredential("student", "nmct");
            req.GetResponse();
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            req.Abort();
            resp.Dispose();
            isBussy = true;
        }

        private void tilt_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://172.23.49.1/axis-cgi/com/ptz.cgi?tilt=" + tilt.Value);
            req.Credentials = new NetworkCredential("student", "nmct");
            req.GetResponse();
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            req.Abort();
            resp.Dispose();
            isBussy = true;
        }

        private void pan_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://172.23.49.1/axis-cgi/com/ptz.cgi?pan=" + pan.Value);
            req.Credentials = new NetworkCredential("student", "nmct");
            req.GetResponse();
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            req.Abort();
            resp.Dispose();
            isBussy = true;
        }

        private void zoom1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://172.23.49.1/axis-cgi/com/ptz.cgi?zoom=" + zoom1.Value);
            req.Credentials = new NetworkCredential("student", "nmct");
            req.GetResponse();
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            req.Abort();
            resp.Dispose();
            isBussy = true;
        }

        private void focus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://172.23.49.1/axis-cgi/com/ptz.cgi?focus=" + focus.Value);
            req.Credentials = new NetworkCredential("student", "nmct");
            req.GetResponse();
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            req.Abort();
            resp.Dispose();
            isBussy = true;
        }

        private void chkAutoFocus_Checked(object sender, RoutedEventArgs e)
        {

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://172.23.49.1/axis-cgi/com/ptz.cgi?autofocus=on");
            req.Credentials = new NetworkCredential("student", "nmct");
            req.GetResponse();
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            req.Abort();
            resp.Dispose();
            focus.IsEnabled = false;
            isBussy = true;
        }

        private void chkAutoFocus_Unchecked(object sender, RoutedEventArgs e)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://172.23.49.1/axis-cgi/com/ptz.cgi?autofocus=of");
            req.Credentials = new NetworkCredential("student", "nmct");
            req.GetResponse();
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            req.Abort();
            resp.Dispose();
            focus.IsEnabled = true;
            isBussy = true;
        }

        #endregion

    }
}
