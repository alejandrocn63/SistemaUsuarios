using SistemaUsuarios.Services;
using System;
using System.Threading.Tasks;

namespace SistemaUsuarios.Presenters
{
    public class LoginPresenter
    {
        private readonly ILoginView _view;
        private readonly IUsuarioService _service;

        public LoginPresenter(ILoginView view, IUsuarioService service)
        {
            _view = view;
            _service = service;
        }

        public async Task LoginAsync()
        {
            try
            {
                var ok = await _service.LoginAsync(_view.Correo, _view.Clave);
                if (ok)
                    _view.ShowMessage("Login exitoso.");
                else
                    _view.ShowError("Usuario o contraseña incorrectos.");
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }
    }
}
