using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace EFTest
{
    public partial class Libro
    {
        public bool? IsAutoreInPensione
        {
            get
            {
                return Autore.InPensione;
            }
        }
    }
}
