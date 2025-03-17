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
    /// Логика взаимодействия для AddFilmPage.xaml
    /// </summary>
    public partial class AddFilmPage : Page
    {
        public AddFilmPage()
        {
            InitializeComponent();
            LoadAges();
            LoadGenres();
        }

        private void LoadGenres()
        {
            
            GenreBox.ItemsSource = AppData.cinemaBD.Genre.ToList();
            GenreBox.DisplayMemberPath = "Title";
        }

        private void LoadAges()
        {
            AgeBox.ItemsSource = AppData.cinemaBD.Age.ToList();    
            AgeBox.DisplayMemberPath = "Title";
        }


        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txb_title.Text) || GenreBox.SelectedItem == null &&
                    AgeBox.SelectedItem == null || string.IsNullOrWhiteSpace(txb_price.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");
                    return;
                }

                var seletedAge = AgeBox.SelectedItem as Age;
                
                var selectedGenre = GenreBox.SelectedItem as Genre;
                

                var newFilm = new Film
                {
                    Title = txb_title.Text,
                   id_age = seletedAge.id,
                   id_genre = selectedGenre.id_genre,
                    Price = int.Parse(txb_price.Text)
                };


                AppData.cinemaBD.Film.Add(newFilm);
                AppData.cinemaBD.SaveChanges();
                MessageBox.Show("Фильм добавлен!");
                NavigationService.Navigate(new AddPages.EmployeePage());

            }
            catch (DbEntityValidationException ex)
            {
                string errorMessage = "";
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        errorMessage += ve.PropertyName + ": " + ve.ErrorMessage + "\n";
                    }
                }
                MessageBox.Show("Ошибка сохранения данных:\n" + errorMessage);
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат цены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }
    }

}



