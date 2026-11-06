using Negocio;
using System;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipoD
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            UsuarioNegocio negocio = new UsuarioNegocio();

            if (e.CommandName == "CambiarEstado")
            {
                string[] datos = e.CommandArgument.ToString().Split(',');

                int id = int.Parse(datos[0]);
                bool activoActual = bool.Parse(datos[1]);

                negocio.cambiarEstado(id, !activoActual);

                cargarUsuarios();
            }

            if (e.CommandName == "Eliminar")
            {
                int id = int.Parse(e.CommandArgument.ToString());

                negocio.eliminar(id);

                cargarUsuarios();
            }
        }
    }
}