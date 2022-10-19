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
using Cora;
using Cora.DataBase;

namespace EcoCivilization.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainApplicationPage.xaml
    /// </summary>
    public partial class MainApplicationPage : Page
    {
        public static List<Cora.DataBase.Application> applicationList {get; set;}
        public MainApplicationPage()
        {
            InitializeComponent();
            applicationList = ApplicationFunction.GetAllApplications();

            User user = Authorization.AuthorizationUser(Properties.Settings.Default.Login, Properties.Settings.Default.Password);
            this.DataContext = this;
        }

        private void lv_applications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lv_applications.SelectedItem != null)
            {
                NavigationService.Navigate(new ViewApplicationPage());
            }
        }

        private void btnAddApplication_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddApplicationPage());
        }

        private void btnStatistic_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StatisticPage());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
