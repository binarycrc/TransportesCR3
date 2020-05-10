/*********************************************************************
 * Copyright 2020 Pablo Ugalde
 * Universidad Estatal A Distancia
 * PRIMER CUATRI-2020 00830 PROGRAMACION AVANZADA
 * 
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportesCRLib
{
    [Serializable]
    public class Viaje
    {
        string _id_viaje;
        string _identificacion;
        string _lugar_inicio;
        string _lugar_final;
        string _descripcion;
        string _tiempoestimado;
        string _estado;

        /// <summary>
        /// Constructor de la clase Camion
        /// </summary>
        /// <param name="id_viaje"></param>
        /// <param name="identificacion"></param>
        /// <param name="lugar_inicio"></param>
        /// <param name="lugar_final"></param>
        /// <param name="descripcion"></param>
        /// <param name="tiempoestimado"></param>
        /// <param name="estado"></param>
        public Viaje(string id_viaje, string identificacion, string lugar_inicio, string lugar_final, string descripcion, string tiempoestimado, string estado)
        {
            Id_viaje = id_viaje;
            Lugar_inicio = lugar_inicio;
            Lugar_final = lugar_final;
            Descripcion = descripcion;
            Tiempoestimado = tiempoestimado;
            Identificacion = identificacion;
            Estado = estado;
        }

        /// <summary>
        /// Propiedad para el campo Id_viaje
        /// </summary>
        public string Id_viaje { get => _id_viaje; set => _id_viaje = value; }

        /// <summary>
        /// Propiedad para el campo Lugar_inicio
        /// </summary>
        public string Lugar_inicio { get => _lugar_inicio; set => _lugar_inicio = value; }

        /// <summary>
        /// Propiedad para el campo Lugar_final
        /// </summary>
        public string Lugar_final { get => _lugar_final; set => _lugar_final = value; }

        /// <summary>
        /// Propiedad para el campo Descripcion
        /// </summary>
        public string Descripcion { get => _descripcion; set => _descripcion = value; }

        /// <summary>
        /// Propiedad para el campo Tiempoestimado
        /// </summary>
        public string Tiempoestimado { get => _tiempoestimado; set => _tiempoestimado = value; }

        /// <summary>
        /// Propiedad para el campo Identificacion
        /// </summary>
        public string Identificacion { get => _identificacion; set => _identificacion = value; }

        /// <summary>
        /// Propiedad para el campo Estado
        /// </summary>
        public string Estado { get => _estado; set => _estado = value; }
    }
}
