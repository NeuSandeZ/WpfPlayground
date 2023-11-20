using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.Commands;

public abstract class AsyncCommandBase : BaseCommand
{
    private bool _isExecuting;

    public bool IsExecuting
    {
        get { return _isExecuting; }
        set
        {
            _isExecuting = value;
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !IsExecuting && base.CanExecute(parameter);
    }

    public override async void Execute(object? parameter)
    {
        IsExecuting = true;
        await ExecuteAsync(parameter);
        IsExecuting = false;
    }

    protected abstract Task ExecuteAsync(object? parameter);
}