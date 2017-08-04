using CabinetMedical.ModeleDiagram;
using CabinetMedical.ViewModel;
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
using System.Windows.Shapes;

namespace CabinetMedical.Views
{
    /// <summary>
    /// Logique d'interaction pour FenetreFichePatient.xaml
    /// </summary>
    public partial class FenetreFichePatient : Window
    {
        Role r1;
        public FenetreFichePatient(Role r)
        {
            r1 = r;
            InitializeComponent();
            this.DataContext = new ListePatientViewModel(r);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FenetrePatients fn = new FenetrePatients(r1);
            fn.Show();
            this.Close();
        }
    }
}
