using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportesCRLib
{
    public class Map
    {
        public static string Serializar(List<string> lista)
        {
            if (lista.Count == 0)
            {
                return null;
            }

            bool isFirst = true;
            var output = new StringBuilder();

            foreach (var linea in lista)
            {
                if (isFirst)
                {
                    output.Append(linea);
                    isFirst = false;
                }
                else
                {
                    output.Append(string.Format(",{0}", linea));
                }
            }
            return output.ToString();
        }

        public static List<string> Deserializar(string entrada)
        {
            string str = entrada;
            var lista = new List<string>();

            if (string.IsNullOrEmpty(str))
            {
                return lista;
            }

            try
            {
                foreach (string linea in entrada.Split(','))
                {
                    lista.Add(linea);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return lista;
        }
    }
}
