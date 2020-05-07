using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportesCRLib
{
    public class MensajeSocket<T>
    {
        public string Mensaje { get; set; }
        public T Valor { get; set; }

    }
}
