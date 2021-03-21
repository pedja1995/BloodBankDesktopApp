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

namespace DesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for InstitutionPage.xaml
    /// </summary>
    public partial class InstitutionsPage : Page
    {
        public List<ZdravstvenaUstanova> zdravstvenaUstanova = null;
        public InstitutionsPage()
        {
            InitializeComponent();
            btnAdd.IsEnabled = false;
            zdravstvenaUstanova = JsonConvert.DeserializeObject<List<ZdravstvenaUstanova>>(REST.GetAll("zdravstvenaustanova"));
            for (int i = 0; i < zdravstvenaUstanova.Count; i++)

            {
                gridInstitutions.Items.Add(zdravstvenaUstanova[i]);
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

        private void gridInstitutions_RowActivated(object sender, Telerik.Windows.Controls.GridView.RowEventArgs e)
        {
            ZdravstvenaUstanova ustanova = this.gridInstitutions.SelectedItem as ZdravstvenaUstanova;

            NavigationService.Navigate(new InstitutionInfoPage(ustanova));
           

           
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!txtAddress.Text.IsNullOrEmpty() && !txtEmail.Text.IsNullOrEmpty() && !txtIdentity.Text.IsNullOrEmpty() && !txtName.Text.IsNullOrEmpty() && !txtPhone.Text.IsNullOrEmpty() && !txtPlace.Text.IsNullOrEmpty())
            {
                btnAdd.IsEnabled = true;
               
            }
            else
                btnAdd.IsEnabled = false;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ZdravstvenaUstanova zu = new ZdravstvenaUstanova();
            Kontakt k = new Kontakt();
            Lokacija l = new Lokacija();

            zu.Naziv = txtName.Text;
            zu.VerifikacioniKod = txtIdentity.Text;

            k.BrojTelefona = txtPhone.Text;
            k.Email = txtEmail.Text;

            l.Adresa = txtAddress.Text;
            l.Mjesto = txtPlace.Text;

            //upis u tabelu
            var response = REST.Post("zdravstvenaustanova", zu);

            //zu ID
            string pom = response.Headers.Location.ToString();

            Regex regex = new Regex(@"api/ZdravstvenaUstanova/(\w+)");
            Match match = regex.Match(pom);
            pom = match.Groups[1].Value;
            int ID = Convert.ToInt32(pom);
            //
            k.ZdravstvenaUstanovaId = ID;
            //upis u tabelu lokacija
            response = REST.Post("lokacija", l);
            //lokacija ID
            pom = response.Headers.Location.ToString();

            regex = new Regex(@"api/Lokacija/(\w+)");
            match = regex.Match(pom);
            pom = match.Groups[1].Value;
            ID = Convert.ToInt32(pom);
            //
            k.LokacijaId = ID;

            //upis u tabelu kontakt
            REST.Post("kontakt", k);
            txtAddress.Text = txtEmail.Text = txtIdentity.Text = txtName.Text = txtPhone.Text = txtPlace.Text = null;
            NavigationService.Navigate(new InstitutionsPage());

        }
    }
}
