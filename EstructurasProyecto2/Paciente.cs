using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EstructurasProyecto2
{
    public class Paciente
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Prioridad { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime? HoraSalida { get; set; }
        public Paciente(string cedula, string nombre, int edad, string prioridad, DateTime horaEntrada, DateTime? horaSalida)
        {
            Cedula = cedula;
            Nombre = nombre;
            Edad = edad;
            Prioridad = prioridad;
            HoraEntrada = horaEntrada;
            HoraSalida = horaSalida;
        }

        public static void IngresoPaciente(Menu datos)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Ingrese los datos del paciente:");

                string cedula = LeerCedula();
                string nombre = LeerString("Nombre Completo: ");
                int edad = LeerEdad();
                string prioridad = LeerPrioridad();
                DateTime horaEntrada = DateTime.Now;

                Paciente nuevoPaciente = new Paciente(cedula, nombre, edad, prioridad, horaEntrada, null);
                datos.colaDeEspera.Enqueue(nuevoPaciente);
                Console.WriteLine($"Paciente agregado a la cola:");

                Console.WriteLine("¿Desea agregar otro paciente? (s/n): ");
                string respuesta = Console.ReadLine().ToLower();
                if (respuesta != "s")
                {
                    break;
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPacientes en la cola de espera:");
            Console.ResetColor();
            foreach (Paciente p in datos.colaDeEspera)
            {
                Console.WriteLine($"{p.Cedula,-14}{p.Nombre,-20}");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nClasificando pacientes...");
            Console.ResetColor();
            while (datos.colaDeEspera.Count > 0)
            {
                Paciente paciente = datos.colaDeEspera.Dequeue();

                if (paciente.Prioridad == "alta")
                {
                    datos.pilaAltaPrioridad.Push(paciente);
                }
                else if (paciente.Prioridad == "media")
                {
                    datos.pilaMediaPrioridad.Push(paciente);
                }
                else if (paciente.Prioridad == "baja")
                {
                    datos.pilaBajaPrioridad.Push(paciente);
                }
            }
            Thread.Sleep(3000);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPacientes en la pila de alta prioridad:");
            Console.ResetColor();
            if (datos.pilaAltaPrioridad.Count > 0)
            {
                MostrarPacientes(datos.pilaAltaPrioridad);
            }
            else
            {
                Console.WriteLine("No hay pacientes con esta clasificación.");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPacientes en la pila de media prioridad:");
            Console.ResetColor();
            if (datos.pilaMediaPrioridad.Count > 0)
            {
                MostrarPacientes(datos.pilaMediaPrioridad);
            }
            else 
            {
                Console.WriteLine("No hay pacientes con esta clasificación.");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPacientes en la pila de baja prioridad:");
            Console.ResetColor();
            if (datos.pilaBajaPrioridad.Count > 0)
            {
                MostrarPacientes(datos.pilaBajaPrioridad);
            }
            else
            {
                Console.WriteLine("No hay pacientes con esta clasificación.");
            }
            Console.ReadKey();

        }

        public static void MostrarPacientes(Stack<Paciente> pila)
        {
            foreach (Paciente p in pila)
            {
                Console.WriteLine($"{p.Cedula,-14}{p.Nombre,-20}{p.Edad}");
            }
        }

        public static void AtenderPacientes(Stack<Paciente> pila, List<Paciente> pacientesAtendidos)
        {
            Paciente pacienteAtender = pila.Pop();
            Console.WriteLine($"Paciente por llamar: {pacienteAtender.Nombre} con prioridad {pacienteAtender.Prioridad}.");
            pacienteAtender.HoraSalida = DateTime.Now;
            pacientesAtendidos.Add(pacienteAtender);
            Console.ReadKey();
        }

        #region Métodos de ingreso y validación de datos

        private static string LeerString(string mensaje)
        {
            string dato;
            while (true)
            {
                Console.Write($"{mensaje}");
                dato = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(dato) && EsSoloLetras(dato))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Solo se acepta el ingreso de letras. Inténtelo nuevamente.");
                    Console.ResetColor();
                }
            }
            return dato;
        }

        private static bool EsSoloLetras(string cadena)
        {
            foreach (char c in cadena)
            {
                if (!char.IsLetter(c) && c != 'ñ' && c != 'Ñ' && c != ' ')
                {
                    return false;
                }
            }
            return true;
        }

        private static int LeerEdad()
        {
            int numero;
            Console.Write("Edad: ");
            while (!int.TryParse(Console.ReadLine(), out numero))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Edad inválida. Por favor, ingrese un número entero.");
                Console.ResetColor();
            }
            return numero;
        }        
        
        private static string LeerPrioridad()
        {
            string prioridad;
            while (true)
            {
                Console.Write("Prioridad (alta, media, baja): ");
                prioridad = Console.ReadLine();

                if (prioridad != "alta" && prioridad != "media" && prioridad != "baja")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Prioridad no válida. Inténtelo de nuevo.");
                    Console.ResetColor();
                }
                else
                {
                    break;
                }
            }
            return prioridad;
        }

        private static string LeerCedula()
        {
            string cedula;
            while (true) 
            {
                Console.Write("Cédula: ");
                cedula = Console.ReadLine();

                if (string.IsNullOrEmpty(cedula))
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("Numero de cédula inválido. Inténtelo de nuevo.");
                    Console.ResetColor();
                }
                else
                {
                    break;
                }
            }
            return cedula;
        }

        #endregion

    }
}
