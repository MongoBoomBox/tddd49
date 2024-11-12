using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatApp.ViewModel;


namespace ChatApp.ViewModel.Commands
{
    internal class HostCommand : ICommand
    {
        private ChatViewModel parent;

        public event EventHandler? CanExecuteChanged;

        public HostCommand(ChatViewModel parent)
        {
            this.parent = parent;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
            //throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            parent.hostServer();
            //throw new NotImplementedException();
        }
    }
}
