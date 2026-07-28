using Dominio;

namespace Consola
{
    internal class Program
    {
        private static Sistema _sistema = new Sistema();

        static void Main(string[] args)
        {
            string opcion = "";

            while (opcion != "5")
            {
                MenuPrincipal();
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        ListarPersonasConActivos();
                        break;
                    case "2":
                        ListarIncidentesDePersona();
                        break;
                    case "3":
                        AltaPersona();
                        break;
                    case "4":
                        ListarActivosSinBackup();
                        break;
                    case "5":
                        Console.WriteLine("Fin del programa.");
                        break;
                    default:
                        Console.WriteLine("Ingrese una opción válida.");
                        break;
                }

                if (opcion != "5")
                {
                    Console.WriteLine();
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void MenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("MENÚ PRINCIPAL");
            Console.WriteLine();
            Console.WriteLine("1- Listar personas con sus activos");
            Console.WriteLine("2- Listar incidentes de una persona");
            Console.WriteLine("3- Alta de persona");
            Console.WriteLine("4- Listar activos sin backup");
            Console.WriteLine("5- Salir");
            Console.WriteLine();
            Console.Write("Elija una opción: ");
        }

        static void ListarPersonasConActivos()
        {
            Console.Clear();
            Console.WriteLine("=== LISTADO DE PERSONAS CON SUS ACTIVOS ===");
            Console.WriteLine();

            foreach (Persona p in _sistema.Personas)
            {
                Console.WriteLine($"Persona: {p.Ci} - {p.Nombre} - {p.Mail}");

                List<Activo> activos = _sistema.ActivosPorPersona(p);

                if (activos.Count == 0)
                {
                    Console.WriteLine("   (sin activos asociados)");
                }
                else
                {
                    foreach (Activo a in activos)
                    {
                        Console.WriteLine($"   - {a.CodA} | {a.Nombre} | {a.TipoActivo}");
                    }
                }
                Console.WriteLine();
            }
        }

        static void ListarIncidentesDePersona()
        {
            Console.Clear();
            Console.WriteLine("=== INCIDENTES DE UNA PERSONA ===");
            Console.WriteLine();
            Console.Write("Ingrese la cédula de la persona: ");
            string ci = Console.ReadLine();

            Persona p = _sistema.BuscarPersona(ci);

            if (p == null)
            {
                Console.WriteLine("No existe ninguna persona con esa cédula.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Incidentes de {p.Nombre}:");
            Console.WriteLine();

            List<Incidente> incidentes = _sistema.IncidentesPorPersona(p);

            if (incidentes.Count == 0)
            {
                Console.WriteLine("Esta persona no se vio involucrada en ningún incidente.");
            }
            else
            {
                foreach (Incidente i in incidentes)
                {
                    Console.WriteLine(i);
                }
            }
        }

        static void AltaPersona()
        {
            Console.Clear();
            Console.WriteLine("=== ALTA DE PERSONA ===");
            Console.WriteLine();

            try
            {
                Console.Write("Cédula: ");
                string ci = Console.ReadLine();

                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();

                Console.Write("Email: ");
                string mail = Console.ReadLine();

                Console.Write("Teléfono: ");
                string telefono = Console.ReadLine();

                Persona nueva = new Persona(ci, nombre, mail, telefono);
                _sistema.AltaPersona(nueva);

                Console.WriteLine();
                Console.WriteLine("Persona dada de alta correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void ListarActivosSinBackup()
        {
            Console.Clear();
            Console.WriteLine("=== ACTIVOS SIN BACKUP ===");
            Console.WriteLine();

            List<Activo> activos = _sistema.ActivoSinBackUp();

            if (activos.Count == 0)
            {
                Console.WriteLine("Todos los activos tienen backup.");
            }
            else
            {
                foreach (Activo a in activos)
                {
                    Console.WriteLine($"{a.CodA} | {a.Nombre} | Tipo: {a.TipoActivo} | Criticidad: {a.Criticidad}");
                }
            }
        }
    }
}