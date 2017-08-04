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
    /// Logique d'interaction pour FenetreUtilisateurs.xaml
    /// </summary>
    public partial class FenetreUtilisateurs : Window
    {
        Role r1;
        public FenetreUtilisateurs(Role r)
        {
            r1 = r;
            InitializeComponent();
            this.DataContext = new ListeUtilisateursViewModel(r);
            AjoutGrid.IsEnabled = false;
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

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            FenetreConsultations fnu = new FenetreConsultations(r1);
            fnu.Show();
            this.Close();
        }
        private void PassChange(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null) { ((dynamic)this.DataContext).Pass = ((PasswordBox)sender).Password; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)

        {
            Role r = new Role();
            r.NomRole = "SuperUser";
            r.RoleID = 1;
            r.IsActive = true;
            Views.FenetreRendezVous X = new Views.FenetreRendezVous(r);
            X.Show();
            this.Close();
        }
    }
}
