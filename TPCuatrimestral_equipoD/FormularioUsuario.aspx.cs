using System;
using Dominio;
using Negocio;

namespace TPCuatrimestral_equipoD
{
    public partial class FormularioUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);

                    cargarUsuario(id);
                }
            }
        }

        private void cargarUsuario(int id)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            Usuario usuario = negocio.buscarPorId(id);

            if (usuario != null)
            {
                txtUsuario.Text = usuario.nombreusuario;
                txtContrasenia.Text = usuario.contrasenia;
                txtNombre.Text = usuario.nombre;
                txtApellido.Text = usuario.apellido;
                ddlPerfil.SelectedValue = ((int)usuario.perfil).ToString();
            }
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();

                int id = 0;

                if (Request.QueryString["id"] != null)
                    id = int.Parse(Request.QueryString["id"]);


                if (txtUsuario.Text == "" ||
                    txtNombre.Text == "" ||
                    txtApellido.Text == "")
                {
                    lblError.Text = "Complete todos los campos.";
                    lblError.Visible = true;
                    return;
                }


                if (negocio.existeNombreUsuario(txtUsuario.Text, id))
                {
                    lblError.Text = "El nombre de usuario ya existe.";
                    lblError.Visible = true;
                    return;
                }


                Usuario usuario = new Usuario();

                usuario.id = id;
                usuario.nombreusuario = txtUsuario.Text;
                usuario.nombre = txtNombre.Text;
                usuario.apellido = txtApellido.Text;
                usuario.perfil = (PerfilUsuario)int.Parse(ddlPerfil.SelectedValue);
                usuario.activo = true;


                if (id == 0)
                {
                    if (txtContrasenia.Text == "")
                    {
                        lblError.Text = "Ingrese una contraseña.";
                        lblError.Visible = true;
                        return;
                    }

                    usuario.contrasenia = txtContrasenia.Text;

                    negocio.agregar(usuario);
                }
                else
                {
                    Usuario usuarioActual = negocio.buscarPorId(id);

                    if (txtContrasenia.Text == "")
                        usuario.contrasenia = usuarioActual.contrasenia;
                    else
                        usuario.contrasenia = txtContrasenia.Text;

                    usuario.activo = usuarioActual.activo;

                    negocio.modificar(usuario);
                }


                Response.Redirect("Usuarios.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }
    }
}