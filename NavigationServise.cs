using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using courseWorkEntity.Pages;

namespace courseWorkEntity
{
    public static class NavigationServise
    {
        public static Frame _mainFrame;

        public static HomePage homePage;
        public static LoginPage loginPage;

        public static void Initialize(Frame mainFrame)
        { 
            _mainFrame = mainFrame;
        }
        public static void NavigateHomePage(int idUser)
        {
            if (homePage == null)
            {
                homePage = new HomePage(idUser);
            }
            _mainFrame.Navigate(homePage);
        }

        public static void NavigateLoginPage()
        {
            if (loginPage == null)
            {
                loginPage = new LoginPage();
            }
            _mainFrame.Navigate(loginPage);
        }
    }

}
