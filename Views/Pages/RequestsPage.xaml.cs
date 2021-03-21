using DesktopApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Telerik.Windows.Controls;

namespace DesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for RequestsPage.xaml
    /// </summary>
    /// 
    public class Pom
    {
        public int ZahtjevIdd { get; set; }
        public string KrvnaGrupaZahtjevv { get; set; }
        public string TipKrvnogDerivataa { get; set; }
        public short ZahtjevanaKolicinaa { get; set; }
        public DateTime DatumPodnosenjaZahtjevaa { get; set; }
        public string Nazivv { get; set; }
        public int UstanovaId { get; set; }

        public Pom()
        {
            ZahtjevIdd = 0;
            KrvnaGrupaZahtjevv = "";
            TipKrvnogDerivataa = "";
            ZahtjevanaKolicinaa = 0;
            DatumPodnosenjaZahtjevaa = DateTime.Now;
            Nazivv = "";
            UstanovaId = 0;
        }
    }

    public partial class RequestsPage : Page
    {
        List<Zahtjev> zahtjevi = null; //0 cekanje 2 odbijen 1 isporucen
        List<ZdravstvenaUstanova> zu = null;
        List<Pom> pom = new List<Pom>();
        List<DozaKrvi> dozaZaIsporuku = new List<DozaKrvi>();


        public RequestsPage()
        {
            InitializeComponent();
            btnDeliver.IsEnabled = false;
            btnDecline.IsEnabled = false;


            zahtjevi = JsonConvert.DeserializeObject<List<Zahtjev>>(REST.GetAll("zahtjev"));
            for (int i = 0; i < zahtjevi.Count; i++)
            {
                zu = JsonConvert.DeserializeObject<List<ZdravstvenaUstanova>>(REST.GetInstitution(zahtjevi[i].ZdravstvenaUstanovaId.Value));
                pom.Add(new Pom());
                pom[i].ZahtjevanaKolicinaa = zahtjevi[i].ZahtjevanaKolicina.Value;
                pom[i].Nazivv = zu[0].Naziv;
                pom[i].KrvnaGrupaZahtjevv = zahtjevi[i].KrvnaGrupaZahtjev;
                pom[i].ZahtjevIdd = zahtjevi[i].ZahtjevId;
                pom[i].DatumPodnosenjaZahtjevaa = zahtjevi[i].DatumPodnosenjaZahtjeva.Value;
                pom[i].TipKrvnogDerivataa = zahtjevi[i].TipKrvnogDerivata;
                pom[i].UstanovaId = zu[0].ZdravstvenaUstanovaId;

                if (zahtjevi[i].ZahtjevPrihvacen == 0)
                {
                    datagridPending.Items.Add(pom[i]);
                }
                else if (zahtjevi[i].ZahtjevPrihvacen == 2)
                {
                    datagridDeclined.Items.Add(pom[i]);
                }
                else
                {
                    datagridDelivered.Items.Add(pom[i]);
                }

            }
            pom.Clear();


        }

        private void RadTabControl_SelectionChanged(object sender, Telerik.Windows.Controls.RadSelectionChangedEventArgs e)
        {
            RadTabItem item = tabMenu.SelectedItem as RadTabItem;

            datagridPending.SelectedItem = null;
            datagridDelivered.SelectedItem = null;
            datagridDeclined.SelectedItem = null;


        }

        private void datagridDelivered_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            listDoses.Items.Clear();

            if (this.datagridDelivered.SelectedItem != null)
            {

                Pom zahtjev = this.datagridDelivered.SelectedItem as Pom;

                btnDecline.Visibility = Visibility.Hidden;
                btnDeliver.Visibility = Visibility.Hidden;

                List<Isporuka> isporuka = JsonConvert.DeserializeObject<List<Isporuka>>(REST.GetIsporuka(zahtjev.ZahtjevIdd));
                List<DozaKrvi> doza = JsonConvert.DeserializeObject<List<DozaKrvi>>(REST.GetDoza(isporuka[0].IsporukaId));
                for (int i = 0; i < doza.Count; i++)
                {
                    listDoses.Items.Add(doza[i].DozaKrviId);
                }


            }

        }

        private void datagridPending_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            btnDeliver.IsEnabled = false;
            btnDecline.IsEnabled = false;
            listDoses.Items.Clear();
            if (this.datagridPending.SelectedItem != null)
            {

                Pom zahtjev = this.datagridPending.SelectedItem as Pom;
                string krvnaGrupa = zahtjev.KrvnaGrupaZahtjevv;
                string tip = zahtjev.TipKrvnogDerivataa;
                List<DozaKrvi> doza = JsonConvert.DeserializeObject<List<DozaKrvi>>(REST.GetdozaZaisporuku(krvnaGrupa, tip));

                if (doza.Count >= zahtjev.ZahtjevanaKolicinaa)
                {
                    btnDeliver.IsEnabled = true;
                    btnDecline.IsEnabled = true;

                    btnDecline.Visibility = Visibility.Visible;
                    btnDeliver.Visibility = Visibility.Visible;
                    for (int i = 0; i < zahtjev.ZahtjevanaKolicinaa; i++)
                    {
                        listDoses.Items.Add(doza[i].DozaKrviId);
                        dozaZaIsporuku.Add(doza[i]);
                    }


                }
                else
                {
                    MessageBox.Show("U magacinu nema dovoljno doza krvi za zahtjevani derivat!!!");

                    btnDeliver.IsEnabled=false;
                    btnDecline.IsEnabled = true;

                }

            }
        }

        private void btnDeliver_Click(object sender, RoutedEventArgs e)
        {
            List<Magacin> mag = new List<Magacin>();
            Pom zahtjev = this.datagridPending.SelectedItem as Pom;
            Zahtjev pomZahtjev = new Zahtjev();
            Isporuka isporuka = new Isporuka();
            isporuka.DatumIsporuke = DateTime.Now;
            isporuka.ZahtjevId = zahtjev.ZahtjevIdd;
            //isporuka id
            var response = REST.Post("isporuka", isporuka);

            string pom = response.Headers.Location.ToString();

            Regex regex = new Regex(@"api/Isporuka/(\w+)");
            Match match = regex.Match(pom);
            pom = match.Groups[1].Value;
            int isporukaID = Convert.ToInt32(pom);
            //doza sa isporukaID
            for (int i = 0; i < dozaZaIsporuku.Count; i++)
            {
                dozaZaIsporuku[i].IsporukaId = isporukaID;
                REST.Put_ID("DozaKrvi", dozaZaIsporuku[i].DozaKrviId, dozaZaIsporuku[i]);
            }




            //zahtjev na isporucen
            pomZahtjev.ZahtjevId = zahtjev.ZahtjevIdd;
            pomZahtjev.ZahtjevanaKolicina = zahtjev.ZahtjevanaKolicinaa;
            pomZahtjev.TipKrvnogDerivata = zahtjev.TipKrvnogDerivataa;
            pomZahtjev.KrvnaGrupaZahtjev = zahtjev.KrvnaGrupaZahtjevv;
            pomZahtjev.ZahtjevPrihvacen = 1;
            pomZahtjev.DatumPodnosenjaZahtjeva = zahtjev.DatumPodnosenjaZahtjevaa;
            pomZahtjev.ZdravstvenaUstanovaId = zahtjev.UstanovaId;
            pomZahtjev.Napomena = null;
            REST.Put_ID("zahtjev", pomZahtjev.ZahtjevId, pomZahtjev);
            //promjena stanja magacina
            mag = JsonConvert.DeserializeObject<List<Magacin>>(REST.GetMagacin(pomZahtjev.KrvnaGrupaZahtjev));

            MessageBox.Show(dozaZaIsporuku.Count.ToString());
            switch (pomZahtjev.TipKrvnogDerivata)
            {
                case "Krv":
                    mag[0].BrojDozaKrvi = mag[0].BrojDozaKrvi - dozaZaIsporuku.Count;
                    MessageBox.Show(mag[0].BrojDozaKrvi.ToString());

                    break;
                case "Plazma":
                    mag[0].BrojDozaPlazme = mag[0].BrojDozaPlazme- dozaZaIsporuku.Count;
                    MessageBox.Show(mag[0].BrojDozaPlazme.ToString());

                    break;
                case "Eritrociti":
                    mag[0].BrojDozaEritrocita = mag[0].BrojDozaEritrocita - dozaZaIsporuku.Count;
                    MessageBox.Show(mag[0].BrojDozaEritrocita.ToString());

                    break;
                case "Trombociti":
                    mag[0].BrojDozaTrombocita = mag[0].BrojDozaTrombocita - dozaZaIsporuku.Count;
                    MessageBox.Show(mag[0].BrojDozaTrombocita.ToString());

                    break;
            }
            REST.Put_ID("magacin", mag[0].MagacinId, mag[0]);



            NavigationService.Navigate(new RequestsPage());

        }

        private void datagridDeclined_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            listDoses.Items.Clear();

            if (this.datagridDeclined.SelectedItem != null)
            {
                btnDecline.Visibility = Visibility.Hidden;
                btnDeliver.Visibility = Visibility.Hidden;

            }
        }

        private void btnDecline_Click(object sender, RoutedEventArgs e)
        {
            Pom zahtjev = this.datagridPending.SelectedItem as Pom;
            Zahtjev pomZahtjev = new Zahtjev();

            pomZahtjev.ZahtjevId = zahtjev.ZahtjevIdd;
            pomZahtjev.ZahtjevanaKolicina = zahtjev.ZahtjevanaKolicinaa;
            pomZahtjev.TipKrvnogDerivata = zahtjev.TipKrvnogDerivataa;
            pomZahtjev.KrvnaGrupaZahtjev = zahtjev.KrvnaGrupaZahtjevv;
            pomZahtjev.ZahtjevPrihvacen = 2;
            pomZahtjev.DatumPodnosenjaZahtjeva = zahtjev.DatumPodnosenjaZahtjevaa;
            pomZahtjev.ZdravstvenaUstanovaId = zahtjev.UstanovaId;
            pomZahtjev.Napomena = null;
            REST.Put_ID("zahtjev", pomZahtjev.ZahtjevId, pomZahtjev);

            NavigationService.Navigate(new RequestsPage());


        }
    }
}
