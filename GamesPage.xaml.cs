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
    /// Логика взаимодействия для GamesPage.xaml
    /// </summary>
    public partial class GamesPage : Page
    {
        public GamesPage()
        {
            InitializeComponent();
            //DGridGames.ItemsSource = Games2Entities.GetContext().Game.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Game));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var gamesForRemoving = DGridGames.SelectedItems.Cast<Game>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {gamesForRemoving.Count()} элементов?", "Внимание", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Games2Entities.GetContext().Game.RemoveRange(gamesForRemoving);
                    Games2Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGridGames.ItemsSource = Games2Entities.GetContext().Game.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Games2Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridGames.ItemsSource = Games2Entities.GetContext().Game.ToList();
            }
        }

        private void DGridGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
