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
    /// Логика взаимодействия для ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {
        public List<ChatUser> chatUsers { get; set; }
        public static User user { get; set; }
        public ChatPage()
        {
            InitializeComponent();
            user = AuthorizationFunction.AuthorizationUser(Properties.Settings.Default.Login, Properties.Settings.Default.Password);
            chatUsers = ChatFunction.GetUserChatt(user);
            DataContext = this;
        }

        private void btnMainApplication_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddApplication_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUserApplication_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUserSignUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStatistic_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
