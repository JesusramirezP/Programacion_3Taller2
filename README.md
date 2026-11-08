# CampusNet — Red Social Académica

## 1. Descripción del proyecto

CampusNet es una aplicación de consola desarrollada en C# que simula una red social académica mediante un **grafo dirigido**, utilizando el patrón de arquitectura **MVC (Modelo-Vista-Controlador)**.

La aplicación representa usuarios de una comunidad universitaria y sus relaciones de seguimiento. Cada usuario corresponde a un vértice y cada relación dirigida corresponde a una arista.

El sistema permite administrar usuarios y relaciones, realizar recorridos mediante algoritmos BFS y DFS, detectar ciclos dirigidos y ejecutar consultas sobre la estructura de la red.

Adicionalmente, incorpora validaciones para garantizar la integridad de los datos ingresados y una presentación organizada de la información en consola.

## 2. Objetivos

- Implementar un grafo dirigido mediante listas de adyacencia.
- Aplicar el patrón de arquitectura MVC en C#.
- Administrar usuarios y relaciones mediante operaciones CRUD.
- Implementar recorridos BFS y DFS.
- Detectar ciclos dirigidos.
- Calcular grados de entrada y salida.
- Identificar usuarios sin seguidores, más influyentes y más activos.
- Comprobar la alcanzabilidad entre usuarios.
- Validar los datos de los usuarios antes de su creación o actualización.
- Separar la lógica del sistema de la presentación de información.

## 3. Tecnologías utilizadas

- **Lenguaje:** C#
- **Plataforma:** .NET 8
- **Tipo de aplicación:** Consola
- **Arquitectura:** Modelo-Vista-Controlador (MVC)
- **Estructura de datos:** Grafo dirigido con listas de adyacencia.
- **Expresiones regulares:** Regex para validación de datos.

## 4. Arquitectura del proyecto

El proyecto utiliza el patrón MVC, separando las responsabilidades en tres capas.

### 4.1. Model

Contiene las estructuras de datos y la lógica del grafo.

- **Vertex.cs:** representa un usuario de la red. Contiene los atributos identificador, nombre y rol. También implementa las validaciones de los datos y la actualización del nombre y rol.
- **Edge.cs:** representa una relación dirigida entre dos usuarios mediante los atributos origen y destino.
- **Graph.cs:** administra los vértices, las aristas y las listas de adyacencia. Implementa las operaciones CRUD, los recorridos BFS y DFS, la detección de ciclos y las consultas del sistema.

### 4.2. View

Contiene los métodos encargados de presentar la información en consola.

- **ConsoleView.cs:** muestra la lista de adyacencia organizada en columnas, los resultados de los recorridos BFS y DFS, los ciclos detectados, las consultas de usuarios, la alcanzabilidad y los mensajes de las operaciones CRUD y validaciones.

La vista utiliza encabezados de ancho uniforme para mantener una presentación organizada y facilitar la lectura de los resultados.

### 4.3. Controller

Coordina la comunicación entre el modelo y la vista.

- **CampusNetController.cs:** carga los usuarios y las relaciones iniciales, coordina los recorridos, ejecuta las consultas, demuestra las operaciones CRUD y realiza pruebas de validación.

El controlador captura las excepciones generadas por el modelo y envía sus mensajes a la vista para mostrarlos en consola.

### 4.4. Program.cs

Es el punto de entrada de la aplicación. Su única responsabilidad es crear el controlador e iniciar la ejecución del sistema.

## 5. Estructura de carpetas

```text
CampusNet/
│
├── Controllers/
│   └── CampusNetController.cs
│
├── Models/
│   ├── Edge.cs
│   ├── Graph.cs
│   └── Vertex.cs
│
├── Views/
│   └── ConsoleView.cs
│
├── Program.cs
├── CampusNet.csproj
└── README.md
```

## 6. Características implementadas

### 6.1. Grafo dirigido

La red académica cuenta con:

- 12 usuarios (vértices).
- 21 relaciones dirigidas (aristas).
- Identificadores únicos para los usuarios.
- Prevención de vértices duplicados.
- Prevención de relaciones duplicadas.
- Dos usuarios con grado de salida igual o superior a 4.
- Dos usuarios con grado de entrada igual a 0.
- Un ciclo dirigido.

La información del grafo se almacena mediante listas de adyacencia.

### 6.2. Lista de adyacencia

El sistema imprime la estructura del grafo en consola, mostrando:

- Nombre del usuario.
- Identificador.
- Usuarios a los que sigue.

La presentación se organiza en columnas para facilitar la lectura de las relaciones dirigidas.

### 6.3. Recorrido BFS

Se implementan tres recorridos BFS desde diferentes usuarios:

