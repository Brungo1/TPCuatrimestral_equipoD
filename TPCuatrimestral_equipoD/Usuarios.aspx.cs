using Dominio;
using Negocio;
using System;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipoD
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.ValidarAcceso(this, true)) return;

            if (!IsPostBack)
            {
                cargarUsuarios();
            }
        }

        private void cargarUsuarios()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            dgvUsuarios.DataSource = negocio.listar();
            dgvUsuarios.DataBind();
        }

        protected void dgvUsuarios_Accion(object sender, GridViewCommandEventArgs e)
        {
            lblMensaje.Visible = false;
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuarioActual = Seguridad.ObtenerUsuario(Session);

            if (e.CommandName == "CambiarEstado")
            {
                string[] datos = e.CommandArgument.ToString().Split(',');

                int id = int.Parse(datos[0]);
                bool activoActual = bool.Parse(datos[1]);

                if (usuarioActual != null && id == usuarioActual.id)
                {
                    lblMensaje.Text = "No podes dar de baja tu propio usuario.";
                    lblMensaje.Visible = true;
                    return;
                }

                negocio.cambiarEstado(id, !activoActual);

                cargarUsuarios();
            }

            if (e.CommandName == "Eliminar")
            {
                int id = int.Parse(e.CommandArgument.ToString());

                if (usuarioActual != null && id == usuarioActual.id)
                {
                    lblMensaje.Text = "No podes eliminar tu propio usuario.";
                    lblMensaje.Visible = true;
                    return;
                }

                try
                {
                    negocio.eliminar(id);
                    cargarUsuarios();
                }
                catch (Exception)
                {
                    lblMensaje.Text = "No se puede eliminar el usuario porque tiene registros asociados. Utilice 'Dar de Baja'.";
                    lblMensaje.Visible = true;
                }
            }
        }
    }
}
