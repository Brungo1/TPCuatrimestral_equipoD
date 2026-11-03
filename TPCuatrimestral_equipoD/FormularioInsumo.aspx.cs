using System;
using System.Linq;
using Dominio;
using Negocio;

namespace TPCuatrimestral_equipoD
{
    public partial class FormularioInsumo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CategoriaInsumoNegocio negocioCategoria = new CategoriaInsumoNegocio();
                ddlCategoria.DataSource = negocioCategoria.listar();
                ddlCategoria.DataTextField = "descripcion";
                ddlCategoria.DataValueField = "id";
                ddlCategoria.DataBind();

                if (Request.QueryString["id"] != null)
                {
                    int idSeleccionado = int.Parse(Request.QueryString["id"]);
                    InsumoNegocio negocio = new InsumoNegocio();

                    Insumo insumoEditar = negocio.listar().Find(x => x.id == idSeleccionado);

                    if (insumoEditar != null)
                    {
                        txtNombre.Text = insumoEditar.nombre;
                        txtDescripcion.Text = insumoEditar.descripcion;
                        txtPrecio.Text = insumoEditar.precio.ToString();
                        txtStock.Text = insumoEditar.stockactual.ToString();
                        ddlCategoria.SelectedValue = insumoEditar.categoria.id.ToString();
                    }
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            Insumo nuevo = new Insumo();
            InsumoNegocio negocio = new InsumoNegocio();

            try
            {
                nuevo.nombre = txtNombre.Text;
                nuevo.descripcion = txtDescripcion.Text;
                nuevo.precio = decimal.Parse(txtPrecio.Text);
                nuevo.stockactual = int.Parse(txtStock.Text);

                nuevo.categoria = new CategoriaInsumo();
                nuevo.categoria.id = int.Parse(ddlCategoria.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    nuevo.id = int.Parse(Request.QueryString["id"]);
                    negocio.modificar(nuevo);
                }
                else
                {
                    negocio.agregar(nuevo);
                }

                Response.Redirect("Insumos.aspx", false);
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
            }
        }
    }
}