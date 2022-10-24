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

namespace wpfProject2
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Hotel currentHotel = new Hotel();
        public AddEditPage()
        {
            InitializeComponent();
            DataContext= currentHotel;
            ComboCountries.ItemsSource = Entities.GetContext().Countries.ToList();
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentHotel.Name))
                errors.AppendLine("Укажите название отеля");
            if (currentHotel.CountOfStars < 1 || currentHotel.CountOfStars > 5)
                errors.AppendLine("Количество звезд - число от 1 до 5");
            if (currentHotel.Country == null)
                errors.AppendLine("Выберите страну");
            if(errors.Length>0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if(currentHotel.Id==0)
                Entities.GetContext().Hotels.Add(currentHotel);
            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ComboCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
