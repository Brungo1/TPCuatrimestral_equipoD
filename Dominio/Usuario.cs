namespace Dominio
{
    public class Usuario
    {
        private int _Id;
        private string _NombreUsuario;
        private string _Contrasenia;
        private string _Nombre;
        private string _Apellido;
        private PerfilUsuario _Perfil;
        private bool _Activo;

        public int id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string nombreusuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }

        public string contrasenia
        {
            get { return _Contrasenia; }
            set { _Contrasenia = value; }
        }

        public string nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string apellido
        {
            get { return _Apellido; }
            set { _Apellido = value; }
        }

        public PerfilUsuario perfil
        {
            get { return _Perfil; }
            set { _Perfil = value; }
        }

        public bool activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }
    }
}