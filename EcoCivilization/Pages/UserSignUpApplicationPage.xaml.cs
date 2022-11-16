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
using Cora.Functionss;
using Cora.DataBase;

namespace EcoCivilization.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserSignUpApplicationPage.xaml
    /// </summary>
    public partial class UserSignUpApplicationPage : Page
    {
        public static List<Application_User> applicationList { get; set; }
        public static User user { get; set; }
        public UserSignUpApplicationPage()
        {
            InitializeComponent();
            user = AuthorizationFunction.AuthorizationUser(Properties.Settings.Default.Login, Properties.Settings.Default.Password);
            applicationList = bd_connection.connection.Application_User.Where(x => x.ID_User == user.ID).ToList();
            cbCity.ItemsSource = CityFunctions.GetCities();
            cbCity.DisplayMemberPath = "Name";
            DataContext = this;
        }

        private void lv_applications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lv_applications.SelectedItem != null)
            {
                var selectApplication = (lv_applications.SelectedItem as Application_User).Application;
                NavigationService.Navigate(new ViewApplicationPage(selectApplication));
            }
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void cbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }


        private void btnUserApplication_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserApplicationPage());
        }

        private void btnAddApplication_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddApplicationPage());
        }

        private void btnMainApplication_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainApplicationPage());
        }
        public void Update()
        {
            List<Application_User> filterApplication = applicationList;
            if (cbCity.SelectedItem != null)
            {
                var selectCity = cbCity.SelectedItem as City;
                filterApplication = filterApplication.Where(x => x.Application.ID_City == selectCity.ID).ToList();
            }

            if (dpDate.SelectedDate != null)
            {
                var selectedDate = dpDate.SelectedDate;
                filterApplication = filterApplication.Where(x => x.Application.Date == selectedDate).ToList();
            }

            if (filterApplication.Count == 0)
            {
                tbEmpty.Visibility = Visibility.Visible;
            }
            else
            {
                tbEmpty.Visibility = Visibility.Hidden;
            }

            lv_applications.ItemsSource = filterApplication;
        }

        private void lvPictures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var application = (sender as ListView).DataContext as Application_User;
            if (application != null)
                NavigationService.Navigate(new ViewApplicationPage(application.Application));
        }
    }
}
