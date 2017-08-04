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
using System.Windows.Controls;
using System.Windows.Input;

namespace CabinetMedical.ViewModel
{
    public class ListePatientViewModel : ViewModelBase
    {
        Role r1;
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
        public ICommand RechercherC { get; set; }
        public ICommand RechercherP { get; set; }
        public static string mcC { get; set; }
        public string mcP { get; set; }
        private static Int32 id = 1; 
        public ICommand Ajouter { get; set; }
        public ICommand SuprimerConsult { get; set; }
        public ICommand AjouterConsult { get; set; }
        public ICommand Suprimer { get; set; }
        public ICommand Afficher { get; set; }
        public ICommand Modifier { get; set; }
        public List<NatureConsultation> natureConsult { get; set; }
        //public ObservableCollection<NatureConsultation> NatureConsult { get; set; }
        public Consultation ConsultSelect { get; set; }
        public Consultation Consult{get;set;}
        private List<Consultation> consult { get; set; }
        public List<Consultation> consultation {
            get { return this.consult; }
            set
            {
                this.consult = value;
                RaisePropertyChanged("consultation");
            }
        }
       // public ObservableCollection<Consultation> Consultation { get; set; }
        DataGrid Liste { get; set; }

        public Patient PatientSelect { get; set; }
        public Patient Patient { get; set; }
        private ObservableCollection<Consultation> listeConsult { get; set; }
        public ObservableCollection<Consultation> ListConsult2 { get; set; }
       /* public ObservableCollection<Consultation> ListeConsult {
            get { return this.listeConsult; }
            set
            {
                this.listeConsult = value;
                RaisePropertyChanged("ListeConsult");
            }
        }*/
        private Patient patientFiche { get; set; }
        public Patient PatientFiche {
            get { return this.patientFiche; }
            set
            {
                this.patientFiche = value;
                RaisePropertyChanged("PatientFiche");
            }
        }
        public CabinetMedicalContainer modele { get; set; }
        private List<Patient> patients { get; set; }
        public List<Patient> Patients
        {
            get { return this.patients; }
            set
            {
                this.patients = value;
                RaisePropertyChanged("Patients");
            }
        }
        private List<Ville> listeVilles { get; set; }
        public List<Ville> ListeVilles
        {
            get { return this.listeVilles; }
            set
            {
                this.listeVilles = value;
                RaisePropertyChanged("ListePatients");
            }
        }
        private ObservableCollection<Patient> listePatients { get; set; }
        public ObservableCollection<Ville> Villes { get; set; }
        Ville ville { get; set; }
		
		
		
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
       // public ObservableCollection<Patient> ListePatient2 { get; set; }
       /* public ObservableCollection<Patient> ListePatients {
            get { return this.listePatients; }
            set
            {
                this.listePatients = value;
                RaisePropertyChanged("ListePatients");
            }
        }*/
        public ListePatientViewModel(Role r)
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
                    isSuper ="Visible";
                    
                }
                else isSuper = "Hidden";
                if (r.NomRole == "Secritaire")
                {
                    isSec = "Visible";
                }
                else isSec = "Hidden";
            }
            r1 = r;
            // Patients = new List<Patient>();
            //Patient.DateNaiss = DateTime.Now;
            Consult = new Consultation();

           // Consultation = new ObservableCollection<Consultation>();
            natureConsult = new List<NatureConsultation>();
           // NatureConsult = new ObservableCollection<NatureConsultation>();
            consultation = new List<Consultation>();
           // ListConsult2 = new ObservableCollection<Consultation>();
            PatientSelect = new Patient();
           // ListeConsult = new ObservableCollection<Consultation>();
            Liste = new DataGrid();
            AjouterConsult = new RelayCommand(AjouterConsultation);
            Suprimer = new RelayCommand(SuprimerPatient);
            SuprimerConsult = new RelayCommand(SuprimerConsultation);
            Afficher = new RelayCommand(AfficherFiche);
            RechercherC = new RelayCommand(RechercherConsult);
            RechercherP = new RelayCommand(RechercherPatient);
            mcC = "";
            mcP = "";
            Modifier = new RelayCommand(ModifierPatient);
            Patient = new Patient();
            PatientFiche = new Patient();
            Ajouter = new RelayCommand(AjouterPatient);
            modele = new CabinetMedicalContainer();
            ConsultSelect = new Consultation();
            
            Patients = modele.PatientSet.ToList();
            ListeVilles = modele.VilleSet.ToList();
            if (r.NomRole == "Patient") { PatientFiche = modele.PatientSet.Find(r.RoleID); }
            else PatientFiche = modele.PatientSet.Find(id);

            consultation = modele.ConsultationSet.ToList();
            natureConsult = modele.NatureConsultationSet.ToList();
          
        }

        public void AjouterPatient()
        {
            
            try
            {
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                var pbkdf2 = new Rfc2898DeriveBytes(Pass, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                string savedPasswordHash = Convert.ToBase64String(hashBytes);

                Patient.PasswordPatient = savedPasswordHash;
                modele.PatientSet.Add(Patient);
                modele.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show("Veillez Verifiez vos informations");
            }
            Patients = modele.PatientSet.ToList();
            
        }

        public void SuprimerPatient()
        {


            if (PatientSelect != null) { modele.PatientSet.Remove(PatientSelect); }
            //=RendezVousSelect;
            try
            {
                modele.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show("Une erreur s'est produite au cours de la supression");
            }
            Patients = modele.PatientSet.ToList();
           
        }
        public void AfficherFiche()
        {
           
            PatientFiche = null;
            id = PatientSelect.PatientID;
            //PatientFiche =modele.PatientSet.Find();
            FenetreFichePatient fn = new FenetreFichePatient(r1);
            //Console.WriteLine(PatientFiche.NomPatient);
            
            
            fn.Show();
        }
        public void AjouterConsultation()
        {
            
            // Consult = null;



            // modele.ConsultationSet.Add(Consult);
            try
            {
                Consultation x = Consult;
                PatientFiche.Consultation.Add(x);
                PatientFiche.DateDernConsult = DateTime.Now;
                modele.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show("Veillez verifiez vos informations");
            }
            // PatientFiche.Consultation = modele.ConsultationSet.Where(u => u.PatientID == PatientFiche.PatientID).ToList();

            Patient p = PatientFiche;
            PatientFiche = null;
            PatientFiche = p;
            
        }
        public void SuprimerConsultation()
        {
            
            try
            {
                modele.ConsultationSet.Remove(ConsultSelect);
                modele.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show("Une erreur s'est produite au cours de la supression");
            }
            Patient p = PatientFiche;
            PatientFiche = null;
            PatientFiche = p;
        }
        public void ModifierPatient()
        {
            Consult.Patient = PatientFiche;


            Console.WriteLine(PatientFiche.PasswordPatient);
            // modele.ConsultationSet.Add(Consult);
            try
            {
                modele.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show("Une erreur s'est produite au cours de la modification");
            }
            BoiteDialogue bd = new BoiteDialogue();
            bd.Show();
           // ListeConsult.Clear();
           /* foreach (var item in modele.ConsultationSet)
            {
                ListConsult2.Add(item);
            }*/
            //Patient p = PatientFiche;
            // PatientFiche = p;
           // ListeConsult = ListConsult2;

            Patients = modele.PatientSet.ToList();
           /* foreach (var item in Patients)
            {
                ListePatient2.Add(item);
            }
            ListePatients = ListePatient2;*/
        }

        public void RechercherConsult()
        {
            //PatientFiche.Consultation.Clear();
            // List<Consultation> lc= PatientFiche.Consultation.Where(u => u.NatureConsultation.NomNature.Contains(mcC)).ToList();
            List<Consultation> lc = modele.ConsultationSet.Where(u => u.NatureConsultation.NomNature.Contains(mcC) && u.Patient.PatientID==PatientFiche.PatientID).ToList();
            PatientFiche.Consultation.Clear();

            Patient p = PatientFiche;
            p.Consultation = lc;
            PatientFiche = null;
            PatientFiche = p;
           
            
           
        }
        public void RechercherPatient()
        {
            //PatientFiche.Consultation.Clear();
            Patients = modele.PatientSet.Where(u => u.NomPatient.Contains(mcP) || u.PrenomPatient.Contains(mcP) || u.Profession.Contains(mcP) || u.NumeroTelephone.Contains(mcP)).ToList();




        }
    }
}
