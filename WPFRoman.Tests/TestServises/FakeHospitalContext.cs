using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFRoman.DbContexts;
using WPFRoman.Models;

namespace WPFRoman.Tests.TestServises
{
    public class FakeHospitalContext
    {
        public static Mock<HospitalContext> Create(
            List<Doctor> doctors,
            List<DoctorsShedule> doctorsShedules,
            List<Patient> patients)
        {
            var contextMock = new Mock<HospitalContext>();
            DbSet<DoctorsShedule> shedules = GetQueryableMockDbSet<DoctorsShedule>(doctorsShedules);
            contextMock.Setup(s => s.DoctorsShedules).Returns(shedules);
            DbSet<Doctor> dcs = GetQueryableMockDbSet<Doctor>(doctors);
            contextMock.Setup(s => s.Doctors).Returns(dcs);
            DbSet<Patient> pats = GetQueryableMockDbSet(patients);
            contextMock.Setup(s => s.Patients).Returns(pats);

            return contextMock;   
        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => 
                sourceList.Add(s));

            return dbSet.Object;
        }
    }
}
