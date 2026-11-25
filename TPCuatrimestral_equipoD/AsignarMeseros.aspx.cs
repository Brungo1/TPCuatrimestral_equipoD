using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipoD
{
    public partial class AsignarMeseros : System.Web.UI.Page
    {
        private List<Usuario> _meserosActivos;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.ValidarAcceso(this, true)) return;

            if (!IsPostBack)
            {
                CargarAsignaciones();
            }
        }

        private void CargarAsignaciones()
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            _meserosActivos = usuarioNegocio.listarMeserosActivos();

            MesaNegocio mesaNegocio = new MesaNegocio();
            dgvAsignaciones.DataSource = mesaNegocio.listar(true);
            dgvAsignaciones.DataBind();
        }

        protected void dgvAsignaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Mesa mesa = (Mesa)e.Row.DataItem;
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlMesero");

                if (ddl != null && mesa != null)
                {
                    if (_meserosActivos == null)
                    {
                        _meserosActivos = new UsuarioNegocio().listarMeserosActivos();
                    }

                    ddl.Items.Clear();
                    ddl.Items.Add(new ListItem("-- Sin asignar --", "0"));

                    foreach (Usuario mesero in _meserosActivos)
                    {
                        ddl.Items.Add(new ListItem(mesero.nombre + " " + mesero.apellido, mesero.id.ToString()));
                    }

                    if (mesa.meseroasignado != null)
                    {
                        string idMeseroStr = mesa.meseroasignado.id.ToString();
                        if (ddl.Items.FindByValue(idMeseroStr) == null)
                        {
                            ddl.Items.Add(new ListItem(mesa.meseroasignado.nombre + " " + mesa.meseroasignado.apellido + " (Inactivo)", idMeseroStr));
                        }
                        ddl.SelectedValue = idMeseroStr;
                    }
                }
            }
        }

        protected void btnGuardarAsignaciones_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            lblExito.Visible = false;

            try
            {
                MesaNegocio mesaNegocio = new MesaNegocio();

                foreach (GridViewRow row in dgvAsignaciones.Rows)
                {
                    HiddenField hfIdMesa = (HiddenField)row.FindControl("hfIdMesa");
                    DropDownList ddlMesero = (DropDownList)row.FindControl("ddlMesero");

                    int idMesa;
                    int idMesero;
                    if (hfIdMesa != null && ddlMesero != null && int.TryParse(hfIdMesa.Value, out idMesa))
                    {
                        int.TryParse(ddlMesero.SelectedValue, out idMesero);
                        Mesa mesa = mesaNegocio.buscarPorId(idMesa);
                        if (mesa != null && mesa.estado == EstadoMesa.Ocupada && idMesero <= 0)
                        {
                            lblError.Text = "La Mesa " + mesa.numeromesa + " esta ocupada y no puede quedar sin mesero asignado.";
                            lblError.Visible = true;
                            return;
                        }
                    }

                }

                foreach (GridViewRow row in dgvAsignaciones.Rows)
                {
                    HiddenField hfIdMesa = (HiddenField)row.FindControl("hfIdMesa");
                    DropDownList ddlMesero = (DropDownList)row.FindControl("ddlMesero");

                    int idMesa;
                    int idMesero;
                    if (hfIdMesa != null && ddlMesero != null && int.TryParse(hfIdMesa.Value, out idMesa))
                    {
                        int.TryParse(ddlMesero.SelectedValue, out idMesero);
                        mesaNegocio.asignarMesero(idMesa, idMesero > 0 ? (int?)idMesero : null);
                    }
                }

                CargarAsignaciones();

                lblExito.Text = "Las asignaciones se guardaron correctamente.";
                lblExito.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al guardar asignaciones: " + ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnLimpiarAsignaciones_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            lblExito.Visible = false;

            try
            {
                MesaNegocio mesaNegocio = new MesaNegocio();
                mesaNegocio.limpiarAsignacionesMesasNoOcupadas();

                CargarAsignaciones();
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al limpiar asignaciones: " + ex.Message;
                lblError.Visible = true;
            }
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

