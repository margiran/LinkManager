using System;
using System.Windows.Input;
namespace LinkManagerClientWPF.Common;

public class Command<T> : ICommand
{
    private readonly Func<T, bool> canExecute;

    private readonly Action<T> execute;

    public Command(Action<T> execute) : this(_ => true, execute) { }

    public Command(Func<T, bool> canExecute, Action<T> execute)
    {
        this.canExecute = canExecute;
        this.execute = execute;
    }

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }


    public bool CanExecute(object? parameter)
    {
        if (parameter == null && typeof(T).IsValueType)
            return false;

        return canExecute((T)parameter);
    }

    public void Execute(object? parameter)
    {
        execute((T)parameter);
    }
}

public class Command : Command<object>
{
    public Command(Func<bool> canExecute, Action execute)
        : base(_ => canExecute(), _ => execute()) { }

    public Command(Action execute) : this(() => true, execute)
    {

    }
}
