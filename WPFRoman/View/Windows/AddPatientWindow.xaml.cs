using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPFRoman.Models.HospitalShedule;
using WPFRoman.ViewModels.AddNewPatientViewModel;

namespace WPFRoman.View.Windows
{
    /// <summary>
    /// Interaction logic for AddPatientWindow.xaml
    /// </summary>
    public partial class AddPatientWindow : Window
    {
        public ReadOnlyObservableCollection<Doctor> Doctors { get; }
        public NewPatientToSheduleViewModel NewPatientToShedule { get; }

        public AddPatientWindow(ReadOnlyObservableCollection<Doctor> doctors, HospitalShedule shedule)
        {
            Doctors = doctors;
            NewPatientToShedule = new NewPatientToSheduleViewModel(
                shedule,
                () => MessageBox.Show("Done", "Sucess", button: MessageBoxButton.OK),
                () => MessageBox.Show("You need to write smth down", "Failure", button: MessageBoxButton.OK));

            this.DataContext = this;

            InitializeComponent();

            MonthlyCalendar.SelectedDate = DateTime.UtcNow;
        }

        public AddPatientWindow()
        {
            InitializeComponent();
        }

        private void MonthlyCalendar_SelectedDatesChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedDateTextBox != null)
                SelectedDateTextBox.Text = MonthlyCalendar.SelectedDate?.ToString("f");
        }
    }
}
