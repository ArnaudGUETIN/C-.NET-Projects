using CabinetMedical.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CabinetMedical.ModeleDiagram;

namespace CabinetMedical.Views
{
    /// <summary>
    /// Logique d'interaction pour FenetrePatients.xaml
    /// </summary>
    public partial class FenetrePatients : Window
    {
        Role r1;
        public FenetrePatients(Role r)
        {
            r1 = r;
            InitializeComponent();
            this.DataContext = new ListePatientViewModel(r);
            AjoutGrid.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            FenetreRendezVous fn = new FenetreRendezVous(r1);
            fn.Show();
            this.Close();
        }

        private void btEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            AjoutGrid.IsEnabled = false;
        }

        private void btAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjoutGrid.IsEnabled = true;
        }

        private void btAnnuler_Click(object sender, RoutedEventArgs e)
        {
            AjoutGrid.IsEnabled = false;
        }

    
        private void fermer(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void PassChange(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null) { ((dynamic)this.DataContext).Pass = ((PasswordBox)sender).Password; }
        }
    }
}
