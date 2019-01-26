using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFRoman.DbContexts;
using WPFRoman.Models;

namespace WPFRoman.ViewModels.Doctors
{
    public class DoctorsViewModel
    {
        private ObservableCollection<Doctor> _doctors;
        private readonly HospitalDb h;

        public DoctorsViewModel(HospitalDb h) : this()
        {
            this.h = h;

            _doctors = h.Hospital.Doctors.Local;
            Doctors = new ReadOnlyObservableCollection<Doctor>(_doctors);
        }

        private DoctorsViewModel()
        {
        }

        public ReadOnlyObservableCollection<Doctor> Doctors { get; set; }

        public void AddDoctor(string n, string sn, Qualification qualication)
        {
            h.Hospital.Doctors.Add(new Models.Doctor()
            {
                Name = n,
                Surname = sn,
                Qualification = qualication
            });

            h.Hospital.SaveChanges();
        }

        internal void RemoveDoctor(Doctor d)
        {
            h.Hospital.Doctors.Remove(d);
            h.Hospital.SaveChanges();
        }

        public void UpdateDoctor(Doctor d)
        {
            var doctor = _doctors.SingleOrDefault(doc => doc.Id == d.Id);
            doctor.Qualification = d.Qualification;
            doctor.Surname = d.Surname;

            h.Hospital.SaveChanges();
        }
    }
}
