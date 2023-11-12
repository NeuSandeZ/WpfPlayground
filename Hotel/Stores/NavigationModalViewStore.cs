namespace Hotel.Stores;

public class NavigationModalViewStore : NavigationStoreBase
{
    public bool IsOpenModal => CurrentViewModel != null;

    public void Close()
    {
        CurrentViewModel = null;
    }
}