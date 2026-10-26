namespace Dominio
{
    public class DetallePedido
    {
        private int _Id;
        private Insumo _Insumo;
        private int _Cantidad;
        private decimal _PrecioUnitario;

        public int id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public Insumo insumo
        {
            get { return _Insumo; }
            set { _Insumo = value; }
        }

        public int cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        public decimal preciounitario
        {
            get { return _PrecioUnitario; }
            set { _PrecioUnitario = value; }
        }

        public decimal subtotal
        {
            get { return _Cantidad * _PrecioUnitario; }
        }
    }
}