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
    /// Interaction logic for ExerciseStart.xaml
    /// </summary>
    public partial class ExerciseStart : Page
    {
        public ExerciseStart()
        {
            InitializeComponent();
            Loaded += pageLoaded;
        }

        private void pageLoaded(object sender, RoutedEventArgs e)
        {
            //MainMenu menu = new MainMenu();
            //this.NavigationService.Navigate(menu);
        }
    }
}
