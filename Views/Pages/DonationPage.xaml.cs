using DesktopApp.Models;
using Newtonsoft.Json;
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

namespace DesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for Donation.xaml
    /// </summary>
    public partial class DonationPage : Page
    {
        List<Donacija> donacije = null;
        Donacija donacija = new Donacija();
        List<LjekarskiPregled> ljekarskiPregledi = null;
        LjekarskiPregled ljekarski = null;
        List<DozaKrvi> doze = null;
        //List<Donor> donori = null;
        Donor donor = new Donor();
        public DonationPage()
        {
            InitializeComponent();
            donacije = JsonConvert.DeserializeObject<List<Donacija>>(REST.GetAll("donacija"));

            for (int i = 0; i < donacije.Count; i++)
            {
                datagridDonations.Items.Add(donacije[i]);
            }
        }
        private void btnAdd_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Opacity = 0.7;
        }

        private void btnAdd_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Opacity = 1;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDonationPage());
        }

        private void datagridDonations_RowActivated(object sender, Telerik.Windows.Controls.GridView.RowEventArgs e)
        {
            /*
            Donacija donacija = this.datagridDonations.SelectedItem as Donacija;

            NavigationService.Navigate(new DonationInfoPage(donacija));*/
        }

        private void datagridDonations_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            txtBloodId.Text = "";
            txtPlazmaId.Text = "";
            txtRedCellsId.Text = "";
            txtPlateletsId.Text = "";
            
            donacija = this.datagridDonations.SelectedItem as Donacija;

            ljekarskiPregledi = JsonConvert.DeserializeObject<List<LjekarskiPregled>>(REST.JoinDonacijaLjekarski(donacija.DonacijaId));
            ljekarski = ljekarskiPregledi[0];

            doze = JsonConvert.DeserializeObject<List<DozaKrvi>>(REST.JoinDonacijaDoza(donacija.DonacijaId));

            txtBloodPressure.Text = ljekarski.KrvniPritisak;
            txtHemoglobineLevel.Text = ljekarski.NivoHemoglobina.ToString();
            txtPulse.Text = ljekarski.Puls;
            txtTemperature.Text = ljekarski.Temperatura.ToString();
            txtWeight.Text = ljekarski.TezinaKg.ToString();
            txtNote.Document.Blocks.Clear();
            txtNote.Document.Blocks.Add(new Paragraph(new Run(ljekarski.Napomena)));

            donor = JsonConvert.DeserializeObject<Donor>(REST.Get_ID("donor", donacija.DonorId.Value));
            txtId.Text = donor.RegistarskiBroj;
            txtName.Text = donor.Ime;
            txtLastName.Text = donor.Prezime;
            txtBloodType.Text = donor.KrvnaGrupaDonor;

            if (doze.Count > 1)
            {
                txtPlazmaId.Text = doze[0].DozaKrviId.ToString();
                txtRedCellsId.Text = doze[1].DozaKrviId.ToString();
                txtPlateletsId.Text = doze[2].DozaKrviId.ToString();
            }
            else
            {
                txtBloodId.Text = doze[0].DozaKrviId.ToString();
            }
        }
    }
}
