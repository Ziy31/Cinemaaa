using Cinema.AddWin;
using Cinema.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace Cinema.AddPages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public static ObservableCollection<Rent> Rents {  get; set; }
        public static ObservableCollection<Rent> FilteredRents { get; set; }
        public AdminPage()
        {
            InitializeComponent();
            Rents = new ObservableCollection<Rent>(AppData.cinemaBD.Rent);
            this.DataContext = this;
        }

        private void EmployeeTb_Click(object sender, RoutedEventArgs e)
        {
            var worker = new WorkerWindow();
            worker.Show();
        }

        private void ClientTb_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPages.AdminClientPage());
        }

        private void FilmTb_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPages.AdminFilmPage());
        }

        private void BtnExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            var alluser = AppData.cinemaBD.Client.ToList().OrderBy(p=>p.Lastname). ToList();

            var application = new Excel.Application();
            application.SheetsInNewWorkbook = alluser.Count();

            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

            int startRowIndex = 1;
            for(int i = 0; i < alluser.Count(); i++)
            {
                Excel.Worksheet worksheet = application.Worksheets.Item[i + 1];
                //worksheet.Name = alluser[i].Lastname.ToString();

                worksheet.Cells[1][startRowIndex] = "Дата аренды";
                worksheet.Cells[2][startRowIndex] = "Арендатор";
                worksheet.Cells[3][startRowIndex] = "Стоимость";
                worksheet.Cells[4][startRowIndex] = "Сумма";

                startRowIndex++;

                var userCat = alluser[i].Rent.OrderBy(p => p.DateOfIssue).GroupBy(p => p.Film).OrderBy(p => p.Key.Title);

                foreach (var groupCat in userCat)
                {
                    Excel.Range headerRange = worksheet.Range[worksheet.Cells[1][startRowIndex], worksheet.Cells[4][startRowIndex]];
                    headerRange.Merge();
                    headerRange.Value = groupCat.Key.Title;
                    headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    headerRange.Font.Italic = true;

                    startRowIndex++;

                    foreach(var payment in groupCat)
                    {
                        worksheet.Cells[1][startRowIndex] = payment.DateOfIssue.ToString("dd.MM.yyyy");
                        worksheet.Cells[2][startRowIndex] = payment.Client.Lastname;
                        worksheet.Cells[3][startRowIndex] = payment.Film.Price;

                        worksheet.Cells[4][startRowIndex] = payment.Film.Price;

                        startRowIndex++;
                    }

                    Excel.Range sumRange = worksheet.Range[worksheet.Cells[1][startRowIndex], worksheet.Cells[3][startRowIndex]];
                    sumRange.Merge();
                    sumRange.Value = "ИТОГО:";
                    sumRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    worksheet.Cells[4][startRowIndex].Value = worksheet.Cells[3][startRowIndex].Value;

                    sumRange.Font.Bold = worksheet.Cells[4][startRowIndex].Font.Bold = true;
                    
                    startRowIndex++;

                    Excel.Range rangeBorders = worksheet.Range[worksheet.Cells[1][1], worksheet.Cells[4][startRowIndex - 1]];
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                    rangeBorders.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                    worksheet.Columns.AutoFit();
                }
            }
            application.Visible = true;
        }
    }
}
