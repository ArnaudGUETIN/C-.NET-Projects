using CabinetMedical.ModeleDiagram;
using CabinetMedical.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CabinetMedical.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        CabinetMedicalContainer model = new CabinetMedicalContainer();
        public ICommand Connexion { get; set; }
        public User Utilisateur { get; set; }
        private string pass { get; set; }
        
        public string Pass
        {
            get { return pass; }
            set
            {
                pass = value;
                RaisePropertyChanged("Pass");
            }
        }
       
    
        public LoginViewModel()
        {
            // Patients = new List<Patient>();
            //Patient.DateNaiss = DateTime.Now;
            Utilisateur = new User();
            Connexion = new RelayCommand(ConnexionUtilisateur);
           
         

        }

        public void ConnexionUtilisateur()
        {

            Utilisateur.Password = Pass;
            Boolean test = true;
            //  Utilisateur.Role= modele.RoleSet.Find(1);
   
            Console.WriteLine(Utilisateur);
            User uti = model.UserSet.Where(u => u.Login == Utilisateur.Login).SingleOrDefault();
            Patient p = model.PatientSet.Where(u => u.LoginPatient == Utilisateur.Login).SingleOrDefault();
            if (uti != null)
            {
                string savedPasswordHash = (model.UserSet.Where(u => u.Login == Utilisateur.Login).SingleOrDefault()).Password;
                /* Extract the bytes */
                byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                /* Get the salt */
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                /* Compute the hash on the password the user entered */
                var pbkdf2 = new Rfc2898DeriveBytes(Utilisateur.Password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                /* Compare the results */
                for (int i = 0; i < 20; i++)
                { if (hashBytes[i + 16] != hash[i]) test = false; }
                if (!test) { MessageBox.Show("Ce Compte n'existe pas"); }

                // User uti = model.UserSet.Where(u => u.Login == Utilisateur.Login && u.Password == Utilisateur.Password).SingleOrDefault();
                //if(uti == null) { MessageBox.Show("Ce Compte n'existe pas"); }
                else
                {
                    Views.FenetreRendezVous X = new Views.FenetreRendezVous(uti.Role);
                    X.Show();
                }
            }

            else if(p!=null)
            {
                string savedPasswordHash = (model.PatientSet.Where(u => u.LoginPatient == Utilisateur.Login).SingleOrDefault()).PasswordPatient;
                /* Extract the bytes */
                byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                /* Get the salt */
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                /* Compute the hash on the password the user entered */
                var pbkdf2 = new Rfc2898DeriveBytes(Utilisateur.Password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                /* Compare the results */
                for (int i = 0; i < 20; i++)
                { if (hashBytes[i + 16] != hash[i]) test = false; }
                if (!test)
                {
                    MessageBox.Show("Ce Compte n'existe pas");
                    FenetreConnexion fn = new FenetreConnexion();
                    fn.Show();
                }

                // User uti = model.UserSet.Where(u => u.Login == Utilisateur.Login && u.Password == Utilisateur.Password).SingleOrDefault();
                //if(uti == null) { MessageBox.Show("Ce Compte n'existe pas"); }
                else
                {
                    Role r = new Role();
                    r.NomRole = "Patient";
                    r.RoleID = p.PatientID;
                    FenetreFichePatient X = new FenetreFichePatient(r);
                    X.Show();
                    
                }
            }
            else
            {
                MessageBox.Show("Veillez saisir des informations valides");
                
               // FenetreConnexion fn = new FenetreConnexion();
                //fn.Show();
            }
        }
    }
}
