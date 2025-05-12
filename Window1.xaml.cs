using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

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
        private bool edycjaTryb;

        public Window1(Uczen uczen = null)
        {
            InitializeComponent();

            if (uczen != null)
            {
                edycjaTryb = true;
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
            if (!WalidujDane())
                return;

            NowyUczen = new Uczen
            {
                Pesel = PeselBox.Text,
                Imie = Formatuj(ImieBox.Text),
                DrugieImie = string.IsNullOrWhiteSpace(DrugieImieBox.Text) ? "" : Formatuj(DrugieImieBox.Text),
                Nazwisko = Formatuj(NazwiskoBox.Text),
                DataUrodzenia = DataUrodzeniaBox.Text,
                Telefon = FormatTelefon(TelefonBox.Text),
                Adres = Formatuj(AdresBox.Text),
                Miejscowosc = Formatuj(MiejscowoscBox.Text),
                KodPocztowy = KodPocztowyBox.Text.Trim()
            };

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (CzyWprowadzonoZmiany())
            {
                var result = MessageBox.Show("Czy na pewno chcesz anulować ", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result != MessageBoxResult.Yes)
                    return;
            }

            DialogResult = false;
            Close();
        }

        private bool WalidujDane()
        {
            bool poprawne = true;

           
            foreach (var ctrl in new TextBox[] { PeselBox, ImieBox, NazwiskoBox, DataUrodzeniaBox, AdresBox, MiejscowoscBox, KodPocztowyBox })
                ctrl.ClearValue(BorderBrushProperty);

            TextBox[] wymagane = { PeselBox, ImieBox, NazwiskoBox, DataUrodzeniaBox, AdresBox, MiejscowoscBox, KodPocztowyBox };
            foreach (var box in wymagane)
            {
                if (string.IsNullOrWhiteSpace(box.Text))
                {
                    box.BorderBrush = Brushes.Red;
                    poprawne = false;
                }
            }

            if (!SprawdzPesel(PeselBox.Text))
            {
                PeselBox.BorderBrush = Brushes.Red;
                poprawne = false;
            }

            if (!ZgodnoscPeselZDataUrodzenia(PeselBox.Text, DataUrodzeniaBox.Text))
            {
                DataUrodzeniaBox.BorderBrush = Brushes.Red;
                poprawne = false;
            }

            return poprawne;
        }

        private bool SprawdzPesel(string pesel)
        {
            if (!Regex.IsMatch(pesel, @"^\d{11}$"))
                return false;

            int[] wagi = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int suma = 0;

            for (int i = 0; i < 10; i++)
                suma += wagi[i] * (pesel[i] - '0');

            int kontrolna = (10 - (suma % 10)) % 10;
            return kontrolna == (pesel[10] - '0');
        }

        private bool ZgodnoscPeselZDataUrodzenia(string pesel, string data)
        {
            if (!DateTime.TryParse(data, out DateTime dataUrodzenia))
                return false;

            string rok = dataUrodzenia.Year.ToString("D4");
            string miesiac = dataUrodzenia.Month.ToString("D2");
            string dzien = dataUrodzenia.Day.ToString("D2");

            string peselData = pesel.Substring(0, 6);

            int miesiacPesel = int.Parse(pesel.Substring(2, 2));
            int miesiacBazowy = dataUrodzenia.Month;
            int rokBazowy = dataUrodzenia.Year;

            if (rokBazowy >= 2000)
                miesiacBazowy += 20;

            string oczekiwany = rok.Substring(2, 2) + miesiacBazowy.ToString("D2") + dzien;
            return peselData == oczekiwany;
        }

        private string Formatuj(string tekst)
        {
            tekst = tekst.Trim();
            return char.ToUpper(tekst[0]) + tekst.Substring(1).ToLower();
        }

        private string FormatTelefon(string telefon)
        {
            string tylkoCyfry = Regex.Replace(telefon, @"\D", "");
            if (tylkoCyfry.Length == 9)
                return "+48 " + tylkoCyfry;
            if (tylkoCyfry.StartsWith("48") && tylkoCyfry.Length == 11)
                return "+" + tylkoCyfry;
            return telefon.Trim(); 
        }

        private bool CzyWprowadzonoZmiany()
        {
            foreach (var ctrl in new TextBox[] { PeselBox, ImieBox, DrugieImieBox, NazwiskoBox, DataUrodzeniaBox, TelefonBox, AdresBox, MiejscowoscBox, KodPocztowyBox })
            {
                if (!string.IsNullOrWhiteSpace(ctrl.Text))
                    return true;
            }
            return false;
        }
    }
}