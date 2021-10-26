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

namespace Games2
{
    /// <summary>
    /// Логика взаимодействия для GamePrewie.xaml
    /// </summary>
    public partial class GamePrewie : Page
    {
        public GamePrewie()
        {
            InitializeComponent();
            var currentGames = Games2Entities.GetContext().Game.ToList();
        }
    }
}
