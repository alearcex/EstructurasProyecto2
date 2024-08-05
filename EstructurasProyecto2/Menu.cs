using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EstructurasProyecto2
{
    public class Menu
    {
        public Queue<Paciente> colaDeEspera = new Queue<Paciente>();

        public Stack<Paciente> pilaAltaPrioridad = new Stack<Paciente>();
        public Stack<Paciente> pilaMediaPrioridad = new Stack<Paciente>();
        public Stack<Paciente> pilaBajaPrioridad = new Stack<Paciente>();
        public List<Paciente> PacientesAtendidos = new List<Paciente>();

        public Menu()
        {
            colaDeEspera = new Queue<Paciente>();
            pilaAltaPrioridad = new Stack<Paciente>();
            pilaMediaPrioridad = new Stack<Paciente>();
            pilaBajaPrioridad = new Stack<Paciente>();
            PacientesAtendidos = new List<Paciente>();
        }

        public static void MostrarMenu()
        {
            Menu menu = new Menu();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("********************************************************");
                Console.WriteLine("**           SISTEMA DE INGRESO DE PACIENTES          **");
                Console.WriteLine("********************************************************");
                Console.WriteLine("1- Ingresar paciente");
                Console.WriteLine("2- Atender paciente");
                Console.WriteLine("3- Mostrar estadísticas");
                Console.WriteLine("4- Salir");
                Console.WriteLine("Seleccione una opción...");

                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Paciente.IngresoPaciente(menu);
                        break;
                    case "2":
                        if(menu.pilaAltaPrioridad.Count > 0)
                        {
                            Paciente.AtenderPacientes(menu.pilaAltaPrioridad, menu.PacientesAtendidos);
                        }
                        else if(menu.pilaMediaPrioridad.Count > 0)
                        {
                            Paciente.AtenderPacientes(menu.pilaMediaPrioridad, menu.PacientesAtendidos);
                        }
                        else if(menu.pilaBajaPrioridad.Count > 0)
                        {
                            Paciente.AtenderPacientes(menu.pilaBajaPrioridad, menu.PacientesAtendidos);
                        }
                        else
                        {
                            Console.WriteLine("No hay pacientes en espera.");
                        }
                        break;
                    case "3":
                        MostrarEstadísticas(menu);
                        break;
                    case "4":
                        Console.WriteLine("Saliendo...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción no válida, por favor inténtelo de nuevo.");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        private static void MostrarEstadísticas(Menu datos)
        {
            int enEspera = datos.pilaBajaPrioridad.Count + datos.pilaAltaPrioridad.Count + datos.pilaMediaPrioridad.Count;

            Console.Clear();
            Console.WriteLine("*************************************************************************************");
            Console.WriteLine("**                       ESTADÍSTICAS DE PACIENTES INGRESADOS                      **");
            Console.WriteLine("*************************************************************************************");
            Console.WriteLine("   Pacientes atendidos:");
            Console.WriteLine("=====================================================================================");
            Console.WriteLine($"{"Cédula",-14}{"Nombre",-25}{"Prioridad",-6}{"Ingreso",-19}{"Atención", -18}");
            foreach (var p in datos.PacientesAtendidos) 
            {
                Console.WriteLine($"{p.Cedula,-14}" +
                                  $"{p.Nombre,-25}" +
                                  $"{p.Prioridad,-6}" +
                                  $"{p.HoraEntrada,-19:dd/MM/yy HH:mm:ss}" +
                                  $"{p.HoraSalida,-19:dd/MM/yy HH:mm:ss}");
            }
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("Pacientes atendidos: " + datos.PacientesAtendidos.Count);
            Console.WriteLine("Pacientes en espera: " + enEspera);
            Console.WriteLine("Pacientes alta prioridad: " + datos.pilaAltaPrioridad.Count);
            Console.WriteLine("Pacientes media prioridad: " + datos.pilaMediaPrioridad.Count);
            Console.WriteLine("Pacientes baja prioridad: " + datos.pilaBajaPrioridad.Count);
            Console.WriteLine("=====================================================================================");
            Console.WriteLine("                    <<< Pulse cualquier tecla para conntinuar >>>                    ");
            Console.ReadKey();

        }
    }
}