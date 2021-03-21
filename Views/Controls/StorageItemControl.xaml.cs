using DesktopApp.Models;
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

namespace DesktopApp.Views.Controls
{
    /// <summary>
    /// Interaction logic for StorageItem.xaml
    /// </summary>
    public partial class StorageItemControl : UserControl
    {
        public StorageItemControl(string slika, Magacin magacin)
        {
            // this.imgBloodType.Source = ImageSource(slika);
         
           
            InitializeComponent();
         

            imgBloodType.Source = new ImageSourceConverter().ConvertFromString(@slika) as ImageSource;
            
            lblBlood.Content = magacin.BrojDozaKrvi;
            lblPlasma.Content = magacin.BrojDozaPlazme;
            lblPlatelets.Content = magacin.BrojDozaTrombocita;
            lblRedCells.Content = magacin.BrojDozaEritrocita;
            if (magacin.BrojDozaKrvi == 0)
                lblBlood.Background = new SolidColorBrush(Color.FromRgb(180,0,0));
            else
                lblBlood.Background = new SolidColorBrush(Color.FromRgb( 0, 116, 21));
            if (magacin.BrojDozaEritrocita == 0)
                lblRedCells.Background = new SolidColorBrush(Color.FromRgb( 180, 0, 0));
            else
                lblRedCells.Background = new SolidColorBrush(Color.FromRgb( 0, 116, 21));
            if (magacin.BrojDozaPlazme == 0)
                lblPlasma.Background = new SolidColorBrush(Color.FromRgb( 180, 0, 0));
            else
                lblPlasma.Background = new SolidColorBrush(Color.FromRgb( 0, 116, 21));
            if (magacin.BrojDozaTrombocita == 0)
                lblPlatelets.Background = new SolidColorBrush(Color.FromRgb( 180, 0, 0));
            else
                lblPlatelets.Background = new SolidColorBrush(Color.FromRgb( 0, 116, 21));
        }

        
    }
}
