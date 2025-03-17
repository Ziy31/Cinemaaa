using Cinema.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для AddClientPage.xaml
    /// </summary>
    public partial class AddClientPage : Page
    {
        public AddClientPage()
        {
            InitializeComponent();
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newClient = new Cinema.Database.Client();
                newClient.Lastname = txb_lastname.Text;
                newClient.Name = txb_name.Text;
                newClient.Surname = txb_patry.Text;
                newClient.Phone = txb_phone.Text;

                AppData.cinemaBD.Client.Add(newClient);

                int savedChanges = AppData.cinemaBD.SaveChanges();

                if (savedChanges > 0)
                {
                    MessageBox.Show("Данные успешно сохранены!");
                    NavigationService.Navigate(new AdminClientPage());
                }
                else
                {
                    MessageBox.Show("Ошибка при сохранении данных!");

                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Ошибка валидации: {validationError.PropertyName} - {validationError.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

    }
}
