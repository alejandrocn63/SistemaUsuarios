using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SistemaUsuarios.Models;
using SistemaUsuarios.Presenters;

namespace SistemaUsuarios.Views
{
    public partial class FrmUsuarios : Form, IUsuariosView
    {
        private readonly UsuariosPresenter _presenter;

        public FrmUsuarios(UsuariosPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
        }

        public string Busqueda => txtBuscar.Text.Trim();

        public void SetUsuarios(IEnumerable<Usuario> usuarios)
        {
            // Vincula lista al DataGridView
            dgvUsuarios.DataSource = usuarios.Select(u => new {
                u.Id,
                u.Nombre,
                u.Correo,
                u.FechaCreacion
            }).ToList();
        }

        public void ShowMessage(string mensaje) =>
            MessageBox.Show(mensaje, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public void ShowError(string mensaje) =>
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private async void FrmUsuarios_Load(object sender, EventArgs e)
        {
            await _presenter.CargarUsuariosAsync();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await _presenter.BuscarAsync();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) return;
            int id = (int)dgvUsuarios.CurrentRow.Cells["Id"].Value;
            await _presenter.EliminarUsuarioAsync(id);
        }

        // btnNuevo y btnEditar abrirían diálogos o formularios para creación/edición (omito por brevedad)
    }
}
