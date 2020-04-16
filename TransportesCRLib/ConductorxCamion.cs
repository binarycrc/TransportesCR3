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
    public class ConductorxCamion
    {
        /// <summary>
        /// Clase para el tipo de ConductorxCamion
        /// </summary>
        string _identificacion;
        string _placa;
        /// <summary>
        /// Constructor de la clase ConductorxCamion
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="placa"></param>
        public ConductorxCamion(string identificacion, string placa)
        {
            _identificacion = identificacion;
            _placa = placa;
        }
        /// <summary>
        /// Propiedad para el campo Identificacion
        /// </summary>
        public string Identificacion { get { return _identificacion; } set { _identificacion = value; } }
        /// <summary>
        /// Propiedad para el campo Placa
        /// </summary>
        public string Placa { get { return _placa; } set { _placa = value; } }
    }
}
