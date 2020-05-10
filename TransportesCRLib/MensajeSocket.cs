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
    /// <summary>
    /// Clase de tipo generico 
    /// para empaquetar otras entidades en la trasmision 
    /// cliente/servidor tcp
    /// </summary>
    public class MensajeSocket<T>
    {
        /// <summary>
        /// Propiedad para el campo Mensaje
        /// </summary>
        public string Mensaje { get; set; }

        /// <summary>
        /// Propiedad para el campo Valor
        /// </summary>
        public T Valor { get; set; }

    }
}
