using System;
using System.Linq;
using WPFRoman.DbContexts;

namespace WPFRoman.Models.HospitalShedule
{
    public class HospitalShedule : IHospitalShedule
    {
        private readonly HospitalDb hospitalDb;

        public HospitalShedule(HospitalDb hospitalDb)
        {
            this.hospitalDb = hospitalDb;
        }

        public void AddPatientToDoctor(IPatientIdentity identity, Doctor doctor, DateTime date)
        {
            var patient = hospitalDb.Hospital.Patients.First(p => p.PassportCode == identity.PassportCode);

            AddOrCreatePatientToDoctor(patient, doctor, date);
        }

        public void AddOrCreatePatientToDoctor(Patient patient, Doctor doctor, DateTime date)
        {
            var pat = hospitalDb.Hospital.GetPatientOrCreateByCode(patient);
            hospitalDb.Hospital.DoctorsShedules.Add(new DoctorsShedule()
            {
                Date = date,
                Doctor = doctor,
                Patient = pat
            });

            hospitalDb.Hospital.SaveChanges();
        }
    }
}