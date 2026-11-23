using Dominio;
using Negocio;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipoD
{
    public partial class GestionDePedidos : System.Web.UI.Page
    {
        protected bool EsPedidoAbierto { get; set; }
      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idPedido;

                if (int.TryParse(Request.QueryString["id"], out idPedido))
                {
                    cargarPedido(idPedido);
                }
                else
                {
                    prepararNuevoPedido();
                }
            }
        }

        private void prepararNuevoPedido()
        {
            pnlNuevoPedido.Visible = true;
            pnlPedido.Visible = false;

            cargarMesas();
            cargarMeseros();
        }

        private void cargarMesas()
        {
            MesaNegocio negocio = new MesaNegocio();

            var mesas = negocio.listar(true)
                .Where(x => x.estado == EstadoMesa.Libre)
                .ToList();

            ddlMesa.Items.Clear();

            ddlMesa.Items.Add(
                new ListItem("Seleccione una mesa", "0")
            );

            foreach (Mesa mesa in mesas)
            {
                ddlMesa.Items.Add(
                    new ListItem(
                        "Mesa " + mesa.numeromesa +
                        " - Capacidad: " + mesa.capacidad,
                        mesa.id.ToString()
                    )
                );
            }
        }

        private void cargarMeseros()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            var meseros = negocio.listarMeserosActivos();

            ddlMesero.Items.Clear();

            ddlMesero.Items.Add(
                new ListItem("Seleccione un mesero", "0")
            );

            foreach (Usuario mesero in meseros)
            {
                ddlMesero.Items.Add(
                    new ListItem(
                        mesero.nombre + " " + mesero.apellido,
                        mesero.id.ToString()
                    )
                );
            }
        }

        private void cargarInsumos()
        {
            InsumoNegocio negocio = new InsumoNegocio();

            var insumos = negocio.listar()
                .Where(x => x.activo && x.stockactual > 0)
                .ToList();

            ddlInsumo.Items.Clear();

            ddlInsumo.Items.Add(
                new ListItem("Seleccione un insumo", "0")
            );

            foreach (Insumo insumo in insumos)
            {
                ddlInsumo.Items.Add(
                    new ListItem(
                        insumo.nombre +
                        " - $" + insumo.precio.ToString("N2") +
                        " - Stock: " + insumo.stockactual,
                        insumo.id.ToString()
                    )
                );
            }
        }

        protected void btnAbrirPedido_Click(object sender, EventArgs e)
        {
            ocultarMensajes();

            try
            {
                int idMesa;
                int idMesero;

                if (!int.TryParse(ddlMesa.SelectedValue, out idMesa) || idMesa == 0)
                {
                    mostrarError("Seleccione una mesa.");
                    return;
                }

                if (!int.TryParse(ddlMesero.SelectedValue, out idMesero) || idMesero == 0)
                {
                    mostrarError("Seleccione un mesero.");
                    return;
                }

                PedidoNegocio negocio = new PedidoNegocio();

                int idPedido = negocio.abrirPedido(idMesa, idMesero);

                Response.Redirect(
                    "GestionDePedidos.aspx?id=" + idPedido,
                    false
                );

                return;
            }
            catch (Exception ex)
            {
                mostrarError(ex.Message);
            }
        }

        private void cargarPedido(int idPedido)
        {
            PedidoNegocio negocio = new PedidoNegocio();

            Pedido pedido = negocio.buscarPorId(idPedido);

            if (pedido == null)
            {
                mostrarError("El pedido no existe.");

                pnlNuevoPedido.Visible = false;
                pnlPedido.Visible = false;

                return;
            }

            pnlNuevoPedido.Visible = false;
            pnlPedido.Visible = true;

            lblNumeroPedido.Text =
                "#" + pedido.id;

            lblMesa.Text =
                "Mesa " + pedido.mesa.numeromesa;

            lblMesero.Text =
                pedido.mesero.nombre + " " +
                pedido.mesero.apellido;

            lblEstado.Text =
                pedido.estado.ToString();

            lblApertura.Text =
                pedido.fechahoraapertura
                    .ToString("dd/MM/yyyy HH:mm");

            if (pedido.fechahoracierre.HasValue)
            {
                lblCierre.Text =
                    pedido.fechahoracierre.Value
                        .ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                lblCierre.Text = "-";
            }

            EsPedidoAbierto =
                pedido.estado == EstadoPedido.Abierto;

            pnlAgregarInsumo.Visible =
                EsPedidoAbierto;

            pnlAcciones.Visible =
                EsPedidoAbierto;

            dgvDetalle.DataSource =
                pedido.items;

            dgvDetalle.DataBind();

            lblTotal.Text =
                "$ " + pedido.total.ToString("N2");

            if (EsPedidoAbierto)
            {
                cargarInsumos();
            }
        }

        protected void btnAgregarInsumo_Click(object sender, EventArgs e)
        {
            ocultarMensajes();

            try
            {
                int idPedido;
                int idInsumo;
                int cantidad;

                if (!int.TryParse(Request.QueryString["id"], out idPedido))
                {
                    mostrarError("Pedido invalido.");
                    return;
                }

                if (!int.TryParse(ddlInsumo.SelectedValue, out idInsumo) || idInsumo == 0)
                {
                    mostrarError("Seleccione un insumo.");
                    return;
                }

                if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad <= 0)
                {
                    mostrarError("La cantidad debe ser mayor a cero.");
                    return;
                }

                PedidoNegocio negocio = new PedidoNegocio();

                negocio.agregarDetalle(
                    idPedido,
                    idInsumo,
                    cantidad
                );

                txtCantidad.Text = "1";

                cargarPedido(idPedido);

                mostrarExito(
                    "Insumo agregado correctamente."
                );
            }
            catch (Exception ex)
            {
                mostrarError(ex.Message);
            }
        }

        protected void dgvDetalle_Accion(
            object sender,
            GridViewCommandEventArgs e)
        {
            ocultarMensajes();

            if (e.CommandName == "Quitar")
            {
                try
                {
                    int idDetalle =
                        int.Parse(
                            e.CommandArgument.ToString()
                        );

                    PedidoNegocio negocio =
                        new PedidoNegocio();

                    negocio.eliminarDetalle(idDetalle);

                    int idPedido =
                        int.Parse(
                            Request.QueryString["id"]
                        );

                    cargarPedido(idPedido);

                    mostrarExito(
                        "Insumo eliminado del pedido."
                    );
                }
                catch (Exception ex)
                {
                    mostrarError(ex.Message);
                }
            }
        }

        protected void btnCerrarPedido_Click(object sender, EventArgs e)
        {
            ocultarMensajes();

            try
            {
                int idPedido;

                if (!int.TryParse(Request.QueryString["id"], out idPedido))
                {
                    mostrarError("Pedido invalido.");
                    return;
                }

                PedidoNegocio negocio =
                    new PedidoNegocio();

                negocio.cerrarPedido(idPedido);

                Response.Redirect(
                    "Pedidos.aspx",
                    false
                );

                return;
            }
            catch (Exception ex)
            {
                mostrarError(ex.Message);
            }
        }

        protected void btnCancelarPedido_Click(object sender, EventArgs e)
        {
            ocultarMensajes();

            try
            {
                int idPedido;

                if (!int.TryParse(Request.QueryString["id"], out idPedido))
                {
                    mostrarError("Pedido invalido.");
                    return;
                }

                PedidoNegocio negocio =
                    new PedidoNegocio();

                negocio.cancelarPedido(idPedido);

                Response.Redirect(
                    "Pedidos.aspx",
                    false
                );

                return;
            }
            catch (Exception ex)
            {
                mostrarError(ex.Message);
            }
        }

        private void mostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;

            lblExito.Visible = false;
        }

        private void mostrarExito(string mensaje)
        {
            lblExito.Text = mensaje;
            lblExito.Visible = true;

            lblError.Visible = false;
        }

        private void ocultarMensajes()
        {
            lblError.Visible = false;
            lblExito.Visible = false;
        }
    }
}