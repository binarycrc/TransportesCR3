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
        public string Conten { get; set; }

        public Package()
        {

        }

        public Package(string comando, string contenido)
        {
            Command = comando;
            Conten = contenido;
        }

        public Package(string datos) // ej: login:usuario,contrasena
        {
            int sepIndex = datos.IndexOf(":", StringComparison.Ordinal);
            Command = datos.Substring(0, sepIndex);
            Conten = datos.Substring(Command.Length + 1);
        }

        public string Serializar()
        {
            return string.Format("{0}:{1}", Command, Conten);
        }

        public static implicit operator string(Package package)
        {
            return package.Serializar();
        }
    }
}
