using SistemaUsuarios.Models;
using SistemaUsuarios.Repositories;
using SistemaUsuarios.repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SistemaUsuarios.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        // Hash simple (SHA256) para la práctica. Explica en la presentación que en producción usar BCrypt/Argon2.
        private string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public async Task CreateAsync(string nombre, string correo, string contraseñaPlain)
        {
            var exists = await _repo.GetByEmailAsync(correo);
            if (exists != null) throw new InvalidOperationException("Correo ya registrado");

            var u = new Usuario
            {
                Nombre = nombre,
                Correo = correo,
                ClaveHash = HashPassword(contraseñaPlain),
                FechaCreacion = DateTime.UtcNow
            };

            await _repo.AddAsync(u);
        }

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

        public async Task<IEnumerable<Usuario>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Usuario> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<Usuario>> SearchAsync(string q) => await _repo.SearchAsync(q ?? string.Empty);

        public async Task UpdateAsync(Usuario usuario, string nuevaContraseñaPlain = null)
        {
            if (!string.IsNullOrWhiteSpace(nuevaContraseñaPlain))
                usuario.ClaveHash = HashPassword(nuevaContraseñaPlain);

            await _repo.UpdateAsync(usuario);
        }

        public async Task<bool> LoginAsync(string correo, string contraseñaPlain)
        {
            var u = await _repo.GetByEmailAsync(correo);
            if (u == null) return false;
            return u.ClaveHash == HashPassword(contraseñaPlain);
        }
    }
}
