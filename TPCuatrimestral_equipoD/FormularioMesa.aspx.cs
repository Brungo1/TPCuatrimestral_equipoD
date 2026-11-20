using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipoD
{
    public partial class FormularioMesa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarMeseros();

                if (Request.QueryString["id"] != null)
                {
                    int id;
                    if (!int.TryParse(Request.QueryString["id"], out id))
                    {
                        Response.Redirect("Mesas.aspx", false);
                        return;
                    }

                    cargarMesa(id);
                }
            }
        }

        private void cargarMeseros()
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            List<Usuario> meseros = usuarioNegocio.listarMeserosActivos();

            ddlMesero.Items.Clear();
            ddlMesero.Items.Add(new ListItem("-- Sin asignar --", "0"));

            foreach (Usuario mesero in meseros)
            {
                ddlMesero.Items.Add(new ListItem(mesero.nombre + " " + mesero.apellido, mesero.id.ToString()));
            }
        }

        private void cargarMesa(int id)
        {
            MesaNegocio negocio = new MesaNegocio();
            Mesa mesa = negocio.buscarPorId(id);

            if (mesa == null)
            {
                Response.Redirect("Mesas.aspx", false);
                return;
            }

            txtNumeroMesa.Text = mesa.numeromesa.ToString();
            txtCapacidad.Text = mesa.capacidad.ToString();
            chkActiva.Checked = mesa.activa;

            if (mesa.meseroasignado != null)
            {
                string idMeseroStr = mesa.meseroasignado.id.ToString();
                if (ddlMesero.Items.FindByValue(idMeseroStr) == null)
                {
                    ddlMesero.Items.Add(new ListItem(mesa.meseroasignado.nombre + " " + mesa.meseroasignado.apellido + " (Inactivo)", idMeseroStr));
                }
                ddlMesero.SelectedValue = idMeseroStr;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Visible = false;
                MesaNegocio negocio = new MesaNegocio();

                int id = 0;
                if (Request.QueryString["id"] != null && !int.TryParse(Request.QueryString["id"], out id))
                {
                    Response.Redirect("Mesas.aspx", false);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNumeroMesa.Text) ||
                    string.IsNullOrWhiteSpace(txtCapacidad.Text))
                {
                    lblMensaje.Text = "Complete los campos obligatorios (Numero de Mesa y Capacidad).";
                    lblMensaje.Visible = true;
                    return;
                }

                int numeroMesa;
                if (!int.TryParse(txtNumeroMesa.Text, out numeroMesa) || numeroMesa <= 0)
                {
                    lblMensaje.Text = "El numero de mesa ingresado no es valido.";
                    lblMensaje.Visible = true;
                    return;
                }

                int capacidad;
                if (!int.TryParse(txtCapacidad.Text, out capacidad) || capacidad <= 0)
                {
                    lblMensaje.Text = "La capacidad ingresada no es valida.";
                    lblMensaje.Visible = true;
                    return;
                }

                if (negocio.existeNumeroMesa(numeroMesa, id))
                {
                    lblMensaje.Text = "El numero de mesa ya existe.";
                    lblMensaje.Visible = true;
                    return;
                }

                if (id > 0 && !chkActiva.Checked)
                {
                    Mesa mesaActual = negocio.buscarPorId(id);
                    if (mesaActual == null)
                    {
                        Response.Redirect("Mesas.aspx", false);
                        return;
                    }

                    if (mesaActual.estado == EstadoMesa.Ocupada)
                    {
                        lblMensaje.Text = "No se puede desactivar una mesa que esta ocupada.";
                        lblMensaje.Visible = true;
                        return;
                    }
                }

                Mesa mesa = new Mesa();
                mesa.id = id;
                mesa.numeromesa = numeroMesa;
                mesa.capacidad = capacidad;
                mesa.activa = chkActiva.Checked;

                int idMesero;
                if (int.TryParse(ddlMesero.SelectedValue, out idMesero) && idMesero > 0)
                    mesa.meseroasignado = new Usuario { id = idMesero };
                else
                    mesa.meseroasignado = null;

                if (id == 0)
                {
                    negocio.agregar(mesa);
                }
                else
                {
                    negocio.modificar(mesa);
                }

                Response.Redirect("Mesas.aspx", false);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.Visible = true;
            }
        }
    }
}
