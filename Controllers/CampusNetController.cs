using CampusNet.Models;
using CampusNet.Views;

namespace CampusNet.Controllers;

public class CampusNetController                        // Controlador principal de CampusNet
{
    private readonly Graph _graph;                      // Modelo encargado de administrar el grafo

    private readonly ConsoleView _view;                 // Vista encargada de mostrar la información

    public CampusNetController()                        // Constructor del controlador
    {
        _graph = new Graph();
        _view = new ConsoleView();
    }


    ////////////////////////////////////////////////////////////////////////////////////////
    // Método principal que coordina la ejecución de CampusNet
    public void Run()
    {
        _view.ShowMessage("\n===================================================================");
        _view.ShowMessage("                       RED SOCIAL CAMPUSNET                        ");
        _view.ShowMessage("===================================================================\n");
       
        LoadUsers();                                                // Cargamos los usuarios iniciales

        LoadRelationships();                                        // Cargamos las relaciones entre usuarios

        ShowGraph();                                                // Mostramos la estructura del grafo

        RunBFS();                                                   // Ejecutamos los tres recorridos BFS.  

        RunDFS();                                                   // Ejecutamos el recorrido DFS

        RunQueries();                                               // Ejecutamos las consultas de usuarios
 
        RunReachability();                                          // Ejecutamos la consulta de alcanzabilidad

        DemonstrateCrud();                                          // Ejecutamos la demostracion del CRUD

        DemonstrateValidation();                                    // Demostracion de validaciones para cuando la aplicacion se escale a ingreso de usuarios por consola

        _view.ShowMessage("\nEjecución de CampusNet finalizada.\n");  // Visualiza mensaje  
    }


    /////////////////////////////////////////////////////////////////////////////////////////
    // Carga los usuarios iniciales de CampusNet
    private void LoadUsers()
    {
        _graph.AddVertex(new Vertex("U01", "Ana", "Estudiante"));
        _graph.AddVertex(new Vertex("U02", "Luis", "Estudiante"));
        _graph.AddVertex(new Vertex("U03", "Carlos", "Profesor"));
        _graph.AddVertex(new Vertex("U04", "Maria", "Estudiante"));
        _graph.AddVertex(new Vertex("U05", "Pedro", "Egresado"));
        _graph.AddVertex(new Vertex("U06", "Laura", "Estudiante"));
        _graph.AddVertex(new Vertex("U07", "Andres", "Profesor"));
        _graph.AddVertex(new Vertex("U08", "Sofia", "Estudiante"));
        _graph.AddVertex(new Vertex("U09", "Diego", "Egresado"));
        _graph.AddVertex(new Vertex("U10", "Valentina", "Estudiante"));
        _graph.AddVertex(new Vertex("U11", "Camilo", "Profesor"));
        _graph.AddVertex(new Vertex("U12", "Daniela", "Egresado"));
    }



    ///////////////////////////////////////////////////////////////////////////////////
    // Carga las relaciones dirigidas entre los usuarios
    private void LoadRelationships()
    {
        _graph.AddEdge("U01", "U02");
        _graph.AddEdge("U01", "U03");
        _graph.AddEdge("U01", "U04");
        _graph.AddEdge("U01", "U06");

        _graph.AddEdge("U02", "U09");
        _graph.AddEdge("U02", "U03");
        _graph.AddEdge("U02", "U06");
        _graph.AddEdge("U02", "U07");

        _graph.AddEdge("U03", "U04");
        _graph.AddEdge("U03", "U08");

        _graph.AddEdge("U04", "U01");
        _graph.AddEdge("U04", "U09");

        _graph.AddEdge("U05", "U06");
        _graph.AddEdge("U05", "U07");

        _graph.AddEdge("U06", "U08");
        _graph.AddEdge("U06", "U10");

        _graph.AddEdge("U07", "U08");
        _graph.AddEdge("U07", "U11");

        _graph.AddEdge("U08", "U09");

        _graph.AddEdge("U09", "U10");

        _graph.AddEdge("U10", "U11");
    }



    ////////////////////////////////////////////////////////////////////////////////
    // Muestra la estructura completa del grafo
    private void ShowGraph()
    {
        _view.ShowTotalUsers(_graph.Vertices.Count);                            // Solicitamos a la vista mostrar la cantidad de usuarios "TOTAL DE USUARIOS: "

        _view.ShowAdjacencyList(_graph.Vertices,_graph.Adjacency);              // Mostramos la lista de adyacencia
    }



    //////////////////////////////////////////////////////////////////////////////
    // Ejecuta tres recorridos BFS desde usuarios diferentes
    private void RunBFS()
    {
        // Primer recorrido BFS
        List<string> result1 = _graph.BreadthFirstSearch("U01");
        _view.ShowBFS("U01", result1);

        // Segundo recorrido BFS
        List<string> result2 = _graph.BreadthFirstSearch("U05");
        _view.ShowBFS("U05", result2);

        // Tercer recorrido BFS
        List<string> result3 = _graph.BreadthFirstSearch("U08");
        _view.ShowBFS("U08", result3);
    }



    /// ///////////////////////////////////////////////////////////////////////////////
    // Ejecuta DFS completo y muestra los ciclos detectados.
    private void RunDFS()
    {
        var result = _graph.DepthFirstSearchAll();              // Solicitamos al modelo ejecutar DFS

        _view.ShowDFS(result.Order, result.Cycles);             // Enviamos los resultados a la vista
    }


