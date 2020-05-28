using System.Windows.Input;

namespace ViewModel
{
    public interface IMyCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
