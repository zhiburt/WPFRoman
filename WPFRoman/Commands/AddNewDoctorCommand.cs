using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFRoman.ViewModels.Doctors;

namespace WPFRoman.Commands
{
    public class AddNewDoctorCommand : ICommand
    {
        private Action<DoctorViewModel> execute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public AddNewDoctorCommand(Action<DoctorViewModel> execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.execute((DoctorViewModel)parameter);
        }
    }
}
