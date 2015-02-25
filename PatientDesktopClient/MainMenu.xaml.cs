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

using FluidKit.Controls;
using Microsoft.Kinect.Wpf.Controls;
using Microsoft.Kinect;

namespace PatientDesktopClient
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {

        private StringCollection dataSource;
        private int currentIndex;

        public MainMenu()
        {
            InitializeComponent();
            exerciseSelect = new ElementFlow();

            KinectRegion.SetKinectRegion(this, kinectRegion);
            this.kinectRegion.KinectSensor = KinectSensor.GetDefault();

            Loaded += pageLoaded;
        }

        private void pageLoaded(object sender, RoutedEventArgs e)
        {
            dataSource = FindResource("DataSource") as StringCollection;

            /* TODO put CC BY somewhere in app!
             * <div>Icons made by <a href="http://www.freepik.com" title="Freepik">Freepik</a> from <a href="http://www.flaticon.com" title="Flaticon">www.flaticon.com</a>             is licensed by <a href="http://creativecommons.org/licenses/by/3.0/" title="Creative Commons BY 3.0">CC BY 3.0</a></div>
             */ 
            dataSource.Insert(0, "res/images/01.png");
            dataSource.Insert(1, "res/images/02.png");
            dataSource.Insert(2, "res/images/03.png");
            dataSource.Insert(3, "res/images/03.png");
            dataSource.Insert(4, "res/images/03.png");
            dataSource.Insert(5, "res/images/03.png");
            dataSource.Insert(6, "res/images/03.png");
            dataSource.Insert(7, "res/images/03.png");
            dataSource.Insert(8, "res/images/03.png");

            currentIndex = 0;

            exerciseSelect.Focus();
            exerciseSelect.Click += elementFlowClicked;
            exerciseSelect.SelectionChanged += elementFlowSelectionChanged;
        }

        private void exitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void elementFlowClicked(object sender, RoutedEventArgs e)
        {
            if (exerciseSelect.SelectedIndex == 1)
            {
                NavigationService.Navigate(new ExerciseStart());
            }
        }

        private void elementFlowSelectionChanged(object sender, RoutedEventArgs e)
        {
            // update label containing exercise name
        }
    }
}
