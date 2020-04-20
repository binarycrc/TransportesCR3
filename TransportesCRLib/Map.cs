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
    public class Map
    {
        public static string Serialize(List<string> list)
        {
            if (list.Count == 0){return null;}

            bool isFirst = true;
            var output = new StringBuilder();

            foreach (var line in list)
            {
                if (isFirst)
                {
                    output.Append(line);
                    isFirst = false;
                }
                else{output.Append(string.Format(",{0}", line));}
            }
            return output.ToString();
        }

        public static List<string> Deserialize(string entry)
        {
            string str = entry;
            var list = new List<string>();

            if (string.IsNullOrEmpty(str)){return list; }

            try
            {
                foreach (string linea in entry.Split(','))
                {
                    list.Add(linea);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return list;
        }
    }
}
