using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class InsumoNegocio
    {
        public List<Insumo> listar()
        {
            List<Insumo> lista = new List<Insumo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT I.Id, I.Nombre, I.Descripcion, I.Precio, I.StockActual, I.Activo, C.Id AS IdCategoria, C.Descripcion AS Categoria FROM Insumos I INNER JOIN CategoriasInsumo C ON I.IdCategoria = C.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Insumo aux = new Insumo();
                    aux.id = (int)datos.Lector["Id"];
                    aux.nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.descripcion = (string)datos.Lector["Descripcion"];

                    aux.precio = (decimal)datos.Lector["Precio"];
                    aux.stockactual = (int)datos.Lector["StockActual"];
                    aux.activo = (bool)datos.Lector["Activo"];

                    aux.categoria = new CategoriaInsumo();
                    aux.categoria.id = (int)datos.Lector["IdCategoria"];
                    aux.categoria.descripcion = (string)datos.Lector["Categoria"];

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

        public void agregar(Insumo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Insumos (Nombre, Descripcion, IdCategoria, Precio, StockActual, Activo) VALUES (@nombre, @descripcion, @idCategoria, @precio, @stockActual, @activo)");
                datos.setearParametro("@nombre", nuevo.nombre);
                datos.setearParametro("@descripcion", string.IsNullOrWhiteSpace(nuevo.descripcion) ? (object)DBNull.Value : nuevo.descripcion);
                datos.setearParametro("@idCategoria", nuevo.categoria.id);
                datos.setearParametro("@precio", nuevo.precio);
                datos.setearParametro("@stockActual", nuevo.stockactual);
                datos.setearParametro("@activo", nuevo.activo);
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

        public void cambiarEstado(int id, bool activo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Insumos SET Activo = @activo WHERE Id = @id");
                datos.setearParametro("@activo", activo);
                datos.setearParametro("@id", id);
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

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM Insumos WHERE Id = @id");
                datos.setearParametro("@id", id);
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
    }
}
