using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class CategoriaInsumoNegocio
    {
        public List<CategoriaInsumo> listar()
        {
            List<CategoriaInsumo> lista = new List<CategoriaInsumo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Descripcion, Activo FROM CategoriasInsumo WHERE Activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    CategoriaInsumo aux = new CategoriaInsumo();
                    aux.id = (int)datos.Lector["Id"];
                    aux.descripcion = (string)datos.Lector["Descripcion"];
                    aux.activo = (bool)datos.Lector["Activo"];

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
