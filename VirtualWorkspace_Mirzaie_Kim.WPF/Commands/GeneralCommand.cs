using System;
using System.Windows.Input;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class GeneralCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private Action<object> _action;
        private Predicate<object> _canExecute;

        public GeneralCommand(Action<object> action, Predicate<object> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public GeneralCommand(Action<object> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _action?.Invoke(parameter);
        }
    }
}
