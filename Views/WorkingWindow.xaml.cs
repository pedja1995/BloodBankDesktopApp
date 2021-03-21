using DesktopApp.Views.Controls;
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
using System.Windows.Resources;
using System.Windows.Shapes;
using DesktopApp.Views.Pages;
using Telerik.Windows.Controls.Diagrams;
using DesktopApp.Models;
using Newtonsoft.Json;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class WorkingWindow : Window
    {


        List<DozaKrvi> doza = new List<DozaKrvi>();
        Radnik radnik;
        List<Magacin> magacin = null;
     /*   public WorkingWindow()
        {
            InitializeComponent();
            DateTime now = DateTime.Now;

            int res;
            doza = JsonConvert.DeserializeObject<List<DozaKrvi>>(REST.GoodDoses());
            for (int i = 0; i <doza.Count; i++)
            {
               res = DateTime.Compare(doza[i].DatumIstekaRoka.Value, now);
                if(res<=0)
                {
                    doza[i].IstekaoRok = 1;
                    REST.Put_ID("dozakrvi", doza[i].DozaKrviId, doza[i]);
                    magacin = JsonConvert.DeserializeObject<List<Magacin>>(REST.GetMagacin(doza[i].KrvnaGrupaDoza));

                    switch (doza[i].TipKrvnogDerivata)
                    {
                        case "Krv":
                            magacin[0].BrojDozaKrvi = magacin[0].BrojDozaKrvi-1;
                            break;
                        case "Plazma":
                            magacin[0].BrojDozaPlazme = magacin[0].BrojDozaPlazme-1;
                            break;
                        case "Eritrociti":
                            magacin[0].BrojDozaEritrocita = magacin[0].BrojDozaEritrocita-1;
                            break;
                        case "Trombociti":
                            magacin[0].BrojDozaTrombocita = magacin[0].BrojDozaTrombocita-1;
                            break;
                    }
                    REST.Put_ID("magacin", magacin[0].MagacinId, magacin[0]);

                }
                
            }

        }
     */
        public WorkingWindow(Radnik radnik)
        {
            InitializeComponent();
            DateTime now = DateTime.Now;
            if (radnik.RadnoMjesto == "administrator")
            {
                listWorkers.Visibility = Visibility.Visible;
            }
            else
            {
                listWorkers.Visibility = Visibility.Hidden;

            }
            int res;
            doza = JsonConvert.DeserializeObject<List<DozaKrvi>>(REST.GoodDoses());
            for (int i = 0; i < doza.Count; i++)
            {
                res = DateTime.Compare(doza[i].DatumIstekaRoka.Value, now);
                if (res <= 0)
                {
                    doza[i].IstekaoRok = 1;
                    REST.Put_ID("dozakrvi", doza[i].DozaKrviId, doza[i]);
                    magacin = JsonConvert.DeserializeObject<List<Magacin>>(REST.GetMagacin(doza[i].KrvnaGrupaDoza));
                    switch (doza[i].TipKrvnogDerivata)
                    {
                        case "Krv":

                            magacin[0].BrojDozaKrvi = magacin[0].BrojDozaKrvi - 1;

                            break;
                        case "Plazma":

                            magacin[0].BrojDozaPlazme = magacin[0].BrojDozaPlazme - 1;

                            break;
                        case "Eritrociti":

                            magacin[0].BrojDozaEritrocita = magacin[0].BrojDozaEritrocita - 1;

                            break;
                        case "Trombociti":

                            magacin[0].BrojDozaTrombocita = magacin[0].BrojDozaTrombocita - 1;

                            break;
                    }
                    REST.Put_ID("magacin", magacin[0].MagacinId, magacin[0]);

                }

            }
            this.radnik = radnik;
            tt_CurrentUser.Content = radnik.KorisnickoIme;
            txtCurrentUser.Text = radnik.KorisnickoIme;
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
           // Set tooltip visibility

            if(Tg_Btn.IsChecked == true)
            {
                tt_storage.Visibility = Visibility.Collapsed;
                tt_donors.Visibility = Visibility.Collapsed;
                tt_donations.Visibility = Visibility.Collapsed;
                tt_institutions.Visibility = Visibility.Collapsed;
                tt_workers.Visibility = Visibility.Collapsed;
                tt_requests.Visibility = Visibility.Collapsed;
                tt_CurrentUser.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_storage.Visibility = Visibility.Visible;
                tt_donors.Visibility = Visibility.Visible;
                tt_donations.Visibility = Visibility.Visible;
                tt_institutions.Visibility = Visibility.Visible;
                tt_workers.Visibility = Visibility.Visible;
                tt_requests.Visibility = Visibility.Visible;
                tt_CurrentUser.Visibility = Visibility.Visible;

            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
           // img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
          //  img_bg.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
           
            
            if (this.WindowState != WindowState.Maximized)
            {
                Uri resourceUri = new Uri("/Resources/btnRestore.png", UriKind.Relative);
                StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);

                BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                var brush = new ImageBrush();
                brush.ImageSource = temp;

                btnMaximize.Background = brush;
                this.WindowState = WindowState.Maximized;
                WindowStyle = WindowStyle.SingleBorderWindow;
                WindowState = WindowState.Maximized;
                WindowStyle = WindowStyle.None;

            }
            else
            {
                Uri resourceUri = new Uri("/Resources/btnMaximize.png", UriKind.Relative);
                StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);

                BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                var brush = new ImageBrush();
                brush.ImageSource = temp;

                btnMaximize.Background = brush;
                       this.WindowState = WindowState.Normal;
            }

              
        }

        private void btnMinimze_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void WorkingWindow1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void btnMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Opacity = 0.7;

        }

        private void btnMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Opacity = 1;
        }

        private void lvMenuItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //   workingGrid.Children.Add(new DataGrid());
             if(listStorage.IsSelected)
            {
                txtChosenOption.Text = "MAGACIN";

                frameWorkingFrame.Content = new StoragePage();
            }
          /* else  if(listLogOutItem.IsSelected)
            {
                this.Close();
            }*/
           else  if(listDonors.IsSelected)
            {
                txtChosenOption.Text = "DAVAOCI";

                frameWorkingFrame.Content = new DonorsPage();

            }
           else if(listDonation.IsSelected)
            {
                txtChosenOption.Text = "DONACIJE";

                frameWorkingFrame.Content = new DonationPage();
            }
             else if(listRequest.IsSelected)
            {
                txtChosenOption.Text = "ZAHTJEVI";

                frameWorkingFrame.Content = new RequestsPage();
            }
            else if (listInstitutions.IsSelected)
            {
                txtChosenOption.Text = "USTANOVE";

                frameWorkingFrame.Content = new InstitutionsPage();
            }
            else if(listWorkers.IsSelected)
            {
                txtChosenOption.Text = "RADNICI";

                frameWorkingFrame.Content = new WorkersPage();
            }
            listCurrentUser.IsSelected = false;
         


        }

        private void WorkingWindow1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            lvMenuItems.Height = this.Height - 155;
        }

        private void lvInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             if (listCurrentUser.IsSelected)
            {
                txtChosenOption.Text = "LIČNI PODACI";
                lvMenuItems.SelectedItem = null;
                frameWorkingFrame.Content = new CurrentUserPage(radnik);
            }
        }
    }
}
