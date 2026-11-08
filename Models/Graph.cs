namespace CampusNet.Models;

public class Graph
{
    /// Diccionario que almacena los vertices del grafo usando su nombre como clave.
    /// Se inicializa como 'readonly' para proteger la referencia de la coleccion 
    /// y utiliza 'OrdinalIgnoreCase' para que la búsqueda ignore las diferencias entre mayusculas y minusculas.
    private readonly Dictionary<string, Vertex> _vertices = new(StringComparer.OrdinalIgnoreCase);

    
    /// Lista de adyacencia que representa las aristas del grafo
    /// Asocia el nombre de un nodo origen con la lista de nombres de sus nodos vecinos (destinos)
    /// Ignora las diferencias entre mayúsculas y minúsculas en las operaciones de búsqueda y ordenamiento.
    private readonly Dictionary<string, List<string>> _adjacency = new(StringComparer.OrdinalIgnoreCase);

    // Permite consultar los usuarios sin modificar el diccionario original.
    public IReadOnlyDictionary<string, Vertex> Vertices => _vertices;


    // Permite consultar las relaciones sin modificarlas directamente.
    public IReadOnlyDictionary<string, List<string>> Adjacency => _adjacency;

    

    //////////////////////////////////////////////////////////////////////////////
    // Metodo AddVertex, agrega un nuevo usuario al grafo.
    public bool AddVertex(Vertex vertex)
    {
        if (_vertices.ContainsKey(vertex.Id))                   // Verifica que el usuario no exista.
        {
            return false;
        }

        _vertices.Add(vertex.Id, vertex);                       // Registra el usuario.

        _adjacency.Add(vertex.Id, new List<string>());          // Crea su lista de relaciones vacia.

        return true;
    }



    ////////////////////////////////////////////////////////////////////////////////////
    // Metodo AddEdge, agrega una relacion dirigida entre dos usuarios.
    public bool AddEdge(string from, string to)
    {
        if (!_vertices.ContainsKey(from) || !_vertices.ContainsKey(to))         // Comprueba que ambos usuarios existan
        {
            return false;
        }

        List<string> neighbors = _adjacency[from];                              // Obtiene la lista de usuarios que sigue el origen

        if (neighbors.Contains(to, StringComparer.OrdinalIgnoreCase))           // Evita registrar relaciones duplicadas
        {
            return false;
        }

        neighbors.Add(to);                                                      // Registra la relacion dirigida

        return true;
    }



    /////////////////////////////////////////////////////////////////////////////////
    // Verifica si existe una relación dirigida entre dos usuarios.
    public bool EdgeExists(string from, string to)
    {
        // Comprueba que el usuario de origen exista.
        if (!_adjacency.TryGetValue(from, out List<string>? neighbors))
        {
            return false;
        }

        // Comprueba si el destino está en la lista de adyacencia.
        return neighbors.Contains(to, StringComparer.OrdinalIgnoreCase);
    }


    
    
    /// //////////////////////////////////////////////////////////////////////////////////
    // Metodo RemoveVertex, elimina un usuario y todas sus relaciones.
    public bool RemoveVertex(string id)
    {
        if (!_vertices.ContainsKey(id))                         // Verifica si el usuario existe
        {
            return false;
        }

        _vertices.Remove(id);                                   // Elimina el usuario del diccionario

        _adjacency.Remove(id);                                  // Elimina su lista de adyacencia

        foreach (var neighbors in _adjacency.Values)            // Elimina las relaciones que apuntaban a ese usuario
        {
 
            // Remueve de la lista 'neighbors' cualquier elemento que sea identico al 'id' proporcionado.
            // Se utiliza 'OrdinalIgnoreCase' para asegurar que la comparacion no dependa de mayusculas y minusculas.
            neighbors.RemoveAll(x => string.Equals(x, id, StringComparison.OrdinalIgnoreCase));
        }

        return true;
    }




