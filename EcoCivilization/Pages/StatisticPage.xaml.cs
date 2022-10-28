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
    /// Логика взаимодействия для StatisticPage.xaml
    /// </summary>
    public partial class StatisticPage : Page
    {
        public static Cora.DataBase.Application application  { get; set; }
        public StatisticPage()
        {
            InitializeComponent();
            application = bd_connection.connection.Application.FirstOrDefault(x=> x.ID == 1);
            int countPhoto = 5;

            if(countPhoto == 3)
            {
                imgFirst.Width = 300;
                imgFirst.Height = 300;

                imgSecond.Width = 300;
                imgSecond.Height = 300;

                imgThirst.Width = 300;
                imgThirst.Height = 300;

                imgFourth.Width = 300;
                imgFourth.Height = 300;

                imgFive.Width = 300;
                imgFive.Height = 300;
            }

            this.DataContext = application;
        }
    }
}
