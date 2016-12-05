using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Eating.ViewModel
{
    public class saveCommand : ICommand
    {
        private readonly Action execute;
        public event EventHandler CanExecuteChanged;

        public saveCommand(Action execute)
        {
            this.execute = execute;
        }



        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            execute();
        }
    }
}
