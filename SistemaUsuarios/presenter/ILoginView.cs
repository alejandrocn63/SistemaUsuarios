namespace SistemaUsuarios.Presenters
{
    public interface ILoginView
    {
        string Correo { get; }
        string Clave { get; }
        void ShowError(string mensaje);
        void ShowMessage(string mensaje);
    }
}
