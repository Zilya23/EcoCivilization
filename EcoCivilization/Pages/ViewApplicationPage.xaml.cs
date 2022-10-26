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
using EcoCivilization.Pages;

namespace EcoCivilization.Pages
{
    /// <summary>
    /// Логика взаимодействия для ViewApplicationPage.xaml
    /// </summary>
    public partial class ViewApplicationPage : Page
    {
        public static Cora.DataBase.Application application { get; set; }
        public static User user { get; set; }
        public ViewApplicationPage(Cora.DataBase.Application selectApplication)
        {
            InitializeComponent();
            application = selectApplication;
            user = AuthorizationFunction.AuthorizationUser(Properties.Settings.Default.Login, Properties.Settings.Default.Password);
            int countSignUp = bd_connection.connection.Application_User.Count(x => x.ID_Application == application.ID);
            pbCountUser.Value = countSignUp;
            int countPhoto = 5;

            if (application.Photo1 == null)
            {
                var child = this.wpFoto.Children[0];
                child.Visibility = child.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                countPhoto--;
            }

            if (application.Photo2 == null)
            {
                var child = this.wpFoto.Children[1];
                child.Visibility = child.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                countPhoto--;
            }

            if (application.Photo3 == null)
            {
                var child = this.wpFoto.Children[2];
                child.Visibility = child.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                countPhoto--;
            }

            if (application.Photo4 == null)
            {
                var child = this.wpFoto.Children[3];
                child.Visibility = child.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                countPhoto--;
            }

            if (application.Photo5 == null)
            {
                var child = this.wpFoto.Children[4];
                child.Visibility = child.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                countPhoto--;
            }

            if (countPhoto == 1)
            {
                imgFirst.Width = 700;
                imgFirst.Height = 700;

                imgSecond.Width = 700;
                imgSecond.Height = 700;

                imgThirst.Width = 700;
                imgThirst.Height = 700;

                imgFourth.Width = 700;
                imgFourth.Height = 700;

                imgFive.Width = 700;
                imgFive.Height = 700;
            }
            else if(countPhoto == 2)
            {
                imgFirst.Width = 650;
                imgFirst.Height = 650;

                imgSecond.Width = 650;
                imgSecond.Height = 650;

                imgThirst.Width = 650;
                imgThirst.Height = 650;

                imgFourth.Width = 650;
                imgFourth.Height = 650;

                imgFive.Width = 650;
                imgFive.Height = 650;
            }
            else if (countPhoto == 3)
            {
                imgFirst.Width = 600;
                imgFirst.Height = 600;

                imgSecond.Width = 600;
                imgSecond.Height = 600;

                imgThirst.Width = 600;
                imgThirst.Height = 600;

                imgFourth.Width = 600;
                imgFourth.Height = 600;

                imgFive.Width = 600;
                imgFive.Height = 600;
            }
            else if (countPhoto == 4)
            {
                imgFirst.Width = 500;
                imgFirst.Height = 500;

                imgSecond.Width = 500;
                imgSecond.Height = 500;

                imgThirst.Width = 500;
                imgThirst.Height = 500;

                imgFourth.Width = 500;
                imgFourth.Height = 500;

                imgFive.Width = 500;
                imgFive.Height = 500;
            }
            else
            {
                imgFirst.Width = 400;
                imgFirst.Height = 400;

                imgSecond.Width = 400;
                imgSecond.Height = 400;

                imgThirst.Width = 400;
                imgThirst.Height = 400;

                imgFourth.Width = 400;
                imgFourth.Height = 400;

                imgFive.Width = 400;
                imgFive.Height = 400;
            }

            var userSignUp = bd_connection.connection.Application_User.FirstOrDefault(x=> x.ID_User == user.ID && x.ID_Application == application.ID);
            if(userSignUp != null)
            {
                btnSignUp.Visibility = Visibility.Hidden;
                btnAnnul.Visibility = Visibility.Visible;
            }

            
            if (application.Count_User == countSignUp)
            {
                pbCountUser.Visibility = Visibility.Hidden;
                btnSignUp.Visibility = Visibility.Hidden;
                tbProgressMax.Visibility = Visibility.Hidden;
                tbProgressMin.Visibility = Visibility.Hidden;
                tbAchived.Visibility = Visibility.Visible;
            }

            this.DataContext = application;
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

        private void btnMainApplication_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainApplicationPage());
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            int countSignUp = bd_connection.connection.Application_User.Count(x => x.ID_Application == application.ID);
            if(application.Count_User >= countSignUp)
            {
                Application_User applicationUser = new Application_User();
                applicationUser.ID_Application = application.ID;
                applicationUser.ID_User = user.ID;
                applicationUser.Date = DateTime.Now;
                bd_connection.connection.Application_User.Add(applicationUser);
                bd_connection.connection.SaveChanges();
            }
            Update();
        }

        public void Update()
        {
            int countSignUp = bd_connection.connection.Application_User.Count(x => x.ID_Application == application.ID);
            pbCountUser.Value = countSignUp;

            var userSignUp = bd_connection.connection.Application_User.FirstOrDefault(x => x.ID_User == user.ID && x.ID_Application == application.ID);
            if (userSignUp != null)
            {
                btnSignUp.Visibility = Visibility.Hidden;
                btnAnnul.Visibility = Visibility.Visible;
            }
            else
            {
                btnSignUp.Visibility = Visibility.Visible;
                btnAnnul.Visibility = Visibility.Hidden;
            }


            if (application.Count_User == countSignUp)
            {
                pbCountUser.Visibility = Visibility.Hidden;
                btnSignUp.Visibility = Visibility.Hidden;
                tbProgressMax.Visibility = Visibility.Hidden;
                tbProgressMin.Visibility = Visibility.Hidden;
                tbAchived.Visibility = Visibility.Visible;
            }
        }

        private void btnAnnul_Click(object sender, RoutedEventArgs e)
        {
            var userSignUp = bd_connection.connection.Application_User.FirstOrDefault(x => x.ID_User == user.ID && x.ID_Application == application.ID);
            if (userSignUp != null)
            {
                bd_connection.connection.Application_User.Remove(userSignUp);
                bd_connection.connection.SaveChanges();
            }
            Update();
        }
    }
}
