using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace EFTest
{
    public partial class Autore
    {
        public List<Libro> getLibri()
        {
             string connectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;
             using (var context = new EFTestEntities(connectionString))
             {
                 var libri = from l in context.Libri.Include("Autore")
                             where l.Autore.ID == this.ID
                             select l;
                 return libri.ToList();
             }
        }
    }
}
