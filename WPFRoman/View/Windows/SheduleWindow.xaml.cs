using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFRoman.Models;

namespace WPFRoman.View.Windows
{
    /// <summary>
    /// Interaction logic for SheduleWindow.xaml
    /// </summary>
    public partial class SheduleWindow : Window
    {
        public SheduleWindow(Doctor doctor, IEnumerable<Tuple<DateTime, Patient>> doctrosShedule)
        {
            InitializeComponent();

            ShowDoctorInformation(doctor);
            ShowDoctorShedule(doctrosShedule);
        }

        public void ShowDoctorInformation(Doctor doctor)
        {
            DoctorNameOnShedule.Text = $"{doctor.Name} {doctor.Surname}";
        }

        public void ShowDoctorShedule(IEnumerable<Tuple<DateTime, Patient>> doctrosShedule)
        {
            foreach (var a in doctrosShedule)
            {
                DoctorsShedule.Appointments.Add(
                    new ScheduleAppointment() {
                        StartTime = a.Item1,
                        EndTime = GetDefaultEndTimeForEvent(a.Item1),
                        Subject = GetPatientSubject(a.Item2),
                        AllDay = false
                    });
            }
        }

        private DateTime GetDefaultEndTimeForEvent(DateTime d)
        {
            return d.AddHours(2).AddMinutes(30);
        }

        private string GetPatientSubject(Patient p)
        {
            return $"Patient {p.Name} {p.Surname}, {p.PassportCode} is going to be";
        }
    }
}
