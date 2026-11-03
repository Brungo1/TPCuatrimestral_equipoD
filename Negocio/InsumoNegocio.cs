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
                datos.setearConsulta("INSERT INTO Insumos (Nombre, Descripcion, IdCategoria, Precio, StockActual, Activo) VALUES (@nombre, @descripcion, @idCategoria, @precio, @stock, 1)");
                datos.setearParametro("@nombre", nuevo.nombre);
                datos.setearParametro("@descripcion", nuevo.descripcion);
                datos.setearParametro("@idCategoria", nuevo.categoria.id);
                datos.setearParametro("@precio", nuevo.precio);
                datos.setearParametro("@stock", nuevo.stockactual);

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

        public void modificar(Insumo insumo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Insumos SET Nombre = @nombre, Descripcion = @descripcion, IdCategoria = @idCategoria, Precio = @precio, StockActual = @stock WHERE Id = @id");

                datos.setearParametro("@nombre", insumo.nombre);
                datos.setearParametro("@descripcion", insumo.descripcion);
                datos.setearParametro("@idCategoria", insumo.categoria.id);
                datos.setearParametro("@precio", insumo.precio);
                datos.setearParametro("@stock", insumo.stockactual);
                datos.setearParametro("@id", insumo.id); 

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

        public void cambiarEstado(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Insumos SET Activo = CASE WHEN Activo = 1 THEN 0 ELSE 1 END WHERE Id = @id");
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
