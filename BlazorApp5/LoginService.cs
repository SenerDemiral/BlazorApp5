using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;


namespace BlazorApp5
{
    public class LoginService
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly StateContainer _stateContainer;
        private readonly NavigationManager _navigationManager;
        private readonly CircuitHandler _circuitHandler;

        public LoginService(
            ProtectedLocalStorage protectedLocalStorage,
            StateContainer stateContainer,
            NavigationManager navigationManager,
            CircuitHandler circuitHandler)
        {
            _protectedLocalStorage = protectedLocalStorage;
            _stateContainer = stateContainer;
            _navigationManager = navigationManager;
            _circuitHandler = circuitHandler;
        }

        public async Task GetLoginStateAsync()
        {
            try
            {
                var toto = await _protectedLocalStorage.GetAsync<LoginModel>("kortLogin");
                if (toto.Success)
                {
                    await TryLoginAsync(toto.Value);
                }
                else
                    _stateContainer.UsrId = 0;
            }
            catch
            {
                Console.WriteLine("Catch");
            }
        }

        public async Task TryLoginAsync(LoginModel loginModel)
        {
            if (loginModel.UsrName == "sener" && loginModel.UsrPwd == "SENER")
            {
                await _protectedLocalStorage.SetAsync("kortLogin", loginModel);

                _stateContainer.UsrName = "Sener";
                _stateContainer.UsrPswd = "SENER";
                _stateContainer.UsrId = 1;

                //_singletonState.incNofUsr();
            }
            else
            {
                await LogoutAsync();
            }
        }

        public async Task LogoutAsync()
        {
            await _protectedLocalStorage.DeleteAsync("kortLogin");
            _stateContainer.UsrName = "NotLoggedIn";
            _stateContainer.UsrPswd = "";
            _stateContainer.UsrId = 0;
            _navigationManager.NavigateTo("/");
        }

    }
}
