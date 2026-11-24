using Dominio;
using Negocio;
using System;

namespace TPCuatrimestral_equipoD
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Seguridad.SesionActiva(Session))
            {
                string destino = Seguridad.EsGerente(Session) ? "Default.aspx" : "Mesas.aspx";
                Response.Redirect(destino, false);
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            string nombreUsuario = txtUsuario.Text.Trim();
            string contrasenia = txtContrasenia.Text;

            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(contrasenia))
            {
                lblError.Text = "Complete usuario y contrase\u00f1a.";
                lblError.Visible = true;
                return;
            }

            Usuario usuario = null;
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                usuario = negocio.loguear(nombreUsuario, contrasenia);
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al iniciar sesion: " + ex.Message;
                lblError.Visible = true;
                return;
            }

            if (usuario == null)
            {
                lblError.Text = "Usuario o contrase\u00f1a incorrectos, o usuario inactivo.";
                lblError.Visible = true;
                return;
            }

            Seguridad.IniciarSesion(Session, usuario);
            string destino = usuario.perfil == PerfilUsuario.Gerente ? "Default.aspx" : "Mesas.aspx";
            Response.Redirect(destino, false);
        }
    }
}