    ///////////////////////////////////////////////////////////////////////////////////
    // Elimina una relacion dirigida.
    public bool RemoveEdge(string from, string to)
    {
        if (!_adjacency.ContainsKey(from))                      // Verifica que exista el usuario de origen.                              
        {
            return false;
        }

        // Elimina todas las ocurrencias del destino ignorando entre mayúsculas y minúsculas.
        // RemoveAll devuelve el conteo de eliminados; si es mayor a 0, significa que la relacion existia.
        return _adjacency[from].RemoveAll(x => string.Equals(x, to, StringComparison.OrdinalIgnoreCase)) > 0;
    }

    

    ////////////////////////////////////////////////////////////////////////////////////
    // Metodo UpdateVertex, actualiza el nombre y el rol de un usuario.
    public bool UpdateVertex(string id, string name, string role)
    {
        if (!_vertices.TryGetValue(id, out var vertex))             // Busca al usuario.
        {
            return false;
        }

        vertex.Update(name, role);                                  // Actualiza sus datos.

        return true;
    }


    
    /////////////////////////////////////////////////////////////////////////////////////
    // Metodo InDegree, calcula cuantos usuarios siguen a un usuario
    public int InDegree(string id)
    {
        int count = 0;

        foreach (var neighbors in _adjacency.Values)            // Recorre las listas de vecinos de todos los nodos en el grafo
        {
             // Cuenta cuántas veces aparece el 'id' ignorando mayúsculas y minúsculas
            count += neighbors.Count(x => string.Equals(x, id, StringComparison.OrdinalIgnoreCase));
        }

        return count;
    }


    
    ////////////////////////////////////////////////////////////////////////////////
    // Metodo OutDegree, calcula cuantos usuarios sigue un usuario
    public int OutDegree(string id)
    {
        if (!_adjacency.ContainsKey(id))        // Si el usuario no esta registrado en la lista de adyacencia, no sigue a nadie
        {
            return 0;
        }

        return _adjacency[id].Count;
    }



    ////////////////////////////////////////////////////////////////////////////////
    // Realiza un recorrido BFS (busqueda en anchura)
    public List<string> BreadthFirstSearch(string start)
    {
        List<string> order = new List<string>();                    // Lista que almacena el orden de visita

        if (!_vertices.ContainsKey(start))                          // Verifica que el usuario inicial exista
        {
            return order;
        }

        HashSet<string> visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);    // Conjunto para controlar los usuarios visitados

        // Cola de usuarios pendientes por visitar.
        Queue<string> queue = new Queue<string>();              //Creamos una cola donde almacenaremos los usuarios pendientes

        visited.Add(start);                                     // Marcamos el usuario inicial como visitado

        queue.Enqueue(start);                                   // Agregamos el usuario inicial a la cola

        while (queue.Count > 0)                                 // Repetimos mientras existan usuarios pendientes
        {
            string current = queue.Dequeue();                   // Sacamos el primer usuario de la cola

            order.Add(current);                                 // Registramos el orden de visita

            foreach (string neighbor in _adjacency[current])    // Revisamos los usuarios que sigue el usuario actual
            {
                if (visited.Add(neighbor))                      // Verificamos que no haya sido visitado
                {
                    queue.Enqueue(neighbor);                    // Lo agregamos a la cola
                }
            }
        }

