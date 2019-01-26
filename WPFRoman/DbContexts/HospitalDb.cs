using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFRoman.DbContexts
{
    public class HospitalDb
    {
        private const string connectionToHospitalContext = "DefaultConnection";

        public HospitalDb(string conn)
        {
            Hospital = new HospitalContext(conn);
        }

        public HospitalDb() : this(connectionToHospitalContext)
        {
        }

        public virtual HospitalContext Hospital { get; set; }
    }
}
