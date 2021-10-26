using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для Regin.xaml
    /// </summary>
    public partial class Regin : Page
    {
        private User _currentUser = new User();

        public Regin()
        {
            InitializeComponent();
            DataContext = _currentUser;
        }

        private void Regins_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentUser.Nickname))
                errors.AppendLine("Ошибка 1: Укажите Ваш никнейм!");
            if (string.IsNullOrWhiteSpace(_currentUser.Email))
                errors.AppendLine("Ошибка 2: Укажите Ваш Email!");
            if (string.IsNullOrWhiteSpace(_currentUser.Password))
                errors.AppendLine("Ошибка 3: Укажите Ваш пароль!");
            if (Password2.Text != _currentUser.Password)
                errors.AppendLine("Ошибка 4: Пароли не совпадают!");
            //if (string.IsNullOrWhiteSpace(Password2))
            //    errors.AppendLine("Ошибка 3: Укажите Ваш пароль!");
            _currentUser.UserStatus = 2;

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentUser.UserID == 0)
                Games2Entities.GetContext().User.Add(_currentUser);

            try
            {
                Games2Entities.GetContext().SaveChanges(); //Выдает ошибку
                MessageBox.Show("Информация сохранена!");
                Manager.MainFramee.GoBack();
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
                //Console.WriteLine(ex);
                //MessageBox.Show(ex.Message.ToString());
            }

        }
    }
    
}
