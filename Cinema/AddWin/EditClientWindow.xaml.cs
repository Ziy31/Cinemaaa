using Cinema.Database;
using Microsoft.Office.Interop.Word;
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

namespace Cinema.AddWin
{
    /// <summary>
    /// Логика взаимодействия для EditClientWindow.xaml
    /// </summary>
    public partial class EditClientWindow : System.Windows.Window
    {
        public EditClientWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new Cinema.Database.Client();
            string lastName = txtLastName.Text;
            string firstName = txtName.Text;
            string middleName = txtSurName.Text;
            string phoneNumber = txtPhoneNumber.Text;

            AppData.cinemaBD.SaveChanges();
            MessageBox.Show("Данные успешно обновлены!");
            this.Close();

        }
    }
}
