using SistemaUsuarios.Models;
using SistemaUsuarios.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaUsuarios.Presenters
{
    public class UsuariosPresenter
    {
        private readonly IUsuariosView _view;
        private readonly IUsuarioService _service;

        public UsuariosPresenter(IUsuariosView view, IUsuarioService service)
        {
            _view = view;
            _service = service;
        }

        public async Task CargarUsuariosAsync()
        {
            try
            {
                var lista = await _service.GetAllAsync();
                _view.SetUsuarios(lista);
            }
            catch (Exception ex) { _view.ShowError(ex.Message); }
        }

        public async Task BuscarAsync()
        {
            try
            {
                var res = await _service.SearchAsync(_view.Busqueda);
                _view.SetUsuarios(res);
            }
            catch (Exception ex) { _view.ShowError(ex.Message); }
        }

        public async Task CrearUsuarioAsync(string nombre, string correo, string clave)
        {
            try
            {
                await _service.CreateAsync(nombre, correo, clave);
                _view.ShowMessage("Usuario creado");
                await CargarUsuariosAsync();
            }
            catch (Exception ex) { _view.ShowError(ex.Message); }
        }

        public async Task EliminarUsuarioAsync(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                await CargarUsuariosAsync();
            }
            catch (Exception ex) { _view.ShowError(ex.Message); }
        }

        public async Task EditarUsuarioAsync(Usuario u, string nuevaClave = null)
        {
            try
            {
                await _service.UpdateAsync(u, nuevaClave);
                _view.ShowMessage("Usuario actualizado");
                await CargarUsuariosAsync();
            }
            catch (Exception ex) { _view.ShowError(ex.Message); }
        }
    }
}
