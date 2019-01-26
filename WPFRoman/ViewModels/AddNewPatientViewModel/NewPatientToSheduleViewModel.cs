using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFRoman.Commands;
using WPFRoman.DbContexts;
using WPFRoman.Models;
using WPFRoman.Models.HospitalShedule;
using WPFRoman.Models.HospitalSheduler;

namespace WPFRoman.ViewModels.AddNewPatientViewModel
{
    public class NewPatientToSheduleViewModel : INotifyPropertyChanged
    {
        private string name;
        private string surname;
        private string passportCode;
        private Doctor doctor;
        private DateTime date;

        public NewPatientToSheduleViewModel(HospitalShedule hospitalShedule, Action sucessAction = null, Action failureAction = null)
        {
            Done = new ActionCommand(() =>
            {
                if (string.IsNullOrEmpty(Name) ||
                    string.IsNullOrEmpty(Surname) ||
                    string.IsNullOrEmpty(PassportCode) ||
                    Doctor is null)
                {
                    if (failureAction != null)
                    {
                        failureAction();
                        return;
                    }

                    throw new Exception("fields must be not empty");
                }

                var p = new Patient() {
                    Name = Name,
                    PassportCode = PassportCode,
                    Surname = Surname
                };
                hospitalShedule.AddOrCreatePatientToDoctor(p, Doctor, Date);

                sucessAction?.Invoke();
            });
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Surname)));
            }
        }
        public string PassportCode
        {
            get { return passportCode; }
            set
            {
                passportCode = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(PassportCode)));
            }
        }
        public Doctor Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Doctor)));
            }
        }
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Date)));
            }
        }
       
    public ICommand Done { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(object o, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(o, e);
        }
    }
}
