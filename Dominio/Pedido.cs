using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio
{
    public class Pedido
    {
        private int _Id;
        private Mesa _Mesa;
        private Usuario _Mesero;
        private DateTime _FechaHoraApertura;
        private DateTime? _FechaHoraCierre;
        private EstadoPedido _Estado;
        private List<DetallePedido> _Items;

        public Pedido()
        {
            _Items = new List<DetallePedido>();
            _FechaHoraApertura = DateTime.Now;
            _Estado = EstadoPedido.Abierto;
        }

        public int id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public Mesa mesa
        {
            get { return _Mesa; }
            set { _Mesa = value; }
        }

        public Usuario mesero
        {
            get { return _Mesero; }
            set { _Mesero = value; }
        }

        public DateTime fechahoraapertura
        {
            get { return _FechaHoraApertura; }
            set { _FechaHoraApertura = value; }
        }

        public DateTime? fechahoracierre
        {
            get { return _FechaHoraCierre; }
            set { _FechaHoraCierre = value; }
        }

        public EstadoPedido estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        public List<DetallePedido> items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        public decimal total
        {
            get { return _Items.Sum(item => item.subtotal); }
        }
    }
}