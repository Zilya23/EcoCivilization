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

namespace EcoCivilization.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        List<City> cities = new List<City>();
        List<Gender> genders = new List<Gender>();
        public RegistrationPage()
        {
            InitializeComponent();
            cities = CityFunctions.GetCities();
            cb_City.ItemsSource = cities;
            cb_City.DisplayMemberPath = "Name";
            genders = CityFunctions.GetGenders();
            cb_gender.ItemsSource = genders;
            cb_gender.DisplayMemberPath = "Name";
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

        private void btn_regist_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Name = tb_name.Text.Trim();
            user.Surname = tb_lastName.Text.Trim();
            user.Telephone = tb_tel.Text.Trim();
            user.ID_Role = 2;
            user.Count_Application = 0;
            user.Login = tb_log.Text.Trim();
            user.Password = tb_pass.Text.Trim();
            if(cb_City.SelectedItem != null)
            {
                var userCity = cb_City.SelectedItem as City;
                user.ID_City = userCity.ID;
            }
            else
            {
                MessageBox.Show("Выберите город");
            }
            if(cb_gender.SelectedItem != null)
            {
                var userGender = cb_gender.SelectedItem as Gender;
                user.ID_Gender = userGender.ID;
            }
            else
            {
                MessageBox.Show("Выберите пол");
            }
            if (TelephoneCorrect(user.Telephone))
            {
                if (AllWrite(user))
                {
                    if (RegistFunction.Registration(user))
                    {
                        MessageBox.Show("Регистрация прошла успешно!");
                        NavigationService.Navigate(new AuthorizationPage());
                    }
                    else
                    {
                        MessageBox.Show("Введите другой логин или телефон");
                    }
                }
            }
        }

        public bool AllWrite(User user)
        {
            if(user.Name.Length ==0)
            {
                MessageBox.Show("Заполните имя");
                return false;
            }
            else if(user.Surname.Length == 0)
            {
                MessageBox.Show("Заполните фамилию");
                return false;
            }
            else if (user.Telephone.Length == 0)
            {
                MessageBox.Show("Заполните телефон");
                return false;
            }
            else if (user.Login.Length == 0)
            {
                MessageBox.Show("Заполните логин");
                return false;
            }
            else if (user.Password.Length == 0)
            {
                MessageBox.Show("Заполните пароль");
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool TelephoneCorrect(string telephone)
        {
            if(telephone.Length == 11 && telephone[0] == '8')
            {
                return true;
            }
            else if(telephone.Length == 12 && telephone[0] == '+' && telephone[1] == '7')
            {
                return true;
            }
            else
            {
                MessageBox.Show("Введите верный телефон");
                return false;
            }
        }
    }
}
