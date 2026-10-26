namespace Dominio
{
    public class CategoriaInsumo
    {
        private int _Id;
        private string _Descripcion;
        private bool _Activo;

        public int id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public bool activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }

        public override string ToString()
        {
            return _Descripcion;
        }
    }
}