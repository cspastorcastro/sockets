using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sockets.Comunicacion;
using System.Configuration;
using System.Net.Sockets;

namespace Sockets
{
    public class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);

            Console.WriteLine("Iniciando servidor en el puerto {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);
            if (servidor.Iniciar())
            {
                // Si el servidor inicia bien
                Console.WriteLine("Servidor iniciado");
                while (true)
                {
                    Console.WriteLine("Esperando cliente...");
                    Socket socketCliente = servidor.ObtenerCliente();
                    // Construir mecanismo para escribir y leer
                    ClienteCom cliente = new ClienteCom(socketCliente);
                    cliente.Escribir("Hola cliente, dime tu nombre.");
                    string respuesta = cliente.Leer();
                    Console.WriteLine("El cliente se llama {0}.", respuesta);
                    cliente.Escribir("Gracias " + respuesta + ", nos vemos.");
                    cliente.Desconectar();
                }

            } else
            {
                Console.WriteLine("Error, el puerto {0} está en uso.", puerto);
            }
            Console.ReadKey();
        }
    }
}
