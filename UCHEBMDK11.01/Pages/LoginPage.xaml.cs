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

namespace UCHEBMDK11._01.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

        public LoginPage()
        {
            InitializeComponent();
        }

        private void CheckLogin(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext())
            {
                Models.User emp = db.users.FirstOrDefault(e => e.name == Log.Text);

                if (emp == null)
                {
                    MessageBox.Show("Пользователь не найден", "Ошибка авторизации");
                    return;
                }


                bool isPasswordCorrect = emp.password == HashPassword(Password.Password);


                if (isPasswordCorrect)
                {

                    if (Window.GetWindow(this) is MainWindow mainWindow)
                    {
                        mainWindow.Frame.Navigate(new Uri(@"Pages\MainPage.xaml", UriKind.Relative));
                    }
                }
                else
                {
                    MessageBox.Show("Неверный пароль", "Ошибка авторизации");
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
                mainWindow.Frame.Navigate(new Uri(@"Pages\Register.xaml", UriKind.Relative));
            }

        }
    }
}

