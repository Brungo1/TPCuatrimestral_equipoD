namespace Dominio
{
    public class Insumo
    {
        private int _Id;
        private string _Nombre;
        private string _Descripcion;
        private CategoriaInsumo _Categoria;
        private decimal _Precio;
        private int _StockActual;
        private bool _Activo;

        public int id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public CategoriaInsumo categoria
        {
            get { return _Categoria; }
            set { _Categoria = value; }
        }

        public decimal precio
        {
            get { return _Precio; }
            set { _Precio = value; }
        }

        public int stockactual
        {
            get { return _StockActual; }
            set { _StockActual = value; }
        }

        public bool activo
        {
            get { return _Activo; }
            set { _Activo = value; }
        }
    }
}