- U01
- U05
- U08

Para cada recorrido se presenta:

- Usuario de inicio.
- Orden de visita.
- Cantidad de usuarios alcanzados.

### 6.4. Recorrido DFS

Se ejecuta un recorrido DFS completo sobre el grafo.

Se presenta:

- Orden de descubrimiento de los usuarios.
- Ciclos dirigidos detectados mediante las aristas de retorno.

### 6.5. Consultas del sistema

La aplicación permite realizar las siguientes consultas:

- **Usuarios sin seguidores:** identifica los usuarios que no tienen relaciones entrantes.
- **Usuarios más influyentes:** identifica los usuarios con mayor grado de entrada.
- **Usuarios más activos:** identifica los usuarios con mayor grado de salida.
- **Alcanzabilidad:** comprueba mediante BFS si existe un camino dirigido entre dos usuarios.

### 6.6. Operaciones CRUD

Se implementan operaciones de creación, consulta, actualización y eliminación.

| Operación | Funcionalidad |
|---|---|
| Create | Crear usuarios y relaciones dirigidas. |
| Read | Consultar usuarios registrados. |
| Update | Actualizar el nombre y el rol de un usuario. |
| Delete | Eliminar usuarios y relaciones dirigidas. |

El sistema incluye una demostración CRUD con un usuario temporal, permitiendo comprobar las operaciones sin modificar la red inicial.

## 7. Validaciones de datos

Se incorporaron validaciones en la clase `Vertex.cs` para garantizar que los datos de cada usuario cumplan las condiciones establecidas.

### 7.1. Validación del identificador

- No puede estar vacío.
- Debe tener entre 3 y 10 caracteres.
- Solo permite letras y números.

### 7.2. Validación del nombre

- No puede estar vacío.
- Debe tener entre 2 y 40 caracteres.
- Solo permite letras, tildes, ñ y espacios entre palabras.
- No permite números ni caracteres especiales.

### 7.3. Validación del rol

- No puede estar vacío.
- Debe tener entre 3 y 20 caracteres.
- Solo permite letras y espacios entre palabras.
- No permite números ni caracteres especiales.

### 7.4. Manejo de excepciones

Cuando un dato no cumple las condiciones establecidas, el modelo genera una excepción `ArgumentException`.

El controlador captura la excepción y envía el mensaje correspondiente a `ConsoleView`, que se encarga de mostrarlo en consola.

### 7.5. Demostración de validaciones

Se implementó una lista de pruebas para comprobar diferentes escenarios:

- Identificadores vacíos.
- Identificadores demasiado cortos o largos.
- Identificadores con caracteres especiales.
- Nombres vacíos.
- Nombres demasiado cortos o largos.
- Nombres con números o caracteres especiales.
- Roles vacíos.
- Roles demasiado cortos o largos.
- Roles con números o caracteres especiales.
- Datos válidos, incluyendo nombres con tildes.

Las pruebas permiten identificar qué datos son aceptados y cuáles son rechazados por el sistema.

## 8. Presentación de información en consola

Se mejoró la presentación visual para facilitar la interpretación de los resultados.

Entre los ajustes realizados se encuentran:

- Encabezados con ancho uniforme.
- Lista de adyacencia organizada en columnas.
- Mensajes centralizados en la clase `ConsoleView`.
- Presentación de resultados de recorridos y consultas.
- Visualización de errores de validación desde la vista.

## 9. Instalación y ejecución

### Requisitos previos

- .NET 8 SDK o una versión compatible con el proyecto.
- Visual Studio, Visual Studio Code u otro editor compatible con C#.

### Compilación

Desde la carpeta principal del proyecto, ejecutar:

```bash
dotnet build
```

### Ejecución

```bash
dotnet run
```

## 10. Conclusiones

El desarrollo de CampusNet permitió aplicar los conceptos fundamentales de grafos dirigidos, listas de adyacencia, recorridos BFS y DFS, detección de ciclos y consultas de conectividad.

La implementación del patrón MVC permitió separar las responsabilidades del sistema, facilitando la organización, el mantenimiento y la comprensión del código.

Asimismo, la incorporación de operaciones CRUD, validaciones de datos y manejo de excepciones fortaleció la integridad de la información y la funcionalidad de la aplicación.

CampusNet constituye una simulación académica de una red social universitaria, integrando estructuras de datos, algoritmos y buenas prácticas de programación orientada a objetos.  


## 11. Información académica  

Autor: Jesus Ramirez.  
Profesor: Carlos Alberto Gutierrez Rodas     
Programacion III.  
Universidad de Manizales.  


---

**Proyecto académico: Grafos Dirigidos (MVC en C#).**