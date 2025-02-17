using Cinema.Database;
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

namespace Cinema.AddPages
{
    /// <summary>
    /// Логика взаимодействия для CashPage.xaml
    /// </summary>
    public partial class CashPage : Page
    {
        public CashPage()
        {
            InitializeComponent();
            var clientCollection = AppData.cinemaBD.Client.ToList();
            ClientGrid.ItemsSource = clientCollection;
        }

        private void AddFilm_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPages.AddFilmPage());
        }
    }
}
