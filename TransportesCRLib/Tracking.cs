using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportesCRLib
{
    [Serializable]
    public class Tracking
    {
        string _id_viaje;
        string _ubicacion;
        string _observaciones;

        public Tracking(string id_viaje, string ubicacion, string observaciones)
        {
            Id_viaje = id_viaje;
            Ubicacion = ubicacion;
            Observaciones = observaciones;
        }

        public string Id_viaje { get => _id_viaje; set => _id_viaje = value; }
        public string Ubicacion { get => _ubicacion; set => _ubicacion = value; }
        public string Observaciones { get => _observaciones; set => _observaciones = value; }
    }
}
