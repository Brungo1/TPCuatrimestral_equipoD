using Dominio;
using Negocio;
using System;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipoD
{
    public partial class CategoriasInsumo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();

                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    cargarCategoria(id);
                }
            }
        }

        private void CargarCategorias()
        {
            CategoriaInsumoNegocio negocio = new CategoriaInsumoNegocio();
            dgvCategorias.DataSource = negocio.listar();
            dgvCategorias.DataBind();
        }

        private void cargarCategoria(int id)
        {
            CategoriaInsumoNegocio negocio = new CategoriaInsumoNegocio();
            CategoriaInsumo categoria = negocio.buscarPorId(id);

            if (categoria != null)
            {
                txtDescripcion.Text = categoria.descripcion;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Visible = false;
                CategoriaInsumoNegocio negocio = new CategoriaInsumoNegocio();

                int id = 0;
                if (Request.QueryString["id"] != null)
                    id = int.Parse(Request.QueryString["id"]);

                string descripcion = txtDescripcion.Text.Trim();

                if (string.IsNullOrWhiteSpace(descripcion))
                {
                    lblMensaje.Text = "Por favor ingrese la descripcion de la categoria.";
                    lblMensaje.Visible = true;
                    return;
                }

                if (negocio.existeCategoria(descripcion, id))
                {
                    lblMensaje.Text = "La categoria ya existe.";
                    lblMensaje.Visible = true;
                    return;
                }

                CategoriaInsumo categoria = new CategoriaInsumo();
                categoria.id = id;
                categoria.descripcion = descripcion;
                categoria.activo = true;

                if (id == 0)
                {
                    negocio.agregar(categoria);
                    txtDescripcion.Text = string.Empty;
                    CargarCategorias();
                }
                else
                {
                    CategoriaInsumo actual = negocio.buscarPorId(id);
                    if (actual != null)
                        categoria.activo = actual.activo;

                    negocio.modificar(categoria);
                    Response.Redirect("CategoriasInsumo.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                lblMensaje.Visible = true;
            }
        }

        protected void dgvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblMensaje.Visible = false;
            CategoriaInsumoNegocio negocio = new CategoriaInsumoNegocio();

            if (e.CommandName == "CambiarEstado")
            {
                string[] datos = e.CommandArgument.ToString().Split(',');
                int id = int.Parse(datos[0]);
                bool activoActual = bool.Parse(datos[1]);

                negocio.cambiarEstado(id, !activoActual);
                CargarCategorias();
            }

            if (e.CommandName == "Eliminar")
            {
                try
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    negocio.eliminar(id);
                    CargarCategorias();
                }
                catch (Exception)
                {
                    lblMensaje.Text = "No se puede eliminar la categoria porque tiene insumos asociados. Utilice 'Dar de Baja'.";
                    lblMensaje.Visible = true;
                }
            }
        }
    }
}
