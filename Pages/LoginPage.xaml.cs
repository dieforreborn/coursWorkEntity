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
using courseWorkEntity.Model;
using courseWorkEntity.Pages;

namespace courseWorkEntity.Pages
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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if ((LoginTextBox.Text != "") && (PasswordTextBox.Text != ""))
            {
                using (var db = new colledgeDepartmentEntities())
                {
                    var user = (from u in db.users where
                               u.login == LoginTextBox.Text && u.password == PasswordTextBox.Text select u).FirstOrDefault();
                    if (user != null)
                    {
                        //ApplicationService.UserId = user.idUser;
                        //NavigationServise.NavigateToHomePage(ApplicationService.UserId);
                        NavigationService.Navigate(new HomePage(user.idUser));
                        //NavigationServise.NavigateHomePage(user.idUser);

                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль введен не верно");
                    }
                }
            }
        }

        private void ClearLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginTextBox.Text = "";
        }
        private void ClearPassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Text = "";
        }
    }
}
