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
        protected void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idSeleccionado = int.Parse(btn.CommandArgument);
            InsumoNegocio negocio = new InsumoNegocio();
            negocio.cambiarEstado(idSeleccionado);

            CargarInsumos();
        }
    }
}