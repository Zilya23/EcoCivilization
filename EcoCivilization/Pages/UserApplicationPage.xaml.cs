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
    /// Логика взаимодействия для UserApplicationPage.xaml
    /// </summary>
    public partial class UserApplicationPage : Page
    {
        public static List<Cora.DataBase.Application> applicationList { get; set; }
        public UserApplicationPage()
        {
            InitializeComponent();
            User user = AuthorizationFunction.AuthorizationUser(Properties.Settings.Default.Login, Properties.Settings.Default.Password);
            applicationList = AplicationFunction.GetAllApplications().Where(x=> x.IDUser == user.ID).ToList();
            cbCity.ItemsSource = CityFunctions.GetCities();
            cbCity.DisplayMemberPath = "Name";
            this.DataContext = this;
        }

        private void btnMainApplication_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainApplicationPage());
        }

        private void btnAddApplication_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddApplicationPage());
        }

        private void btnUserSignUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStatistic_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StatisticPage());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void cbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void lv_applications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv_applications.SelectedItem != null)
            {
                var selectApplication = lv_applications.SelectedItem as Cora.DataBase.Application;
                NavigationService.Navigate(new ViewApplicationPage(selectApplication));
            }
        }

        private void lvPictures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var application = (sender as ListView).DataContext as Cora.DataBase.Application;
            if (application != null)
                NavigationService.Navigate(new ViewApplicationPage(application));
        }

        public void Update()
        {
            List<Cora.DataBase.Application> filterApplication = applicationList;
            if (cbCity.SelectedItem != null)
            {
                var selectCity = cbCity.SelectedItem as City;
                filterApplication = filterApplication.Where(x => x.ID_City == selectCity.ID).ToList();
            }

            if (dpDate.SelectedDate != null)
            {
                var selectedDate = dpDate.SelectedDate;
                filterApplication = filterApplication.Where(x => x.Date == selectedDate).ToList();
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
    }
}
