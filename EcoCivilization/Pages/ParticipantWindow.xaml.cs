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
using System.Windows.Shapes;
using Cora.DataBase;
using Cora.Functionss;
using EcoCivilization.Pages;

namespace EcoCivilization.Pages
{
    /// <summary>
    /// Логика взаимодействия для ParticipantWindow.xaml
    /// </summary>
    public partial class ParticipantWindow : Window
    {
        public List<Application_User> participantsList { get; set; }
        public ParticipantWindow(Cora.DataBase.Application application)
        {
            InitializeComponent();
            participantsList = bd_connection.connection.Application_User.Where(x => x.ID_Application == application.ID).ToList();
            DataContext = this;
        }
    }
}
