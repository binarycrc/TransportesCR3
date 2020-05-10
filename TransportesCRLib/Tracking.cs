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

        /// <summary>
        /// Constructor de la clase Tracking
        /// </summary>
        /// <param name="id_viaje"></param>
        /// <param name="ubicacion"></param>
        /// <param name="observaciones"></param>
        public Tracking(string id_viaje, string ubicacion, string observaciones)
        {
            Id_viaje = id_viaje;
            Ubicacion = ubicacion;
            Observaciones = observaciones;
        }

        /// <summary>
        /// Propiedad para el campo Id_viaje
        /// </summary>
        public string Id_viaje { get => _id_viaje; set => _id_viaje = value; }

        /// <summary>
        /// Propiedad para el campo Ubicacion
        /// </summary>
        public string Ubicacion { get => _ubicacion; set => _ubicacion = value; }
        
        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get => _observaciones; set => _observaciones = value; }
    }
}
