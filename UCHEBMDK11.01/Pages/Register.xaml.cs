using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using UCHEBMDK11._01.Models;

namespace UCHEBMDK11._01.Pages
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext())
            {
                string password = Passbox.Password;
                string hashedPassword = HashPassword(password);

                if (password.Length > 5 && db.users.Any(u => u.name == Log.Text) == false)
                {
                    db.users.AddRange(new Models.User(Log.Text, hashedPassword, 1));
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Пароль меньше 5 символов или такой пользователь уже существует");
                }


            }
        }

        private string HashPassword(string password)
        {

            byte[] inputBytes = Encoding.UTF8.GetBytes(password);

            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private void SwitchPage(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.Frame.Navigate(new Uri("Login.xaml", UriKind.Relative));
            }

        }
    }
}
