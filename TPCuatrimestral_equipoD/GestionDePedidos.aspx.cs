using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipoD
{
    public partial class GestionDePedidos : System.Web.UI.Page
    {
        protected bool EsPedidoAbierto { get; set; }
        protected bool PuedeQuitarInsumo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.ValidarAcceso(this)) return;

            if (!IsPostBack)
            {
                int idPedido;
                int idMesa;

                if (int.TryParse(Request.QueryString["id"], out idPedido))
                {
                    cargarPedido(idPedido);
                }
                else if (int.TryParse(Request.QueryString["idMesa"], out idMesa))
                {
                    manejarAccesoPorMesa(idMesa);
                }
                else
                {
                    prepararNuevoPedido(0);
                }
            }
        }

        private void manejarAccesoPorMesa(int idMesa)
        {
            MesaNegocio mesaNegocio = new MesaNegocio();
            Mesa mesa = mesaNegocio.buscarPorId(idMesa);

            if (mesa == null || !mesa.activa)
            {
                Response.Redirect("Mesas.aspx", false);
                return;
            }

            if (!Seguridad.EsGerente(Session))
            {
                Usuario usuario = Seguridad.ObtenerUsuario(Session);
                if (usuario == null || mesa.meseroasignado == null || mesa.meseroasignado.id != usuario.id)
                {
                    Response.Redirect("Mesas.aspx", false);
                    return;
                }
            }

            PedidoNegocio pedidoNegocio = new PedidoNegocio();
            Pedido abierto = pedidoNegocio.buscarAbiertoPorMesa(idMesa);
            if (abierto != null)
            {
                Response.Redirect("GestionDePedidos.aspx?id=" + abierto.id, false);
                return;
            }

            if (mesa.estado == EstadoMesa.Libre)
            {
                prepararNuevoPedido(idMesa);
            }
            else
            {
                Response.Redirect("Mesas.aspx", false);
            }
        }

        private void prepararNuevoPedido(int idMesaPreseleccionada = 0)
        {
            pnlNuevoPedido.Visible = true;
            pnlPedido.Visible = false;

            cargarMesas(idMesaPreseleccionada);
            cargarMeseros(idMesaPreseleccionada);
        }

        private void cargarMesas(int idMesaPreseleccionada = 0)
        {
            MesaNegocio negocio = new MesaNegocio();
            List<Mesa> mesas;

            if (Seguridad.EsGerente(Session))
            {
                mesas = negocio.listar(true)
                    .Where(x => x.estado == EstadoMesa.Libre)
                    .ToList();
            }
            else
            {
                Usuario usuario = Seguridad.ObtenerUsuario(Session);
                mesas = negocio.listarPorMesero(usuario.id)
                    .Where(x => x.estado == EstadoMesa.Libre)
                    .ToList();
            }

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

            if (idMesaPreseleccionada > 0 && ddlMesa.Items.FindByValue(idMesaPreseleccionada.ToString()) != null)
            {
                ddlMesa.SelectedValue = idMesaPreseleccionada.ToString();
            }
        }

        private void cargarMeseros(int idMesaPreseleccionada = 0)
        {
            ddlMesero.Items.Clear();

            if (Seguridad.EsGerente(Session))
            {
                ddlMesero.Enabled = true;

                UsuarioNegocio negocio = new UsuarioNegocio();
                var meseros = negocio.listarMeserosActivos();

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

                if (idMesaPreseleccionada > 0)
                {
                    Mesa mesa = new MesaNegocio().buscarPorId(idMesaPreseleccionada);
                    if (mesa != null && mesa.meseroasignado != null)
                    {
                        string idMeseroStr = mesa.meseroasignado.id.ToString();
                        if (ddlMesero.Items.FindByValue(idMeseroStr) != null)
                        {
                            ddlMesero.SelectedValue = idMeseroStr;
                        }
                    }
                }
            }
            else
            {
                Usuario usuario = Seguridad.ObtenerUsuario(Session);
                ddlMesero.Items.Add(
                    new ListItem(
                        usuario.nombre + " " + usuario.apellido,
                        usuario.id.ToString()
                    )
                );
                ddlMesero.SelectedValue = usuario.id.ToString();
                ddlMesero.Enabled = false;
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

        private bool mesaAsignadaAlMesero(int idMesa, int idMesero)
        {
            Mesa mesa = new MesaNegocio().buscarPorId(idMesa);
            return mesa != null && mesa.meseroasignado != null && mesa.meseroasignado.id == idMesero;
        }

        private bool puedeAccederPedido(Pedido pedido)
        {
            if (Seguridad.EsGerente(Session))
                return true;

            if (pedido == null)
                return false;

            Usuario usuario = Seguridad.ObtenerUsuario(Session);
            if (usuario == null)
                return false;

            return (pedido.mesero != null && pedido.mesero.id == usuario.id) ||
                   (pedido.mesa != null && mesaAsignadaAlMesero(pedido.mesa.id, usuario.id));
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

                if (!Seguridad.EsGerente(Session))
                {
                    Usuario usuario = Seguridad.ObtenerUsuario(Session);
                    idMesero = usuario.id;

                    Mesa mesa = new MesaNegocio().buscarPorId(idMesa);
                    if (mesa == null || !mesa.activa || mesa.estado != EstadoMesa.Libre ||
                        mesa.meseroasignado == null || mesa.meseroasignado.id != usuario.id)
                    {
                        mostrarError("No tenes permiso para abrir un pedido en esa mesa.");
                        return;
                    }
                }
                else
                {
                    if (!int.TryParse(ddlMesero.SelectedValue, out idMesero) || idMesero == 0)
                    {
                        mostrarError("Seleccione un mesero.");
                        return;
                    }
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

            if (!puedeAccederPedido(pedido))
            {
                Response.Redirect("Mesas.aspx", false);
                return;
            }

            pnlNuevoPedido.Visible = false;
            pnlPedido.Visible = true;

            lblNumeroPedido.Text = "#" + pedido.id;
            lblMesa.Text = "Mesa " + pedido.mesa.numeromesa;
            lblMesero.Text = pedido.mesero.nombre + " " + pedido.mesero.apellido;
            lblEstado.Text = pedido.estado.ToString();
            lblApertura.Text = pedido.fechahoraapertura.ToString("dd/MM/yyyy HH:mm");

            if (pedido.fechahoracierre.HasValue)
            {
                lblCierre.Text = pedido.fechahoracierre.Value.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                lblCierre.Text = "-";
            }

            EsPedidoAbierto = pedido.estado == EstadoPedido.Abierto;
            PuedeQuitarInsumo = EsPedidoAbierto || (pedido.estado == EstadoPedido.Cerrado && Seguridad.EsGerente(Session));

            pnlAgregarInsumo.Visible = EsPedidoAbierto;
            pnlAcciones.Visible = EsPedidoAbierto;

            dgvDetalle.Columns[4].Visible = PuedeQuitarInsumo;
            dgvDetalle.DataSource = pedido.items;
            dgvDetalle.DataBind();

            lblTotal.Text = "$ " + pedido.total.ToString("N2");

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

                PedidoNegocio negocio = new PedidoNegocio();
                Pedido pedido = negocio.buscarPorId(idPedido);
                if (!puedeAccederPedido(pedido))
                {
                    mostrarError("No tenes permiso para modificar este pedido.");
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

                negocio.agregarDetalle(idPedido, idInsumo, cantidad);

                txtCantidad.Text = "1";

                cargarPedido(idPedido);
            }
            catch (Exception ex)
            {
                mostrarError(ex.Message);
            }
        }

        protected void dgvDetalle_Accion(object sender, GridViewCommandEventArgs e)
        {
            ocultarMensajes();

            if (e.CommandName == "Quitar")
            {
                try
                {
                    int idPedido;
                    if (!int.TryParse(Request.QueryString["id"], out idPedido))
                    {
                        mostrarError("Pedido invalido.");
                        return;
                    }

                    PedidoNegocio negocio = new PedidoNegocio();
                    Pedido pedido = negocio.buscarPorId(idPedido);
                    if (!puedeAccederPedido(pedido))
                    {
                        mostrarError("No tenes permiso para modificar este pedido.");
                        return;
                    }

                    bool esGerente = Seguridad.EsGerente(Session);
                    if (pedido.estado == EstadoPedido.Cancelado || (!esGerente && pedido.estado != EstadoPedido.Abierto))
                    {
                        mostrarError("No tenes permiso para quitar insumos de este pedido.");
                        return;
                    }

                    int idDetalle = int.Parse(e.CommandArgument.ToString());

                    negocio.eliminarDetalle(idDetalle, esGerente);

                    cargarPedido(idPedido);
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

                PedidoNegocio negocio = new PedidoNegocio();
                Pedido pedido = negocio.buscarPorId(idPedido);
                if (!puedeAccederPedido(pedido))
                {
                    mostrarError("No tenes permiso para modificar este pedido.");
                    return;
                }

                negocio.cerrarPedido(idPedido);

                Response.Redirect("Pedidos.aspx", false);

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

                PedidoNegocio negocio = new PedidoNegocio();
                Pedido pedido = negocio.buscarPorId(idPedido);
                if (!puedeAccederPedido(pedido))
                {
                    mostrarError("No tenes permiso para modificar este pedido.");
                    return;
                }

                negocio.cancelarPedido(idPedido);

                Response.Redirect("Pedidos.aspx", false);

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
        }

        private void ocultarMensajes()
        {
            lblError.Visible = false;
        }
    }
}
