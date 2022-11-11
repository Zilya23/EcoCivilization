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
                tbProgressMin.Visibility = Visibility.Hidden;
                tbAchived.Visibility = Visibility.Visible;
            }

            if(user.ID == application.IDUser)
            {
                btnSignUp.Visibility = Visibility.Hidden;
                btnRedaction.Visibility = Visibility.Visible;
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

        private void btnRedactionClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RedactionApplicationPage(application));
        }
    }
}
