using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFRoman.Models;

namespace WPFRoman.DbContexts
{
    public class HospitalContext : DbContext
    {
        public HospitalContext() : base("DefaultConnection")
        {
        }

        public HospitalContext(string conntectStr) : base(conntectStr)
        {
            this.Doctors.Load();
            this.Patients.Load();
            this.DoctorsShedules.Load();
        }

        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<DoctorsShedule> DoctorsShedules { get; set; }

        public virtual Patient GetPatientOrCreateByCode(Patient patient)
        {
            var pat = Patients.FirstOrDefault(p => p.PassportCode.Equals(patient.PassportCode));
            if (pat == null)
            {
                pat = Patients.Add(patient);

                this.SaveChanges();
            }

            return pat;
        }

        public static async Task<HospitalContext> LoadHospitalContext(string str)
        {
            var h = new HospitalContext(str);

            await h.Doctors.LoadAsync();
            await h.Patients.LoadAsync();
            await h.DoctorsShedules.LoadAsync();

            return h;
        }

        public ObservableCollection<Doctor> LoadDoctors
        {
            get
            {
                this.Doctors.Load();
                return this.Doctors.Local;
            }
        }
    }
}
