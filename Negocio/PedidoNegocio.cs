using System;
using Dominio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PedidoNegocio
    {
        public List<Pedido> listar(EstadoPedido? estado = null, int? idMesero = null)
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta =
                    "SELECT P.Id, P.IdMesa, M.NumeroMesa, M.Capacidad, " +
                    "P.IdMesero, U.Nombre, U.Apellido, " +
                    "P.FechaHoraApertura, P.FechaHoraCierre, P.IdEstado " +
                    "FROM Pedidos P " +
                    "INNER JOIN Mesas M ON P.IdMesa = M.Id " +
                    "INNER JOIN Usuarios U ON P.IdMesero = U.Id";

                List<string> condiciones = new List<string>();
                if (estado.HasValue)
                    condiciones.Add("P.IdEstado = @estado");
                if (idMesero.HasValue)
                    condiciones.Add("P.IdMesero = @idMesero");

                if (condiciones.Count > 0)
                    consulta += " WHERE " + string.Join(" AND ", condiciones);

                consulta += " ORDER BY P.FechaHoraApertura DESC";

                datos.setearConsulta(consulta);
                if (estado.HasValue)
                    datos.setearParametro("@estado", (int)estado.Value);
                if (idMesero.HasValue)
                    datos.setearParametro("@idMesero", idMesero.Value);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido aux = mapearPedido(datos);
                    lista.Add(aux);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            foreach (Pedido p in lista)
            {
                p.items = listarDetalles(p.id);
            }

            return lista;
        }

        public Pedido buscarPorId(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            Pedido pedido = null;

            try
            {
                datos.setearConsulta(
                    "SELECT P.Id, P.IdMesa, M.NumeroMesa, M.Capacidad, " +
                    "P.IdMesero, U.Nombre, U.Apellido, " +
                    "P.FechaHoraApertura, P.FechaHoraCierre, P.IdEstado " +
                    "FROM Pedidos P " +
                    "INNER JOIN Mesas M ON P.IdMesa = M.Id " +
                    "INNER JOIN Usuarios U ON P.IdMesero = U.Id " +
                    "WHERE P.Id = @id"
                );
                datos.setearParametro("@id", idPedido);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    pedido = mapearPedido(datos);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            if (pedido != null)
                pedido.items = listarDetalles(pedido.id);

            return pedido;
        }

        public Pedido buscarAbiertoPorMesa(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            int idPedido = 0;

            try
            {
                datos.setearConsulta(
                    "SELECT TOP 1 Id FROM Pedidos WHERE IdMesa = @idMesa AND IdEstado = @estado ORDER BY Id DESC"
                );
                datos.setearParametro("@idMesa", idMesa);
                datos.setearParametro("@estado", (int)EstadoPedido.Abierto);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    idPedido = (int)datos.Lector["Id"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return idPedido > 0 ? buscarPorId(idPedido) : null;
        }

        public List<DetallePedido> listarDetalles(int idPedido)
        {
            List<DetallePedido> detalles = new List<DetallePedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT D.Id, D.IdInsumo, I.Nombre, D.Cantidad, D.PrecioUnitario " +
                    "FROM DetallesPedido D " +
                    "INNER JOIN Insumos I ON D.IdInsumo = I.Id " +
                    "WHERE D.IdPedido = @idPedido " +
                    "ORDER BY D.Id ASC"
                );
                datos.setearParametro("@idPedido", idPedido);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetallePedido aux = new DetallePedido();
                    aux.id = (int)datos.Lector["Id"];
                    aux.cantidad = (int)datos.Lector["Cantidad"];
                    aux.preciounitario = (decimal)datos.Lector["PrecioUnitario"];
                    aux.insumo = new Insumo
                    {
                        id = (int)datos.Lector["IdInsumo"],
                        nombre = (string)datos.Lector["Nombre"]
                    };

                    detalles.Add(aux);
                }

                return detalles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int abrirPedido(int idMesa, int idMesero)
        {
            MesaNegocio mesaNegocio = new MesaNegocio();
            Mesa mesa = mesaNegocio.buscarPorId(idMesa);

            if (mesa == null || !mesa.activa)
                throw new Exception("La mesa seleccionada no existe o esta inactiva.");

            if (mesa.estado != EstadoMesa.Libre)
                throw new Exception("Solo se puede abrir un pedido en una mesa libre.");

            if (buscarAbiertoPorMesa(idMesa) != null)
                throw new Exception("La mesa ya tiene un pedido abierto.");

            int idPedidoGenerado = 0;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "INSERT INTO Pedidos (IdMesa, IdMesero, FechaHoraApertura, IdEstado) " +
                    "OUTPUT INSERTED.Id " +
                    "VALUES (@idMesa, @idMesero, GETDATE(), @estado)"
                );
                datos.setearParametro("@idMesa", idMesa);
                datos.setearParametro("@idMesero", idMesero);
                datos.setearParametro("@estado", (int)EstadoPedido.Abierto);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    idPedidoGenerado = (int)datos.Lector["Id"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            mesaNegocio.actualizarEstadoYMesero(idMesa, EstadoMesa.Ocupada, idMesero);

            return idPedidoGenerado;
        }

        public void agregarDetalle(int idPedido, int idInsumo, int cantidad)
        {
            if (cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a cero.");

            Pedido pedido = buscarPorId(idPedido);
            if (pedido == null || pedido.estado != EstadoPedido.Abierto)
                throw new Exception("El pedido no existe o ya no esta abierto.");

            InsumoNegocio insumoNegocio = new InsumoNegocio();
            Insumo insumo = insumoNegocio.buscarPorId(idInsumo);

            if (insumo == null || !insumo.activo)
                throw new Exception("El insumo seleccionado no esta disponible.");

            if (insumo.stockactual < cantidad)
                throw new Exception("Stock insuficiente. Disponible: " + insumo.stockactual + ".");

            DetallePedido existente = pedido.items.Find(x => x.insumo.id == idInsumo);

            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (existente != null)
                {
                    datos.setearConsulta("UPDATE DetallesPedido SET Cantidad = Cantidad + @cantidad WHERE Id = @idDetalle");
                    datos.setearParametro("@cantidad", cantidad);
                    datos.setearParametro("@idDetalle", existente.id);
                    datos.ejecutarAccion();
                }
                else
                {
                    datos.setearConsulta(
                        "INSERT INTO DetallesPedido (IdPedido, IdInsumo, Cantidad, PrecioUnitario) " +
                        "VALUES (@idPedido, @idInsumo, @cantidad, @precio)"
                    );
                    datos.setearParametro("@idPedido", idPedido);
                    datos.setearParametro("@idInsumo", idInsumo);
                    datos.setearParametro("@cantidad", cantidad);
                    datos.setearParametro("@precio", insumo.precio);
                    datos.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            actualizarStock(idInsumo, -cantidad);
        }

        public void eliminarDetalle(int idDetalle, bool permitirEnCerrado = false)
        {
            int idPedido = 0;
            int idInsumo = 0;
            int cantidad = 0;

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdPedido, IdInsumo, Cantidad FROM DetallesPedido WHERE Id = @id");
                datos.setearParametro("@id", idDetalle);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    idPedido = (int)datos.Lector["IdPedido"];
                    idInsumo = (int)datos.Lector["IdInsumo"];
                    cantidad = (int)datos.Lector["Cantidad"];
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            Pedido pedido = buscarPorId(idPedido);
            if (pedido == null)
                throw new Exception("El pedido no existe.");

            if (pedido.estado == EstadoPedido.Cancelado)
                throw new Exception("No se pueden quitar insumos de un pedido cancelado.");

            if (pedido.estado != EstadoPedido.Abierto && !(permitirEnCerrado && pedido.estado == EstadoPedido.Cerrado))
                throw new Exception("Solo se pueden quitar insumos de un pedido abierto.");

            AccesoDatos datosDelete = new AccesoDatos();
            try
            {
                datosDelete.setearConsulta("DELETE FROM DetallesPedido WHERE Id = @id");
                datosDelete.setearParametro("@id", idDetalle);
                datosDelete.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosDelete.cerrarConexion();
            }

            actualizarStock(idInsumo, cantidad);
        }

        public void cerrarPedido(int idPedido)
        {
            Pedido pedido = buscarPorId(idPedido);
            if (pedido == null || pedido.estado != EstadoPedido.Abierto)
                throw new Exception("El pedido no existe o ya fue finalizado.");

            if (pedido.items.Count == 0)
                throw new Exception("No se puede cerrar un pedido sin insumos cargados. Si no hubo consumo, utilice Cancelar.");

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    "UPDATE Pedidos SET IdEstado = @estado, FechaHoraCierre = GETDATE() WHERE Id = @id"
                );
                datos.setearParametro("@estado", (int)EstadoPedido.Cerrado);
                datos.setearParametro("@id", idPedido);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            MesaNegocio mesaNegocio = new MesaNegocio();
            mesaNegocio.cambiarEstadoMesa(pedido.mesa.id, EstadoMesa.Libre);
        }

        public void cancelarPedido(int idPedido)
        {
            Pedido pedido = buscarPorId(idPedido);
            if (pedido == null || pedido.estado != EstadoPedido.Abierto)
                throw new Exception("El pedido no existe o ya fue finalizado.");

            foreach (DetallePedido item in pedido.items)
            {
                actualizarStock(item.insumo.id, item.cantidad);
            }

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    "UPDATE Pedidos SET IdEstado = @estado, FechaHoraCierre = GETDATE() WHERE Id = @id"
                );
                datos.setearParametro("@estado", (int)EstadoPedido.Cancelado);
                datos.setearParametro("@id", idPedido);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            MesaNegocio mesaNegocio = new MesaNegocio();
            mesaNegocio.cambiarEstadoMesa(pedido.mesa.id, EstadoMesa.Libre);
        }

        private void actualizarStock(int idInsumo, int delta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Insumos SET StockActual = StockActual + @delta WHERE Id = @id");
                datos.setearParametro("@delta", delta);
                datos.setearParametro("@id", idInsumo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private Pedido mapearPedido(AccesoDatos datos)
        {
            Pedido aux = new Pedido();
            aux.id = (int)datos.Lector["Id"];
            aux.fechahoraapertura = (DateTime)datos.Lector["FechaHoraApertura"];

            if (!(datos.Lector["FechaHoraCierre"] is DBNull))
                aux.fechahoracierre = (DateTime)datos.Lector["FechaHoraCierre"];

            aux.estado = (EstadoPedido)(int)datos.Lector["IdEstado"];

            aux.mesa = new Mesa
            {
                id = (int)datos.Lector["IdMesa"],
                numeromesa = (int)datos.Lector["NumeroMesa"],
                capacidad = (int)datos.Lector["Capacidad"]
            };

            aux.mesero = new Usuario
            {
                id = (int)datos.Lector["IdMesero"],
                nombre = (string)datos.Lector["Nombre"],
                apellido = (string)datos.Lector["Apellido"]
            };

            return aux;
        }
    }
}
