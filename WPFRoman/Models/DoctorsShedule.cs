using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFRoman.Models
{
    public class DoctorsShedule
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[ForeignKey("Id")]
        public virtual Doctor Doctor { get; set; }
        //[ForeignKey("Id")]
        public virtual Patient Patient { get; set; }
        public DateTime Date { get; set; }
    }
}
