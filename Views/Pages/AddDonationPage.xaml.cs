using DesktopApp.Models;
using Newtonsoft.Json;
using ServiceStack;
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
using Telerik.Windows.Documents.FormatProviders.Txt;

namespace DesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class AddDonationPage : Page
    {
        List<Donor> donori = null;
        Donor donor = null;
        Donacija donacija = new Donacija();
        LjekarskiPregled ljekarski = new LjekarskiPregled();
        List<DozaKrvi> doze = null;
        DozaKrvi doza = new DozaKrvi();
        List<Magacin> magacin = null;
        DateTime danas = DateTime.Now;
        int dozaKrviID;
        enum Expires
        {
            blood = 35,
            redCells = 35,
            platelets = 5,
            plasma = 365,
        };
        public AddDonationPage()
        {
            InitializeComponent();

            magacin = JsonConvert.DeserializeObject<List<Magacin>>(REST.GetAll("magacin"));

            donori = JsonConvert.DeserializeObject<List<Donor>>(REST.GetAll("donor"));
            int j;
            for (int i = 0; i < donori.Count; i++)
            {
                j = DateTime.Compare(donori[i].DatumNajranijegSledecegDoniranja.GetValueOrDefault(), danas);
                if (j <= 0)
                    datagridDonors.Items.Add(donori[i]);
            }
            dateDateOfDonation.SelectedDate = DateTime.Now;
            btnDivide.IsChecked = false;

            btnDivide.IsEnabled = true;
            
           doze = JsonConvert.DeserializeObject<List<DozaKrvi>>(REST.GetAll("dozakrvi"));

            if (doze.Count > 0)
            {
                dozaKrviID = doze[doze.Count - 1].DozaKrviId + 1;
                
            }
            else 
            {
                dozaKrviID = 1;
              
            }

            txtBloodId.Text = dozaKrviID.ToString();


        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DonationPage());
        }
        private void btnBack_MouseEnter(object sender, MouseEventArgs e)
        {
           btnBack.Opacity = 0.7;
        }

        private void btnBack_MouseLeave(object sender, MouseEventArgs e)
        {
            btnBack.Opacity = 1;

        }

        private void btnDivide_Activate(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RadComboBoxItem donationType = cmbDonationType.SelectedItem as RadComboBoxItem;

            if (btnDivide.IsChecked.GetValueOrDefault())
            {
                txtBloodId.Text = "";

                txtPlasmaId.Text = (dozaKrviID).ToString();
                txtPlateletsId.Text = (dozaKrviID+2).ToString();
                txtRedCellsId.Text = (dozaKrviID+1).ToString();

            }
            else
            {
                txtBloodId.Text = "";
                txtPlasmaId.Text = "";
                txtPlateletsId.Text = "";
                txtRedCellsId.Text = "";
                txtBloodId.Text = dozaKrviID.ToString();

                /*   if (donationType.Content.ToString() == "Krv")
                       txtBloodId.Text = dozaKrviID.ToString();
                   else if (donationType.Content.ToString() == "Plazma")
                       txtPlasmaId.Text = dozaKrviID.ToString();
                   else if (donationType.Content.ToString() == "Eritrociti")
                       txtRedCellsId.Text = dozaKrviID.ToString();
                   else if (donationType.Content.ToString() == "Trombociti")
                       txtPlateletsId.Text = dozaKrviID.ToString();*/
            }
        }
        /*
        private void cmbDonationType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RadComboBoxItem item = cmbDonationType.SelectedItem as RadComboBoxItem;
            if (item.Content == "Krv")
            {
                btnDivide.IsEnabled = true;
            }
            else
            {
                btnDivide.IsChecked = false;
                btnDivide.IsEnabled = false;
            }
        }
        */
        private void datagridDonors_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
           donor = this.datagridDonors.SelectedItem as Donor;
            txtID.Text = donor.RegistarskiBroj;
            txtName.Text = donor.Ime;
            txtLastName.Text = donor.Prezime;
            txtBloodType.Text = donor.KrvnaGrupaDonor.ToUpper();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //RadComboBoxItem donationType = cmbDonationType.SelectedItem as RadComboBoxItem;
           // string napomena = new TextRange(txtNote.Document.ContentStart, txtNote.Document.ContentEnd).Text;
            DateTime datumDoniranja = dateDateOfDonation.SelectedDate.Value;
           

            if (!txtBlooPressure.Text.IsNullOrEmpty() && !txtHemoglobineLevel.Text.IsNullOrEmpty() && !txtTemperature.Text.IsNullOrEmpty() && !txtPulse.Text.IsNullOrEmpty()
                && !txtID.Text.IsNullOrEmpty() && datagridDonors.Items.Count>0)
            {
                if (donor.Pol == "m")

                    datumDoniranja = datumDoniranja.AddDays(90);
                else
                    datumDoniranja = datumDoniranja.AddDays(120);
                            string napomena = new TextRange(txtNote.Document.ContentStart, txtNote.Document.ContentEnd).Text;


                donacija.DatumDoniranja = dateDateOfDonation.SelectedDate;
                donacija.TipDonacije = "Krv";
                donacija.DonorId = donor.DonorId;
              //  donacija.Donor = donor;
                //donacija ID
               var response = REST.Post("Donacija", donacija);
               string  pom = response.Headers.Location.ToString();

                Regex regex = new Regex(@"api/Donacija/(\w+)");
                Match match = regex.Match(pom);
                pom = match.Groups[1].Value;
               int ID = Convert.ToInt32(pom);
                //ljekarski
                ljekarski.NivoHemoglobina = txtHemoglobineLevel.Text.ToDecimal();
                ljekarski.KrvniPritisak = txtBlooPressure.Text;
                ljekarski.Temperatura = txtTemperature.Text.ToDecimal();
                ljekarski.Puls = txtPulse.Text;
                ljekarski.Napomena = napomena;
                ljekarski.DonacijaId = ID;

                REST.Post("LjekarskiPregled", ljekarski);

                //donor
                donor.DatumPoslednjegDoniranja = dateDateOfDonation.SelectedDate;
                donor.DatumNajranijegSledecegDoniranja = datumDoniranja;
                donor.TipPoslednjegDoniranja = "Krv";
                REST.Put_ID("donor", donor.DonorId, donor);


                //doze
                doza.DonacijaId = ID;
              //  doza.Donacija = donacija;
                doza.IstekaoRok = 0;
                doza.KrvnaGrupaDoza = donor.KrvnaGrupaDonor;


                if (btnDivide.IsChecked.GetValueOrDefault())
                {
                    DateTime pomDate = dateDateOfDonation.SelectedDate.Value;
                    pomDate = pomDate.AddDays((int)Expires.plasma);
                    doza.DatumIstekaRoka = pomDate;
                    doza.TipKrvnogDerivata = "Plazma";
                    doza.KolicinaKrvnogDerivataMl = (short)247;
                    REST.Post("DozaKrvi", doza);

                    pomDate = dateDateOfDonation.SelectedDate.Value;
                    pomDate = pomDate.AddDays((int)Expires.redCells);
                    doza.DatumIstekaRoka = pomDate;
                    doza.TipKrvnogDerivata = "Eritrociti";
                    doza.KolicinaKrvnogDerivataMl = (short)198;
                    REST.Post("DozaKrvi", doza);

                    pomDate = dateDateOfDonation.SelectedDate.Value;
                    pomDate = pomDate.AddDays((int)Expires.platelets);
                    doza.DatumIstekaRoka = pomDate;
                    doza.TipKrvnogDerivata = "Trombociti";
                    doza.KolicinaKrvnogDerivataMl = (short)5;
                    REST.Post("DozaKrvi", doza);

                    /* switch (doza.KrvnaGrupaDoza)
                     {
                         case "A+":

                             break;
                         case "A-":
                             break;
                         case "B+":
                             break;
                         case "B-":
                             break;
                         case "O+":
                             break;
                         case "O-":
                             break;
                         case "AB+":
                             break;
                         case "AB-":
                             break;

                     }*/
                    int i;
                    for (i = 0; i < 8; i++)
                    {
                        if(magacin[i].KrvnaGrupaMagacin == doza.KrvnaGrupaDoza)
                        {
                            magacin[i].BrojDozaEritrocita++;
                            magacin[i].BrojDozaPlazme++;
                            magacin[i].BrojDozaTrombocita++;
                            break;
                        }
                    }
                    REST.Put_ID("magacin", magacin[i].MagacinId, magacin[i]);

                }
                else
                {
                    
                        DateTime pomDate = dateDateOfDonation.SelectedDate.Value;
                       pomDate= pomDate.AddDays((int)Expires.blood);
                        doza.DatumIstekaRoka = pomDate;
                        doza.TipKrvnogDerivata = "Krv";
                        doza.KolicinaKrvnogDerivataMl = (short)450;
                        REST.Post("DozaKrvi", doza);
                    int i;
                    for ( i = 0; i < 8; i++)
                    {
                        if (magacin[i].KrvnaGrupaMagacin == doza.KrvnaGrupaDoza)
                        {
                            magacin[i].BrojDozaKrvi++;
                            break;
                        }
                    }
                    REST.Put_ID("magacin", magacin[i].MagacinId, magacin[i]);

                    /*
                    else if (donationType.Content.ToString() == "Plazma")
                    {
                        DateTime pomDate = dateDateOfDonation.SelectedDate.Value;
                        pomDate.AddDays((int)Expires.plasma);
                        doza.DatumIstekaRoka = pomDate;
                        doza.TipKrvnogDerivata = "Plazma";
                        doza.KolicinaKrvnogDerivataMl = (short)450;
                        REST.Post("DozaKrvi", doza);
                    }
                    else if (donationType.Content.ToString() == "Eritrociti")
                    {
                        DateTime pomDate = dateDateOfDonation.SelectedDate.Value;
                        pomDate.AddDays((int)Expires.redCells);
                        doza.DatumIstekaRoka = pomDate;
                        doza.TipKrvnogDerivata = "Eritrociti";
                        doza.KolicinaKrvnogDerivataMl = (short)450;
                        REST.Post("DozaKrvi", doza);
                    }
                    else if (donationType.Content.ToString() == "Trombociti")
                    {
                        DateTime pomDate = dateDateOfDonation.SelectedDate.Value;
                        pomDate.AddDays((int)Expires.platelets);
                        doza.DatumIstekaRoka = pomDate;
                        doza.TipKrvnogDerivata = "Trombociti";
                        doza.KolicinaKrvnogDerivataMl = (short)450;
                        REST.Post("DozaKrvi", doza);
                    }
                    */

                }
                NavigationService.Navigate(new DonationPage());

            }

        }
    }
}
