using DesktopApp.Models;
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
using Newtonsoft.Json;
using System.Diagnostics.Tracing;

namespace DesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for InstitutionInfoPage.xaml
    /// </summary>
    public partial class InstitutionInfoPage : Page
    {
        ZdravstvenaUstanova zdravstvenaUstanova = null;
        List<Kontakt> kontakti = null;
        Lokacija lokacija = null;
        Kontakt kontakt = null;
        List<Zahtjev> zahtjev = new List<Zahtjev>();
        public InstitutionInfoPage()
        {
            InitializeComponent();
        }
        public InstitutionInfoPage(ZdravstvenaUstanova ustanova)
        {
            InitializeComponent();

                btnChange.Content = "Izmjeni podatke";
            

            txtAddress.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtPlace.IsEnabled = false;
            txtPhone.IsEnabled = false;
            txtName.IsEnabled = false;

            //     kontakt = JsonConvert.DeserializeObject<ZdravstvenaUstanova>(REST.Get_ID("kontakt"));


            zdravstvenaUstanova = new ZdravstvenaUstanova();
            zdravstvenaUstanova = ustanova;

            txtName.Text = zdravstvenaUstanova.Naziv;
            txtIdentity.Text = zdravstvenaUstanova.VerifikacioniKod;

            kontakti = JsonConvert.DeserializeObject<List<Kontakt>>(REST.JoinKontaktUstanova(ustanova.ZdravstvenaUstanovaId));

            lokacija = kontakti[0].Lokacija;
            kontakt = kontakti[0];

            txtEmail.Text = kontakt.Email;
            txtPhone.Text = kontakt.BrojTelefona;

            txtAddress.Text = lokacija.Adresa;
            txtPlace.Text = lokacija.Mjesto;

            zahtjev = JsonConvert.DeserializeObject<List<Zahtjev>>(REST.GetZahtjevInstitucija(zdravstvenaUstanova.ZdravstvenaUstanovaId));
           
            for (int i = 0; i < zahtjev.Count; i++)
            {
                if (zahtjev[i].ZahtjevPrihvacen==0)
                {
                    gridRequestsOnWait.Items.Add(zahtjev[i]);
                }
                else if(zahtjev[i].ZahtjevPrihvacen == 1)
                {
                    gridRequestsFinished.Items.Add(zahtjev[i]);

                }
                else if (zahtjev[i].ZahtjevPrihvacen == 2)
                {
                    gridRequestsDeclined.Items.Add(zahtjev[i]);

                }
            }


        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (btnChange.Content.ToString() == "Izmjeni podatke")
            {
                btnChange.Content = "Sačuvaj izmjene";

                txtAddress.IsEnabled = true;
                txtEmail.IsEnabled = true;
                txtPlace.IsEnabled = true;
                txtPhone.IsEnabled = true;
                txtName.IsEnabled = true;
            }
            else if (btnChange.Content.ToString() == "Sačuvaj izmjene")
            {
                btnChange.Content = "Izmjeni podatke";
                txtAddress.IsEnabled = false;
                txtEmail.IsEnabled = false;
                txtPlace.IsEnabled = false;
                txtPhone.IsEnabled = false;
                txtName.IsEnabled = false;

                kontakt.Email = txtEmail.Text;
                kontakt.BrojTelefona = txtPhone.Text;

                lokacija.Adresa = txtAddress.Text;
                lokacija.Mjesto = txtPlace.Text;

                zdravstvenaUstanova.Naziv=txtName.Text;
                zdravstvenaUstanova.VerifikacioniKod = txtIdentity.Text;

                REST.Put_ID("kontakt", kontakt.KontaktId, kontakt);
                REST.Put_ID("lokacija", lokacija.LokacijaId, lokacija);
                REST.Put_ID("zdravstvena_ustanova", zdravstvenaUstanova.ZdravstvenaUstanovaId, zdravstvenaUstanova);

            }
        }

        private void btnBack_MouseEnter(object sender, MouseEventArgs e)
        {
            btnBack.Opacity = 0.7;

        }

        private void btnBack_MouseLeave(object sender, MouseEventArgs e)
        {
            btnBack.Opacity = 1;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new InstitutionsPage());
        }
    }
}
