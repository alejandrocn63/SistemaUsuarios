using System;

using System.Windows.Forms;

using SistemaUsuarios.Presenters;



namespace SistemaUsuarios.Views

{

    public partial class FrmLogin : Form, ILoginView

    {

        private readonly LoginPresenter _presenter;


        public FrmLogin(LoginPresenter presenter)

        {
            InitializeComponent();
        }



        public string Correo => txtCorreo.Text.Trim();

        public string Clave => txtClave.Text;



        private async void btnLogin_Click(object sender, EventArgs e)

        {

            await _presenter.LoginAsync();

        }



        public void ShowError(string mensaje)

        {

            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }



        public void ShowMessage(string mensaje)

        {

            MessageBox.Show(mensaje, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

    }

}

