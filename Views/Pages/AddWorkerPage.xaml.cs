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
using Telerik.Windows.Controls.Animation;

namespace DesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for WorkerInfoPage.xaml
    /// </summary>
    public partial class AddWorkerPage : Page
    {
        Kontakt kontakt = null;
        Lokacija lokacija = null;
        Radnik radnik = null;
        Radnik pom = null;
        public AddWorkerPage()
        {
            InitializeComponent();
            dateDateOfEmployment.SelectedDate = DateTime.Now;
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

            NavigationService.Navigate(new WorkersPage());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
           
                if (txtAddress.Text != null && txtAddress.Text != "" && txtPlace.Text != null && txtPlace.Text != "" && txtEmail.Text != null && txtEmail.Text != "" && txtPhone.Text != null && txtPhone.Text != "" && txtWorkingPlace.Text != null && txtWorkingPlace.Text != "" && txtName.Text != null && txtName.Text != "" && txtLastname.Text != null && txtLastname.Text != ""
                 && txtUsername.Text != null && txtUsername.Text != "" && passPassword.Password != null && passPassword.Password != "" && dateDateOfEmployment.SelectedDate != null && dateDateOfBirth.SelectedDate != null)
                {
                    kontakt = new Kontakt();
                    lokacija = new Lokacija();
                    radnik = new Radnik();

                    lokacija.Adresa = txtAddress.Text;
                    lokacija.Mjesto = txtPlace.Text;

                    kontakt.Email = txtEmail.Text;
                    kontakt.BrojTelefona = txtPhone.Text;

                    radnik.RadnoMjesto = txtWorkingPlace.Text;
                    radnik.Ime = txtName.Text;
                    radnik.Prezime = txtLastname.Text;
                    radnik.KorisnickoIme = txtUsername.Text;
                    radnik.Lozinka = passPassword.Password;
                    radnik.DatumZaposlenja = dateDateOfEmployment.SelectedDate;
                    radnik.DatumRodjenja = dateDateOfBirth.SelectedDate;

                    //string jsonString = JsonConvert.SerializeObject(radnik);

                    //upis u tabelu radnik
                    var response = REST.Post("radnik", radnik);

                    //radnik ID
                    string pom = response.Headers.Location.ToString();

                    Regex regex = new Regex(@"api/Radnik/(\w+)");
                    Match match = regex.Match(pom);
                    pom = match.Groups[1].Value;
                    int ID = Convert.ToInt32(pom);
                    //
                    kontakt.RadnikId = ID;
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

                    NavigationService.Navigate(new WorkersPage());
                }


        }
    }
}
