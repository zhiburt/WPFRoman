using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFRoman.DbContexts;
using WPFRoman.Models.HospitalShedule;
using WPFRoman.Tests.TestServises;
using WPFRoman.ViewModels.AddNewPatientViewModel;

namespace WPFRoman.Tests.ViewModels
{
    [TestFixture]
    class NewPatientToSheduleViewModelTests
    {
        [Test]
        public void Done_WhenDataValid()
        {
            var shed = new List<WPFRoman.Models.DoctorsShedule>();
            var hospContext = FakeHospitalContext.Create(
                new List<WPFRoman.Models.Doctor>(),
                shed,
                new List<WPFRoman.Models.Patient>());
            var mHospitalDb = new Mock<HospitalDb>();
            mHospitalDb.Setup(_ => _.Hospital).Returns(hospContext.Object);
            var hospitalShedule = new HospitalShedule(mHospitalDb.Object);
            var viewModel = new NewPatientToSheduleViewModel(hospitalShedule) {
                Date = default(DateTime),
                Doctor = new WPFRoman.Models.Doctor(),
                Name = "it is not empty string",
                PassportCode = "some code",
                Surname = "some surname"
            };
            
            viewModel.Done.Execute(new object());

            Assert.AreEqual(1, shed.Count);
            //hospContext.Verify(e => e.Patients, Times.Once());
            //hospContext.Verify(e => e.DoctorsShedules, Times.Once());
        }

        [Test]
        public void Done_WhenDataIsEmpty_Exeption()
        {
            var hospContext = FakeHospitalContext.Create(
                new List<WPFRoman.Models.Doctor>(),
                new List<WPFRoman.Models.DoctorsShedule>(),
                new List<WPFRoman.Models.Patient>());
            var mHospitalDb = new Mock<HospitalDb>();
            mHospitalDb.Setup(_ => _.Hospital).Returns(hospContext.Object);
            var hospitalShedule = new HospitalShedule(mHospitalDb.Object);
            var viewModel = new NewPatientToSheduleViewModel(hospitalShedule);

            Assert.Catch(() => viewModel.Done.Execute(new object()));
        }
    }
}
