using System;

namespace ViewModel
{
    public class RelayCommand : IMyCommand
    {
        public event EventHandler CanExecuteChanged;
        Action<object> _command;
        Func<object, bool> _canExecute;
        public RelayCommand(Action<object> command, Func<object, bool> canExecute)
        {
            _command = command;
            _canExecute = canExecute;
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _command.Invoke(parameter);
        }
    }
}