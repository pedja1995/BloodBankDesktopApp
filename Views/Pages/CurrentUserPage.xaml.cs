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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for CurrentUser.xaml
    /// </summary>
    public partial class CurrentUserPage : Page
    {
        Radnik radnik = null;
        List<Kontakt> kontakti = null;
        Kontakt kontakt = null;
        Lokacija lokacija = null;
        public CurrentUserPage(Radnik radnik)
        {
            InitializeComponent();
            btnCancel.Visibility = Visibility.Hidden;

            this.radnik = radnik;
            kontakti = JsonConvert.DeserializeObject<List<Kontakt>>(REST.JoinKontaktRadnik(radnik.RadnikId));
            lokacija = kontakti[0].Lokacija;
            kontakt = kontakti[0];
            txtName.IsEnabled = false;
            txtLastname.IsEnabled = false;
            txtUsername.IsEnabled = false;
            passPasword.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtPhone.IsEnabled = false;
            txtAddress.IsEnabled = false;
            txtPlace.IsEnabled = false;

            txtID.Text = radnik.RadnikId.ToString();
            txtWorkingPlace.Text = radnik.RadnoMjesto;
            txtName.Text = radnik.Ime;
            txtLastname.Text = radnik.Prezime;
            txtUsername.Text = radnik.KorisnickoIme;
            passPasword.Password = radnik.Lozinka;

            txtEmail.Text = kontakt.Email;
            txtPhone.Text = kontakt.BrojTelefona;

            txtAddress.Text = lokacija.Adresa;
            txtPlace.Text = lokacija.Mjesto;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
           
            if (btnChange.Content.ToString() == "Promjeni podatke")
            {
                txtName.IsEnabled = true;
                txtLastname.IsEnabled = true;
                txtUsername.IsEnabled = true;
                passPasword.IsEnabled = true;
                txtEmail.IsEnabled = true;
                txtPhone.IsEnabled = true;
                txtAddress.IsEnabled = true;
                txtPlace.IsEnabled = true;
                btnChange.Content = "Sačuvaj izmjene";
                btnCancel.Visibility = Visibility.Visible;
            }
            else if(btnChange.Content.ToString()=="Sačuvaj izmjene")
            {
                btnCancel.Visibility = Visibility.Hidden;
                radnik.Ime = txtName.Text;
                radnik.Prezime = txtLastname.Text;
                radnik.KorisnickoIme = txtUsername.Text;
                radnik.Lozinka = passPasword.Password;

                kontakt.Email = txtEmail.Text;
                kontakt.BrojTelefona = txtPhone.Text;

                lokacija.Adresa = txtAddress.Text;
                lokacija.Mjesto = txtPlace.Text;

                REST.Put_ID("lokacija", lokacija.LokacijaId, lokacija);
                REST.Put_ID("kontakt", kontakt.KontaktId, kontakt);
                REST.Put_ID("radnik", radnik.RadnikId, radnik);

                btnChange.Content = "Promjeni podatke";
                txtName.IsEnabled = false;
                txtLastname.IsEnabled = false;
                txtUsername.IsEnabled = false;
                passPasword.IsEnabled = false;
                txtEmail.IsEnabled = false;
                txtPhone.IsEnabled = false;
                txtAddress.IsEnabled = false;
                txtPlace.IsEnabled = false;
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            btnCancel.Visibility = Visibility.Hidden;
            NavigationService.Navigate(new CurrentUserPage(radnik));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StoragePage());

        }

        private void btnBack_MouseEnter(object sender, MouseEventArgs e)
        {
            btnBack.Opacity = 0.7;
        }

        private void btnBack_MouseLeave(object sender, MouseEventArgs e)
        {
            btnBack.Opacity = 1;

        }
    }
}
