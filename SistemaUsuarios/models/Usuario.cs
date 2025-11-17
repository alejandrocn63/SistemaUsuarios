using System;

namespace SistemaUsuarios.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string ClaveHash { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
