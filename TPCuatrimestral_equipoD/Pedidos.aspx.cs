using Dominio;
using Negocio;
using System;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipoD
{
    public partial class Pedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.ValidarAcceso(this)) return;

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

            if (Seguridad.EsGerente(Session))
                dgvPedidos.DataSource = negocio.listar(estado);
            else
                dgvPedidos.DataSource = negocio.listar(estado, Seguridad.ObtenerUsuario(Session).id);

            dgvPedidos.DataBind();
        }

        private bool PuedeModificarPedido(PedidoNegocio negocio, int idPedido)
        {
            if (Seguridad.EsGerente(Session))
                return true;

            Usuario usuario = Seguridad.ObtenerUsuario(Session);
            Pedido pedido = negocio.buscarPorId(idPedido);
            if (usuario == null || pedido == null)
                return false;

            if (pedido.mesero != null && pedido.mesero.id == usuario.id)
                return true;

            Mesa mesa = new MesaNegocio().buscarPorId(pedido.mesa.id);
            return mesa != null && mesa.meseroasignado != null && mesa.meseroasignado.id == usuario.id;
        }

        protected void ddlFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
            CargarPedidos();
        }

        protected void dgvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblError.Visible = false;

            if (e.CommandName == "Cerrar")
            {
                int idPedido;
                if (int.TryParse(e.CommandArgument.ToString(), out idPedido))
                {
                    try
                    {
                        PedidoNegocio negocio = new PedidoNegocio();
                        if (!PuedeModificarPedido(negocio, idPedido))
                        {
                            lblError.Text = "No tenes permiso para modificar este pedido.";
                            lblError.Visible = true;
                            return;
                        }

                        negocio.cerrarPedido(idPedido);

                        CargarPedidos();
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

                if (int.TryParse(e.CommandArgument.ToString(), out idPedido))
                {
                    try
                    {
                        PedidoNegocio negocio = new PedidoNegocio();
                        if (!PuedeModificarPedido(negocio, idPedido))
                        {
                            lblError.Text = "No tenes permiso para modificar este pedido.";
                            lblError.Visible = true;
                            return;
                        }

                        negocio.cancelarPedido(idPedido);

                        CargarPedidos();
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
