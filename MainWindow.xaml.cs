using System.Security.Claims;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Collections.ObjectModel;


namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Window1.Uczen> uczniowie = new ObservableCollection<Window1.Uczen>();

        public MainWindow()
        {
            InitializeComponent();
            listView.ItemsSource = uczniowie;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            var okno = new Window1();
            if (okno.ShowDialog() == true)
            {
                uczniowie.Add(okno.NowyUczen);
            }
        }

        private void Edytuj_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem is Window1.Uczen wybranyUczen)
            {
                var okno = new Window1(wybranyUczen);
                if (okno.ShowDialog() == true)
                {
                    int index = uczniowie.IndexOf(wybranyUczen);
                    uczniowie[index] = okno.NowyUczen;
                }
            }
        }

        private void Usun_Click(object sender, RoutedEventArgs e)
        {
            var zaznaczeni = listView.SelectedItems;
            if (zaznaczeni.Count > 0)
            {
                var doUsuniecia = new List<Window1.Uczen>();
                foreach (Window1.Uczen u in zaznaczeni)
                    doUsuniecia.Add(u);

                foreach (var u in doUsuniecia)
                    uczniowie.Remove(u);
            }
        }
        private void Wczytaj_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}

