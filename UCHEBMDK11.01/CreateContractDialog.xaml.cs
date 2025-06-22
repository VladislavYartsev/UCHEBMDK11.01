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
using System.Windows.Shapes;

namespace UCHEBMDK11._01
{
    public partial class CreateContractDialog : Window
    {
        public int DocNumber { get; private set; }
        public string DocTitle { get; private set; }
        public string FirstSide { get; private set; }
        public string SecondSide { get; private set; }

        public CreateContractDialog()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(DocNumberBox.Text, out int docNumber))
            {
                DocNumber = docNumber;
                DocTitle = DocTitleBox.Text;
                FirstSide = FirstSideBox.Text;
                SecondSide = SecondSideBox.Text;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректный номер документа", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
