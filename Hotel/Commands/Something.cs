using System;

namespace Hotel.Commands;

public class Something : BaseCommand
{
    private readonly Action _myDelagate;

    public Something(Action myDelagate)
    {
        _myDelagate = myDelagate;
    }

    public override void Execute(object? parameter)
    {
        _myDelagate();
    }
}