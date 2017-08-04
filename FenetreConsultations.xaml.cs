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
    public partial class FenetreConsultations : Window
    {
        public FenetreConsultations(Role r)
        {
            InitializeComponent();
            this.DataContext = new ListeConsultationsViewModel(r);
            AjoutGrid.IsEnabled = false;
        }

       

        private void btEnregistrer1_Click(object sender, RoutedEventArgs e)
        {
            AjoutGrid.IsEnabled = false;
        }

        private void btAjouter1_Click(object sender, RoutedEventArgs e)
        {
            AjoutGrid.IsEnabled = true;
        }

        private void btAnnuler1_Click(object sender, RoutedEventArgs e)
        {
            AjoutGrid.IsEnabled = false;
        }

        private void btEnregistrer2_Click(object sender, RoutedEventArgs e)
        {
            AjoutGrid.IsEnabled = false;
        }

        private void btAjouter2_Click(object sender, RoutedEventArgs e)
        {
            AjoutGrid.IsEnabled = true;
        }

        private void btAnnuler2_Click(object sender, RoutedEventArgs e)
        {
            AjoutGrid.IsEnabled = false;
        }

        private void Liste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
