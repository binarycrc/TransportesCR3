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
    public class Camion
    {
        /// <summary>
        /// Clase para el tipo de Camion
        /// </summary>
        string _placa;
        string _modelo;
        string _marca;
        string _capacidadkilos;
        string _capacidadvolumen;
        /// <summary>
        /// Constructor de la clase Camion
        /// </summary>
        /// <param name="placa"></param>
        /// <param name="modelo"></param>
        /// <param name="marca"></param>
        /// <param name="capacidadkilos"></param>
        /// <param name="capacidadvolumen"></param>
        public Camion(string placa, string modelo, string marca, string capacidadkilos, string capacidadvolumen)
        {
            _placa = placa;
            _modelo = modelo;
            _marca = marca;
            _capacidadkilos = capacidadkilos;
            _capacidadvolumen = capacidadvolumen;
        }

        /// <summary>
        /// Propiedad para el campo placa
        /// </summary>
        public string Placa { get { return _placa; } set { _placa = value; } }
        /// <summary>
        /// Propiedad para el campo Modelo
        /// </summary>
        public string Modelo { get { return _modelo; } set { _modelo = value; } }
        /// <summary>
        /// Propiedad para el campo Marca
        /// </summary>
        public string Marca { get { return _marca; } set { _marca = value; } }
        /// <summary>
        /// Propiedad del campo capacidad de kilos 
        /// </summary>
        public string capacidadkilos { get { return _capacidadkilos; } set { _capacidadkilos = value; } }
        /// <summary>
        /// Propiedad del campo capacidad de volumen
        /// </summary>
        public string capacidadvolumen { get { return _capacidadvolumen; } set { _capacidadvolumen = value; } }
    }
}
