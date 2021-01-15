using System;
using System.Windows.Input;

namespace KaiserMVVM
{
    public class RelayCommand : ICommand
    {
        private Action execute;
        private Func<object, bool> canExecute;

        public RelayCommand(Action ex, Func<object, bool> canEx = null)
        {
            this.execute = ex;
            this.canExecute = canEx;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute?.Invoke(parameter) == true;
        }

        public void Execute(object parameter)
        {
            this.execute?.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }

    public class RelayCommand<T> : ICommand
    {
        private Action<T> execute;
        private Func<object, bool> canExecute;

        public RelayCommand(Action<T> ex, Func<object, bool> canEx = null)
        {
            this.execute = ex;
            this.canExecute = canEx;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute?.Invoke(parameter) == true;
        }

        public void Execute(object parameter)
        {
            this.execute?.Invoke((T)parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
