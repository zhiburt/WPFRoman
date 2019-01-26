using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFRoman.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public uint Age { get; set; }
        public string PassportCode { get; set; }
        public ICollection<string> MedicalHistory { get; set; }

        public string MedicalHistoryAsString
        {
            get { return string.Join(",", MedicalHistory ?? new List<string>()); }
            set { MedicalHistory = value.Split(',').ToList(); }
        }   
    }
}
