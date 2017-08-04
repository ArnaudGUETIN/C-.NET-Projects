using CabinetMedical.ModeleDiagram;
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
    public class ListeUtilisateursViewModel : ViewModelBase
    {
        public ICommand Ajouter { get; set; }
        public ICommand Suprimer { get; set; }
        private String isSec;
        private String isDoctor;
        private String isSuper;
        
        public String
            IsDoctor
        {
            get
            {
                return isDoctor;
            }

            set
            {
                isDoctor = value;
            }
        }

        public String IsSuper
        {
            get
            {
                return isSuper;
            }

            set
            {
                isSuper = value;
            }
        }

        public String IsSec
        {
            get
            {
                return isSec;
            }

            set
            {
                isSec = value;
            }
        }
        public String isPat
        {
            get
            {
                return isPat;
            }

            set
            {
                isPat = value;
            }
        }
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


        public User utilisateurSelect { get; set; }
        public User Utilisateur { get; set; }
        public Role RoleSelect { get; set; }
        public CabinetMedicalContainer modele { get; set; }
        public List<User> Users { get; set; }
        public List<Ville> ListeVilles { get; set; }
        public List<Role> listeRole { get; set; }
        public ObservableCollection<Role> ListeRole { get; set; }
        private ObservableCollection<User> listeUsers { get; set; }
        public ObservableCollection<Ville> Villes { get; set; }
        Ville ville { get; set; }
        public ObservableCollection<User> ListeUsers2 { get; set; }
        public ObservableCollection<User> ListeUsers
        {
            get { return this.listeUsers; }
            set
            {
                this.listeUsers = value;
                RaisePropertyChanged("ListeUsers");
            }
        }
        public ListeUtilisateursViewModel(Role r)
        {
            if (r != null)
            {
                if (r.NomRole == "Docteur")
                {
                    isDoctor = "Visible";
                }
                else isDoctor = "Hidden";
                if (r.NomRole == "SuperUser")
                {
                    isSuper = "Visible";
                    
                }
                else isSuper = "Hidden";
                if (r.NomRole == "Secritaire")
                {
                    isSec = "Visible";
                }
                else isSec = "Hidden";
               

            }
            // Patients = new List<Patient>();
            //Patient.DateNaiss = DateTime.Now;
            utilisateurSelect = new User();
            Suprimer = new RelayCommand(SuprimerUser);
            Utilisateur = new User();
            Ajouter = new RelayCommand(AjouterUser);
            modele = new CabinetMedicalContainer();
            ListeUsers = new ObservableCollection<User>();
            ListeUsers2 = new ObservableCollection<User>();
            Users = modele.UserSet.ToList();
            listeRole = new List<Role>();
            ListeRole = new ObservableCollection<Role>();
            listeRole = modele.RoleSet.ToList();
            RoleSelect = new Role();
            foreach (var item in Users)
            {
                ListeUsers.Add(item);
            }

            foreach (var item in listeRole)
            {
                ListeRole.Add(item);
            }

        }

        public void AjouterUser()
        {

            
            try
            {
                Utilisateur.Password = Pass;
                //  Utilisateur.Role= modele.RoleSet.Find(1);
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                var pbkdf2 = new Rfc2898DeriveBytes(Utilisateur.Password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                string savedPasswordHash = Convert.ToBase64String(hashBytes);

                Utilisateur.Password = savedPasswordHash;
                Console.WriteLine(Utilisateur);
                modele.UserSet.Add(Utilisateur);
                modele.SaveChanges();
            }
            catch(Exception e)
            {
                MessageBox.Show("Veillez Verifiez vos informations");
            }
            ListeUsers.Clear();
            Users = modele.UserSet.ToList();
            foreach (var item in Users)
            {
                ListeUsers2.Add(item);
            }
            ListeUsers = ListeUsers2;
        }

        public void SuprimerUser()
        {


            if (utilisateurSelect != null) { modele.UserSet.Remove(utilisateurSelect); }
            //=RendezVousSelect;
            try
            {
                modele.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show("Une erreur s'est produite au cours de la supression");
            }
            Users = modele.UserSet.ToList();
            ListeUsers.Clear();
            foreach (var item in Users)
            {
                ListeUsers2.Add(item);
            }
            ListeUsers = ListeUsers2;
        }
    }
}
