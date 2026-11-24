using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TPCuatrimestral_equipoD
{
    public partial class Reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.ValidarAcceso(this, true)) return;

            if (!IsPostBack)
            {
                cargarReportes();
            }
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            cargarReportes();
        }

        private void cargarReportes()
        {
            PedidoNegocio negocio = new PedidoNegocio();

            List<Pedido> pedidos =
                negocio.listar(EstadoPedido.Cerrado);

            if (!string.IsNullOrEmpty(txtDesde.Text))
            {
                DateTime desde = DateTime.Parse(txtDesde.Text);

                pedidos = pedidos
                    .Where(x => x.fechahoracierre.HasValue &&
                                x.fechahoracierre.Value.Date >= desde.Date)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(txtHasta.Text))
            {
                DateTime hasta = DateTime.Parse(txtHasta.Text);

                pedidos = pedidos
                    .Where(x => x.fechahoracierre.HasValue &&
                                x.fechahoracierre.Value.Date <= hasta.Date)
                    .ToList();
            }

            lblCantidadPedidos.Text =
                pedidos.Count.ToString();

            decimal total =
                pedidos.Sum(x => x.total);

            lblTotalRecaudado.Text =
                "$ " + total.ToString("N2");


            var reporteMesas =
                pedidos
                .GroupBy(x => x.mesa.numeromesa)
                .Select(x => new
                {
                    Mesa = "Mesa " + x.Key,
                    Pedidos = x.Count(),
                    Total = x.Sum(p => p.total)
                })
                .OrderBy(x => x.Mesa)
                .ToList();

            dgvMesas.DataSource = reporteMesas;
            dgvMesas.DataBind();


            var reporteMeseros =
                pedidos
                .GroupBy(x => new
                {
                    x.mesero.id,
                    x.mesero.nombre,
                    x.mesero.apellido
                })
                .Select(x => new
                {
                    Mesero =
                        x.Key.nombre + " " + x.Key.apellido,

                    Pedidos = x.Count(),

                    Total = x.Sum(p => p.total)
                })
                .OrderByDescending(x => x.Total)
                .ToList();

            dgvMeseros.DataSource = reporteMeseros;
            dgvMeseros.DataBind();
        }
    }
}