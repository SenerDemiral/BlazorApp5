public class SingletonState
{
    private int _nofUsr = 0;

    public int NofUsr
    {
        get => _nofUsr;
        set
        {
            _nofUsr = value;
            NotifyStateChanged();
        }
    }

    public void incNofUsr()
    {
        NofUsr++;
    }

    public Action? OnChange;
    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}
