using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MesaNegocio
    {
        public List<Mesa> listar(bool soloActivas = false)
        {
            List<Mesa> lista = new List<Mesa>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta =
                    "SELECT M.Id, M.NumeroMesa, M.Capacidad, M.IdEstado, M.IdMeseroAsignado, M.Activa, " +
                    "U.Nombre, U.Apellido " +
                    "FROM Mesas M " +
                    "LEFT JOIN Usuarios U ON M.IdMeseroAsignado = U.Id";

                if (soloActivas)
                    consulta += " WHERE M.Activa = 1";

                consulta += " ORDER BY M.NumeroMesa ASC";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Mesa aux = new Mesa();
                    aux.id = (int)datos.Lector["Id"];
                    aux.numeromesa = (int)datos.Lector["NumeroMesa"];
                    aux.capacidad = (int)datos.Lector["Capacidad"];
                    aux.estado = (EstadoMesa)(int)datos.Lector["IdEstado"];
                    aux.activa = (bool)datos.Lector["Activa"];

                    if (!(datos.Lector["IdMeseroAsignado"] is DBNull))
                    {
                        aux.meseroasignado = new Usuario();
                        aux.meseroasignado.id = (int)datos.Lector["IdMeseroAsignado"];
                        aux.meseroasignado.nombre = (string)datos.Lector["Nombre"];
                        aux.meseroasignado.apellido = (string)datos.Lector["Apellido"];
                    }

                    lista.Add(aux);
                }

                return lista;
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

        public Mesa buscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT M.Id, M.NumeroMesa, M.Capacidad, M.IdEstado, M.IdMeseroAsignado, M.Activa, " +
                    "U.Nombre, U.Apellido " +
                    "FROM Mesas M " +
                    "LEFT JOIN Usuarios U ON M.IdMeseroAsignado = U.Id " +
                    "WHERE M.Id = @id"
                );
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Mesa aux = new Mesa();
                    aux.id = (int)datos.Lector["Id"];
                    aux.numeromesa = (int)datos.Lector["NumeroMesa"];
                    aux.capacidad = (int)datos.Lector["Capacidad"];
                    aux.estado = (EstadoMesa)(int)datos.Lector["IdEstado"];
                    aux.activa = (bool)datos.Lector["Activa"];

                    if (!(datos.Lector["IdMeseroAsignado"] is DBNull))
                    {
                        aux.meseroasignado = new Usuario();
                        aux.meseroasignado.id = (int)datos.Lector["IdMeseroAsignado"];
                        aux.meseroasignado.nombre = (string)datos.Lector["Nombre"];
                        aux.meseroasignado.apellido = (string)datos.Lector["Apellido"];
                    }

                    return aux;
                }

                return null;
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

        public void agregar(Mesa nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "INSERT INTO Mesas (NumeroMesa, Capacidad, IdEstado, IdMeseroAsignado, Activa) " +
                    "VALUES (@numero, @capacidad, @estado, @idMesero, @activa)"
                );
                datos.setearParametro("@numero", nueva.numeromesa);
                datos.setearParametro("@capacidad", nueva.capacidad);
                datos.setearParametro("@estado", (int)EstadoMesa.Libre);
                datos.setearParametro("@idMesero", (nueva.meseroasignado != null && nueva.meseroasignado.id > 0) ? (object)nueva.meseroasignado.id : DBNull.Value);
                datos.setearParametro("@activa", nueva.activa);
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

        public void modificar(Mesa mesa)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "UPDATE Mesas SET NumeroMesa = @numero, Capacidad = @capacidad, IdMeseroAsignado = @idMesero, Activa = @activa " +
                    "WHERE Id = @id"
                );
                datos.setearParametro("@numero", mesa.numeromesa);
                datos.setearParametro("@capacidad", mesa.capacidad);
                datos.setearParametro("@idMesero", (mesa.meseroasignado != null && mesa.meseroasignado.id > 0) ? (object)mesa.meseroasignado.id : DBNull.Value);
                datos.setearParametro("@activa", mesa.activa);
                datos.setearParametro("@id", mesa.id);
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

        public void actualizarEstadoYMesero(int idMesa, EstadoMesa estado, int? idMesero)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "UPDATE Mesas SET IdEstado = @estado, IdMeseroAsignado = @idMesero WHERE Id = @id"
                );
                datos.setearParametro("@estado", (int)estado);
                datos.setearParametro("@idMesero", idMesero.HasValue ? (object)idMesero.Value : DBNull.Value);
                datos.setearParametro("@id", idMesa);
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

        public void asignarMesero(int idMesa, int? idMesero)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Mesas SET IdMeseroAsignado = @idMesero WHERE Id = @id");
                datos.setearParametro("@idMesero", (idMesero.HasValue && idMesero.Value > 0) ? (object)idMesero.Value : DBNull.Value);
                datos.setearParametro("@id", idMesa);
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

        public void limpiarAsignacionesMesasNoOcupadas()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Mesas SET IdMeseroAsignado = NULL WHERE IdEstado <> @ocupada");
                datos.setearParametro("@ocupada", (int)EstadoMesa.Ocupada);
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

        public void cambiarEstadoMesa(int idMesa, EstadoMesa estado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Mesas SET IdEstado = @estado WHERE Id = @id");
                datos.setearParametro("@estado", (int)estado);
                datos.setearParametro("@id", idMesa);
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

        public void cambiarActiva(int idMesa, bool activa)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Mesas SET Activa = @activa WHERE Id = @id");
                datos.setearParametro("@activa", activa);
                datos.setearParametro("@id", idMesa);
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

        public void eliminar(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM Mesas WHERE Id = @id");
                datos.setearParametro("@id", idMesa);
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

        public bool existeNumeroMesa(int numeroMesa, int idExcluir = 0)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) AS Cantidad FROM Mesas WHERE NumeroMesa = @numero AND Id <> @id");
                datos.setearParametro("@numero", numeroMesa);
                datos.setearParametro("@id", idExcluir);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector["Cantidad"] > 0;

                return false;
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
    }
}
