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
    public partial class FormularioMesa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    cargarMesa(id);
                }
            }
        }

        private void cargarMesa(int id)
        {
            MesaNegocio negocio = new MesaNegocio();
            Mesa mesa = negocio.buscarPorId(id);

            if (mesa != null)
            {
                txtNumeroMesa.Text = mesa.numeromesa.ToString();
                txtCapacidad.Text = mesa.capacidad.ToString();
                chkActiva.Checked = mesa.activa;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Visible = false;
                MesaNegocio negocio = new MesaNegocio();

                int id = 0;
                if (Request.QueryString["id"] != null)
                    id = int.Parse(Request.QueryString["id"]);

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

                Mesa mesa = new Mesa();
                mesa.id = id;
                mesa.numeromesa = numeroMesa;
                mesa.capacidad = capacidad;
                mesa.activa = chkActiva.Checked;

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