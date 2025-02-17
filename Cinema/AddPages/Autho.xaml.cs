using Cinema.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace Cinema.AddPages
{
    /// <summary>
    /// Логика взаимодействия для Autho.xaml
    /// </summary>
    public partial class Autho : Page
    {
        public Autho()
        {
            InitializeComponent();
        }

        private void Autho_btn_Click(object sender, RoutedEventArgs e)
        {
            var CurrentUser = AppData.cinemaBD.Authorization.FirstOrDefault(u => u.Login == log_txb.Text && u.Password == pass_txb.Password);
            
            if (CurrentUser != null)
            {
                if (CurrentUser.id_role == 1)
                {
                    NavigationService.Navigate(new AddPages.EmployeePage());
                }
                else if(CurrentUser.id_role == 2)
                {
                    NavigationService.Navigate(new AddPages.CashPage());
                }
                else
                {
                    NavigationService.Navigate(new AddPages.EmployeePage());
                }
            } 
            else
            {
               MessageBox.Show("Ошибка...", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
