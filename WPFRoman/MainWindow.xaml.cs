using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFRoman.DbContexts;
using WPFRoman.Log;
using WPFRoman.Models;
using WPFRoman.View.Searcher;
using WPFRoman.View.SorterDataGrid;
using WPFRoman.View.Windows;
using WPFRoman.ViewModels.Windows;

namespace WPFRoman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeSpan defaultDelayToUI = TimeSpan.FromSeconds(3);
        HospitalDb hospital;
        Logger logger = Logger.Log;
        MainWindowViewModel mainWindowViewModel;

        public MainWindow()
        {
            hospital = new HospitalDb();

            mainWindowViewModel = new MainWindowViewModel(hospital);
            mainWindowViewModel.ChangeDoctorsEvent += () =>
            {
                MainDataGrid.Items.Refresh();
                logger.Information("Doctors was changed");
            };
            DataContext = mainWindowViewModel;

            logger.SubLog += (o, mss) =>
            {
                this.Dispatcher.Invoke(async () =>
                {
                    await ChangeAmauntRows();
                    var t = ProgresBarWorkAsync(defaultDelayToUI.Seconds);
                    await WriteToStatusString(mss);
                    await t;
                });

                MainDataGrid.SelectedItem = null;
            };

            InitializeComponent();

            ChangeAmauntRows().Wait();
        }

        private Task ChangeAmauntRows()
        {
            RowsCount.Text = $"count rows : {(DataContext as MainWindowViewModel).Doctors.Count.ToString()}";
            return Task.CompletedTask;
        }

        private async Task ProgresBarWorkAsync(int seconds)
        {
            var delta = 2;
            var step = seconds * 1000 / 100 / delta;
            for (int i = 0; i < 100; i++)
            {
                ProgressBar.Value++;
                await Task.Delay(step);
            }
            ProgressBar.Value = 0;
        }

        private async Task WriteToStatusString(string mss)
        {
            Status.Text = mss;
            await Task.Delay(defaultDelayToUI);
            Status.Text = string.Empty;
        }

        private void MainDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var current = Data.CurrentRow;
            //if (current != null) // Means that you've not clicked the column header
            //{
            //    //Display the value of a DataGridView's cell to a TextBox    
            //}

        }

        private void DataGridColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            var columnHeader = sender as DataGridColumnHeader;
            if (columnHeader != null)
            {
                SorterDataGrid.SortDataGrid(MainDataGrid, GetIndexColumnByName(columnHeader.Content.ToString()));
            }
        }

        private int GetIndexColumnByName(string name)
        {
            return MainDataGrid.Columns.Single(c => c.Header.ToString() == name).DisplayIndex;
        }


        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            SelectedItemChange("Surname", (e.Source as TextBox).Text);
        }

        private void SelectedItemChange(string field, object value)
        {
            var doctor = (Doctor)MainDataGrid?.SelectedItem;
            
            var fieldType = doctor?.GetType().GetProperty(field);

            fieldType?.SetValue(doctor, value);
        }

        private void QualificationAddedDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e == null) return;

            var q = Models.Qualification.Intern;
            Enum.TryParse((e.AddedItems[0]).ToString(), out q);

            SelectedItemChange("Qualification", q);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchedContent = SearchBox.Text;
            if (string.IsNullOrEmpty(searchedContent))
            {
                MessageBox.Show("You haven't typed anything to look up smth.");
                return;
            }

            ShowSearchWindow(searchedContent);
        }

        private void ShowSearchWindow(string searchedContent)
        {
            var rows = ConvertItemsToIEnumerable(MainDataGrid);
            var foundItems = Searcher.LookUpRows(searchedContent, rows);
            var columnsHeader = MainDataGrid.Columns.Select(c => c.Header as string);
            var s = new SearchWindow(columnsHeader, foundItems.ToArray());
            try
            {
                s.ShowDialog();
            }
            catch (Exception)
            {}
        }

        private IEnumerable<IEnumerable<string>> ConvertItemsToIEnumerable(DataGrid grid)
        {
            List<IEnumerable<string>> rows = new List<IEnumerable<string>>(grid.Items.Count);
            
            foreach (var item in grid.Items)
            {
                rows.Add(item.ToString().Split(','));
            }

            return rows;
        }

        private void Button_AddPatiend_Click(object sender, RoutedEventArgs e)
        {
            var w = new AddPatientWindow(mainWindowViewModel.Doctors, new Models.HospitalShedule.HospitalShedule(hospital));
            w.Show();
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var doc = MainDataGrid.SelectedItem as Doctor;
            var patientsToDoctor = hospital.Hospital.DoctorsShedules.Where(d => d.Doctor.Id.Equals(doc.Id)).ToList();
            var timeToPatient = patientsToDoctor.Select(t => new Tuple<DateTime, Patient>(t.Date, t.Patient));

            new SheduleWindow(doc, timeToPatient).ShowDialog();
        }
    }
}

