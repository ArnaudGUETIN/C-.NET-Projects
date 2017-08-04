using CabinetMedical.ModeleDiagram;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace CabinetMedical.ViewModel
{
    public class ListeConsultationsViewModel : ViewModelBase
    {
        public ICommand AjouterC { get; set; }
        public ICommand AjouterV { get; set; }
        public ICommand SuprimerC { get; set; }
        public ICommand SuprimerV { get; set; }
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

        public NatureConsultation ConsultationSelect { get; set; }
        public NatureConsultation Consultation { get; set; }

        public Ville VilleSelect { get; set; }
        public Ville ville { get; set; }
        public CabinetMedicalContainer modele { get; set; }
        public List<NatureConsultation> Consultations { get; set; }
        private ObservableCollection<NatureConsultation> listeConsultations { get; set; }
        public ObservableCollection<NatureConsultation> ListeConsultations2 { get; set; }

        public List<Ville> Villes { get; set; }
        private ObservableCollection<Ville> listeVilles { get; set; }
        public ObservableCollection<Ville> ListeVilles2 { get; set; }

        public ObservableCollection<NatureConsultation> ListeConsultations
        {
            get { return this.listeConsultations; }
            set
            {
                this.listeConsultations = value;
                RaisePropertyChanged("ListeConsultations");
            }
        }

             public ObservableCollection<Ville> ListeVilles
        {
            get { return this.listeVilles; }
            set
            {
                this.listeVilles = value;
                RaisePropertyChanged("ListeVilles");
            }
        }
        public ListeConsultationsViewModel(Role r)
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
            ConsultationSelect = new NatureConsultation();
            VilleSelect = new Ville();
            SuprimerC = new RelayCommand(SuprimerConsultation);
            SuprimerV = new RelayCommand(SuprimerVille);
            Consultation = new NatureConsultation();
            ville = new Ville();
            AjouterC = new RelayCommand(AjouterConsultation);
            AjouterV = new RelayCommand(AjouterVille);
            modele = new CabinetMedicalContainer();
            ListeConsultations = new ObservableCollection<NatureConsultation>();
            ListeConsultations2 = new ObservableCollection<NatureConsultation>();
            Consultations = modele.NatureConsultationSet.ToList();

            ListeVilles = new ObservableCollection<Ville>();
            ListeVilles2 = new ObservableCollection<Ville>();
            Villes = modele.VilleSet.ToList();
            foreach (var item in Villes)
            {
                ListeVilles.Add(item);
            }
            foreach (var item in Consultations)
            {
                ListeConsultations.Add(item);
            }


        }

        public void AjouterConsultation()
        {


            //  Utilisateur.Role= modele.RoleSet.Find(1);
            
            Console.WriteLine(Consultation);
            modele.NatureConsultationSet.Add(Consultation);
            try
            {
                modele.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show("Veillez verifier vos informations");
            }
            Consultations = modele.NatureConsultationSet.ToList();
            foreach (var item in Consultations)
            {
                ListeConsultations2.Add(item);
            }
            ListeConsultations = ListeConsultations2;
        }

        public void SuprimerConsultation()
        {


            if (ConsultationSelect != null) { modele.NatureConsultationSet.Remove(ConsultationSelect); }
            //=RendezVousSelect;
            try
            {
                modele.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show("Une erreur s'est produite au cours de la supression");
            }
            Consultations = modele.NatureConsultationSet.ToList();
            ListeConsultations.Clear();
            foreach (var item in Consultations)
            {
                ListeConsultations2.Add(item);
            }
            ListeConsultations = ListeConsultations2;
        }
        public void AjouterVille()
        {


            //  Utilisateur.Role= modele.RoleSet.Find(1);
           // MessageBox.Show("Nassim was here");
            Console.WriteLine(ville);
            Ville v = modele.VilleSet.Where(u => u.NomVille.Contains(ville.NomVille)).SingleOrDefault();
            if (v==null) modele.VilleSet.Add(ville);
            else
            {
                MessageBox.Show("Ville exixtante");
            }

            try
            {
                modele.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show("Veillez vérifier vos informations");
            }
            Villes = modele.VilleSet.ToList();
            foreach (var item in Villes)
            {
                ListeVilles2.Add(item);
            }
            ListeVilles = ListeVilles2;
        }

        public void SuprimerVille()
        {


            if (VilleSelect != null) { modele.VilleSet.Remove(VilleSelect); }
            //=RendezVousSelect;
            try
            {
                modele.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show("Une erreur s'est produite au cours de la supression");
            }
            Villes = modele.VilleSet.ToList();
            ListeVilles.Clear();
            foreach (var item in Villes)
            {
                ListeVilles2.Add(item);
            }
            ListeVilles = ListeVilles2;
        }

    } }