using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipoD
{
    public partial class Insumos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarInsumos();
            }
        }

        private void CargarInsumos()
        {
            InsumoNegocio negocio = new InsumoNegocio();
            dgvInsumos.DataSource = negocio.listar();
            dgvInsumos.DataBind();
        }

        protected void dgvInsumos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblMensaje.Visible = false;
            InsumoNegocio negocio = new InsumoNegocio();

            if (e.CommandName == "CambiarEstado")
            {
                string[] datos = e.CommandArgument.ToString().Split(',');
                int id = int.Parse(datos[0]);
                bool activoActual = bool.Parse(datos[1]);

                negocio.cambiarEstado(id, !activoActual);
                CargarInsumos();
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    negocio.eliminar(id);
                    CargarInsumos();
                }
                catch (Exception)
                {
                    lblMensaje.Text = "No se puede eliminar el insumo porque tiene registros asociados. Utilice 'Dar de Baja'.";
                    lblMensaje.Visible = true;
                }
            }
        }
    }
}
