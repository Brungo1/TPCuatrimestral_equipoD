using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipoD
{
    public partial class Mesas : System.Web.UI.Page
    {
        protected bool EsGerente
        {
            get { return Seguridad.EsGerente(Session); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.ValidarAcceso(this)) return;

            divAccionesGerente.Visible = EsGerente;

            if (!IsPostBack)
            {
                CargarMesas();
            }
        }

        private void CargarMesas()
        {
            MesaNegocio negocio = new MesaNegocio();
            List<Mesa> lista;

            if (EsGerente)
            {
                lista = negocio.listar(false);
                lblSinMesas.Text = "No hay mesas cargadas. Podes crear la primera desde \"Agregar Mesa\".";
            }
            else
            {
                Usuario usuario = Seguridad.ObtenerUsuario(Session);
                lista = negocio.listarPorMesero(usuario.id);
                lblSinMesas.Text = "No tenes mesas asignadas por el momento.";
            }

            pnlSinMesas.Visible = (lista.Count == 0);
            repMesas.DataSource = lista;
            repMesas.DataBind();
        }

        protected void repMesas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!Seguridad.EsGerente(Session)) return;

            lblMensaje.Visible = false;
            MesaNegocio negocio = new MesaNegocio();

            if (e.CommandName == "CambiarEstadoMesa")
            {
                string[] datos = e.CommandArgument.ToString().Split(',');
                int id;
                int estadoInt;
                if (datos.Length == 2 && int.TryParse(datos[0], out id) && int.TryParse(datos[1], out estadoInt))
                {
                    Mesa mesa = negocio.buscarPorId(id);
                    if (mesa == null)
                    {
                        CargarMesas();
                        return;
                    }

                    if (!mesa.activa || mesa.estado == EstadoMesa.Ocupada)
                    {
                        lblMensaje.Text = "No se puede cambiar el estado de una mesa inactiva u ocupada.";
                        lblMensaje.Visible = true;
                        CargarMesas();
                        return;
                    }

                    EstadoMesa nuevoEstado = (mesa.estado == EstadoMesa.Libre) ? EstadoMesa.Cerrada : EstadoMesa.Libre;
                    negocio.cambiarEstadoMesa(id, nuevoEstado);
                    CargarMesas();
                }
            }
            else if (e.CommandName == "CambiarActiva")
            {
                string[] datos = e.CommandArgument.ToString().Split(',');
                int id;
                bool activaActual;
                if (datos.Length == 2 && int.TryParse(datos[0], out id) && bool.TryParse(datos[1], out activaActual))
                {
                    if (activaActual)
                    {
                        Mesa mesa = negocio.buscarPorId(id);
                        if (mesa != null && mesa.estado == EstadoMesa.Ocupada)
                        {
                            lblMensaje.Text = "No se puede dar de baja una mesa que esta ocupada.";
                            lblMensaje.Visible = true;
                            return;
                        }
                    }

                    negocio.cambiarActiva(id, !activaActual);
                    CargarMesas();
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                int id;
                if (int.TryParse(e.CommandArgument.ToString(), out id))
                {
                    Mesa mesa = negocio.buscarPorId(id);
                    if (mesa != null && mesa.estado == EstadoMesa.Ocupada)
                    {
                        lblMensaje.Text = "No se puede eliminar una mesa que esta ocupada.";
                        lblMensaje.Visible = true;
                        return;
                    }

                    try
                    {
                        negocio.eliminar(id);
                        CargarMesas();
                    }
                    catch (Exception)
                    {
                        lblMensaje.Text = "No se puede eliminar la mesa porque tiene pedidos asociados. Utilice 'Dar de Baja'.";
                        lblMensaje.Visible = true;
                    }
                }
            }
        }

        protected string ObtenerClaseBadge(EstadoMesa estado)
        {
            switch (estado)
            {
                case EstadoMesa.Libre:
                    return "badge bg-success";
                case EstadoMesa.Ocupada:
                    return "badge bg-danger";
                default:
                    return "badge bg-secondary";
            }
        }
    }
}
