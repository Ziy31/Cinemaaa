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
using System.Windows.Shapes;

namespace Cinema.AddWin
{
    /// <summary>
    /// Логика взаимодействия для WorkerWindow.xaml
    /// </summary>
    public partial class WorkerWindow : Window
    {
        public static List<Employee> Employees { get; set; }
        public WorkerWindow()
        {
            InitializeComponent();
            Employees = new List<Employee>(AppData.cinemaBD.Employee.ToList());
            this.DataContext = this;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbSortOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedOption = cmbSortOptions.SelectedItem as ComboBoxItem;
            if (selectedOption != null)
            {
                var optionText = selectedOption.Content.ToString();

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Employees);
                view.SortDescriptions.Clear();

                switch (optionText)
                {
                    case "По фамилии":
                        view.SortDescriptions.Add(new SortDescription("Lastname", ListSortDirection.Ascending));
                        break;
                    case "По id":
                        view.SortDescriptions.Add(new SortDescription("id_employee", ListSortDirection.Ascending));
                        break;

                }
            }
        }

    }
}
