using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportesCRLib
{
    public class Packages
    {
        public static Package LoginOk(string response)
        {
            return new Package(response);
        }
    }
}
