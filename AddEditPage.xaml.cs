using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
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

    public class Status
    {
        public bool GameStatus { get; set; } = false;

        public override string ToString()
        {
            return GameStatus ? "Активна" : "Не активна";
        }

    }

    public class Mode
    {
        public bool Gamemode { get; set; } = false;

        public override string ToString()
        {
            return Gamemode ? "Однопользовательская" : "Многопользовательская";
        }
    }

    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {

        private Game _currentGame = new Game();

        public AddEditPage(Game selectedGame)
        {
            InitializeComponent();

            if (selectedGame != null)
                _currentGame = selectedGame;

            DataContext = _currentGame;
            ComboGenre.ItemsSource = Games2Entities.GetContext().Genre.ToList();
            ComboDevel.ItemsSource = Games2Entities.GetContext().Developer.ToList();
            ComboPubl.ItemsSource = Games2Entities.GetContext().Publisher.ToList();
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? SelectedDate = calendar.SelectedDate;
            MessageBox.Show(SelectedDate.Value.Date.ToShortDateString());
        }

        

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentGame.GameName))
                errors.AppendLine("Ошибка 1: Укажите название игры!");
            if (_currentGame.Genre == null)
                errors.AppendLine("Ошибка 2: Выберите жанр игры!");
            if (_currentGame.ReleaseDate == null)
                errors.AppendLine("Ошибка 3: Выберите дату издания игры!");
            if (_currentGame.Price < 0 || _currentGame.Price > 10000)
                errors.AppendLine("Ошибка 4: Укажите стоимость игры в рублях!");
            if (_currentGame.Mode == null)
                errors.AppendLine("Ошибка 5: Выберите режим игры!");
            if (_currentGame.GameStatus == null)
                errors.AppendLine("Ошибка 6: Выберите статус игры!");
            if (_currentGame.Developer.DeveloperName == null)  
                errors.AppendLine("Ошибка 7: Введите разработчика игры!");
            if (_currentGame.Publisher.PublisherName == null)
                errors.AppendLine("Ошибка 8: Введите издателя игры!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentGame.ID == 0)
                Games2Entities.GetContext().Game.Add(_currentGame);

            try
            {
                Games2Entities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MainFrame.GoBack();
            }
            catch (DbEntityValidationException ex)
            {
                List<string> errorMessages = new List<string>();
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    string entityName = validationResult.Entry.Entity.GetType().Name;
                    foreach (DbValidationError error in validationResult.ValidationErrors)
                    {
                        errorMessages.Add(entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                        MessageBox.Show(ex.Message.ToString());
                        MessageBox.Show(entityName.ToString());

                    }
                }
            }   
        }
    }
}
