using Dominio;
using System.Web.SessionState;
using System.Web.UI;

namespace TPCuatrimestral_equipoD
{
    public static class Seguridad
    {
        public static Usuario ObtenerUsuario(HttpSessionState session)
        {
            return session != null ? session["usuario"] as Usuario : null;
        }

        public static bool SesionActiva(HttpSessionState session)
        {
            return ObtenerUsuario(session) != null;
        }

        public static bool EsGerente(HttpSessionState session)
        {
            Usuario usuario = ObtenerUsuario(session);
            return usuario != null && usuario.perfil == PerfilUsuario.Gerente;
        }

        public static bool EsMesero(HttpSessionState session)
        {
            Usuario usuario = ObtenerUsuario(session);
            return usuario != null && usuario.perfil == PerfilUsuario.Mesero;
        }

        public static void IniciarSesion(HttpSessionState session, Usuario usuario)
        {
            session["usuario"] = usuario;
        }

        public static void CerrarSesion(HttpSessionState session)
        {
            if (session != null)
            {
                session.Clear();
                session.Abandon();
            }
        }

        public static bool ValidarAcceso(Page page, bool soloGerente = false)
        {
            if (!SesionActiva(page.Session))
            {
                page.Response.Redirect("Login.aspx", true);
                return false;
            }

            if (soloGerente && !EsGerente(page.Session))
            {
                page.Response.Redirect("Mesas.aspx", true);
                return false;
            }

            return true;
        }
    }
}

