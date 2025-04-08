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


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public List<MyItem> Items { get; set; } = new List<MyItem>();

        public MainWindow()
        {
            InitializeComponent();
            //listView.ItemsSource = Items;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Window1 myWindow = new Window1();
            //if (myWindow.ShowDialog() == true)
            //{
            //    Items.Add(myWindow.Item);
            //    listView.Items.Refresh();
            //}
        }
    }
}

