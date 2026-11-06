using Dominio;
using Negocio;
using System;
using System.Globalization;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipoD
{
    public partial class FormularioInsumo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarCategorias();

                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    cargarInsumo(id);
                }
            }
        }

        private void cargarCategorias()
        {
            CategoriaInsumoNegocio categoriaNegocio = new CategoriaInsumoNegocio();
            ddlCategoria.DataSource = categoriaNegocio.listar(true);
            ddlCategoria.DataTextField = "descripcion";
            ddlCategoria.DataValueField = "id";
            ddlCategoria.DataBind();
        }

        private void cargarInsumo(int id)
        {
            InsumoNegocio negocio = new InsumoNegocio();
            Insumo insumo = negocio.buscarPorId(id);

            if (insumo != null)
            {
                txtNombre.Text = insumo.nombre;
                txtDescripcion.Text = insumo.descripcion;

                string idCatStr = insumo.categoria.id.ToString();
                if (ddlCategoria.Items.FindByValue(idCatStr) == null)
                {
                    ddlCategoria.Items.Add(new ListItem(insumo.categoria.descripcion, idCatStr));
                }
                ddlCategoria.SelectedValue = idCatStr;

                txtPrecio.Text = insumo.precio.ToString("0.00", CultureInfo.InvariantCulture);
                txtStock.Text = insumo.stockactual.ToString();
                chkActivo.Checked = insumo.activo;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Visible = false;
                InsumoNegocio negocio = new InsumoNegocio();

                int id = 0;
                if (Request.QueryString["id"] != null)
                    id = int.Parse(Request.QueryString["id"]);

                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                    string.IsNullOrWhiteSpace(txtStock.Text))
                {
                    lblMensaje.Text = "Complete los campos obligatorios (Nombre, Precio y Stock).";
                    lblMensaje.Visible = true;
                    return;
                }

                if (negocio.existeNombreInsumo(txtNombre.Text.Trim(), id))
                {
                    lblMensaje.Text = "El nombre del insumo ya existe.";
                    lblMensaje.Visible = true;
                    return;
                }

                decimal precio;
                if (!decimal.TryParse(txtPrecio.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out precio) || precio <= 0)
                {
                    lblMensaje.Text = "El precio ingresado no es valido.";
                    lblMensaje.Visible = true;
                    return;
                }

                int stock;
                if (!int.TryParse(txtStock.Text, out stock) || stock < 0)
                {
                    lblMensaje.Text = "El stock ingresado no es valido.";
                    lblMensaje.Visible = true;
                    return;
                }

                Insumo insumo = new Insumo();
                insumo.id = id;
                insumo.nombre = txtNombre.Text.Trim();
                insumo.descripcion = txtDescripcion.Text.Trim();
                insumo.categoria = new CategoriaInsumo();
                insumo.categoria.id = int.Parse(ddlCategoria.SelectedValue);
                insumo.precio = precio;
                insumo.stockactual = stock;
                insumo.activo = chkActivo.Checked;

                if (id == 0)
                {
                    negocio.agregar(insumo);
                }
                else
                {
                    negocio.modificar(insumo);
                }

                Response.Redirect("Insumos.aspx", false);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.Visible = true;
            }
        }
    }
}
