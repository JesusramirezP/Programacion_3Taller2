
using System.Text.RegularExpressions;                       // Permite utilizar expresiones regulares para validar los datos.

namespace CampusNet.Models;


public class Vertex                                         // Clase que representa un usuario (vértice) dentro del grafo.
{
    public string Id { get; private set; }                  // Identificador único del usuario. Solo puede modificarse desde esta clase.

    public string Name { get; private set; }                // Nombre del usuario.

    public string Role { get; private set; }                // Rol del usuario dentro de la comunidad académica.


    public Vertex(string id, string name, string role)      // Constructor: recibe los datos y los valida antes de asignarlos.
    {
        Id = ValidateId(id);                                // Valida el identificador.
        Name = ValidateName(name);                          // Valida el nombre.
        Role = ValidateRole(role);                          // Valida el rol.
    }


   
    ////////////////////////////////////////////////////////////////////////////
    // VALIDACIÓN DEL IDENTIFICADOR

    // Comprueba que el ID cumpla las condiciones establecidas.
    private static string ValidateId(string id)
    {
        // Verifica que el identificador no sea nulo, vacío o espacios.
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("El ID no puede estar vacío.");

        
        id = id.Trim();     // Elimina los espacios al principio y al final.

        // Comprueba que el ID tenga entre 3 y 10 caracteres.
        if (id.Length < 3 || id.Length > 10)
            throw new ArgumentException("El ID debe tener entre 3 y 10 caracteres.");

        // Permite únicamente letras de la A a la Z y números.
        if (!Regex.IsMatch(id, @"^[a-zA-Z0-9]+$"))
            throw new ArgumentException("El ID solo puede contener letras y números.");

        return id;          // Si todas las condiciones se cumplen, devuelve el ID validado.
    }


    
    ////////////////////////////////////////////////////////////////////////////////
    // VALIDACIÓN DEL NOMBRE
    // ============================================================

    // Comprueba que el nombre del usuario sea válido.
    private static string ValidateName(string name)
    {
        // Verifica que el nombre no sea nulo, vacío o espacios.
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre no puede estar vacío.");

        name = name.Trim();         // Elimina los espacios al principio y al final.

        // Comprueba que el nombre tenga entre 2 y 40 caracteres.
        if (name.Length < 2 || name.Length > 40)
            throw new ArgumentException("El nombre debe tener entre 2 y 40 caracteres.");

        // Permite letras, incluidas tildes y ñ, y espacios entre palabras.
        // No permite números ni caracteres especiales.
        if (!Regex.IsMatch(name, @"^[\p{L}]+(?: [\p{L}]+)*$"))
            throw new ArgumentException("El nombre solo puede contener letras y espacios.");

        
        return name;        // Devuelve el nombre después de superar las validaciones.
    }



    ////////////////////////////////////////////////////////////////////////
    // VALIDACIÓN DEL ROL

    // Comprueba que el rol del usuario sea válido.
    private static string ValidateRole(string role)
    {
        // Verifica que el rol no sea nulo, vacío o espacios.
        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("El rol no puede estar vacío.");

        
        role = role.Trim();             // Elimina los espacios al principio y al final.

        // Comprueba que el rol tenga entre 3 y 20 caracteres.
        if (role.Length < 3 || role.Length > 20)
            throw new ArgumentException("El rol debe tener entre 3 y 20 caracteres.");

        // Permite únicamente letras y espacios entre palabras.
        if (!Regex.IsMatch(role, @"^[\p{L}]+(?: [\p{L}]+)*$"))
            throw new ArgumentException("El rol solo puede contener letras y espacios.");

        return role;            // Devuelve el rol después de superar las validaciones.
    }



    ////////////////////////////////////////////////////////////////////////
    // ACTUALIZACIÓN DE LOS DATOS DEL USUARIO

    // Permite actualizar el nombre y el rol de un usuario.
    public void Update(string name, string role)
    {
        string validatedName = ValidateName(name);                      // Primero valida el nuevo nombre.

        string validatedRole = ValidateRole(role);                      // Después valida el nuevo rol.

        // Solo asigna los datos cuando ambas validaciones son correctas.
        Name = validatedName;
        Role = validatedRole;
    }


    //////////////////////////////////////////////////////////////////////////
    // REPRESENTACIÓN DEL USUARIO COMO TEXTO

    // Define cómo se visualizará el usuario cuando se imprima en consola.
    public override string ToString()
    {
        return $"{Id} - {Name} ({Role})";
    }
}

