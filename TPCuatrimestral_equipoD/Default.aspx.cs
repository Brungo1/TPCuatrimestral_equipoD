using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipoD
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.ValidarAcceso(this)) return;

            bool esGerente = Seguridad.EsGerente(Session);
            cardInsumos.Visible = esGerente;
            cardUsuarios.Visible = esGerente;
            cardReportes.Visible = esGerente;
        }
    }
}