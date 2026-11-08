namespace CampusNet.Models;

// Representa una relacion dirigida entre dos usuarios.
public class Edge
{
    public string From { get; }                       // Propiedad From, almacena el identificador del usuario que sigue a otro

    public string To { get; }                         // Propiedad To, almacena el identificador del usuario al que siguen


    public Edge(string from, string to)               // Constructor de la arista. Permite crear una relacion indicando su origen y destino
    {
        From = from;
        To = to;
    }


    public override string ToString()                 // Metodo ToString, permite mostrar la relacion de una manera facil de interpretar
    {
        return $"{From} -> {To}";
    }
    
}