    /////////////////////////////////////////////////////////////////////////////////////
    // Ejecuta las consultas principales de CampusNet.
    private void RunQueries()
    {
        var withoutFollowers = _graph.GetUsersWithoutFollowers();                   // Consulta 1: usuarios sin seguidores

        _view.ShowUsers("USUARIOS SIN SEGUIDORES",withoutFollowers);

        var influential = _graph.GetMostInfluentialUsers();                         // Consulta 2: usuarios más influyentes

        _view.ShowUsers("USUARIOS MAS INFLUYENTES",influential);

        var active = _graph.GetMostActiveUsers();                                   // Consulta 3: usuarios más activos

        _view.ShowUsers("USUARIOS MAS ACTIVOS",active);
    }



    ////////////////////////////////////////////////////////////////////////////////////
    // Ejecuta una consulta de alcanzabilidad entre dos usuarios.
    private void RunReachability()
    {
        
        string from = "U01";                                    // Definimos el usuario de origen

        
        string to = "U10";                                      // Definimos el usuario de destino

        
        bool reachable = _graph.CanReach(from, to);             // Consultamos si existe un camino dirigido

        
        _view.ShowReachability(from, to, reachable);            // Mostramos el resultado en consola
    }


    private void DemonstrateCrud()
    {
        _view.ShowMessage("\n======================= DEMOSTRACIÓN CRUD =========================");

        // CREATE: crear un usuario temporal.
        _graph.AddVertex(new Vertex("U13", "Mateo", "Estudiante"));
        _view.ShowMessage("CREATE: usuario U13 creado.");              

        // READ: consultar el usuario creado.
        if (_graph.Vertices.TryGetValue("U13", out Vertex? user))
        {
            _view.ShowMessage($"READ: usuario encontrado: {user}");
        }

        // UPDATE: actualizar su nombre y rol.
        _graph.UpdateVertex("U13", "Mateo Gómez", "Profesor");
        _view.ShowMessage("UPDATE: usuario U13 actualizado."); 

        // CREATE: crear una relación dirigida.
        _graph.AddEdge("U13", "U01");
        _view.ShowMessage("CREATE: relación U13 -> U01 creada.");

        // READ: consultar si existe la relación U13 -> U01.
        bool relationExists = _graph.EdgeExists("U13", "U01");

        _view.ShowMessage($"READ: ¿Existe la relación U13 -> U01? {(relationExists ? "Sí" : "No")}");

        // DELETE: eliminar la relación.
        _graph.RemoveEdge("U13", "U01");
        _view.ShowMessage("DELETE: relación U13 -> U01 eliminada.");

        // DELETE: eliminar el usuario temporal.
        _graph.RemoveVertex("U13");
        _view.ShowMessage("DELETE: usuario U13 eliminado.");

        _view.ShowMessage("===================================================================\n");
    }


    
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    // Demuestra la validación de los datos de un usuario.
    
    // Demuestra las diferentes validaciones de los datos de un usuario.
    private void DemonstrateValidation()
    {
        _view.ShowMessage("\n================== DEMOSTRACIÓN DE VALIDACIONES ===================\n");

        // Lista de pruebas con diferentes datos de usuarios.
        var testCases = new List<(string Description, string Id, string Name, string Role)>
        {
            // Pruebas de identificador.
            ("ID válido", "U13", "Mateo Gomez", "Estudiante"),
            ("ID vacío", "", "Mateo Gomez", "Estudiante"),
            ("ID demasiado corto", "U1", "Mateo Gomez", "Estudiante"),
            ("ID demasiado largo", "USUARIO12345", "Mateo Gomez", "Estudiante"),
            ("ID con caracteres especiales", "U@13", "Mateo Gomez", "Estudiante"),

            // Pruebas de nombre.
            ("Nombre vacío", "U14", "", "Estudiante"),
            ("Nombre demasiado corto", "U14", "A", "Estudiante"),
            ("Nombre demasiado largo", "U14", new string('A', 41), "Estudiante"),
            ("Nombre con números", "U14", "Mateo123", "Estudiante"),
            ("Nombre con caracteres especiales", "U14", "Mateo@", "Estudiante"),

            // Pruebas de rol.
            ("Rol vacío", "U15", "Mateo Gomez", ""),
            ("Rol demasiado corto", "U15", "Mateo Gomez", "AB"),
            ("Rol demasiado largo", "U15", "Mateo Gomez", "EstudianteUniversitario"),
            ("Rol con números", "U15", "Mateo Gomez", "Profesor123"),
            ("Rol con caracteres especiales", "U15", "Mateo Gomez", "Profesor@"),

            // Pruebas de datos correctos.
            ("Nombre con tilde", "U16", "María Gómez", "Estudiante"),
            ("Datos completamente válidos", "U17", "Carlos Perez", "Profesor")
        };

        // Recorremos todas las pruebas.
        foreach (var test in testCases)
        {
            try
            {
                Vertex user = new Vertex(test.Id, test.Name, test.Role);                // Intentamos crear un usuario con los datos de la prueba.

                _view.ShowMessage($"[CORRECTO] {test.Description}: {user}");            // Si se crea correctamente, mostramos el resultado.
            }
            catch (ArgumentException ex)
            {
                _view.ShowError($"[RECHAZADO] {test.Description}: {ex.Message}");       // Si ocurre un error, enviamos el mensaje a la vista.
            }
        }

        _view.ShowMessage("\n======================= FIN DE VALIDACIONES =======================");
    }

}


