using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class CategoriaInsumoNegocio
    {
        public List<CategoriaInsumo> listar(bool soloActivas = false)
        {
            List<CategoriaInsumo> lista = new List<CategoriaInsumo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT Id, Descripcion, Activo FROM CategoriasInsumo";
                if (soloActivas)
                    consulta += " WHERE Activo = 1";

                datos.setearConsulta(consulta);
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

        public void agregar(CategoriaInsumo nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO CategoriasInsumo (Descripcion, Activo) VALUES (@descripcion, 1)");
                datos.setearParametro("@descripcion", nueva.descripcion);
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
                datos.setearConsulta("UPDATE CategoriasInsumo SET Activo = @activo WHERE Id = @id");
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
                datos.setearConsulta("DELETE FROM CategoriasInsumo WHERE Id = @id");
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
