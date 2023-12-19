using System;

namespace Hotel.Commands;

public class ActionBaseCommand : BaseCommand
{
    private readonly Func<bool> _canExecute;
    private readonly Action _command;

    public ActionBaseCommand(Action command, Func<bool> canExecute = null)
    {
        if (command == null)
            throw new ArgumentNullException("command");
        _canExecute = canExecute;
        _command = command;
    }

    public override void Execute(object? parameter)
    {
        _command();
    }
}