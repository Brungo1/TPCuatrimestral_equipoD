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
    public partial class CategoriasInsumo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
            }
        }

        private void CargarCategorias()
        {
            CategoriaInsumoNegocio negocio = new CategoriaInsumoNegocio();
            dgvCategorias.DataSource = negocio.listar();
            dgvCategorias.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;
            string descripcion = txtDescripcion.Text.Trim();

            if (string.IsNullOrWhiteSpace(descripcion))
            {
                lblMensaje.Text = "Por favor ingrese la descripcion de la categoria.";
                lblMensaje.Visible = true;
                return;
            }

            CategoriaInsumo nueva = new CategoriaInsumo();
            nueva.descripcion = descripcion;
            nueva.activo = true;

            CategoriaInsumoNegocio negocio = new CategoriaInsumoNegocio();
            negocio.agregar(nueva);

            txtDescripcion.Text = string.Empty;
            CargarCategorias();
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
            else if (e.CommandName == "Eliminar")
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

