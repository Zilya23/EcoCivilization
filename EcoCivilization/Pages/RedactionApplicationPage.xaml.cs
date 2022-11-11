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
    /// Логика взаимодействия для RedactionApplicationPage.xaml
    /// </summary>
    public partial class RedactionApplicationPage : Page
    {
        public static Application applications { get; set; }
        public RedactionApplicationPage(Application application)
        {
            InitializeComponent();
            applications = application;
            List<City> cities = CityFunctions.GetCities();
            cbCity.ItemsSource = cities;
            cbCity.DisplayMemberPath = "Name";
            cbCity.SelectedItem = application.City;
            tpTimeStart.SelectedTime = DateTime.Now.Date + application.TimeStart;
            DataContext = this;
        }

        private void btnMainApplication_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddApplication_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStatistic_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            int countUser = Convert.ToInt32(tbCountUser.Text);
            if (countUser != 2)
            {
                tbCountUser.Text = (countUser - 1).ToString();
            }
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            int countUser = Convert.ToInt32(tbCountUser.Text);
            if (countUser != 200)
            {
                tbCountUser.Text = (countUser + 1).ToString();
            }
        }

        private void btnDelPhoto_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
