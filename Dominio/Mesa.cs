namespace Dominio
{
    public class Mesa
    {
        private int _Id;
        private int _NumeroMesa;
        private int _Capacidad;
        private EstadoMesa _Estado;
        private Usuario _MeseroAsignado;
        private bool _Activa;

        public int id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int numeromesa
        {
            get { return _NumeroMesa; }
            set { _NumeroMesa = value; }
        }

        public int capacidad
        {
            get { return _Capacidad; }
            set { _Capacidad = value; }
        }

        public EstadoMesa estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        public Usuario meseroasignado
        {
            get { return _MeseroAsignado; }
            set { _MeseroAsignado = value; }
        }

        public bool activa
        {
            get { return _Activa; }
            set { _Activa = value; }
        }
    }
}