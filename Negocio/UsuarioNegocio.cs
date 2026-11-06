using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT Id, NombreUsuario, Contrasenia, Nombre, Apellido, IdPerfil, Activo FROM Usuarios"
                );

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.id = (int)datos.Lector["Id"];
                    aux.nombreusuario = (string)datos.Lector["NombreUsuario"];
                    aux.contrasenia = (string)datos.Lector["Contrasenia"];
                    aux.nombre = (string)datos.Lector["Nombre"];
                    aux.apellido = (string)datos.Lector["Apellido"];
                    aux.perfil = (PerfilUsuario)(int)datos.Lector["IdPerfil"];
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

        public Usuario buscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT Id, NombreUsuario, Contrasenia, Nombre, Apellido, IdPerfil, Activo " +
                    "FROM Usuarios WHERE Id = @id"
                );

                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.id = (int)datos.Lector["Id"];
                    aux.nombreusuario = (string)datos.Lector["NombreUsuario"];
                    aux.contrasenia = (string)datos.Lector["Contrasenia"];
                    aux.nombre = (string)datos.Lector["Nombre"];
                    aux.apellido = (string)datos.Lector["Apellido"];
                    aux.perfil = (PerfilUsuario)(int)datos.Lector["IdPerfil"];
                    aux.activo = (bool)datos.Lector["Activo"];

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

        public void agregar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "INSERT INTO Usuarios " +
                    "(NombreUsuario, Contrasenia, Nombre, Apellido, IdPerfil, Activo) " +
                    "VALUES (@usuario, @contrasenia, @nombre, @apellido, @perfil, @activo)"
                );

                datos.setearParametro("@usuario", nuevo.nombreusuario);
                datos.setearParametro("@contrasenia", nuevo.contrasenia);
                datos.setearParametro("@nombre", nuevo.nombre);
                datos.setearParametro("@apellido", nuevo.apellido);
                datos.setearParametro("@perfil", (int)nuevo.perfil);
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

        public void modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "UPDATE Usuarios SET " +
                    "NombreUsuario = @usuario, " +
                    "Contrasenia = @contrasenia, " +
                    "Nombre = @nombre, " +
                    "Apellido = @apellido, " +
                    "IdPerfil = @perfil, " +
                    "Activo = @activo " +
                    "WHERE Id = @id"
                );

                datos.setearParametro("@usuario", usuario.nombreusuario);
                datos.setearParametro("@contrasenia", usuario.contrasenia);
                datos.setearParametro("@nombre", usuario.nombre);
                datos.setearParametro("@apellido", usuario.apellido);
                datos.setearParametro("@perfil", (int)usuario.perfil);
                datos.setearParametro("@activo", usuario.activo);
                datos.setearParametro("@id", usuario.id);

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
                datos.setearConsulta(
                    "UPDATE Usuarios SET Activo = @activo WHERE Id = @id"
                );

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

        public bool existeNombreUsuario(string nombreUsuario, int idExcluir)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT COUNT(*) AS Cantidad " +
                    "FROM Usuarios " +
                    "WHERE NombreUsuario = @usuario AND Id <> @id"
                );

                datos.setearParametro("@usuario", nombreUsuario);
                datos.setearParametro("@id", idExcluir);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["Cantidad"] > 0;
                }

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
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "DELETE FROM Usuarios WHERE Id = @id"
                );

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