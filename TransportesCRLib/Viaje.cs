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

        public string Id_viaje { get => _id_viaje; set => _id_viaje = value; }
        public string Lugar_inicio { get => _lugar_inicio; set => _lugar_inicio = value; }
        public string Lugar_final { get => _lugar_final; set => _lugar_final = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public string Tiempoestimado { get => _tiempoestimado; set => _tiempoestimado = value; }
        public string Identificacion { get => _identificacion; set => _identificacion = value; }
        public string Estado { get => _estado; set => _estado = value; }
    }
}
