using Cinema.Database;
using Microsoft.Office.Interop.Word;
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
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Shapes;

namespace Cinema.AddPages
{
    /// <summary>
    /// Логика взаимодействия для AdminFilmPage.xaml
    /// </summary>
    public partial class AdminFilmPage : System.Windows.Controls.Page
    {
        public static List<Film> Films { get; set; }
        public AdminFilmPage()
        {
            InitializeComponent();
            Films = new List<Film>(AppData.cinemaBD.Film.ToList());
            this.DataContext = this;
        }

        private void AddFilm_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPages.AddFilmPage());
        }

        private void BtnExportToWord_Click(object sender, RoutedEventArgs e)
        {
            var allFilm = AppData.cinemaBD.Film.ToList();
            var application = new Word.Application();

            Word.Document document = application.Documents.Add();

            foreach (var user in allFilm)
            {
                Word.Paragraph filmParagraph = document.Paragraphs.Add();
                Word.Range filmRange = filmParagraph.Range;
                filmRange.Text = user.id_film.ToString();
                filmParagraph.set_Style("Title");
                filmRange.InsertParagraphAfter();

                Word.Paragraph tableParagraph = document.Paragraphs.Add();
                Word.Range tableRange = tableParagraph.Range;
                Word.Table paymentsTable = document.Tables.Add(tableRange, allFilm.Count() + 1, 2);
                paymentsTable.Borders.InsideLineStyle = paymentsTable.Borders.OutsideLineStyle
                    = Word.WdLineStyle.wdLineStyleSingle;
                paymentsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                Word.Range cellRange;

                cellRange = paymentsTable.Cell(1, 1).Range;
                cellRange.Text = "Фильм";
                cellRange = paymentsTable.Cell(1, 2).Range;
                cellRange.Text = "Сумма расходов";

                paymentsTable.Rows[1].Range.Bold = 1;
                paymentsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                for (int i = 0; i < allFilm.Count(); i++)
                {
                    var currentFilm = allFilm[i];

                    cellRange = paymentsTable.Cell(i + 1, 1).Range;
                    cellRange.Text = currentFilm.Title;
                }

                if (user != allFilm.LastOrDefault())
                {
                    document.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                }
            }

            application.Visible = true;
            document.SaveAs2(@"C:\941.docx");
            document.SaveAs2(@"C:\941.pdf", Word.WdExportFormat.wdExportFormatPDF);
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var ser = (sender as Button).DataContext as Film;
            if (MessageBox.Show($"Вы действительно хотите удалить фильм {ser.Title}?", "Подтверждение удаления", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ser.IsDelete = true;
                AppData.cinemaBD.SaveChanges();
                FilmsLv.ItemsSource = new List<Film>(AppData.cinemaBD.Film.Where(i => i.IsDelete == false).ToList());
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

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Films);
                view.SortDescriptions.Clear();

                switch (optionText)
                {
                    case "По названию":
                        view.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
                        break;
                    case "По id":
                        view.SortDescriptions.Add(new SortDescription("id_film", ListSortDirection.Ascending));
                        break;
                    case "По возрастанию цены":
                        view.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Ascending));
                        break;
                    case "По убыванию цены":
                        view.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Descending));
                        break;

                }
            }
        }

    }
}
