﻿using System.Windows.Input;

namespace KaiserMVVMCore;

public class RelayCommand : ICommand
{
    private Action execute;
    private Func<Task> executeAsync;
    private Func<object, bool> canExecute;

    public RelayCommand(Action ex, Func<object, bool> canEx = null)
    {
        this.execute = ex;
        this.canExecute = canEx;
    }

    public RelayCommand(Func<Task> ex, Func<object, bool> canEx = null)
    {
        this.executeAsync = ex;
        this.canExecute = canEx;
    }

    public bool CanExecute(object parameter)
    {
        return (this.execute != null || this.executeAsync != null) &&
               (this.canExecute == null || this.canExecute?.Invoke(parameter) == true);
    }

    public void Execute(object parameter)
    {
        if (this.execute != null)
            this.execute();
        else if (this.executeAsync != null)
            this.executeAsync();
    }

    public event EventHandler CanExecuteChanged;

    public void RaiseCanExecuteChanged()
    {
        this.CanExecuteChanged?.Invoke(null, EventArgs.Empty);
    }
}

public class RelayCommand<T> : ICommand
{
    private Action<T> execute;
    private Func<T, Task> executeAsync;
    private Func<object, bool> canExecute;

    public RelayCommand(Action<T> ex, Func<object, bool> canEx = null)
    {
        this.execute = ex;
        this.canExecute = canEx;
    }

    public RelayCommand(Func<T, Task> ex, Func<object, bool> canEx = null)
    {
        this.executeAsync = ex;
        this.canExecute = canEx;
    }

    public bool CanExecute(object parameter)
    {
        return this.canExecute == null || this.canExecute?.Invoke(parameter) == true;
    }

    public void Execute(object parameter)
    {
        if (this.execute != null)
        {
            this.execute.Invoke((T)parameter);
        }
        else if (this.executeAsync != null)
        {
            this.executeAsync((T)parameter).GetAwaiter().GetResult();
        }        
    }

    public event EventHandler CanExecuteChanged;

    public void RaiseCanExecuteChanged()
    {
        this.CanExecuteChanged?.Invoke(null, EventArgs.Empty);
    }
}
