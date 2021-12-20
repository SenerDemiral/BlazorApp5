public class StateContainer
{
    private int _usrId = 0;
    private string _usrName = "";
    private string _usrPswd = "";

    public int UsrId
    {
        get => _usrId;
        set
        {
            _usrId = value;
            NotifyUserChanged();
        }
    }
    public string UsrName
    {
        get => _usrName;
        set
        {
            _usrName = value;
        }
    }
    public string UsrPswd
    {
        get => _usrPswd;
        set
        {
            _usrPswd = value;
        }
    }

    public Action? OnChange;
    public Action? OnChangeUser;

    private void NotifyUserChanged() => OnChangeUser?.Invoke();
    private void NotifyStateChanged() => OnChange?.Invoke();
}
