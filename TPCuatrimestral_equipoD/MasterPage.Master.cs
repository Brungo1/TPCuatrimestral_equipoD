using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipoD
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = Seguridad.ObtenerUsuario(Session);
            if (usuario != null)
            {
                lblUsuario.Text = usuario.nombre + " " + usuario.apellido + " (" + usuario.perfil + ")";

                bool esGerente = Seguridad.EsGerente(Session);
                liInsumos.Visible = esGerente;
                liUsuarios.Visible = esGerente;
                liReportes.Visible = esGerente;
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Seguridad.CerrarSesion(Session);
            Response.Redirect("Login.aspx");
        }
    }
}