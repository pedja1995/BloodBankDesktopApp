using DesktopApp.Models;
using Newtonsoft.Json;
using ServiceStack;
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
using Telerik.Windows.Diagrams.Core;

namespace DesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for DonorInfoPage.xaml
    /// </summary>
    public partial class DonorInfoPage : Page
    {
        Donor donor = null;
        Lokacija lokacija = null;
        List<Kontakt> kontakti = null;
        Kontakt kontakt = null;
        List<Donacija> donacije = null;
        public DonorInfoPage()
        {
         
            InitializeComponent();
            txtID.IsEnabled = false;
            txtName.IsEnabled = false;
            txtLastname.IsEnabled = false;
            txtAddress.IsEnabled = false;
            txtPlace.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtPhone.IsEnabled = false;
            txtNextDate.IsEnabled = false;
            txtDateOfBirth.IsEnabled = false;
            txtSex.IsEnabled = false;
        }


        public DonorInfoPage(Donor donor)
        {
            InitializeComponent();
            txtID.IsEnabled = false;
            txtName.IsEnabled = false;
            txtLastname.IsEnabled = false;
            txtAddress.IsEnabled = false;
            txtPlace.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtPhone.IsEnabled = false;
            txtNextDate.IsEnabled = false;
            txtDateOfBirth.IsEnabled = false;
            txtSex.IsEnabled = false;
            imgBloodType.Source = new ImageSourceConverter().ConvertFromString(@"../../Resources/Blood_types/blood_" + donor.KrvnaGrupaDonor.ToString().ToLower() + ".png") as ImageSource;


            kontakti = JsonConvert.DeserializeObject<List<Kontakt>>(REST.JoinKontaktDonor(donor.DonorId));
            kontakt = kontakti[0];
            this.donor = donor;
            lokacija = kontakt.Lokacija;
            txtDateOfBirth.Text = donor.DatumRodjenja.ToString();
            txtID.Text = donor.RegistarskiBroj;
            txtName.Text = donor.Ime;
            txtLastname.Text = donor.Prezime;
            txtAddress.Text = lokacija.Adresa;
            txtPlace.Text = lokacija.Mjesto;
            txtEmail.Text = kontakt.Email;
            txtPhone.Text = kontakt.BrojTelefona;
            if(donor.DatumNajranijegSledecegDoniranja.HasValue)
            txtNextDate.Text = donor.DatumNajranijegSledecegDoniranja.Value.ToString();
            if (donor.Pol == "m")
                txtSex.Text = "Muški";
            else
                txtSex.Text = "Ženski";

            donacije = JsonConvert.DeserializeObject<List<Donacija>>(REST.JoinDonacijaDonor(donor.DonorId));

            for (int i = 0; i < donacije.Count; i++)
            {
                datagridHistory.Items.Add(donacije[i]);
            }



        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (btnChange.Content.ToString() == "Izmjeni podatke")
            {
                btnChange.Content = "Sačuvaj izmjene";
                //txtID.IsEnabled = true;
                txtAddress.IsEnabled = true;
                txtPlace.IsEnabled = true;
                txtEmail.IsEnabled = true;
                txtPhone.IsEnabled = true;
                //   txtNextDate.IsEnabled = true;


            }
            else if (btnChange.Content.ToString() == "Sačuvaj izmjene")
            {
                btnChange.Content = "Izmjeni podatke";

         //       donor.Ime = txtName.Text;
               // donor.KrvnaGrupaDonor
            //    donor.Prezime = txtLastname.Text;
                lokacija.Adresa = txtAddress.Text;
                lokacija.Mjesto = txtPlace.Text;
                kontakt.Email = txtEmail.Text;
                kontakt.BrojTelefona = txtPhone.Text;

                txtAddress.IsEnabled = false;
                txtPlace.IsEnabled = false;
                txtEmail.IsEnabled = false;
                txtPhone.IsEnabled = false;






                REST.Put_ID("kontakt", kontakt.KontaktId, kontakt);
                REST.Put_ID("lokacija", lokacija.LokacijaId, lokacija);


            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DonorsPage());
        }

       
    }
}
