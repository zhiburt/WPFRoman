using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFRoman.Models
{
    public enum Qualification
    {
        Intern = 0,
        Phd
    }

    public class Doctor : IComparable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Qualification Qualification { get; set; }

        public int CompareTo(object obj)
        {
            var rhs = obj as Doctor;
            if (rhs != null)
            {
                int qualityComp = this.Qualification.CompareTo(rhs.Qualification);
                if (qualityComp != 0)
                {
                    return qualityComp;
                }
                else
                {
                    return this.Name.CompareTo(rhs.Name);
                }
            }
            else
                throw new Exception($"Cann't compare this obj <type> {obj.GetType().ToString()} with <type> {GetType().ToString()}");
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Name}, {Surname}, {Qualification}";
        }
    }
}
