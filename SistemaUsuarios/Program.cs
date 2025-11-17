using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SistemaUsuarios.Models;
using SistemaUsuarios.Presenters;
using SistemaUsuarios.Repositories;
using SistemaUsuarios.repositorio;
using SistemaUsuarios.Services;
using SistemaUsuarios.Views;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace SistemaUsuarios
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Leer connection string desde app.config (key: "DefaultConnection")
            var connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(connStr))
            {
                MessageBox.Show("Falta connection string 'DefaultConnection' en app.config", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connStr));

            // Repositorios y servicios
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            // Presenters y vistas:
            // Nota: En WinForms con DI muchas veces se registra factories para crear forms con sus presenters.
            services.AddTransient<LoginPresenter>();
            services.AddTransient<UsuariosPresenter>();

            // Forms: inyectamos presenters manualmente con factory
            services.AddTransient<FrmLogin>(sp => {
                var presenter = sp.GetRequiredService<LoginPresenter>();
                return new FrmLogin(presenter);
            });

            services.AddTransient<FrmUsuarios>(sp => {
                var presenter = sp.GetRequiredService<UsuariosPresenter>();
                return new FrmUsuarios(presenter);
            });

            var provider = services.BuildServiceProvider();

            // Asegurarse que la BD exista (simple para la práctica)
            using (var scope = provider.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                ctx.Database.EnsureCreated(); // o usar migraciones en un proyecto real
            }

            // Mostrar login
            var loginForm = provider.GetRequiredService<FrmLogin>();
            Application.Run(loginForm);
        }
    }
}
