using System;
using CommunityToolkit.Mvvm.Messaging;
using Hotel.Services.Interfaces;

namespace Hotel.Commands;

public class TestCommand : BaseCommand
{
    private readonly string _dupa;

    public TestCommand(string dupa)
    {
        _dupa = dupa;
    }
    public override void Execute(object? parameter)
    {
        WeakReferenceMessenger.Default.Send<string>(_dupa);
    }
}