using Microsoft.Kinect;
using Microsoft.Kinect.Wpf.Controls;
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

namespace PatientDesktopClient
{
    /// <summary>
    /// Interaction logic for Calibration.xaml
    /// </summary>
    public partial class Calibration : Page
    {

        private static readonly float Z_DISTANCE = 2;

        public Calibration()
        {
            InitializeComponent();

            KinectRegion.SetKinectRegion(this, kinectRegion);
            this.kinectRegion.KinectSensor = KinectSensor.GetDefault();
            
            // TODO find way to resize kinect user view

            Loaded += pageLoaded;
        }

        private void pageLoaded(object sender, RoutedEventArgs e)
        {
            BodyDataReader.Instance.BodyDataRead += bodyDataRead;
        }

        private void bodyDataRead(object source, BodyDataReadEventArgs e)
        {
            distance.Content = "Distance: " + (int) e.Joints[JointType.SpineBase].Position.Z;
            bodies.Content = "Bodies Detected: " + BodyDataReader.Instance.BodyCount;

            if (BodyDataReader.Instance.BodyCount != 1)
                return;

            foreach (KeyValuePair<JointType, Joint> j in e.Joints)
                if (j.Value.TrackingState == TrackingState.NotTracked)
                    return;

            if (((int) e.Joints[JointType.SpineBase].Position.Z) != Z_DISTANCE)
                return;

            // calibration seems to be done
            BodyDataReader.Instance.BodyDataRead -= bodyDataRead;
            this.NavigationService.Navigate(new MainMenu());
        }
    }
}
