using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFRoman.Models.HospitalShedule;

namespace WPFRoman.Models.HospitalSheduler
{
    class PatientIdentity : IPatientIdentity
    {
        public PatientIdentity(string passportCode)
        {
            PassportCode = passportCode ?? throw new ArgumentNullException(nameof(passportCode));
        }

        public string PassportCode { get ; set ; }
    }
}
