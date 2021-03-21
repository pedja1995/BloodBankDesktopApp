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
using System.Windows.Shapes;
using DesktopApp.Models;
using Newtonsoft.Json;

namespace DesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for Magacin.xaml
    /// </summary>
    public partial class StoragePage : Page
    {
        List<Magacin> magacin = null;

        public StoragePage()
        {
            InitializeComponent();
            magacin = JsonConvert.DeserializeObject<List<Magacin>>(REST.GetAll("magacin"));



            for (int i = 0; i < 8; i++)
            {
                wrpStorageitems.Children.Add(new StorageItemControl("../../Resources/Blood_types/blood_"+magacin[i].KrvnaGrupaMagacin.ToString()+".png", magacin[i]));
            }
        }
    }
}
