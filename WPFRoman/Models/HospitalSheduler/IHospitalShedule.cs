using System;

namespace WPFRoman.Models.HospitalShedule
{
    public interface IHospitalShedule
    {
        void AddPatientToDoctor(IPatientIdentity identity, Doctor doctor, DateTime date);
        void AddOrCreatePatientToDoctor(Patient identity, Doctor doctor, DateTime date);
    }
}