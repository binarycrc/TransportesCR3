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
    public class Conductor
    {
        /// <summary> 3
        /// Clase base para registro para tipos de empleados
        /// </summary>
        string _identificacion;
        string _nombre;
        string _papellido;
        string _sapellido;
        string _ruta;
        string _username;
        string _acceso;
        string _placa;
        string _modelo;
        string _marca;

        /// <summary>
        /// Constructor de la clase Empleado
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="nombre"></param>
        /// <param name="papellido"></param>
        /// <param name="sapellido"></param>
        /// <param name="rutaasignada"></param>
        /// <param name="username"></param>
        /// <param name="acceso"></param>
        public Conductor(string identificacion, string nombre, string papellido, string sapellido, string rutaasignada, string username, string acceso
            , string placa, string modelo, string marca)
        {
            Identificacion = identificacion;
            Nombre = nombre;
            PApellido = papellido;
            SApellido = sapellido;
            Ruta = rutaasignada;
            UserName = username;
            Acceso = acceso;
            Placa = placa;
            Modelo = modelo;
            Marca = marca;
        }

        /// <summary>
        /// Propiedad para el campo identificacon
        /// </summary>
        public string Identificacion { get => _identificacion; set => _identificacion = value; }
        /// <summary>
        /// Propiedad para el campo nombre
        /// </summary>
        public string Nombre { get => _nombre;  set => _nombre = value;  }

        /// <summary>
        /// Propiedad para el cambpo primer apellido
        /// </summary>
        public string PApellido { get => _papellido;  set => _papellido = value; }
        /// <summary>
        /// Propiedad para el campo segundo apellido
        /// </summary>
        public string SApellido { get => _sapellido; set => _sapellido = value; } 
        /// <summary>
        /// Propiedad del campo ruta
        /// </summary>
        public string Ruta { get => _ruta;  set => _ruta = value; } 
        /// <summary>
        /// Propiedad del campo user_name
        /// </summary>
        public string UserName { get => _username; set => _username = value; } 
        /// <summary>
        /// Propiedad del campo acceso
        /// </summary>
        public string Acceso { get => _acceso; set => _acceso = value; }
        public string Placa { get => _placa; set => _placa = value; }
        public string Modelo { get => _modelo; set => _modelo = value; }
        public string Marca { get => _marca; set => _marca = value; }

    }
}
