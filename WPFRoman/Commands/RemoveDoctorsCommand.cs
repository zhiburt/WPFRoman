using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFRoman.Models;

namespace WPFRoman.Commands
{
    public class RemoveDoctorsCommand : ICommand
    {
        private Action<object> execute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RemoveDoctorsCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            System.Collections.IList items = (System.Collections.IList)parameter;
            var collection = items.Cast<Doctor>();

            this.execute(collection);
        }
    }
}
