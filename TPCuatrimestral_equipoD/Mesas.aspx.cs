using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipoD
{
    public partial class Mesas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMesas();
            }
        }

        private void CargarMesas()
        {
            MesaNegocio negocio = new MesaNegocio();
            repMesas.DataSource = negocio.listar(false);
            repMesas.DataBind();
        }

        protected string ObtenerClaseBadge(EstadoMesa estado)
        {
            switch (estado)
            {
                case EstadoMesa.Libre:
                    return "badge bg-success";
                case EstadoMesa.Ocupada:
                    return "badge bg-danger";
                default:
                    return "badge bg-secondary";
            }
        }
    }
}