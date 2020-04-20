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
using System.Threading.Tasks;

namespace TransportesCRLib
{
    public class Package
    {
        public string Command { get; set; }
        public string Content { get; set; }
        public Package(){}

        public Package(string comand, string content)
        {
            Command = comand;
            Content = content;
        }

        public Package(string data) // ej: login:usuario,contrasena
        {
            int sepIndex = data.IndexOf(":", StringComparison.Ordinal);
            Command = data.Substring(0, sepIndex);
            Content = data.Substring(Command.Length + 1);
        }

        public string Serialize()
        {
            return string.Format("{0}:{1}", Command, Content);
        }

        public static implicit operator string(Package package)
        {
            return package.Serialize();
        }
    }
}
