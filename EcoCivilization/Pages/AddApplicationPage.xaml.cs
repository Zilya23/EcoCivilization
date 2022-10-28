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
using Cora.DataBase;
using Cora.Functionss;
using Application = Cora.DataBase.Application;

namespace EcoCivilization.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddApplicationPage.xaml
    /// </summary>
    public partial class AddApplicationPage : Page
    {
        public static User user { get; set; }
        public AddApplicationPage()
        {
            InitializeComponent();
            user = AuthorizationFunction.AuthorizationUser(Properties.Settings.Default.Login, Properties.Settings.Default.Password);
            List<City> cities = CityFunctions.GetCities();
            cbCity.ItemsSource = cities;
            cbCity.DisplayMemberPath = "Name";
        }

        private void btnMainApplication_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainApplicationPage());
        }

        private void btnStatistic_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StatisticPage());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Application application = new Application();
                TimeSpan time = TimeSpan.Parse(tpTimeStart.Text);
                application.TimeStart = time;
                application.Date = dpStartDate.SelectedDate;
                application.Name = tbName.Text.Trim();
                application.Description = tbDescription.Text.Trim();
                application.Count_User = int.Parse(tbYears.Text);
                application.Place = tbPlace.Text.Trim();
                application.ID_City = (cbCity.SelectedItem as City).ID;
                application.IDUser = user.ID;
                bd_connection.connection.Application.Add(application);
                bd_connection.connection.SaveChanges();
                MessageBox.Show("Успешно создано");
                NavigationService.Navigate(new MainApplicationPage());
            }
            catch
            {
                MessageBox.Show("Заполните все обязательные поля");
            }
            
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            int countUser = Convert.ToInt32(tbYears.Text);
            if (countUser != 2)
            {
                tbYears.Text = (countUser - 1).ToString();
            }
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            int countUser = Convert.ToInt32(tbYears.Text);
            if (countUser != 200)
            {
                tbYears.Text = (countUser + 1).ToString();
            }
        }
    }
}
