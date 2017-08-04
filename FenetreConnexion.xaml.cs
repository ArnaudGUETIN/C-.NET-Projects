using CabinetMedical.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Logique d'interaction pour FenetreConnexion.xaml
    /// </summary>
    public partial class FenetreConnexion : Window
    {

        
        public FenetreConnexion()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }
        
        private void ConnexionButton_Click(object sender, RoutedEventArgs e)
        {
            FenetreRendezVous fenetre = new FenetreRendezVous(null);
            fenetre.Show();
        }
        private void PassChange(object sender,RoutedEventArgs e)
        {
            if (this.DataContext != null) { ((dynamic)this.DataContext).Pass = ((PasswordBox)sender).Password; }
        }

        
    }
}
