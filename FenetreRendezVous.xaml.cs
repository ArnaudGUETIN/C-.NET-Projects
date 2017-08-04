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
    /// Logique d'interaction pour FenetreRendezVous.xaml
    /// </summary>
    public partial class FenetreRendezVous : Window
    {
        public Role r1;

        public Boolean Test;
        public FenetreRendezVous(Role r)
        {
            r1 = r;
            InitializeComponent();
            this.DataContext = new ListeRendezVousViewModel(r);
            AjoutGrid.IsEnabled = false;

        }
        
       /* private void ModifierButton_Click(object sender, RoutedEventArgs e)
        {
            BoiteDialogue bt = new BoiteDialogue();
            this.Hide();
            bt.ShowDialog();

        }*/

        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            //FenetreAjout fenetre = new FenetreAjout();

            //fenetre.Show();
            // this.Close();
            AjoutGrid.IsEnabled = true;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            AjoutGrid.IsEnabled = false;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            AjoutGrid.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FenetrePatients fn = new FenetrePatients(r1);
            fn.Show();
            this.Close();
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            FenetreUtilisateurs fnu = new FenetreUtilisateurs(r1);
            fnu.Show();
            this.Close();
        }
    }
}
