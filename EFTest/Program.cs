using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFTest;
using System.Configuration;

namespace EFTestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            testDbAndEntities("SqlServerConnection");
            testDbAndEntities("OracleConnection");

            Console.WriteLine("\nTest finished, press any key to close the application..");
            Console.ReadKey();
        }

        static void testDbAndEntities(string conn)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[conn].ConnectionString;
            using (var context = new EFTestEntities(connectionString))
            {
                //string script = context.CreateDatabaseScript();
                //Console.WriteLine(script);

                try
                {
                    context.DeleteDatabase();
                    Console.WriteLine(conn + " > Database Deleted Successfully");
                }
                catch (Exception e)
                {
                    Console.WriteLine(conn + " > Database Not Deleted: " + e.Message);
                }

                try {
                    context.CreateDatabase();
                    Console.WriteLine(conn + " > Database Created Successfully");
                }
                catch (Exception e) {
                    Console.WriteLine(conn + " > Database Not Created: " + e.Message);
                }

                try
                {
                    if (context.Autori.FirstOrDefault() != null)
                        context.Autori.DeleteObject(context.Autori.FirstOrDefault());
                    if (context.Libri.FirstOrDefault() != null)
                        context.Libri.DeleteObject(context.Libri.FirstOrDefault());

                    var autore = new Autore { Nome = "Bruno", Età = 26, InPensione = false };
                    context.Autori.AddObject(autore);

                    var libro = new Libro { Titolo = "Prova", DataDiPubblicazione = DateTime.Now, Prezzo = 12.87m, Autore = autore };
                    context.Libri.AddObject(libro);
                    context.SaveChanges();

                    List<Libro> libri = autore.getLibri();
                    foreach (Libro l in libri)
                    {
                        if (l.IsAutoreInPensione.Value) ;
                    }

                    Console.WriteLine(conn + " > Entities Read/Write Success");
                }
                catch (Exception e) {
                    Console.WriteLine(conn + " > Entities Read/Write Exception: " + e.Message);
                }


            }
           
        }

    }
}
