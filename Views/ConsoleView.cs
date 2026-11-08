using CampusNet.Models;
namespace CampusNet.Views;


// Clase encargada de mostrar información en consola.
public class ConsoleView
{
    ///////////////////////////////////////////////////////////////////////
    // Muestra la lista de adyacencia del grafo.
    public void ShowAdjacencyList(IReadOnlyDictionary<string, Vertex> vertices, IReadOnlyDictionary<string, List<string>> adjacency)
    {
        Console.WriteLine("\n======================= LISTA DE ADYACENCIA =======================\n");
        
        Console.WriteLine("{0,-20} {1,-12} {2}","USUARIO", "ID", "USUARIOS QUE SIGUE");                 // Encabezados de las columnas.
        Console.WriteLine("-------------------------------------------------------------------");


        foreach (var vertex in vertices.Values)                                                         // Recorremos todos los usuarios.
        {
            if (!adjacency.TryGetValue(vertex.Id, out List<string>? neighbors))                         // Obtenemos los usuarios que sigue el vértice actual.
            {
                Console.WriteLine("{0,-20} {1,-12} {2}", vertex.Name, vertex.Id, "No sigue a nadie");
                continue;
            }

            if (neighbors.Count == 0)                                                                   // Si no tiene conexiones, mostramos el mensaje.
            {
                Console.WriteLine("{0,-20} {1,-12} {2}", vertex.Name, vertex.Id, "No sigue a nadie");
            }
            else
            {
                string followedUsers = string.Join(", ", neighbors.Select(id => vertices[id].Name));    // Convertimos los identificadores en nombres.

                Console.WriteLine("{0,-20} {1,-12} {2}", vertex.Name, vertex.Id, followedUsers);        // Imprimimos la información organizada en columnas.
            }
        }

        Console.WriteLine("\n===================================================================\n");
    }


    ////////////////////////////////////////////////////////////////////////////////
    // Muestra el resultado del recorrido BFS.
    public void ShowBFS(string start, List<string> order)
    {
        Console.WriteLine($"\n====================== RECORRIDO BFS DESDE {start} ====================\n");

        Console.WriteLine("Orden de visita:");                                      // Mostramos el orden en que se visitaron los usuarios

        foreach (string id in order)
        {
            Console.Write($"{id} ");
        }

        Console.WriteLine($"\nCantidad de usuarios alcanzados: {order.Count}");     // Mostramos la cantidad de usuarios alcanzados

        Console.WriteLine("===================================================================\n");
    }



    /////////////////////////////////////////////////////////////////////////////////
    // Muestra el resultado del recorrido DFS y los ciclos detectados.
    public void ShowDFS(List<string> order, List<Edge> cycles)
    {
        Console.WriteLine("\n========================== RECORRIDO DFS ==========================\n");

        Console.WriteLine("Orden de descubrimiento:");                          // Mostramos el orden de descubrimiento.

        foreach (string id in order)
        {
            Console.Write($"{id} ");
        }

        Console.WriteLine("\n\nCiclos detectados:");                           // Mostramos los ciclos detectados.

        if (cycles.Count == 0)
        {
            Console.WriteLine("No se detectaron ciclos dirigidos.");
        }
        else
        {
            foreach (Edge edge in cycles)
            {
                Console.WriteLine($"{edge.From} -> {edge.To}");
            }
        }

        Console.WriteLine("===================================================================\n");
    }



    ///////////////////////////////////////////////////////////////////////////
    // Muestra una lista de usuarios con sus datos.
    public void ShowUsers(string title, List<Vertex> users)
    {
        // Lineas de codigo para lograr que todos los encabezados de este metodo queden todos del mismo tamaño
        int totalWidth = 68;
        string text = $" {title} ";
        int remaining = totalWidth - text.Length;

        int left = remaining / 2;
        int right = remaining - left;

        Console.WriteLine("\n" + new string('=', left) + text + new string('=', right) +"\n");  // Muestra mensaje en consola

        if (users.Count == 0)                                           // Verificamos si la lista esta vacia
        {
            Console.WriteLine("No se encontraron usuarios.");
            return;
        }

        foreach (Vertex user in users)                                  // Recorremos y mostramos los usuarios
        {
            Console.WriteLine(user);
        }

        Console.WriteLine("===================================================================\n");
    }



    /////////////////////////////////////////////////////////////////////////////
    // Muestra el resultado de una consulta de alcanzabilidad.
    public void ShowReachability(string from, string to, bool reachable)
    {
        Console.WriteLine("\n=================== CONSULTA DE ALCANZABILIDAD ====================");

        Console.WriteLine($"Usuario origen: {from}");
        Console.WriteLine($"Usuario destino: {to}");

        if (reachable)
        {
            Console.WriteLine("Existe un camino dirigido entre los usuarios.");
        }
        else
        {
            Console.WriteLine("No existe un camino dirigido entre los usuarios.");
        }

        Console.WriteLine("===================================================================\n");
    }


    
    /// ///////////////////////////////////////////////////////////////////////////
    // Muestra mensajes en general.
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }


    // Muestra la cantidad total de usuarios registrados.
    public void ShowTotalUsers(int total)
    {
        Console.WriteLine($"\nTOTAL DE USUARIOS: {total}");
    }

    
    ///////////////////////////////////////////////////////////////////////////////
    // Muestra los mensajes de error generados por las validaciones.
    public void ShowError(string message)
    {
        Console.WriteLine($"\nERROR: {message}");
    }
}