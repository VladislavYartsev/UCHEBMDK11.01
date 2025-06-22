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
using Microsoft.EntityFrameworkCore;

namespace UCHEBMDK11._01.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {

            using (AppContext dbContext = new AppContext())
            {
                InitializeComponent();
                var activeContracts = from side in dbContext.sides_of_contract
                                      select side;
                SQL.ItemsSource = activeContracts.ToList();
            }
        }

        private void ShowActiveDocuments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (AppContext _context = new AppContext())
                {
                    var activeDocs = _context.ActiveDocumentsView
                        .OrderByDescending(d => d.creation_date);

                    SQL.ItemsSource = activeDocs.ToList();
                    StatusText.Text = $"Загружено {activeDocs.ToList().Count} активных документов";
                }
            }
            catch (Exception ex)
            {
                using (AppContext _context = new AppContext())
                {
                    var activeDocs = _context.ActiveDocumentsView
                        .OrderByDescending(d => d.creation_date);

                    foreach (var doc in activeDocs)
                    {
                        doc.first_side = "null";
                        doc.second_side = "null";


                    }
                    SQL.ItemsSource = activeDocs.ToList();
                    StatusText.Text = $"Загружено {activeDocs.ToList().Count} активных документов";
                }

                MessageBox.Show($"Ошибка загрузки документов: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext _context = new AppContext())
            {

                try
                {
                    _context.Database.ExecuteSqlRaw("CALL archive_old_documents({0})", 12);
                    ShowActiveDocuments_Click(sender, e);
                    StatusText.Text = "Старые документы успешно архивированы";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка архивации документов: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void CreateNewContract_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext _context = new AppContext())
            {
                var dialog = new CreateContractDialog();
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        // Вызываем хранимую процедуру с параметрами
                        _context.Database.ExecuteSqlRaw(
                            "CALL create_new_contract({0}, {1}, {2}, {3})",
                            dialog.DocNumber,
                            dialog.DocTitle,
                            dialog.FirstSide,
                            dialog.SecondSide);

                        _context.SaveChanges();
                        var activeContracts = from side in _context.sides_of_contract
                                              select side;
                        SQL.ItemsSource = activeContracts.ToList();
                        StatusText.Text = "Новый договор успешно создан";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка создания договора: {ex.Message}", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
