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
using Cora.Functionss;

namespace EcoCivilization.Pages
{
    /// <summary>
    /// Логика взаимодействия для StatisticPage.xaml
    /// </summary>
    public partial class StatisticPage : Page
    {
        public static Cora.DataBase.Application application  { get; set; }
        public static User user { get; set; }
        public StatisticPage()
        {
            InitializeComponent();
            user = AuthorizationFunction.AuthorizationUser(Properties.Settings.Default.Login, Properties.Settings.Default.Password);
            tbCountApplication.Text = tbCountApplication.Text + StatisticFunction.CountApplication(user);
            tbCountFollowApplication.Text = tbCountFollowApplication.Text + StatisticFunction.CountFollowApplication(user);
            tbDayActiv.Text = tbDayActiv.Text + StatisticFunction.CountActivDays(user);
            tbCountApplicationThisYear.Text = tbCountApplicationThisYear.Text + StatisticFunction.ApplicationYear();
            tbCountApplicationInYourCity.Text = tbCountApplicationInYourCity.Text + StatisticFunction.CountApplicationCity(user.City);
            tbCountUserInYourCity.Text = tbCountUserInYourCity.Text + StatisticFunction.CountUserInCity(user);
            tbCountUser.Text = tbCountUser.Text + StatisticFunction.CountUser();
            tbAllCountApplication.Text = tbAllCountApplication.Text + StatisticFunction.CountUser();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void btnUserSignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserSignUpApplicationPage());
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
    }
}
