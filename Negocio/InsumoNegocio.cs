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
    }
}
