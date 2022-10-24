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
    /// Логика взаимодействия для HotelsPage.xaml
    /// </summary>
    public partial class HotelsPage : Page
    {
        public HotelsPage()
        {
            InitializeComponent();
            DgridHotels.ItemsSource = Entities.GetContext().Hotels.ToList();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Hotel));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = DgridHotels.SelectedItems.Cast<Hotel>().ToList();
            if(MessageBox.Show($"Вы точно хотите удалить следующие {hotelsForRemoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo,MessageBoxImage.Question)==MessageBoxResult.Yes)
            {
                try
                {
                    Entities.GetContext().Hotels.RemoveRange(hotelsForRemoving);
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Даныые удалены");
                    DgridHotels.ItemsSource = Entities.GetContext().Hotels.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_isVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DgridHotels.ItemsSource = Entities.GetContext().Hotels.ToList();
            }
        }
    }
}
