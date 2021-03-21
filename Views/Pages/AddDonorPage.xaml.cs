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

namespace DesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for AddDonorxaml.xaml
    /// </summary>
    public partial class AddDonorPage : Page
    {
        int br_donora;
        char pol;
        public AddDonorPage()
        {
            InitializeComponent();

            List<Donor> donori = JsonConvert.DeserializeObject<List<Donor>>(REST.GetAll("donor"));

            if (donori.Count > 0)
                br_donora = donori[donori.Count - 1].DonorId + 1;
            else
                br_donora = 1;

        }

        private void comboBloodType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RadComboBoxItem selectedItem = comboBloodType.SelectedItem as RadComboBoxItem;

            imgBloodType.Source = new ImageSourceConverter().ConvertFromString(@"../../Resources/Blood_types/blood_" + selectedItem.Content.ToString().ToLower() + ".png") as ImageSource;



        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtName.Text != null && txtName.Text != "" && txtLastname.Text != null && txtLastname.Text != "")
                txtID.Text = txtName.Text[0].ToString().ToLower() + txtLastname.Text[0].ToString().ToLower() + br_donora.ToString();
        }

        private void txtLastname_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (txtName.Text != null && txtName.Text != "" && txtLastname.Text != null && txtLastname.Text != "")
                txtID.Text = txtName.Text[0].ToString().ToLower() + txtLastname.Text[0].ToString().ToLower() + br_donora.ToString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RadComboBoxItem selectedItem = comboSex.SelectedItem as RadComboBoxItem;
            Kontakt kontakt = new Kontakt();
            Lokacija lokacija = new Lokacija();



            string pom1 = REST.EmailCheck(txtEmail.Text.ToString());
            if (pom1.ToString()=="[]")
            {
                if (txtAddress.Text != null && txtAddress.Text != "" && txtPlace.Text != null && txtPlace.Text != "" && txtEmail.Text != null && txtEmail.Text != "" && txtPhone.Text != null && txtPhone.Text != "" && txtName.Text != null && txtName.Text != "" && txtLastname.Text != null && txtLastname.Text != ""
            && txtID.Text != null && txtID.Text != "" && passPassword.Password != null && passPassword.Password != "" && dateDateOfBirth.SelectedDate != null && selectedItem.Content.ToString() != null && selectedItem.Content.ToString() != "")
                {
                    if (selectedItem.Content.ToString() == "Ž")
                        pol = 'z';
                    else
                        pol = 'm';
                    selectedItem = comboBloodType.SelectedItem as RadComboBoxItem;
                    Donor donor = new Donor();
                    donor.Ime = txtName.Text;
                    donor.Prezime = txtLastname.Text;
                    donor.Pol = pol.ToString();
                    donor.RegistarskiBroj = txtID.Text;
                    donor.Lozinka = passPassword.Password;
                    donor.DatumRodjenja = dateDateOfBirth.SelectedDate;
                    donor.KrvnaGrupaDonor = selectedItem.Content.ToString();
                    lokacija.Mjesto = txtPlace.Text;
                    lokacija.Adresa = txtAddress.Text;
                    kontakt.BrojTelefona = txtPhone.Text;
                    kontakt.Email = txtEmail.Text;


                    //upis u tabelu donor
                    var response = REST.Post("donor", donor);

                    //donor ID
                    string pom = response.Headers.Location.ToString();

                    Regex regex = new Regex(@"api/Donor/(\w+)");
                    Match match = regex.Match(pom);
                    pom = match.Groups[1].Value;
                    int ID = Convert.ToInt32(pom);
                    //
                    kontakt.DonorId = ID;
                    //upis u tabelu lokacija
                    response = REST.Post("lokacija", lokacija);
                    //lokacija ID
                    pom = response.Headers.Location.ToString();

                    regex = new Regex(@"api/Lokacija/(\w+)");
                    match = regex.Match(pom);
                    pom = match.Groups[1].Value;
                    ID = Convert.ToInt32(pom);
                    //
                    kontakt.LokacijaId = ID;

                    //upis u tabelu kontakt
                    REST.Post("kontakt", kontakt);

                    NavigationService.Navigate(new DonorsPage());



                }



            }
            else
            {
                txtEmail.Foreground = Brushes.Red;
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DonorsPage());

        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtEmail.Foreground == Brushes.Red)
                txtEmail.Foreground = Brushes.Black;

        }
    }
}
