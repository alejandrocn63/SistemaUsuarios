using SistemaUsuarios.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaUsuarios.Services
{
    public interface IUsuarioService
    {
        Task<bool> LoginAsync(string correo, string contraseñaPlain);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<IEnumerable<Usuario>> SearchAsync(string q);
        Task<Usuario> GetByIdAsync(int id);
        Task CreateAsync(string nombre, string correo, string contraseñaPlain);
        Task UpdateAsync(Usuario usuario, string nuevaContraseñaPlain = null);
        Task DeleteAsync(int id);
    }
}
