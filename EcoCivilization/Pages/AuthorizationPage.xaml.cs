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
using Cora.Functionss;
using Cora.DataBase;


namespace EcoCivilization.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public static User user;
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void btn_authoriz_Click(object sender, RoutedEventArgs e)
        {
            string login = tb_Login.Text.Trim();
            string password = tb_Pass.Password.Trim();
            user = AuthorizationFunction.AuthorizationUser(login, password);
            if(user != null)
            {
                Properties.Settings.Default.Login = user.Email;
                Properties.Settings.Default.Password = user.Password;
                Properties.Settings.Default.Save();
                NavigationService.Navigate(new MainApplicationPage());
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            tb_reg.Foreground = new SolidColorBrush(Colors.DarkSeaGreen);
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            tb_reg.Foreground = new SolidColorBrush(Colors.ForestGreen);
        }
    }
}
