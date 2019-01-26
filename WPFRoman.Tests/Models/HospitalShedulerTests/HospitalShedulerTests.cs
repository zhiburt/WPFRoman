using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFRoman.DbContexts;
using WPFRoman.Models;
using WPFRoman.Models.HospitalShedule;
using WPFRoman.Tests.TestServises;

namespace WPFRoman.Tests.Models.HospitalShedulerTests
{
    [TestFixture]
    public class HospitalShedulerTests
    {
        private HospitalShedule hospitalShedule;

        private List<Doctor> doctors;
        private List<Patient> patients;
        private List<DoctorsShedule> shedule;
        private Mock<HospitalDb> hospitalDb;
        private readonly Doctor existDoctor = new Doctor() { Name = "ExistDoctor" };

        [SetUp]
        public void SetUp()
        {
            doctors = new List<Doctor>(new Doctor[]{ existDoctor });
            patients = new List<Patient>();
            shedule = new List<DoctorsShedule>();

            hospitalDb = new Mock<HospitalDb>();
            var fake = FakeHospitalContext.Create(doctors, shedule, patients);
            hospitalDb.Setup(s => s.Hospital).Returns(fake.Object);
        }

        [Test]
        public void AddOrCreatePatientToDoctor_DoctorExist_Valid()
        {
            var patient = new Patient();
            DateTime date = default(DateTime);
            var hospitalShedule = new HospitalShedule(hospitalDb.Object);

            hospitalShedule.AddOrCreatePatientToDoctor(patient, existDoctor, date);

            Assert.AreEqual(1, shedule.Count);
        }

        [Test]
        public void AddOrCreatePatientToDoctor_DoctorDoensntExist_Valid()
        {
            var patient = new Patient();
            DateTime date = default(DateTime);
            var hospitalShedule = new HospitalShedule(hospitalDb.Object);

            hospitalShedule.AddOrCreatePatientToDoctor(patient, new Doctor(), date);

            Assert.AreEqual(1, shedule.Count);
        }

        [Test]
        public void AddOrCreatePatientToDoctor_DoctorIsNull_Valid()
        {
            var patient = new Patient();
            DateTime date = default(DateTime);
            var hospitalShedule = new HospitalShedule(hospitalDb.Object);

            hospitalShedule.AddOrCreatePatientToDoctor(patient, null, date);

            Assert.AreEqual(1, shedule.Count);
        }
    }
}
