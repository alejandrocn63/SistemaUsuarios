using Microsoft.EntityFrameworkCore;
using SistemaUsuarios.Models;
using SistemaUsuarios.repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaUsuarios.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _ctx;
        public UsuarioRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _ctx.Usuarios.AddAsync(usuario);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var u = await _ctx.Usuarios.FindAsync(id);
            if (u != null)
            {
                _ctx.Usuarios.Remove(u);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _ctx.Usuarios.OrderBy(u => u.Nombre).ToListAsync();
        }

        public async Task<Usuario> GetByEmailAsync(string correo)
        {
            return await _ctx.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _ctx.Usuarios.FindAsync(id);
        }

        public async Task<IEnumerable<Usuario>> SearchAsync(string q)
        {
            // Ejemplo de LINQ: búsqueda por nombre o correo
            return await _ctx.Usuarios
                .Where(u => u.Nombre.Contains(q) || u.Correo.Contains(q))
                .OrderBy(u => u.Nombre)
                .ToListAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _ctx.Usuarios.Update(usuario);
            await _ctx.SaveChangesAsync();
        }
    }
}
