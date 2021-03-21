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
    /// Interaction logic for Donors.xaml
    /// </summary>
    public partial class DonorsPage : Page
    {
        List<Donor> donor = null;

        public DonorsPage()
        {
            InitializeComponent();

            donor = JsonConvert.DeserializeObject<List<Donor>>(REST.GetAll("donor"));

            for (int i = 0; i < donor.Count; i++)
            {
                datagridDonors.Items.Add(donor[i]);
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

        private void datagridDonors_RowActivated(object sender, Telerik.Windows.Controls.GridView.RowEventArgs e)
        {
            Donor donor = this.datagridDonors.SelectedItem as Donor;

            NavigationService.Navigate(new DonorInfoPage(donor));


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDonorPage());

        }

     
        private void datagridDonors_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            Donor donor = this.datagridDonors.SelectedItem as Donor;
            List<Kontakt> kontakt = JsonConvert.DeserializeObject<List<Kontakt>>(REST.JoinKontaktDonor(donor.DonorId));
            Lokacija lokacija = kontakt[0].Lokacija;

            txtAddress.Text = lokacija.Adresa;
            txtPlace.Text = lokacija.Mjesto;
            txtEmail.Text = kontakt[0].Email;
            txtPhone.Text = kontakt[0].BrojTelefona;

        }
    }

        
   
}
