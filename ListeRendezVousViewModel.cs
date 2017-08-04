using CabinetMedical.ModeleDiagram;
using CabinetMedical.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CabinetMedical.ViewModel
{
    public class ListeRendezVousViewModel : ViewModelBase
    {
        public ICommand Ajouter { get; set; }
        public ICommand Modifier { get; set; }
        public ICommand Rechercher { get; set; }
        public string mc { get; set; }
        private DateTime datebox;
        public ICommand Suprimer { get; set; }
        public RendezVous RendezVousSelect{get;set;}

      

        public DateTime Datebox {
            get { return this.datebox; }
            set { this.datebox= value;
                RaisePropertyChanged("Datebox");
                    }}
        public Patient patient { get; set; }
        DataGrid ListeDg;

        public CabinetMedicalContainer Rendez { get; set; }
        private List<RendezVous> rendezVous { get; set; }
        public List<RendezVous> RendezVous
        {
            get { return this.rendezVous; }
            set
            {
                this.rendezVous = value;
                RaisePropertyChanged("RendezVous");
            }
        }
        public List<Patient> PatientsListe { get; set; }
        private ObservableCollection<RendezVous> listeRendezVous { get; set; }
        public ObservableCollection<RendezVous> ListeRendezVous2 { get; set; }
        public ObservableCollection<RendezVous> ListeRendezVous {
            get { return this.listeRendezVous; }
            set
            {
                this.listeRendezVous = value;
                RaisePropertyChanged("ListeRendezVous");
            }
        }
        public ObservableCollection<RendezVous> Liste { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public RendezVous AjoutRendezVous { get; set; }
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

        public ListeRendezVousViewModel(Role r)
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
            

            Datebox = DateTime.Now;
            ListeDg = new DataGrid();
            RendezVousSelect = new RendezVous();
            Suprimer = new RelayCommand(SuprimerRendezVous);
            Modifier = new RelayCommand(ModifierRendezVous);
            Ajouter =new RelayCommand(AjouterRendezVous);
            mc ="";
            Rechercher = new RelayCommand(RechercherR);
            Patient p1 = new Patient { NomPatient = "Guetin",PrenomPatient="Arnaud",DateDernConsult= DateTime.Parse(" 2017-03-05") };
            ListeRendezVous = new ObservableCollection<RendezVous>();
            ListeRendezVous2 = new ObservableCollection<RendezVous>();
            Patients = new ObservableCollection<Patient>();
            Rendez = new CabinetMedicalContainer();
            RendezVous= Rendez.RendezVousSet.ToList();
            PatientsListe = Rendez.PatientSet.ToList();
           

            foreach (var item in RendezVous)
            {
                //item.DateRendezVous = item.DateRendezVous.;

                ListeRendezVous.Add(item);
            }
            foreach (var item in PatientsListe)
            {
                Patients.Add(item);
            }
           // Patients.Add(p1);
           // ListeRendezVous=(ObservableCollection<RendezVous>) ListeRendezVous.Where(u => u.Patient.NomPatient =="Guetin");
           // ListeRendezVous.Add(new RendezVous { DateRendezVous = DateTime.Now, Statut = "Valide", Patient = p1, PatientID = p1.PatientID });
            //Liste = ListeRendezVous.Where(item => item.DateRendezVous = DateTime.Now.GetDateTimeFormats);
        }

       public void AjouterRendezVous()
        {
            ListeDg.IsReadOnly = false;
            Console.WriteLine(datebox);
            //Datebox = DateTime.Now;
            
            

           // DateTime enteredDate = DateTime.Parse(datebox);
            

            try
            {
                Rendez.RendezVousSet.Add(new RendezVous { DateRendezVous = datebox, Statut = "Valide", Patient = patient, PatientID = patient.PatientID });
                Rendez.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show("Veillez verifiez vos informations");
            }
            ListeRendezVous.Clear();
            RendezVous = Rendez.RendezVousSet.ToList();
            foreach (var item in RendezVous)
            {
                ListeRendezVous2.Add(item);
            }
            ListeRendezVous = ListeRendezVous2;
            
        }
        public void SuprimerRendezVous()
        {
            
            
            
            //=RendezVousSelect;
            try
            {
                if (RendezVousSelect.PatientID != 0) { Rendez.RendezVousSet.Remove(RendezVousSelect); }
                Rendez.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show("Une erreur s'est produite au cours de la supression");
            }
            RendezVous = Rendez.RendezVousSet.ToList();
            ListeRendezVous.Clear();
            foreach (var item in RendezVous)
            {
                ListeRendezVous2.Add(item);
            }
            ListeRendezVous = ListeRendezVous2;
        }
        public void ModifierRendezVous()
        {
            ListeDg.IsReadOnly = false;

            // Console.WriteLine(x);
            // Rendez.PatientSet.Find(RendezVousSelect.RendezVousID);
            try
            {
                Rendez.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show("Une erreur s'est produite au cours de la modification");
            }
            ListeRendezVous2 = ListeRendezVous;
            //Rendez.PatientSet.SqlQuery("UPDATE dbo.PatientSet SET nom_colonne_1 = 'nouvelle valeur'WHERE condition");
            RendezVous = Rendez.RendezVousSet.ToList();
            ListeRendezVous.Clear();
           /* foreach (var item in RendezVous)
            {
                ListeRendezVous2.Add(item);
            }*/
            ListeRendezVous = ListeRendezVous2;
        }

        public void RechercherR()
        {
            RendezVous = Rendez.RendezVousSet.Where(u => u.Patient.NomPatient.Contains(mc) || u.Patient.PrenomPatient.Contains(mc) ).ToList();
        }
           
        
    }
}
