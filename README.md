# Introducción a C#

## 1. Primeros pasos: Nociones básicas

### 1.1 La directiva `using`

````csharp
using Microsoft.Xna.Framework;
````

> 🌏 https://dev.to/rafaeljcamara/c-using-keyword-1fih

El uso de `using` es llamado en c# **directive using**. Esta directiva lo que nos permite es **no tener que estar instanciando el objeto que queremos utilizar continuamente**.

Por ejemplo, en este caso: `using Microsoft.Xna.Framework.Graphics;` nos permite utilizar la instrucción que hay más abajo:
`_graphics = new GraphicsDeviceManager(this);` **sin necesidad de tener que estar haciendo**:

`var graphics = new Microsoft.Xna.Framework.Graphics.GraphicsDeviceManager(this);`

Así que sería como "pre-instanciar" la clase que nos permita luego hacer la instacia per sé.

### 1.2 La directiva `namespace`

`namespace c_tgc_game;`

Lo que hace es definir como un "scope" al que van a pertenecer las variables. Es decir, que todas las variables que creemos dentro de este `namespace` van a pertenecer únicamente a éste, y si hay otra variable llamada **exactamente igual** en otro `namespace`, **no** presentarán conflictos entre ellos.

### 1.3 Modificadores de acceso

- Ya sabemos que hay ciertos lenguajes de backend (Java, C#, C++) que son deominados como `lenguajes de programación orientado a ibjetos (POO)`. En este tipo de lenguajes, existe algo llamado `modificadores de acceso` (`public`, `private` y `protected`), que determina el **grado de accesibilidad** de una variable:

- Si una variable es `public` (**pública**) es accesible desde cualquier parte del código. 
- Si es `private` (**privada**), lo es solo desde el mismo fichero. 

- Si es `protected`, **protegida**, es accesible desde cualquier parte del código, **excepto** desde el mismo fichero.


## 2. Cómo se crea una API REST con .NET

Para empezar, la herramienta utilizada para desarrollar aplicaciones web, en el caso de C#, es **ASP.NET**. 

> 🌏 https://learn.microsoft.com/es-es/aspnet/core/fundamentals/apis?view=aspnetcore-9.0

Vamos a realizar una **API mínima**, que es lo recomendado por Microsoft para configuraciones mínimas.

La otra opción serían las **APIS basadas en controladores**, pero eso lo dejaremos para otro proyecto futuro.

> 🌏 https://learn.microsoft.com/es-es/aspnet/core/tutorials/min-web-api?view=aspnetcore-9.0&tabs=visual-studio-code#create-an-api-project

**Dentro de la carpeta** donde vayamos a crear el proyecto, debemos escribir en la terminal la siguiente instrucción:

```bash
dotnet new web -o TodoApi
cd TodoApi
code -r ../TodoApi
```

**TodoApi** es el nombre del proyecto que le dan en el tutorial, pero nosotros vamos a llamarlo **c-basic-api**

Nos lanzará por terminal un mensaje como este:

````bash
The template "ASP.NET Core Empty" was created successfully.

Processing post-creation actions...
Restoring /c-basic-api/c-basic-api.csproj:
Restore succeeded.
````

Y veremos que se ha creado una carpeta con el nombre elegido por nosotros y con lo necesario para comenzar. 

Si vemos el fichero `Program.cs` veremos el siguiente código:

````csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
````

Según la documentación oficial:

> Crea los elementos WebApplicationBuilder y WebApplication con valores predeterminados preconfigurados.
> Crea un punto de conexión / HTTP GET que devuelve Hello World!.

Es decir, que la instrucción ``var builder = WebApplication.CreateBuilder(args)`` nos permite crear una instancia de la clase ``WebApplication`` con la cual comenzar a definir la API, y que ``.MapGet`` es una función que define una ruta (`/`) y que devuelve un string (si hacemos una llamada a esa ruta) que devuelve ``Hello World``.

> Para que nuestra API funcione correctamente, debemos ejecutar el siguiente comando en la terminal:
> ``dotnet dev-certs https --trust``.
> Esto nos permitirá **obtener un importante certificado** para hacer funcionar nuestra API en nuestro entorno local.

> 🌏 https://learn.microsoft.com/es-es/aspnet/core/tutorials/min-web-api?view=aspnetcore-9.0&tabs=visual-studio-code#run-the-app

Pero a mí me surge una pregunta: ¿Qué estructura se supone que debo utilizar en una API realizada en C#?

### 2.1 Arquitectura de una API mínima en .NET

> 🌏 Fuente: https://treblle.com/blog/how-to-structure-your-minimal-api-in-net

**¿Qué es una *API mínima*?**

> Minimal API is a streamlined approach to building REST APIs in .NET, focusing on brevity
> in code, minimal configuration, and a significant reduction in the usual formalities associated with traditional methods.

Es decir, está hecha para *APIS simples* donde no requiramos de una gran lógica ni de un gran desarrollo. En aplicaciones grandes se usarán en la mayoría de los casos una API basada en controladores, pero en este caso vamos a ver cómo organizar una API mínima.

Aunque en la página nos desarrollan un poco más las diferencias, vamos a fijarnos concretamente en esta:

> Better fit for *Vertical slice architecture*: Minimal API aligns perfectly with vertical 
> slice architecture, which centers around building applications around specific features, 
> often involving just one endpoint. This focus on feature logic aligns seamlessly with Minimal API's principles.

### 2.2 ¿Qué es *Vertical slice architecture*?

> 🌏 https://www.jimmybogard.com/vertical-slice-architecture/

En esta página, lo define así:

> So what is a "Vertical Slice Architecture"? In this style, my architecture is built around distinct requests, encapsulating and grouping all concerns from front-end to back. You take a normal "n-tier" or hexagonal/whatever architecture and remove the gates and barriers across those layers, and couple along the axis of change:

Pero podemos encontrar una definición más sencilla en esta otra:

> 🌏 https://medium.com/@anujguptaninja/vertical-slice-architecture-structuring-vertical-slices-in-your-application-674825367c3d

> Vertical Slice Architecture focuses on separating features into individual vertical slices instead of organizing the entire system by layers. Each slice encapsulates all aspects of a feature, including business logic, data access, and presentation logic.

Es decir, *cada feature, con sus TODOs, casos de uso, endpoints, etc se agrupan en su propia capa, quedando así separadas del resto*. 

> - Loose coupling between features.
> - Better scalability as new features can be added without affecting others.
> - Higher maintainability as each feature is isolated and self-contained.

El objetivo es *aislar el acoplamiento entre las distintas features*, *tener una mejor escalabilidad donde al modificar un caso de uso afecte a una menor proporción de capas*, y *ayudar al mantenimiento y aislamiento de las mismas*.

#### 2.2.1 Vertical Slice Architecture vs Featured Architecture

Una de las dudas que pueden surgir, es que la *Vertical Slice Architecture* (VSA) se *parece bastante* a la *Featured Architecture*, pues ambas tienen una organización *muy parecida*. Las dos se estructuran *a partir de features*, lo cual puede inducir a confusión. Sin embargo, en lo que se diferencian  es en el *planteamiento de la misma*:

- VSA es *un principio*, donde enfoca el código en torno a *casos de uso*. Es decir, que luego se emplee la denominación de *feature* no es más que una conveniencia etimológica que nos permite organizar el código en torno a ese concepto, pero <u>siempre teniendo en cuenta que tratamos *casos de uso</u>. Eso significa que *no siempre* corresponderán a una feature, pues depende más bien aquello que englobe y no tanto de qué se trate o no de una feature como tal.
Cada capa (layer) encapsula todo lo necesario para ese caso: endpoint, handler/command/query, validación, acceso a datos, mapeos, etc. La idea clave es acoplar a lo que cambia junto (feature/caso de uso) y no por capas técnicas.

- Por otro, *Featured Architecture* se enfoca en, literalmente, eso: *las features*, por lo que existe la posibilidad de un *mayor acoplamiento* dado que no se busca lo que sí pretende *VSA* (es decir,  el mayor desacople posible entre capas), sino el agrupamiento por *features* como tal, independientemente de a cuántas capas se terminen afectando.

> 📝 En la fuente de _medium_ mencionada anteriormente, en el apartado de _Folder Structure_, puedes ver un ejemplo tangible de VSA

### 3. Definición de la API

#### 3.1 El modelo de datos

Si ponemos en el navegador: ``https://servicios.ine.es/wstempus/js/ES/OPERACIONES_DISPONIBLES`` veremos que nos sale una lista de operaciones disponibles.
Vamos a basarnos en uno de los objetos que se nos devuelve dentro de esta lista:

````json
 {
    "Id": 4,
    "Cod_IOE": "30147",
    "Nombre": "Estadística de Efectos de Comercio Impagados",
    "Codigo": "EI"
  }
````

Para definir el modelo de `IActivityOperationModel`:

````csharp
public interface IActivityOperationModel
{
    public string Id { get; }
    public string Cod_IOE { get; }
    public string Name { get; }
    public string Code { get; }
}
````

🦄Vamos a hablar sobre **dos detalles** importantes de las interfaces:

1. 📋 La nomenclatura

Si nos fijamos en la documentación hallada en la mayoría de los sitios (dejo a continuación dos ejemplos):

> 🌏https://education.launchcode.org/csharp-web-dev-curriculum/interfaces-and-polymorphism/reading/interfaces/index.html
> 🌏https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/interfaces

Veremos que **el nombre de la interfaz va precedido de la letra ``I``**. Esto es para poder **identificarla rápidamente como interfaz**.
En el ``typescript`` no es una práctica común, pero en backend (en este caso, en C#) sí es algo más usual. Sin embargo, es cierto que en otros lenguajes, como
``Go``, tampoco es común usar la letra ``I`` para identificar una interfaz (https://go.dev/tour/methods/9).

2. Funciones de acceso (``{get; set; }``)

En los lenguajes de backend (al menos, en Java, que es lo que estudié en su momento), cuando declaras una clase que actúa como el modelo (o representación) de un objeto,
las propiedades del objeto se declaran como ``private`` y utilizas lo que se llaman **funciones de acceso** para **acceder** (valga la redundancia) a las mismas. Por ejemplo:

````
public interface Vehiculo {
    private String matricula = "";
    
    public String getMatricula() {
        return matricula;
    }
    public void setMatricula(String matricula) {
        this.matricula = matricula;
    }
}
````

En este ejemplo, basado en el lenguaje de ``java``, tenemos una propiedad de clase llamada ``matrícula``, que es de tipo ``string``. Esa propiedad es **privada**, pero podemos
"acceder a ella" gracias a dos funciones de acceso: ``getMatricula()`` y ``setMatricula()``, lo que se llaman ``setter`` y ``getter``.

🤔 ¿Por qué no hacemos que la propiedad matrícula sea pública? Porque eso violaría el ``principio de encapsulamiento``, una de las bases de la programación
orientada a objetos (POO) (https://www.reddit.com/r/csharp/comments/ye4kmz/why_exactly_is_it_bad_to_have_public_fields/).

> 📝 _Regla de encapsulamiento_: https://medium.com/@AIbatros/c-encapsulation-6b59be896312

Privatizar la propiedad nos da un **mayor control** sobre **qué acciones queremos regular sobre ella**. Si fuera pública, cualquiera podría obtener/sobreescribir la información; sin embargo, si
la privatizamos, podremos definir mediante las funciones de acceso u otras **qué operaciones permitimos hacer sobre las propiedades**.

Por tanto, si en nuestra interfaz de C# escribimos:

````csharp
public interface IActivityOperationModel
{
    public string Id { get; }
}
````

Significa que **solo permitimos obtener la propiedad**, no permitimos modificarla. Y en este caso solo permitimos obtenerla porque `ActivityOperationModel` solo pretende ser
una **representación en código** del objeto que nos llega desde la petición realizada al INE. En caso de que quisiéramos poder modificar alguna propiedad del objeto, sería más adecuado
crear **otro modelo** que represente **el objeto que almacenamos nosotros, como servidor, en la base de datos** (o donde sea). Mantener separados
los objetos según representen a uno **llegado desde una petición externa** a uno que se encuentra **almacenado en , lo que diríamos, **nuestro dominio conocido**, evita problemas futuros. Estos aspectos se desarrollarán mejor cuando hablemos de los **DTO**, pero de momento
simplemente entendamos que, al ser un objeto **ajeno** a nuestro entorno, no debemos modificarlo.

### 3.2 Creando nuestra primera ruta.

Vamos a crear la primera ruta donde devolveremos unos datos obtenidos del **INE** (Instituto Nacional de Estadística). 

Lo primero de todo es crear el fichero donde escribiremos el código. Teniendo en cuenta el VSA, vamos a llamar a la carpeta `DatosINE`.

> 🖌️ Es un nombre provisional, susceptible a cambio.

Bien, ahora que sabemos que lo que queremos es crear una ruta `GET` 
(porque queremos devolver unos datos cuando desde el lado cliente se nos haga una petición), 
vamos a hacerlo siguiendo el patrón **CQRS**.

#### 3.2.1  Integrando CQRS

##### 3.2.1.1 ¿Qué es CQRS?

> 🌏 https://martinfowler.com/bliki/CQRS.html

**CQRS**, que responde a la abreviatura de **Command Query Responsability Segregation**, es un patrón que pretende **separar** las peticiones http en **dos tipos**: 
1. **Query**, que son aquellas consultas **que no modifican nada**.
2. **Command**, que son aquellas que **sí** modifican algo.

Por ejemplo: una petición `GET` **siempre** será **query**, porque es una mera consulta de datos; por otro, las peticiones `POST` y `PUT` será consideradas **commands**, porque ambas **modifican** algo (ya sea creando un objeto o actualizándolo).

> 📝 Tienes más información del problema que pretende resolver y su enfoque aquí: https://learn.microsoft.com/es-es/azure/architecture/patterns/cqrs

> ‼️ Es mi primera vez aplicándolo en un lenguaje de backend, con unas reglas léxicas bastante distintas al front, así que no te preocupes si cometes errores 📚.


Si buscamos información sobre cómo implementar CQRS en .NET, encontraremos una librería llamada `MediatR`:

> 🌏 https://www.netmentor.es/entrada/tutorial-mediatr-dotnet

Se trata de una librería **muy popular** que se utiliza frecuentemente con este patrón, puesto que permite incluir el patrón `mediator` de una manera escalable y funcional. Sin embargo, para un proyecto pequeño puede resultar *overkill*. Dado que estamos aprendiendo, vamos a intentar gestionar algunos aspectos nosotros mismos para aprovechar y aprender.

> 👩🏼‍💻 Si quieres saber cuáles son las ventajas de implementar CQRS, puedes leer más al respecto aquí:
> https://learn.microsoft.com/es-es/azure/architecture/patterns/cqrs#benefits-of-cqrs

> 🦄 En esta otra web también está muy bien explicado: https://www.kurrent.io/cqrs-pattern

Tomemos un ejemplo de código de cómo implementar CQRS (según la documentación oficial de Microsoft):

> 🌏 > 🔗 https://learn.microsoft.com/es-es/azure/architecture/patterns/cqrs#example

1. Supongamos que tenemos esta clase simple: 

````csharp
namespace ReadModel
{
  public class ProductInventory
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int CurrentStock { get; set; }
  }
}
````

Una clase pública que tiene como propiedades un `Id`, `Name` y `CurrentStock`

> 📚 En C# es buena práctica nombrar a las propiedes **públicas** y **protected** utilizando el formato `PascalCase`, mientras que las **privadas** se escriben en minúscula y precedidas por **_**. Ejemplo en código:
> `````csharp
> public class Animal
> {
>    protected int Age;
>    public string Name
>    private string _internalId;
> }
> `````

2. Ahora, supongamos que queremos es poder hacer una serie de acciones con la clase que hemos creado. En este caso, como son productos, queremos poder **añadir productos a nuestro inventario**.
La manera común sería hacerlo en un archivo distinto con otro nombre: quizás una clase llamada `ProductInventoryRepository` donde desarrollaramos esa acción, por ejemplificar.

Sin embargo, si seguimos el patrón CQRS, lo adecuado será **crearnos una clase `Handler` que maneje estas vicisitudes**:

``````csharp
public class ProductsCommandHandler :
    ICommandHandler<AddToInventory>,
{
  private readonly IRepository<Product> repository;

  public ProductsCommandHandler (IRepository<Product> repository)
  {
    this.repository = repository;
  }


  void Handle (AddToInventory command)
  {
    ...
  }
}
``````

> 👀 A diferencia de en otros lenguajes, en `C#` el **tipo de la variable** se coloca **a la izquierda**, mientras que en otros, como `Typescript`, se tipan **en la derecha**:
> | C# | Typescript |
> |-----------|-----------|
> | ICommandHandler<Product> _repository; | _repository: ICommandHandler<Product> |


> 📝 En la documentación oficial de de Microsoft, no se nos desarrolla la interfaz `ICommandHandler`, así que vamos a hacer una nosotros para complementar la documentación.

````csharp
public interface ICommandHandler<TCommand>
{
    void Handle(TCommand command);
}

````

La interfaz de `ICommandHandler` nos proporciona el método `Handle`, con el que realizaremos las acciones como la que queríamos crear antes: **añadir un producto al inventario**.


Esto sería el **vistazo general** del patrón **CQRS**. Más adelante profundizaremos en el mismo y añadiremos más contenido. 

> 👉 También puedes leer más sobre CQRS aquí: https://ironpdf.com/blog/net-help/cqrs-pattern-csharp/

#### 3.2.2 Elaborando la petición query

> 🌏 https://www.milanjovanovic.tech/blog/vertical-slice-architecture

Vamos a crear los archivos necesarios para hacer una petición a la base de datos del INE para poder recibir las operaciones disponibles sobre las que suelo buscar información. Teniendo en cuanta lo desarrollado anteriormente (**VSA** y **CQRS**) deberíamos generar una estructura de archivos muy parecida a esto:
En la raíz del proyecto creemos una carpeta llamada `INE`:
````
c-basic-api/
└── INE/
    └── AvailableOperations/
         └── AvailableOperationsHttpQuery.cs
````

- **INE**: (Directorio) como nombre de la Feature donde vamos a englobar las cosas.
- **AvailableOperations**: (Directorio) Como otra feature. Hay una tabla en el INE que se llama OPERACIONES_DISPONIBLES, así que trataremos las tablas como `features` dentro de nuestro proyecto.
- **AvailableOperationsHttpQuery**: Será la **clase** que realize la petición al INE y que reciba los datos.

##### 3.2.2.1 Primera parte del ``CQRS``: interfaz IQuery

Esta interfaz nos va a permitir definir una metodología de trabajo común para todas las futuras queries que vayamos a definir.

> 👉Recordemos que:
> 1️⃣ En el patrón ``CQRS``, la `Q` significa `query`, y es el término que debemos utilizar cuando hacemos una **petición de datos** sin modificar nada.
> 2️⃣ En C# es común iniciar el nombre de la interfaz con la letra ``I`` para identificarla como tal.

Vamos a crear una carpeta llamada ``Core`` al nivel de la raíz del proyecto y, dentro de ella, la interfaz `IQuery`:

````csharp
c-basic-api/
    └── Core/
        └── IQuery.cs
````

Definamos la interfaz 👇

````csharp
namespace c_basic_api.Core.IQuery;

public interface IQuery<T>
{
    public T Execute(IHttpClientFactory httpClientFactory);
}
````

El método ``Execute`` deberá recibir por parámetro un objeto de tipo `IServiceCollection` (que es una interfaz que nos permitirá crear conexiones para realizar peticiones http y que veremos más adelante).
Además, hemos declarado un ``tipo genérico`` en la interfaz para poder hacerla más dinámica. 
Ese tipo genérico nos permite tener la flexibilidad de que, cuando la implementemos, definamos en ese momento
qué es lo que la Query va a devolver (porque podría ser un único elemento, varios, un objeto concreto...). 

De esta manera, definimos lo que es la **metodología de trabajo**, pero nos permitimos ser lo suficientemente flexibles para que sea reusable a interés.

¿Y quién va a implementar esta interfaz? La clase que desarrolle esa llamada: ``AvailableOperationsHttpQuery.cs``

#### 3.2.2.2 Segunda parte del ``CQRS``: creación de la clase ``AvailableOperationsHttpQuery.cs``

Ahora que ya hemos creado la definición del método (es decir, qué método va a tener que ejecutar la clase que creemos que desarrolle toda
la petición), vamos a crear al ejecutor en sí mismo:

```csharp
namespace c_basic_api.INE.AvailableOperations;
using Core.IQuery;

public class AvailableOperationsHttpQuery: IQuery<IActivityOperationModel[]>

{
    public IActivityOperationModel[] Execute(IHttpClientFactory httpClientFactory)
    {
        HttpClient client = httpClientFactory.CreateClient("QueryOperationsAvailable");
    }
}
```

Gracias al parámetro de tipo ```IHttpClientFactory``` podemos utilizar un método llamado ``CreateClient``.

> 📝 https://medium.com/asp-dotnet/why-use-httpclientfactory-1fa857db78de

🧑‍💻 Vamos a aclarar un poco esta función porque su nombre puede resultar un poco confuso. ```CreateClient``` lo que hace es otorgarnos una configuración que **ya hemos creado
anteriormente mediante otro servicio que aún no hemos visto (``IServiceCollection``). 

Este ``CreateClient`` nos permite acceder al resultado obtenido por la petición http, pero más adelante terminaremos de desarrollar este punto. De momento
dejémoslo aquí y hagamos un interludio para ver cómo definimos estas conexiones mediante ``IServiceCollection``.

##### IServiceCollection: ```ConfigureServices```

> 🌏https://medium.com/@MatinGhanbari/mastering-dependency-injection-with-iservicecollection-in-net-core-6b46f62a584c

Un estándar dentro de ``C#`` es crear una clase aparte llamada ``ConfigureServices.cs`` donde se inicialice los servicios necesarios durante el tiempo de configuración de la aplicación.

Algo como esto 👇

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<ITransientService, TransientService>();
    services.AddScoped<IScopedService, ScopedService>();
    services.AddSingleton<ISingletonService, SingletonService>();
}
```

Es las aplicaciones pequeñas, este proceso puede hacerse dentro del propio fichero ``Program.cs`` (o bien en un fichero aparte llamado ``ConfigureServices.cs``),
pero por mantener una cohesión con el resto de la organización, vamos a hacerlo como sería en una aplicación más grande.

##### Métodos de extensión (``Extension methods``)

> 📚https://www.thomasclaudiushuber.com/2025/08/01/c-14-0-extension-members/
> 📚 https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
> 📚 https://medium.com/@lfilipecosta3/c-extension-methods-with-practical-use-cases-530948a8f8d9#e6be

Vamos a utilizar los ejemplos de la documentación anterior para explicar esto.

Imagina que tenemos la clase ``Developer`` que proviene de una librería externa, o de un código ajeno; es decir, de un código al que no tenemos acceso:

````csharp
public class Developer
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
````

Y queremos obtener el nombre completo de esta clase. Como no tenemos la posibilidad de modificar esta misma clase, podemos hacer lo que se conoce como
``Extension methods``:

````csharp
public static class DeveloperExtensions
{
    public static string GetFullName(this Developer dev)
    {
        return $"{dev.FirstName} {dev.LastName}";
    }
}
````

Y entonces podemos utilizar el método ``GetFullName`` como si fuera un **método estático** ya existente de la clase original ``Developer``:

````csharp
var dev = new Developer
{
    FirstName = "Thomas",
    LastName = "Huber"
};

// Call the GetFullName method like a normal static method
var fullName = DeveloperExtensions.GetFullName(dev);
````

Crear `Extension methods` tiene ciertas ventajas:

1. Son, realmente, "trucos visuales". No crean tipos nuevos ni modifican el original, sino que simplemente agregan funcionalidad a un tipo existente.
2. Preservan **el principio del desacoplamiento** (explicado en el punto 1).

Para que un método de extensión funcione correctamente es necesario que se cumplan los siguientes requisitos:

> _To use an extension method like the GetFullName extension method, the class containing the extension method – in our case the DeveloperExtensions class – must be known in the file where you want to use the extension method._

En los ejemplos anteriores no agregamos ningún ``namespace``, por lo que, según este requerimiento, el código **no funcionaría**. Vamos a añadir los ``namespaces`` para completarlo y entender bien esta regla:

````csharp
namespace c_basic_api.Core.Developer

public class Developer
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

namespace c_basic_api.Core.DeveloperExtensions
using c_basic_api.Core.Developer;

public static class DeveloperExtensions
{
    public static string GetFullName(this Developer dev)
    {
        return $"{dev.FirstName} {dev.LastName}";
    }
}
````

Y ahora deberíamos utilizar los dos ``namespaces`` creados (`Developer` y `DeveloperExtensions`) en el fichero donde vayamos a hacer uso
del método de extensión. Vamos a suponer que lo queremos utilizar dentro de ``Program.cs``:

````csharp
using TCH.Models;
using TCH.Extensions; // Without this, the GetFullName extension method is not available

var dev = new Developer
{
    FirstName = "Thomas",
    LastName = "Huber"
};

var fullName = dev.GetFullName();
````

Además de utilizar correctamente los ``namespaces``, si nos fijamos en ``DeveloperExtensions``:

```csharp
namespace c_basic_api.Core.DeveloperExtensions
using c_basic_api.Core.Developer;

public static class DeveloperExtensions
{
    public static string GetFullName(this Developer dev)
    {
        return $"{dev.FirstName} {dev.LastName}";
    }
}
```

En la función ``GetFullName`` estamos pasando una referencia de la clase ``Developer``, precediéndola con `this`:

> ``public static string GetFullName(this Developer dev)``

Esto es **obligatorio** para que la clase sea _realmente_ considerada como un método de extensión. Digamos que es "el ancla" 
que lo permite. Recordemos que las clases que actúan como métodos de extensión tienen funciones que están "flotando en el aire" (porque estos métodos de extensión
**no** se usan para crear nuevas instancias ni pretenden crear nuevos tipos), y necesitan del ancla ⚓️ para poder estar **conectados** a una clase que les permita existir.

##### Aplicando lo aprendido

> 🌏https://medium.com/@parsapanahpoor/understanding-iservicecollection-and-iserviceprovider-in-asp-net-f798c4adef70

Ahora que ya sabemos lo que son los **métodos de extension**, vamos a aplicarlo a ``IServiceCollection``.

Como dijimos en en el punto anterior (donde desarrollábamos el IServiceCollection), una práctica común es crear el fichero ``ConfigureServices.cs``,
así que empecemos por ahí.

A nivel de la carpeta ``AvailableOperations`` creemos el fichero:

````csharp
c-basic-api/
    └── INE/
        └── AvailableOperations/
            └── ActivityOperationServices.cs
            └── AvailableOperationsHttpQuery.cs
````

Y ahora vamos a añadir e siguiente código:

```csharp
namespace c_basic_api.INE.AvailableOperations;

public static class ActivityOperationServices
{
    public static void RegisterActivityOperations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient("QueryOperationsAvailable", client => 
            client.BaseAddress = new Uri(""));
    }
}
```

> ‼️¡Ojo!
>
> Anteriormente hablamos de la clase ``ConfigurationService.cs``. 
> Ya no es necesaria porque, gracias a lo que sabemos de
> los métodos de extensión, podemos crear un servicio por cada tipo de cliente que necesitemos agregar.
> Recuerda que el objeto ``IServiceCollection`` lo obtenemos en el fichero `Program.cs`, justo en esta instrucción:
> ```csharp
> var services = builder.Services;
> ```

Ahora podemos hacer esto:

````csharp
services.RegisterActivityOperations();
````

Y ya tenemos registrado nuestro cliente.


#### 3.2.2.3 Tercera parte del ``CQRS``: la inyección de dependencias

> Tipos primitivos en C#: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/reference-types
> Los arrays: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/arrays


#### Preparando la inyección de dependencias (``dependency injection``, abreviado como `DI`) 

> 🌏 https://medium.com/@bromanv/dependency-injection-c-f73bc303b221

Al igual que hemos hecho una interfaz definiendo el modelo de datos que vamos a recibir por parte del INE, 
toca definir la interfaz de la query que vamos a usar para obtenerlos.

Por tanto, quedaría así:

```csharp
public interface IAvailableOperationsQueryHandler
{
    public IActivityOperationModel[] Handle();
}
```

> 📝 Notas importantes 
> 1. Usar `I` como letra precedente a las interfaces.
> 2. Definir la propiedad de acceso como ``public`` y utilizar ``Handle`` como nombre de la función asociada al handler.


#### El handler

Ahora que ya tenemos los dos "pre-constructores" (la interfaz asociada al modelo y la asociada a la definición de la propia query) podemos definir la query en sí misma; es decir, la clase:

````csharp
public class AvailableOperationsQueryHandler: IAvailableOperationsQueryHandler
{
    public IActivityOperationModel[] Handle()
    {
        throw new NotImplementedException();
    }
}
````

### 3.2 La petición http: HttpClient en .NET

Al igual que ocurre en frameworks como ``angular``, `.NET` pone a nuestra disposición el objeto `HttpClient` para poder realizar nuestras peticiones http.
La manera más **simple** de realizar una llamada es la siguiente:

````csharp
   HttpClient client = new HttpClient();
   client.Dispose();
````

1. La primera línea crea **una instancia** para el objeto ``HttpClient`` con el cual realizaremos la petición.
2. La segunda, da por concluida la petición.

El problema es que **cada nueva instancia de HttpClient crea una nueva conexión**:

> 🌏 https://medium.com/@iamprovidence/http-client-in-c-best-practices-for-experts-840b36d8f8c4

> "_With each HttpClient instance a new HTTP connection is created. But even when the client is disposed, the TCP socket is not immediately released. If your application constantly creates new connections, it can lead to the exhaustion of available ports."_

Esto significa que, en verdad, ``HttpClient`` está pensado **para ser instanciado una vez por aplicación**.

Existen varias maneras de **solucionar este hecho** que se describen en el post mencionado anteriormente:

1. Utilizar una instancia **estática** de ``HttpClient`` (`static instance`): 

```csharp
static readonly HttpClient client = new HttpClient();

app.MapGet("/", async () =>
{
    var response = await client.GetAsync("https://dummyjson.com/quotes");
              . . .
});
```

🚧 Sin embargo, si el DNS cambia regularmente, el servidor **no realizará esos cambios**, porque el DNS se estableció una única vez al crear la instancia ``HtppClient``.

2. Por ello existe la segunda opción (siendo, además, la propuesta oficial de Microsoft): El ``HttpClientFactory``.

> 🌏 https://learn.microsoft.com/es-es/dotnet/core/extensions/httpclient-factory

Las ventajas que nos ofrece (aparte de eliminar el problema de la reasignación del DNS que describíamos en el punto anterior 👆) son **reutilización**, integración con "pool de peticiones" (más adelante desarrollaremos este punto) y configuración customizada.

#### Creación de HttpClientFactory

> 📝 https://medium.com/asp-dotnet/why-use-httpclientfactory-1fa857db78de

Si nos fijamos en el ejemplo que nos proporciona la página oficial de [microsoft](https://learn.microsoft.com/es-es/dotnet/core/extensions/httpclient-factory):

```csharp
using Shared;
using NamedHttp.Example;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

string? httpClientName = builder.Configuration["TodoHttpClientName"];
ArgumentException.ThrowIfNullOrEmpty(httpClientName);

builder.Services.AddHttpClient(
    httpClientName,
    client =>
    {
        // Set the base address of the named client.
        client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

        // Add a user-agent default request header.
        client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-docs");
    });
```

Aunque tiene ``factory`` en el nombre, realmente es una nomenclatura utilizada por ``.NET`` por detrás, pero no es que nosotros tengamos que hacer un patrón de factoría (`factory pattern`) por detrás.

📋Vamos a estudiar por línea qué es este código.

##### Desglosando el código

> 🌏 https://dev.to/airarrazabald/utilizando-httpclient-con-ihttpclientfactory-en-net-6-2iem

Gracias a este artículo de Medium podemos comprender mejor de qué trata esto.

Microsoft define ``HttpClientFactory`` como:

> [...] Una interfaz que se usa para configurar y crear HttpClient instancias en una aplicación mediante inserción de dependencias (DI). También proporciona extensiones para el middleware basado en Polly a fin de aprovechar los controladores de delegación en HttpClient.

Que esto es lo que ya sabíamos: nos permite crear una instancia de ``HttpClient`` que sea reutiliizable y que no nos bloquee cuando se produzcan cambios en la configuración.

Basándonos en el ejemplo anterior, si atendemos a esta parte del código:

```csharp
builder.Services.AddHttpClient(
    httpClientName,
    client =>
    {
        // Set the base address of the named client.
        client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

        // Add a user-agent default request header.
        client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-docs");
    });
```

Vemos que se está utilizando la función ``AddHttpClient`` que, según la documentación oficial de Microsoft:

> _Para registrar IHttpClientFactory, llame a AddHttpClient_

> 🌏https://learn.microsoft.com/es-es/dotnet/core/extensions/httpclient-factory#basic-usage

Es decir, que esa línea **crea por detrás** todo el `HttpClientFactory` que necesitamos, evitándonos a nosotros hacer todo el trabajo.

> ‼️También podemos llamar a la función sin pasarle ningún parámetro:
> ```builder.Services.AddHttpClient();```

##### Extra: Reorganizar el código

Aunque en los ejemplos extraídos de microsoft se utiliza ``AddHttpClient`` directamente en el archivo de ``Program.cs``, podemos
separarlo para que no quede todo tan aglomerado.

En este artículo de medium: 🌏 https://medium.com/asp-dotnet/why-use-httpclientfactory-1fa857db78de vemos que podemos crear 
un fichero aparte con una clase llamada ``ConfigureServices``, así que vamos a hacer lo mismo.

Vamos a crear una carpeta llamada ``Core`` y crear el fichero dentro:

````csharp
c-basic-api/
    └── Core/
        └── ConfigureServices.cs
````

Y vamos a escribir la siguiente clase:

```csharp
namespace c_basic_api.Core.Configuration;

public class ConfigureServices
{
    public void Add(IServiceCollection services)
    {
        services.AddHttpClient();
    }
}
```

Vamos a pararnos un momento a analizar ``AddHttpClient``.

##### Entendiendo ``AddHttpClient``

Como dijimos anteriormente, esta función lo que hace es "activar el sistema de peticiones HTTP", y le pide a .NET que
cree la factoría de HttpClient para ser usada.

Puede tanto recibir parámetros como no recibirlos, y lo que cambia es que si los recibe **creamos una conexión por defecto**:

```csharp
string? httpClientName = builder.Configuration["TodoHttpClientName"];

builder.Services.AddHttpClient(
    httpClientName,
    client =>
    {
        // Set the base address of the named client.
        client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

        // Add a user-agent default request header.
        client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-docs");
    });
```

En el ejemplo superior 👆, pasamos por parámetro:
a) El **nombre de la conexión** mediante la variable `httpClientName` (que obtenemos de un fichero llamado `appsettings.json` y que desarrollaremos más adelante 🖌️)
b) El cliente (`client`) que nos permitirá establecer los parámetros de la conexión (como los `headers`).

``AddHttpClient`` nos da un "cliente en blanco". Entonces, esto nos deja dos opciones: pre-configurarlo en el momento en el que le pedimos un cliente a la factoría, o simplemente cogerlo y cada vez que lo usemos, configurar los aspectos necesarios.

Vamos a entender primero qué significa **pre-configurar**. Pre-configurar sería lo mismo que decir:
_"Para esta conexión `httpClientName` quiero establecer una pre-configuración, que será establecer cuál es la Uri por defecto (`client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");`)_.

En esta línea ``client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");`` establecemos una Uri por defecto para este cliente, por lo que cada vez que hagamos una conexión con este cliente, accederemos a la misma Uri.
Si no hiciéramos el paso previo de la pre-configuración, cada vez que iniciáramos una conexión tendríamos que especificar la ``BaseAddress``.

Ahora teniendo en cuenta esto, vamos entonces a desarrollar mejor nuestra clase ``ConfigureServices``:

Vamos a cambiar la clase de ``ConfigureServices.cs`` por ``AvailableOperationsHttpQuery.cs``, y vamos a colocar el fichero dentro de la carpeta ``AvailableOperations``:

> 🚧 Recuerda eliminar ``ConfigureServices.cs`` si hiciste una copia.

Y ahora vamos a crear la llamada:

```csharp
namespace c_basic_api.INE.AvailableOperations;

public class AvailableOperationsHttpQuery
{
    public void Execute(IServiceCollection services)
    {
        services.AddHttpClient("QueryOperationsAvailable",client =>
        {
            client.BaseAddress = new Uri("https://servicios.ine.es/wstempus/js/ES/OPERACIONES_DISPONIBLES");
        });
    }
}
```

> Antes de continuar, vamos a esclarecer una posible duda: ``AddHttpClient`` y ``builder.Configuration``, aunque tras bambalinas hacen lo mismo
> (crear/obtener conexiones), se usan para objetivos diferentes.
> 1️⃣ ``builder.Configuration``, por un lado, se utiliza para crear las **peticiones** de la API que queramos construir (las peticiones GET, POST, PUT...).
> Por ejemplo: Cuando creemos una ruta como ``/api/available_operations``, la almacenaremos en el `appsettings.json` y obtendremos a configuración con ``builder.Configuration``.
> 
> 1️⃣ ``AddHttpClient``, por otro, se utiliza para crear **conexiones** (o **llamadas**) a servicios externos (como otras APIs, bases de datos... cualquier servicio que no se encuentre **dentro** del dominio de nuestra aplicación).
> Por ejemplo: En este tutorial, para obtener la información del INE, la llamada que hagamos a su API la configuraremos en este punto.



 En esta línea:

```
string? httpClientName = builder.Configuration["TodoHttpClientName"];
ArgumentException.ThrowIfNullOrEmpty(httpClientName);
```

Se crea una variable llamada ``httpClientName`` donde indicamos que ésta _podría ser_ de tipo `string` (no es un `OR`, sino más bien es como decir "creo que esta variable es de tipo `string` pero no estoy seguro).

Por otro lado, esta instrucción ``builder.Configuration["TodoHttpClientName"]`` dice que "queremos obtener la configuración correspondiente a `TodoHttpClientName`".

> ‼️Es importante que aclaremos que `TodoHttpClientName` ahora mismo *no existe en el fichero `appsettings.json`.
> Simplemente vamos a asumir que esa conexión existe, y más adelante veremos cómo se crea en el fichero en cuestión.

> _En una aplicación ASP.NET Core, builder.Configuration (que es de tipo IConfiguration) es el lugar central donde se almacenan todos los ajustes de configuración._ (Fuente: Gemini 2.5 Pro).

Y la pregunta es: **¿De dónde sale esta configuración?**

Si nos fijamos en los ficheros de nuestra aplicación, hay uno llamado ``appsettings.json``.

> 🌏 https://medium.com/@sdbala/net-core-configuration-in-net-8-4a8365f24ff1

Este es su contenido:

````json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
````

Antiguamente el archivo `appsettings.json` era un archivo `XML` como este:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="RetryCount" value="5" />
    <add key="QueueLength" value="100" />
  </appSettings>
  <connectionStrings>
    <add name="MyDatabase" connectionString="Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;" providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>
```

Pero su funcionalidad era realmente la misma. De hecho, en esta línea: 

```xml
  <connectionStrings>
    <add name="MyDatabase" connectionString="Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;" providerName="System.Data.SqlClient" />
  </connectionStrings>
```

Podemos ver un adelanto de lo que vamos a tener que añadir a nuestro ``json``: El nombre correspondiente a la conexión que queremos configurar.

> 🌏 Puedes encontrar más información aquí: https://dotnetfullstackdev.medium.com/appsettings-in-net-core-the-game-changer-for-configurations-a994d842e34c

Por tanto, podemos decir que el fichero ``appsettings.json``:

> _[...] is a JSON-based configuration file used in .NET Core applications to store:_
> 1. _Connection strings._
> 2. _API keys._
> 3. _Application settings._
> 4. _Environment-specific configurations._
> 5. _This file supports hierarchical structures, making it easier to organize related settings._

Antes vimos que con esta línea:

```
string? httpClientName = builder.Configuration["TodoHttpClientName"];
ArgumentException.ThrowIfNullOrEmpty(httpClientName);
```

Accedíamos a la configuración definida en el `appsettings.json`. 

> ‼️Recordemos que **aún no la hemos configurado como tal, estamos asumiendo que existe**.

La instrucción ``builder.Configuration`` proviene del paquete de Microsoft: ``using Microsoft.Extensions.Configuration;``.

Vamos a organizar el código un poco mejor para que nos sea más sencillo entender esto.


#### Inicializando la configuración de las conexiones

Si nos fijamos en la fuente de ``medium``:

> https://dotnetfullstackdev.medium.com/appsettings-in-net-core-the-game-changer-for-configurations-a994d842e34c

Tiene creada una clase llamada ``Program`` donde inicializa la configuración de la conexión a la API:

````csharp
using Microsoft.Extensions.Configuration;
using System;

class Program
{
    static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var appName = config["AppSettings:ApplicationName"];
        var maxUsers = config["AppSettings:MaxUsers"];

        Console.WriteLine($"Application Name: {appName}");
        Console.WriteLine($"Max Users: {maxUsers}");
    }
}
````

Vamos a hacer algo parecido. Dentro de la carpeta ``Core`` creada anteriormente, vamos a crear un fichero llamado `ApiConfiguration.cs`.

````csharp
c-basic-api/
    └── Core/
        └── ConfigureServices.cs
        └── ApiConfiguration.cs
[...]
````
Y dentro de ``ApiConfiguration.cs``, creamos la siguiente clase:

```csharp
namespace c_basic_api.Core.Configuration;
using Microsoft.Extensions.Configuration;

public class ApiConfiguration
{
    public static void Start(IConfiguration builder) {


    }
}
```

> ‼️Cuando inicializamos el programa desde ``Program.cs`` y se llega a esta línea:
> ```var builder = WebApplication.CreateBuilder(args)```
> El fichero `appsettings.json` y la configuración **ya han sido cargadas**. Por tanto, lo que realmente queremos hacer desde
> ``ApiConfiguration.cs`` es **acceder a esa configuración y extraer los datos que queremos**.


Ahora, vamos a añadir la conexión que queremos hacer a ``appsettings.json``:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "INEApi": {
    "AvailableOperations": "https://servicios.ine.es/wstempus/js/ES/OPERACIONES_DISPONIBLES"
  },
  "AllowedHosts": "*"
}
```

> 👉 Lo que hemos añadido es:
>  ```
>  "INEApi": {
>    "AvailableOperations": "https://servicios.ine.es/wstempus/js/ES/OPERACIONES_DISPONIBLES"
> },```
> 

Y dentro de la clase ``ApiConfiguration.cs``:

````
c-basic-api/
└── INE/
    └── AvailableOperations/
        └── AvailableOperationsHttpQuery.cs
        └── AvailableOperationsQuery.cs
         └── AvailableOperationsQueryHandler.cs
````

Ahora vamos a desarrollar la petición:



```csharp
namespace c_basic_api.Core.Configuration;
using Microsoft.Extensions.Configuration;

public class ApiConfiguration
{
    public static void Start(IConfiguration configuration)
    {
        string? url = configuration["INEApi:AvailableOperations"];
        
    }
}
```

> 👉``string? url = configuration["INEApi:AvailableOperations"];``


Ahora ya tenemos acceso a la url de la API del INE, pero nos falta hacer la conexión.