        return order;                                           // Devolvemos el orden del recorrido
    }



    ////////////////////////////////////////////////////////////////////////////////////
    // Realiza un recorrido DFS completo y detecta ciclos dirigidos.
    public (List<string> Order, List<Edge> Cycles) DepthFirstSearchAll()
    {
        var order = new List<string>();                             // Guarda el orden en que se visitan los usuarios

        var cycles = new List<Edge>();                              // Guarda las relaciones que permiten detectar ciclos

        // 0 = no visitado, 1 = en proceso, 2 = terminado
        var state = _vertices.Keys.ToDictionary(id => id, id => 0,StringComparer.OrdinalIgnoreCase);


        ////////////// Metodo local que explora cada usuario //////////////
        void Visit(string current)
        {
            state[current] = 1;                                 // Marcamos el usuario como "en proceso"

            order.Add(current);                                 // Registramos su visita

            foreach (var neighbor in _adjacency[current])       // Revisamos sus usuarios vecinos
            {
                if (state[neighbor] == 0)
                {
                    Visit(neighbor);                            // Si no ha sido visitado, seguimos explorando
                }
                else if (state[neighbor] == 1)
                {
                    cycles.Add(new Edge(current, neighbor));    // Si esta en proceso, encontramos una posible evidencia de ciclo
                }
            }

            state[current] = 2;                                 // Marcamos el usuario como terminado
        }

        foreach (var id in _vertices.Keys)                      // Recorremos todos los usuarios, incluso si estan desconectados
        {
            if (state[id] == 0)
            {
                Visit(id);
            }
        }

        return (order, cycles);                                 // Devolvemos el orden y las relaciones que detectan ciclos
    }


    //////////////////////////////////////////////////////////////////////////
    // Obtiene los usuarios que no tienen seguidores.
    public List<Vertex> GetUsersWithoutFollowers()
    {
        List<Vertex> result = new List<Vertex>();                   // Lista donde guardaremos los usuarios encontrados

        foreach (var vertex in _vertices.Values)                    // Recorremos todos los usuarios del grafo
        {
            if (InDegree(vertex.Id) == 0)                           // Verificamos si su grado de entrada es cero
            {
                result.Add(vertex);
            }
        }

        return result;                                              // Devolvemos la lista de usuarios sin seguidores
    }



    //////////////////////////////////////////////////////////////////////////
    // Obtiene los usuarios con mayor número de seguidores
    public List<Vertex> GetMostInfluentialUsers()
    {
        List<Vertex> result = new List<Vertex>();                       // Creamos una lista para guardar los resultados

        if (_vertices.Count == 0)                                       // Si no hay usuarios, devolvemos la lista vacia
            return result;

        int maxInDegree = _vertices.Keys.Max(id => InDegree(id));       // Buscamos el mayor grado de entrada

        foreach (var vertex in _vertices.Values)                        // Recorremos todos los usuarios
        {
            if (InDegree(vertex.Id) == maxInDegree)                     // Comparamos su grado de entrada con el máximo
            {
                result.Add(vertex);
            }
        }

        return result;                                                  // Devolvemos los usuarios más influyentes
    }



    //////////////////////////////////////////////////////////////////
    // Obtiene los usuarios que siguen a más personas
    public List<Vertex> GetMostActiveUsers()
    {
        List<Vertex> result = new List<Vertex>();                       // Creamos una lista para guardar los resultados

        if (_vertices.Count == 0)                                       // Si no hay usuarios, devolvemos la lista vacia
            return result;

        int maxOutDegree = _vertices.Keys.Max(id => OutDegree(id));     // Buscamos el mayor grado de salida

        foreach (var vertex in _vertices.Values)                        // Recorremos todos los usuarios
        {
            if (OutDegree(vertex.Id) == maxOutDegree)                   // Comparamos su grado de salida con el máximo
            {
                result.Add(vertex);
            }
        }

        return result;                                                  // Devolvemos los usuarios mas activos
    }



    //////////////////////////////////////////////////////////////////////////
    // Verifica si un usuario puede llegar a otro.
    public bool CanReach(string from, string to)
    {
        if (!_vertices.ContainsKey(from) || !_vertices.ContainsKey(to))             // Verificamos que ambos usuarios existan
            return false;

        if (string.Equals(from, to, StringComparison.OrdinalIgnoreCase))            // Si es el mismo usuario, puede llegar a sí mismo
            return true;

        List<string> visited = BreadthFirstSearch(from);                            // Realizamos el recorrido BFS desde el usuario inicial

        return visited.Contains(to, StringComparer.OrdinalIgnoreCase);              // Verificamos si el usuario destino fue alcanzado
    }


}
