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
    public partial class Pedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPedidos();
            }
        }

        private void CargarPedidos()
        {
            PedidoNegocio negocio = new PedidoNegocio();
            int filtro = int.Parse(ddlFiltroEstado.SelectedValue);

            EstadoPedido? estado = filtro > 0 ? (EstadoPedido?)filtro : null;
            dgvPedidos.DataSource = negocio.listar(estado);
            dgvPedidos.DataBind();
        }

        protected void ddlFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblExito.Visible = false;
            lblError.Visible = false;
            CargarPedidos();
        }

        protected void dgvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblExito.Visible = false;
            lblError.Visible = false;

            if (e.CommandName == "Cerrar")
            {
                int idPedido;
                if (int.TryParse(e.CommandArgument.ToString(), out idPedido))
                {
                    try
                    {
                        PedidoNegocio negocio = new PedidoNegocio();
                        negocio.cerrarPedido(idPedido);

                        CargarPedidos();
                        lblExito.Text = "Pedido #" + idPedido + " cerrado correctamente. La mesa quedo libre.";
                        lblExito.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = ex.Message;
                        lblError.Visible = true;
                    }
                }
            }
            if (e.CommandName == "Cancelar")
            {
                int idPedido;

                if (int.TryParse(
                    e.CommandArgument.ToString(),
                    out idPedido))
                {
                    try
                    {
                        PedidoNegocio negocio =
                            new PedidoNegocio();

                        negocio.cancelarPedido(idPedido);

                        CargarPedidos();

                        lblExito.Text =
                            "Pedido #" + idPedido +
                            " cancelado correctamente.";

                        lblExito.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = ex.Message;
                        lblError.Visible = true;
                    }
                }
            }
        }

        protected string ObtenerClaseBadge(EstadoPedido estado)
        {
            switch (estado)
            {
                case EstadoPedido.Abierto:
                    return "badge bg-success";
                case EstadoPedido.Cerrado:
                    return "badge bg-secondary";
                default:
                    return "badge bg-danger";
            }
        }
    }
}