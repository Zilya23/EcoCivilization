﻿using System;
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
using Cora;

namespace EcoCivilization.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        List<City> cities = new List<City>(); 
        public RegistrationPage()
        {
            InitializeComponent();
            cities = CityFunction.GetCities();
            cb_City.ItemsSource = cities;
            cb_City.DisplayMemberPath = "Name";
        }

        private void tb_back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void tb_back_MouseEnter(object sender, MouseEventArgs e)
        {
            tb_back.Foreground = new SolidColorBrush(Colors.ForestGreen);
        }

        private void tb_back_MouseLeave(object sender, MouseEventArgs e)
        {
            tb_back.Foreground = new SolidColorBrush(Colors.DarkSeaGreen);
        }
    }
}
