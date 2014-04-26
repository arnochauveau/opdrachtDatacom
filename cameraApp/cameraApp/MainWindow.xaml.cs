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

namespace cameraApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer tmr = new DispatcherTimer();
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
            tmr.Interval = TimeSpan.FromMilliseconds(1000);
            tmr.Tick += tmr_Tick;
            tmr.Start();
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

            //using (BinaryReader reader = new BinaryReader(resp.GetResponseStream()))
            //{
            //    Byte[] arr = reader.ReadBytes(704 * 576 * 24);
            //    using (MemoryStream ms = new MemoryStream(arr))
            //    {


            //    }

            //}

            Stream receiveStream = resp.GetResponseStream();
            //Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            // Pipes the stream to a higher level stream reader with the required encoding format. 

            //System.Drawing.Image.FromStream(receiveStream);
            BitmapImage im = new BitmapImage();
            im.BeginInit();
            im.StreamSource = receiveStream;
            im.EndInit();

            img.Source = im;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tmr.Stop();
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://172.23.49.1/axis-cgi/com/ptz.cgi?rpan=10");
            req.Credentials = new NetworkCredential("student", "nmct");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            tmr.Start();
        }
  
    }
}
