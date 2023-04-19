using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
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
            NavigationService.Navigate(new MainApplicationPage());
            bd_connection.connection = new EcoCivilizationEntities1();
        }

        private void btnAddApplication_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddApplicationPage());
            bd_connection.connection = new EcoCivilizationEntities1();
        }

        //private void btnStatistic_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationFunction());
            bd_connection.connection = new EcoCivilizationEntities1();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (applications.PhotoApplication.Count >= 1)
            {
                try
                {
                    TimeSpan time = TimeSpan.Parse(tpTimeStart.Text);
                    applications.TimeStart = time;
                    applications.Date = dpStartDate.SelectedDate;
                    applications.Name = tbName.Text.Trim();
                    applications.Description = tbDescription.Text.Trim();
                    applications.Count_User = int.Parse(tbCountUser.Text);
                    applications.Place = tbPlace.Text.Trim();
                    applications.ID_City = (cbCity.SelectedItem as City).ID;
                    bd_connection.connection.SaveChanges();
                    MessageBox.Show("Успешно изменено");
                    NavigationService.Navigate(new MainApplicationPage());
                }
                catch
                {
                    MessageBox.Show("Заполните все обязательные поля");
                }
            }
            else
            {
                MessageBox.Show("Добавьте фото");
            }
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
            if (lvPhoto.SelectedItem != null)
            {
                var selecPhoto = lvPhoto.SelectedItem as PhotoApplication;
                bd_connection.connection.PhotoApplication.Remove(selecPhoto);
                lvPhoto.ItemsSource = applications.PhotoApplication;
                lvPhoto.Items.Refresh();
            }
        }

        private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (applications.PhotoApplication.Count <= 4)
            {
                OpenFileDialog openFile = new OpenFileDialog()
                {
                    Filter = "*.jpg|*.jpg|*.png|*.png"
                };
                if (openFile.ShowDialog().GetValueOrDefault())
                {
                    PhotoApplication photo = new PhotoApplication();
                    photo.Photo = File.ReadAllBytes(openFile.FileName);
                    photo.Application = applications;
                    bd_connection.connection.PhotoApplication.Add(photo);
                    lvPhoto.ItemsSource = applications.PhotoApplication;
                    lvPhoto.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Нельзя добавить более 5 фотографий");
            }
        }

        private void btnUserApplication_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserApplicationPage());
        }

        private void btnUserSignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserSignUpApplicationPage());
        }
    }
}
