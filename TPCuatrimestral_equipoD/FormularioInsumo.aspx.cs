using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipoD
{
    public partial class FormularioInsumo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CategoriaInsumoNegocio categoriaNegocio = new CategoriaInsumoNegocio();
                ddlCategoria.DataSource = categoriaNegocio.listar(true);
                ddlCategoria.DataTextField = "descripcion";
                ddlCategoria.DataValueField = "id";
                ddlCategoria.DataBind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text))
            {
                lblMensaje.Text = "Por favor complete Nombre, Precio y Stock.";
                lblMensaje.Visible = true;
                return;
            }

            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out precio))
            {
                lblMensaje.Text = "El precio ingresado no es valido.";
                lblMensaje.Visible = true;
                return;
            }

            int stock;
            if (!int.TryParse(txtStock.Text, out stock))
            {
                lblMensaje.Text = "El stock ingresado no es valido.";
                lblMensaje.Visible = true;
                return;
            }

            Insumo nuevo = new Insumo();
            nuevo.nombre = txtNombre.Text.Trim();
            nuevo.descripcion = txtDescripcion.Text.Trim();
            nuevo.categoria = new CategoriaInsumo();
            nuevo.categoria.id = int.Parse(ddlCategoria.SelectedValue);
            nuevo.precio = precio;
            nuevo.stockactual = stock;
            nuevo.activo = chkActivo.Checked;

            InsumoNegocio negocio = new InsumoNegocio();
            negocio.agregar(nuevo);

            Response.Redirect("Insumos.aspx", false);
        }
    }
}
