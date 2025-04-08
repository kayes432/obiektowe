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

namespace WpfApp1
{
    public partial class Window1 : Window
    {
        public class Uczen
        {
            public string Pesel { get; set; }
            public string Imie { get; set; }
            public string DrugieImie { get; set; }
            public string Nazwisko { get; set; }
            public string DataUrodzenia { get; set; }
            public string Telefon { get; set; }
            public string Adres { get; set; }
            public string Miejscowosc { get; set; }
            public string KodPocztowy { get; set; }
        }
        public Uczen NowyUczen { get; private set; }

        public Window1(Uczen uczen = null)
        {
            InitializeComponent();
            if (uczen != null)
            {
                PeselBox.Text = uczen.Pesel;
                ImieBox.Text = uczen.Imie;
                DrugieImieBox.Text = uczen.DrugieImie;
                NazwiskoBox.Text = uczen.Nazwisko;
                DataUrodzeniaBox.Text = uczen.DataUrodzenia;
                TelefonBox.Text = uczen.Telefon;
                AdresBox.Text = uczen.Adres;
                MiejscowoscBox.Text = uczen.Miejscowosc;
                KodPocztowyBox.Text = uczen.KodPocztowy;
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            NowyUczen = new Uczen
            {
                Pesel = PeselBox.Text,
                Imie = ImieBox.Text,
                DrugieImie = DrugieImieBox.Text,
                Nazwisko = NazwiskoBox.Text,
                DataUrodzenia = DataUrodzeniaBox.Text,
                Telefon = TelefonBox.Text,
                Adres = AdresBox.Text,
                Miejscowosc = MiejscowoscBox.Text,
                KodPocztowy = KodPocztowyBox.Text
            };
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}