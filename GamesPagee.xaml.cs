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
    /// Логика взаимодействия для GamesPagee.xaml
    /// </summary>
    public partial class GamesPagee : Page
    {
        public GamesPagee()
        {
            InitializeComponent();

            var allgenre = Games2Entities.GetContext().Genre.ToList();
            allgenre.Insert(0, new Genre
            {
                GenreName = "Все жанры"
            });
            ComboGenre.ItemsSource = allgenre;
            ComboGenre.SelectedIndex = 0;

            //var currentGames = Games2Entities.GetContext().Game.ToList();
            //LViewGames.ItemsSource = currentGames;
        }
    

        private void UpdateGames()
        {
            var currentGames = Games2Entities.GetContext().Game.ToList();
            if (ComboGenre.SelectedIndex > 0)
            
                currentGames = currentGames.Where(p => p.Genre==ComboGenre.SelectedItem).ToList();

            currentGames = currentGames.Where(p => p.GameName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            LViewGames.ItemsSource = currentGames.OrderBy(p => p.GameName).ToList();
        }
    

   

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGames();
        }

        private void ComboGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGames();
        }
    }
}
