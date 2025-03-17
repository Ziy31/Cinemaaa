using Cinema.AddWin;
using Cinema.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для AdminClientPage.xaml
    /// </summary>
    public partial class AdminClientPage : Page
    {
        public static List<Client> Clients { get; set; }
        public AdminClientPage()
        {
            InitializeComponent();
            Clients = new List<Client>(AppData.cinemaBD.Client.ToList());
            this.DataContext = this;
        }

        private void AddFilm_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPages.AddClientPage());
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var ser = (sender as Button).DataContext as Client;
            if (MessageBox.Show($"Вы уверены, что хотите удалить клиента {ser.Lastname + " " + ser.Name}?", "Подтверждение удаления", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ser.IsDelete = true;
                AppData.cinemaBD.SaveChanges();
                ClientsLv.ItemsSource = new List<Client>(AppData.cinemaBD.Client.Where(i => i.IsDelete == false).ToList());
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPage());
        }

        private void cmbSortOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedOption = cmbSortOptions.SelectedItem as ComboBoxItem;
            if (selectedOption != null)
            {
                var optionText = selectedOption.Content.ToString();

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Clients);
                view.SortDescriptions.Clear();

                switch (optionText)
                {
                    case "По фамилии":
                        view.SortDescriptions.Add(new SortDescription("Lastname", ListSortDirection.Ascending));
                        break;
                    case "По id":
                        view.SortDescriptions.Add(new SortDescription("id_client", ListSortDirection.Ascending));
                        break;

                }
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var set = (sender as Button).DataContext as Client;
            var editWindow = new EditClientWindow { DataContext = set };
            if (editWindow.ShowDialog() == true)
            {
                ClientsLv.ItemsSource = new List<Film>(AppData.cinemaBD.Film.Where(i => i.IsDelete == false).ToList());
            }
        }

    }
}
