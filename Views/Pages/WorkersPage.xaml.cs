using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
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
using DesktopApp.Models;
using Newtonsoft.Json;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace DesktopApp.Views.Pages
{

    /// <summary>
    /// Interaction logic for Workers.xaml
    /// </summary>
    public partial class WorkersPage : Page
    {
        List<Radnik> radnici = null;
        List<Kontakt> kontakti = null;
        Radnik radnik = null;
        Radnik pom = null;
        Kontakt kontakt = null;
       Lokacija lokacija = null;
        public WorkersPage()
        {


            InitializeComponent();
            lblPassword.Visibility = Visibility.Hidden;
            passPassword.Visibility = Visibility.Hidden;
            btnChange.IsEnabled = false;
            radnici = JsonConvert.DeserializeObject<List<Radnik>>(REST.GetAll("radnik"));

            for (int i = 0; i < radnici.Count; i++)
            {
                gridWorkers.Items.Add(radnici[i]);
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

        private void gridWorkers_RowActivated(object sender, RowEventArgs e)
        {
            /*
            radnik = this.gridWorkers.SelectedItem as Radnik;
 
            if (btnChange.Content == "Izmjeni podatke")
            {
                lblPassword.Visibility = Visibility.Hidden;
                passPassword.Visibility = Visibility.Hidden;
            }
            btnChange.IsEnabled = true;

            kontakti = JsonConvert.DeserializeObject<List<Kontakt>>(REST.JoinKontaktRadnik(radnik.RadnikId));

            kontakt = kontakti[0];
            lokacija = kontakt.Lokacija;


          
            txtID.Text = radnik.RadnikId.ToString();
            txtName.Text = radnik.Ime;
            txtLastname.Text = radnik.Prezime;
            txtUsername.Text = radnik.KorisnickoIme;
            txtWorkingPlace.Text = radnik.RadnoMjesto;
            txtPhone.Text = kontakt.BrojTelefona;
            txtEmail.Text = kontakt.Email;
            txtAddress.Text = lokacija.Adresa;
            txtPlace.Text = lokacija.Mjesto;
            if (radnik.Admin == 1)
            {
                checkAdmin.IsChecked = true;
            }
            else
                checkAdmin.IsChecked = false;
            */
            
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (btnChange.Content.ToString() == "Izmjeni podatke")
            {
                btnChange.Content = "Sačuvaj izmjene";

                txtUsername.IsEnabled = true;
                txtWorkingPlace.IsEnabled = true;

                checkAdmin.IsEnabled = true;
                lblPassword.Visibility = Visibility.Visible;
                passPassword.Visibility = Visibility.Visible;
                passPassword.Password = radnik.Lozinka;
            }
            else if (btnChange.Content.ToString() == "Sačuvaj izmjene")
            {
                btnChange.Content = "Izmjeni podatke";
                radnik.RadnoMjesto = txtWorkingPlace.Text;
                radnik.Lozinka = passPassword.Password;
                radnik.KorisnickoIme = txtUsername.Text;
                if (checkAdmin.IsChecked.Value)
                {
                    radnik.Admin = 1;
                }
                else
                    radnik.Admin = 0;



               // string jsonRadnik = JsonConvert.SerializeObject(radnik);

                REST.Put_ID("radnik", radnik.RadnikId, radnik);
                txtUsername.IsEnabled = false;
                txtWorkingPlace.IsEnabled = false;

                checkAdmin.IsEnabled = false;
                lblPassword.Visibility = Visibility.Hidden;
                passPassword.Visibility = Visibility.Hidden;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddWorkerPage());
        }

        private void gridWorkers_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            radnik = this.gridWorkers.SelectedItem as Radnik;

            if (btnChange.Content == "Izmjeni podatke")
            {
                lblPassword.Visibility = Visibility.Hidden;
                passPassword.Visibility = Visibility.Hidden;
            }
            btnChange.IsEnabled = true;

            kontakti = JsonConvert.DeserializeObject<List<Kontakt>>(REST.JoinKontaktRadnik(radnik.RadnikId));

            kontakt = kontakti[0];
            lokacija = kontakt.Lokacija;



            txtID.Text = radnik.RadnikId.ToString();
            txtName.Text = radnik.Ime;
            txtLastname.Text = radnik.Prezime;
            txtUsername.Text = radnik.KorisnickoIme;
            txtWorkingPlace.Text = radnik.RadnoMjesto;
            txtPhone.Text = kontakt.BrojTelefona;
            txtEmail.Text = kontakt.Email;
            txtAddress.Text = lokacija.Adresa;
            txtPlace.Text = lokacija.Mjesto;
            if (radnik.Admin == 1)
            {
                checkAdmin.IsChecked = true;
            }
            else
                checkAdmin.IsChecked = false;
        }
    }
}
