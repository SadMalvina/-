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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Users = Games2Entities.GetContext().User.ToList();

            bool Prov = false;
            foreach (User Nickname in Users)
            {
                if ((Nikname.Text == Nickname.Nickname) && (Password.Password == Nickname.Password))
                {
                    Nicknameeeee.Prov = true;
                    Nicknameeeee.Nickname = Nikname.Text;
                    Nicknameeeee.Status = Nickname.UserStatus;
                    InGame xaskWindow = new InGame();
                    xaskWindow.Show();
                   
                }
            }
            if (Prov == false)
            {
                Manager.MainFramee.Navigate(new Login());
                StringBuilder errors = new StringBuilder();

                if (Prov == false)
                    errors.AppendLine("Ошибка 1: Некорректные логин или пароль!!");
                

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

               
            }

        }
        



        private void Regin_Click_1(object sender, RoutedEventArgs e)
        {
            Manager.MainFramee.Navigate(new Regin());
            //MainFramee.Navigate(new Regin());
            //Manager.MainFramee = MainFramee;
        }
    }
}

