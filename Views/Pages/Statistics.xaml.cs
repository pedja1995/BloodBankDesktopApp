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
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Page
    {
        List<Magacin> magacin = null;
        int[] br = new int[8];//a+ a- b+ b- o+ o- ab+ ab-
        public Statistics()
        {
            InitializeComponent();

            magacin = JsonConvert.DeserializeObject<List<Magacin>>(REST.GetAll("magacin"));
            for (int i = 0; i < magacin.Count; i++)
            {
                br[i] = magacin[i].BrojDozaEritrocita.Value + magacin[i].BrojDozaKrvi.Value + magacin[i].BrojDozaPlazme.Value+ magacin[i].BrojDozaTrombocita.Value;
            }


            blood_a_plus.Value = br[0];
            blood_a_minus.Value = br[1];
            blood_b_plus.Value = br[2];
            blood_b_minus.Value = br[3];
            blood_o_plus.Value = br[4];
            blood_o_minus.Value = br[5];
            blood_ab_plus.Value = br[6];
            blood_ab_minus.Value = br[7];

            


        }
    }
}
