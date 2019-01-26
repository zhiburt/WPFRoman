using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFRoman.Commands;
using WPFRoman.DbContexts;
using WPFRoman.Models;
using WPFRoman.ViewModels.Doctors;

namespace WPFRoman.ViewModels.Windows
{
    public class MainWindowViewModel
    {
        public Doctors.DoctorsViewModel DoctorsViewModel;

        public ReadOnlyObservableCollection<Doctor> Doctors { get; }

        public ICommand AddNewDoctor { get; }
        public ICommand RemoveDoctorCommand { get; }
        public ICommand UpdateDoctorCommand { get; }

        public MainWindowViewModel(HospitalDb hospitalDb)
        {
            DoctorsViewModel = new Doctors.DoctorsViewModel(hospitalDb);
            Doctors = DoctorsViewModel.Doctors;

            AddNewDoctor = new AddNewDoctorCommand((d) => {
                DoctorsViewModel.AddDoctor(d.Name, d.Surname, d.Qualification);
                OnChangeDoctorEvent();
            }); 

            RemoveDoctorCommand = new RemoveDoctorsCommand((i) => {
                RemoveManyRows(i as IEnumerable<Doctor>, (o) => DoctorsViewModel.RemoveDoctor(o));
            });

            UpdateDoctorCommand = new RemoveDoctorsCommand(UpdateManyRows);
        }



        public event Action ChangeDoctorsEvent;

        public void OnChangeDoctorEvent()
        {
            ChangeDoctorsEvent?.Invoke();
        }

        public void UpdateManyRows(object o)
        {
            if (o == null)
            {
                MessageBox.Show("You need to choose some row", "update row");
                return;
            }

            var doctors = (o as IEnumerable<Doctor>).ToList();
            foreach (var doc in doctors)
            {
                DoctorsViewModel.UpdateDoctor(doc);
                OnChangeDoctorEvent();
            }
        }

        private void RemoveManyRows<Instanse>(IEnumerable<Instanse> collections, Action<Instanse> action)
        {
            if (collections == null)
            {
                MessageBox.Show("You need to choose some row", "update row");
                return;
            }

            var res = MessageBox.Show("Are you sure?", "the data will be loss", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                foreach (var obj in collections.ToList())
                {
                    action(obj);
                    OnChangeDoctorEvent();
                }
            }
        }
    }
}
