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
    class Conductor
    {
        /// <summary> 3
        /// Clase base para registro para tipos de empleados
        /// </summary>
        string _identificacion;
        string _nombre;
        string _papellido;
        string _sapellido;
        string _ruta;
        /// <summary>
        /// Constructor de la clase Empleado
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="nombre"></param>
        /// <param name="papellido"></param>
        /// <param name="sapellido"></param>
        /// <param name="rutaasignada"></param>
        public Conductor(string identificacion, string nombre, string papellido, string sapellido, string rutaasignada)
        {
            _identificacion = identificacion;
            _nombre = nombre;
            _papellido = papellido;
            _sapellido = sapellido;
            _ruta = rutaasignada;
        }
        /// <summary>
        /// Propiedad para el campo identificacon
        /// </summary>
        public string Identificacion { get { return _identificacion; } set { _identificacion = value; } }
        /// <summary>
        /// Propiedad para el campo nombre
        /// </summary>
        public string Nombre { get { return _nombre; } set { _nombre = value; } }

        /// <summary>
        /// Propiedad para el cambpo primer apellido
        /// </summary>
        public string PApellido { get { return _papellido; } set { _papellido = value; } }
        /// <summary>
        /// Propiedad para el campo segundo apellido
        /// </summary>
        public string SApellido { get { return _sapellido; } set { _sapellido = value; } }
        /// <summary>
        /// Propiedad del campo ruta
        /// </summary>
        public string Ruta { get { return _ruta; } set { _ruta = value; } }
    }
}
