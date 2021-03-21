using DesktopApp.Models;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Net;
using Newtonsoft.Json;

namespace DesktopApp
{
   

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Radnik> radnici = null;
        public MainWindow()
        {
            
            InitializeComponent();

            try
            {
                radnici = JsonConvert.DeserializeObject<List<Radnik>>(REST.GetAll("radnik"));

            }
            catch (Exception)
            {

          
            }

        }


        private void btnPrijaviSe_Click(object sender, RoutedEventArgs e)
        {
            int pom = 0, i=0;
            if (!radnici.IsNullOrEmpty())
            {
                for (i = 0; i < radnici.Count; i++)
                {

                    if (txtUserName.Text == radnici[i].KorisnickoIme && passboxPassword.Password == radnici[i].Lozinka)
                    {

                        pom++;
                        break;
                    }
                }
                if (pom != 0)
                {
                    WorkingWindow workingWindow = new WorkingWindow(radnici[i]);
                    this.Visibility = Visibility.Hidden;
                    workingWindow.ShowDialog();
                    this.Visibility = Visibility.Visible;
                    passboxPassword.Password = "";


                }
                else
                {
                    MessageBox.Show("Pogrešno korisničko ime ili lozinka!!!");
                    txtUserName.Text = "";
                    passboxPassword.Password = "";
                }
            }
            
        }

        private void btnMinimze_Click(object sender, RoutedEventArgs e)
        {
                this.WindowState = WindowState.Minimized;
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoginWindow_MouseDown(object sender, MouseButtonEventArgs e)
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


    }

}
