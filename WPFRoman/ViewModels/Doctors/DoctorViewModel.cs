using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFRoman.Models;

namespace WPFRoman.ViewModels.Doctors
{
    public class DoctorViewModel : INotifyPropertyChanged
    {
        public DoctorViewModel()
        {
        }

        public DoctorViewModel(string name, string surname, Qualification qualification)
        {
            Name = name;
            Surname = surname;
            Qualification = qualification;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public Qualification Qualification { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
