using SistemaUsuarios.Models;
using System.Collections.Generic;

namespace SistemaUsuarios.Presenters
{
    public interface IUsuariosView
    {
        void SetUsuarios(IEnumerable<Usuario> usuarios);
        string Busqueda { get; }
        void ShowMessage(string mensaje);
        void ShowError(string mensaje);
    }
}
