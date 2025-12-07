# Introducción a C#

## 1. Primeros pasos: Nociones básicas

### 1.1 La directiva `using`

````csharp
using Microsoft.Xna.Framework;
````

> 🌏 https://dev.to/rafaeljcamara/c-using-keyword-1fih

El uso de `using` es llamado en c# **directive using**. Esta directiva lo que nos permite es **no tener que estar
instanciando el objeto que queremos utilizar continuamente**.

Por ejemplo, en este caso: `using Microsoft.Xna.Framework.Graphics;` nos permite utilizar la instrucción que hay más
abajo:
`_graphics = new GraphicsDeviceManager(this);` **sin necesidad de tener que estar haciendo**:

`var graphics = new Microsoft.Xna.Framework.Graphics.GraphicsDeviceManager(this);`

Así que sería como "pre-instanciar" la clase que nos permita luego hacer la instacia per sé.

### 1.2 La directiva `namespace`

`namespace c_tgc_game;`

Lo que hace es definir como un "scope" al que van a pertenecer las variables. Es decir, que todas las variables que
creemos dentro de este `namespace` van a pertenecer únicamente a éste, y si hay otra variable llamada **exactamente
igual** en otro `namespace`, **no** presentarán conflictos entre ellos.

### 1.3 Modificadores de acceso

- Ya sabemos que hay ciertos lenguajes de backend (Java, C#, C++) que son deominados como
  `lenguajes de programación orientado a ibjetos (POO)`. En este tipo de lenguajes, existe algo llamado
  `modificadores de acceso` (`public`, `private` y `protected`), que determina el **grado de accesibilidad** de una
  variable:

- Si una variable es `public` (**pública**) es accesible desde cualquier parte del código.
- Si es `private` (**privada**), lo es solo desde el mismo fichero.

- Si es `protected`, **protegida**, es accesible desde cualquier parte del código, **excepto** desde el mismo fichero.

## 2. Cómo se crea una API REST con .NET

Para empezar, la herramienta utilizada para desarrollar aplicaciones web, en el caso de C#, es **ASP.NET**.

> 🌏 https://learn.microsoft.com/es-es/aspnet/core/fundamentals/apis?view=aspnetcore-9.0

Vamos a realizar una **API mínima**, que es lo recomendado por Microsoft para configuraciones mínimas.

La otra opción serían las **APIS basadas en controladores**, pero eso lo dejaremos para otro proyecto futuro.

>
🌏 https://learn.microsoft.com/es-es/aspnet/core/tutorials/min-web-api?view=aspnetcore-9.0&tabs=visual-studio-code#create-an-api-project

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

Es decir, que la instrucción ``var builder = WebApplication.CreateBuilder(args)`` nos permite crear una instancia de la
clase ``WebApplication`` con la cual comenzar a definir la API, y que ``.MapGet`` es una función que define una ruta (
`/`) y que devuelve un string (si hacemos una llamada a esa ruta) que devuelve ``Hello World``.

> Para que nuestra API funcione correctamente, debemos ejecutar el siguiente comando en la terminal:
> ``dotnet dev-certs https --trust``.
> Esto nos permitirá **obtener un importante certificado** para hacer funcionar nuestra API en nuestro entorno local.

>
🌏 https://learn.microsoft.com/es-es/aspnet/core/tutorials/min-web-api?view=aspnetcore-9.0&tabs=visual-studio-code#run-the-app

Pero a mí me surge una pregunta: ¿Qué estructura se supone que debo utilizar en una API realizada en C#?

### 2.1 Arquitectura de una API mínima en .NET

> 🌏 Fuente: https://treblle.com/blog/how-to-structure-your-minimal-api-in-net

**¿Qué es una *API mínima*?**

> Minimal API is a streamlined approach to building REST APIs in .NET, focusing on brevity
> in code, minimal configuration, and a significant reduction in the usual formalities associated with traditional
> methods.

Es decir, está hecha para *APIS simples* donde no requiramos de una gran lógica ni de un gran desarrollo. En
aplicaciones grandes se usarán en la mayoría de los casos una API basada en controladores, pero en este caso vamos a ver
cómo organizar una API mínima.

Aunque en la página nos desarrollan un poco más las diferencias, vamos a fijarnos concretamente en esta:

> Better fit for *Vertical slice architecture*: Minimal API aligns perfectly with vertical
> slice architecture, which centers around building applications around specific features,
> often involving just one endpoint. This focus on feature logic aligns seamlessly with Minimal API's principles.

### 2.2 ¿Qué es *Vertical slice architecture*?

> 🌏 https://www.jimmybogard.com/vertical-slice-architecture/

En esta página, lo define así:

> So what is a "Vertical Slice Architecture"? In this style, my architecture is built around distinct requests,
> encapsulating and grouping all concerns from front-end to back. You take a normal "n-tier" or hexagonal/whatever
> architecture and remove the gates and barriers across those layers, and couple along the axis of change:

Pero podemos encontrar una definición más sencilla en esta otra:

>
🌏 https://medium.com/@anujguptaninja/vertical-slice-architecture-structuring-vertical-slices-in-your-application-674825367c3d

> Vertical Slice Architecture focuses on separating features into individual vertical slices instead of organizing the
> entire system by layers. Each slice encapsulates all aspects of a feature, including business logic, data access, and
> presentation logic.

Es decir, *cada feature, con sus TODOs, casos de uso, endpoints, etc se agrupan en su propia capa, quedando así
separadas del resto*.

> - Loose coupling between features.
> - Better scalability as new features can be added without affecting others.
> - Higher maintainability as each feature is isolated and self-contained.

El objetivo es *aislar el acoplamiento entre las distintas features*, *tener una mejor escalabilidad donde al modificar
un caso de uso afecte a una menor proporción de capas*, y *ayudar al mantenimiento y aislamiento de las mismas*.

#### 2.2.1 Vertical Slice Architecture vs Featured Architecture

Una de las dudas que pueden surgir, es que la *Vertical Slice Architecture* (VSA) se *parece bastante* a la *Featured
Architecture*, pues ambas tienen una organización *muy parecida*. Las dos se estructuran *a partir de features*, lo cual
puede inducir a confusión. Sin embargo, en lo que se diferencian es en el *planteamiento de la misma*:

- VSA es *un principio*, donde enfoca el código en torno a *casos de uso*. Es decir, que luego se emplee la denominación
  de *feature* no es más que una conveniencia etimológica que nos permite organizar el código en torno a ese concepto,
  pero <u>siempre teniendo en cuenta que tratamos *casos de uso</u>. Eso significa que *no siempre* corresponderán a una
  feature, pues depende más bien aquello que englobe y no tanto de qué se trate o no de una feature como tal.
  Cada capa (layer) encapsula todo lo necesario para ese caso: endpoint, handler/command/query, validación, acceso a
  datos, mapeos, etc. La idea clave es acoplar a lo que cambia junto (feature/caso de uso) y no por capas técnicas.

- Por otro, *Featured Architecture* se enfoca en, literalmente, eso: *las features*, por lo que existe la posibilidad de
  un *mayor acoplamiento* dado que no se busca lo que sí pretende *VSA* (es decir, el mayor desacople posible entre
  capas), sino el agrupamiento por *features* como tal, independientemente de a cuántas capas se terminen afectando.

> 📝 En la fuente de _medium_ mencionada anteriormente, en el apartado de _Folder Structure_, puedes ver un ejemplo
> tangible de VSA

### 3. Definición de la API

#### 3.1 El modelo de datos

Si ponemos en el navegador: ``https://servicios.ine.es/wstempus/js/ES/OPERACIONES_DISPONIBLES`` veremos que nos sale una
lista de operaciones disponibles.
Vamos a basarnos en uno de los objetos que se nos devuelve dentro de esta lista:

````json
 {
  "Id": 4,
  "Cod_IOE": "30147",
  "Nombre": "Estadística de Efectos de Comercio Impagados",
  "Codigo": "EI"
}
````

Para definir el modelo de `IAvailableOperationsModel`:

````csharp
public interface IAvailableOperationsModel
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

Veremos que **el nombre de la interfaz va precedido de la letra ``I``**. Esto es para poder **identificarla rápidamente
como interfaz**.
En el ``typescript`` no es una práctica común, pero en backend (en este caso, en C#) sí es algo más usual. Sin embargo,
es cierto que en otros lenguajes, como
``Go``, tampoco es común usar la letra ``I`` para identificar una interfaz (https://go.dev/tour/methods/9).

2. Funciones de acceso (``{get; set; }``)

En los lenguajes de backend (al menos, en Java, que es lo que estudié en su momento), cuando declaras una clase que
actúa como el modelo (o representación) de un objeto,
las propiedades del objeto se declaran como ``private`` y utilizas lo que se llaman **funciones de acceso** para *
*acceder** (valga la redundancia) a las mismas. Por ejemplo:

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

En este ejemplo, basado en el lenguaje de ``java``, tenemos una propiedad de clase llamada ``matrícula``, que es de tipo
``string``. Esa propiedad es **privada**, pero podemos
"acceder a ella" gracias a dos funciones de acceso: ``getMatricula()`` y ``setMatricula()``, lo que se llaman ``setter``
y ``getter``.

🤔 ¿Por qué no hacemos que la propiedad matrícula sea pública? Porque eso violaría el ``principio de encapsulamiento``,
una de las bases de la programación
orientada a objetos (
POO) (https://www.reddit.com/r/csharp/comments/ye4kmz/why_exactly_is_it_bad_to_have_public_fields/).

> 📝 _Regla de encapsulamiento_: https://medium.com/@AIbatros/c-encapsulation-6b59be896312

Privatizar la propiedad nos da un **mayor control** sobre **qué acciones queremos regular sobre ella**. Si fuera
pública, cualquiera podría obtener/sobreescribir la información; sin embargo, si
la privatizamos, podremos definir mediante las funciones de acceso u otras **qué operaciones permitimos hacer sobre las
propiedades**.

Por tanto, si en nuestra interfaz de C# escribimos:

````csharp
public interface IAvailableOperationsModel
{
    public string Id { get; }
}
````

Significa que **solo permitimos obtener la propiedad**, no permitimos modificarla. Y en este caso solo permitimos
obtenerla porque `AvailableOperationsModel` solo pretende ser
una **representación en código** del objeto que nos llega desde la petición realizada al INE. En caso de que quisiéramos
poder modificar alguna propiedad del objeto, sería más adecuado
crear **otro modelo** que represente **el objeto que almacenamos nosotros, como servidor, en la base de datos** (o donde
sea). Mantener separados
los objetos según representen a uno **llegado desde una petición externa** a uno que se encuentra **almacenado en , lo
que diríamos, **nuestro dominio conocido**, evita problemas futuros. Estos aspectos se desarrollarán mejor cuando
hablemos de los **DTO**, pero de momento
simplemente entendamos que, al ser un objeto **ajeno** a nuestro entorno, no debemos modificarlo.

### 3.2 Creando nuestra primera ruta.

Vamos a crear la primera ruta donde devolveremos unos datos obtenidos del **INE** (Instituto Nacional de Estadística).

Lo primero de todo es crear el fichero donde escribiremos el código. Teniendo en cuenta el VSA, vamos a llamar a la
carpeta `DatosINE`.

> 🖌️ Es un nombre provisional, susceptible a cambio.

Bien, ahora que sabemos que lo que queremos es crear una ruta `GET`
(porque queremos devolver unos datos cuando desde el lado cliente se nos haga una petición),
vamos a hacerlo siguiendo el patrón **CQRS**.

#### 3.2.1  Integrando CQRS

##### Antes de nada, ¿cómo se hacen peticiones http en .NET?

`.NET` pone a nuestra disposición el objeto `HttpClient` para poder realizar nuestras peticiones http.

La manera más **simple** de realizar una llamada es la siguiente:

````csharp
   HttpClient client = new HttpClient();
   client.Dispose();
````

1. La primera línea crea **una instancia** para el objeto ``HttpClient`` con el cual realizaremos la petición.
2. La segunda, da por concluida la petición.

Pero no podemos hacerlo simplemente así, porque **cada nueva instancia de HttpClient crea una nueva conexión**, lo que puede
**saturar** y causar problemas en nuestra aplicación.

> 🌏 https://medium.com/@iamprovidence/http-client-in-c-best-practices-for-experts-840b36d8f8c4

> "_With each HttpClient instance a new HTTP connection is created. But even when the client is disposed, the TCP socket
is not immediately released. If your application constantly creates new connections, it can lead to the exhaustion of
available ports."_

Esto significa que, en verdad, ``HttpClient`` está pensado **para ser instanciado una vez por aplicación**.

👉 Es por eso que `.NET` pone a nuestra disposición una factoría llamada ``HttpClientFactory``.

> 🌏 https://learn.microsoft.com/es-es/dotnet/core/extensions/httpclient-factory

Microsoft define ``HttpClientFactory`` como:

> [...] Una interfaz que se usa para configurar y crear HttpClient instancias en una aplicación mediante inserción de
> dependencias (DI). También proporciona extensiones para el middleware basado en Polly a fin de aprovechar los
> controladores de delegación en HttpClient.

Las ventajas que nos ofrece (aparte de eliminar el problema de la reasignación del DNS que describíamos en el punto
anterior 👆) son **reutilización**, integración con "pool de peticiones" y configuración customizada.

> 🧑‍💻 Puedes saber más de cómo funciona ``HttpClientFactory`` por su cuenta en este artículo: > 🌏 https://dev.to/airarrazabald/utilizando-httpclient-con-ihttpclientfactory-en-net-6-2iem

> 🦄 https://medium.com/asp-dotnet/why-use-httpclientfactory-1fa857db78de
> 🦄 https://juliocasal.com/blog/ASP.NET-Core-HttpClient-Tutorial

Lo mejor 💫 es que el uso de esta factoría combina ✨**muy bien**✨ con el uso del patrón CQRS.

> 🦄 De momento vamos a dejar a parar aquí con la explicación de ``HttpClient``.

##### 3.2.1.1 ¿Qué es CQRS?

> 🌏 https://martinfowler.com/bliki/CQRS.html

**CQRS**, que responde a la abreviatura de **Command Query Responsability Segregation**, es un patrón que pretende *
*separar** las peticiones http en **dos tipos**:

1. **Query**, que son aquellas consultas **que no modifican nada**.
2. **Command**, que son aquellas que **sí** modifican algo.

Por ejemplo: una petición `GET` **siempre** será **query**, porque es una mera consulta de datos; por otro, las
peticiones `POST` y `PUT` será consideradas **commands**, porque ambas **modifican** algo (ya sea creando un objeto o
actualizándolo).

> 📝 Tienes más información del problema que pretende resolver y su enfoque
> aquí: https://learn.microsoft.com/es-es/azure/architecture/patterns/cqrs

> ‼️ Es mi primera vez aplicándolo en un lenguaje de backend, con unas reglas léxicas bastante distintas al front, así
> que no te preocupes si cometes errores 📚.

Si buscamos información sobre cómo implementar CQRS en .NET, encontraremos una librería llamada `MediatR`:

> 🌏 https://www.netmentor.es/entrada/tutorial-mediatr-dotnet

Se trata de una librería **muy popular** que se utiliza frecuentemente con este patrón, puesto que permite incluir el
patrón `mediator` de una manera escalable y funcional. Sin embargo, para un proyecto pequeño puede resultar *overkill*.
Dado que estamos aprendiendo, vamos a intentar gestionar algunos aspectos nosotros mismos para aprovechar y aprender.

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

> 📚 En C# es buena práctica nombrar a las propiedes **públicas** y **protected** utilizando el formato `PascalCase`,
> mientras que las **privadas** se escriben en minúscula y precedidas por **_**. Ejemplo en código:
> `````csharp
> public class Animal
> {
>    protected int Age;
>    public string Name
>    private string _internalId;
> }
> `````

2. Ahora, supongamos que queremos es poder hacer una serie de acciones con la clase que hemos creado. En este caso, como
   son productos, queremos poder **añadir productos a nuestro inventario**.
   La manera común sería hacerlo en un archivo distinto con otro nombre: quizás una clase llamada
   `ProductInventoryRepository` donde desarrollaramos esa acción, por ejemplificar.

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

> 👀 A diferencia de en otros lenguajes, en `C#` el **tipo de la variable** se coloca **a la izquierda**, mientras que en
> otros, como `Typescript`, se tipan **en la derecha**:
> | C# | Typescript |
> |-----------|-----------|
> | ICommandHandler<Product> _repository; | _repository: ICommandHandler<Product> |


> 📝 En la documentación oficial de de Microsoft, no se nos desarrolla la interfaz `ICommandHandler`, así que vamos a
> hacer una nosotros para complementar la documentación.

````csharp
public interface ICommandHandler<TCommand>
{
    void Handle(TCommand command);
}

````

La interfaz de `ICommandHandler` nos proporciona el método `Handle`, con el que realizaremos las acciones como la que
queríamos crear antes: **añadir un producto al inventario**.

Esto sería el **vistazo general** del patrón **CQRS**. Más adelante profundizaremos en el mismo y añadiremos más
contenido.

> 👉 También puedes leer más sobre CQRS aquí: https://ironpdf.com/blog/net-help/cqrs-pattern-csharp/

#### 3.2.2 Elaborando la petición query

> 🌏 https://www.milanjovanovic.tech/blog/vertical-slice-architecture

Vamos a crear los archivos necesarios para hacer una petición a la base de datos del INE para poder recibir las
operaciones disponibles sobre las que suelo buscar información. Teniendo en cuanta lo desarrollado anteriormente (**VSA
** y **CQRS**) deberíamos generar una estructura de archivos muy parecida a esto:
En la raíz del proyecto creemos una carpeta llamada `INE`:

````
c-basic-api/
└── INE/
    └── AvailableOperations/
         └── AvailableOperationsHttpQuery.cs
````

- **INE**: (Directorio) como nombre de la Feature donde vamos a englobar las cosas.
- **AvailableOperations**: (Directorio) Como otra feature. Hay una tabla en el INE que se llama OPERACIONES_DISPONIBLES,
  así que trataremos las tablas como `features` dentro de nuestro proyecto.
- **AvailableOperationsHttpQuery**: Será la **clase** que realize la petición al INE y que reciba los datos.

##### 3.2.2.1 Primera parte del ``CQRS``: interfaz IQuery

Esta interfaz nos va a permitir definir una metodología de trabajo común para todas las futuras queries que vayamos a
definir.

> 👉Recordemos que:
> 1️⃣ En el patrón ``CQRS``, la `Q` significa `query`, y es el término que debemos utilizar cuando hacemos una *
*petición de datos** sin modificar nada.
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

El método ``Execute`` deberá recibir por parámetro un objeto de tipo `IServiceCollection` (que es una interfaz que nos
permitirá crear conexiones para realizar peticiones http y que veremos más adelante).
Además, hemos declarado un ``tipo genérico`` en la interfaz para poder hacerla más dinámica.
Ese tipo genérico nos permite tener la flexibilidad de que, cuando la implementemos, definamos en ese momento
qué es lo que la Query va a devolver (porque podría ser un único elemento, varios, un objeto concreto...).

De esta manera, definimos lo que es la **metodología de trabajo**, pero nos permitimos ser lo suficientemente flexibles
para que sea reusable a interés.

¿Y quién va a implementar esta interfaz? La clase que desarrolle esa llamada: ``AvailableOperationsHttpQuery.cs``

#### 3.2.2.2 Segunda parte del ``CQRS``: creación de la clase ``AvailableOperationsHttpQuery.cs``

Ahora que ya hemos creado la definición del método (es decir, qué método va a tener que ejecutar la clase que creemos
que desarrolle toda
la petición), vamos a crear al ejecutor en sí mismo:

```csharp
namespace c_basic_api.INE.AvailableOperations;
using Core.IQuery;

public class AvailableOperationsHttpQuery: IQuery<IAvailableOperationsModel[]>

{
    public IAvailableOperationsModel[] Execute(IHttpClientFactory httpClientFactory)
    {
        HttpClient client = httpClientFactory.CreateClient("QueryOperationsAvailable");
    }
}
```

> 📚 Tipos primitivos en C#: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/reference-types

Gracias al parámetro de tipo ```IHttpClientFactory``` podemos utilizar un método llamado ``CreateClient``.

> 📝 https://medium.com/asp-dotnet/why-use-httpclientfactory-1fa857db78de

🧑‍💻 Vamos a aclarar un poco esta función porque su nombre puede resultar un poco confuso. ```CreateClient``` lo que hace
es otorgarnos una configuración que **ya hemos creado anteriormente mediante otro servicio que aún no hemos visto (``IServiceCollection``).

Este ``CreateClient`` nos permite acceder al resultado obtenido por la petición http, pero más adelante terminaremos de
desarrollar este punto. De momento, dejémoslo aquí, y expliquemos en su lugar cómo definimos estas conexiones mediante ``IServiceCollection``.

##### IServiceCollection: ```ConfigureServices```

> 🌏https://medium.com/@MatinGhanbari/mastering-dependency-injection-with-iservicecollection-in-net-core-6b46f62a584c

Un estándar dentro de ``C#`` es crear una clase aparte llamada ``ConfigureServices.cs`` donde se inicialice los
servicios necesarios durante el tiempo de configuración de la aplicación.

Algo como esto 👇

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<ITransientService, TransientService>();
    services.AddScoped<IScopedService, ScopedService>();
    services.AddSingleton<ISingletonService, SingletonService>();
}
```

Es las aplicaciones pequeñas, este proceso puede hacerse dentro del propio fichero ``Program.cs`` (o bien en un fichero
aparte llamado ``ConfigureServices.cs``),
pero por mantener una cohesión con el resto de la organización, vamos a hacerlo como sería en una aplicación más grande.

##### Métodos de extensión (``Extension methods``)

> 📚https://www.thomasclaudiushuber.com/2025/08/01/c-14-0-extension-members/
> 📚 https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
> 📚 https://medium.com/@lfilipecosta3/c-extension-methods-with-practical-use-cases-530948a8f8d9#e6be

Vamos a utilizar los ejemplos de la documentación anterior para explicar esto.

Imagina que tenemos la clase ``Developer`` que proviene de una librería externa, o de un código ajeno; es decir, de un
código al que no tenemos acceso:

````csharp
public class Developer
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
````

Y queremos obtener el nombre completo de esta clase. Como no tenemos la posibilidad de modificar esta misma clase,
podemos hacer lo que se conoce como
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

Y entonces podemos utilizar el método ``GetFullName`` como si fuera un **método estático** ya existente de la clase
original ``Developer``:

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

1. Son, realmente, "trucos visuales". No crean tipos nuevos ni modifican el original, sino que simplemente agregan
   funcionalidad a un tipo existente.
2. Preservan **el principio del desacoplamiento** (explicado en el punto 1).

Para que un método de extensión funcione correctamente es necesario que se cumplan los siguientes requisitos:

> _To use an extension method like the GetFullName extension method, the class containing the extension method – in our
case the DeveloperExtensions class – must be known in the file where you want to use the extension method._

En los ejemplos anteriores no agregamos ningún ``namespace``, por lo que, según este requerimiento, el código **no
funcionaría**. Vamos a añadir los ``namespaces`` para completarlo y entender bien esta regla:

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

Y ahora deberíamos utilizar los dos ``namespaces`` creados (`Developer` y `DeveloperExtensions`) en el fichero donde
vayamos a hacer uso
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

Esto es **obligatorio** para que la clase sea _realmente_ considerada como un método de extensión. Digamos que es "el
ancla" que lo permite. Recordemos que las clases que actúan como métodos de extensión tienen funciones que están "flotando en
el aire" (porque estos métodos de extensión **no** se usan para crear nuevas instancias ni pretenden crear nuevos tipos), y necesitan del ancla ⚓️ para poder estar
**conectados** a una clase que les permita existir.

##### Aplicando lo aprendido

> 🌏https://medium.com/@parsapanahpoor/understanding-iservicecollection-and-iserviceprovider-in-asp-net-f798c4adef70

Ahora que ya sabemos lo que son los **métodos de extension**, vamos a aplicarlo a ``IServiceCollection``.

A nivel de la carpeta ``AvailableOperations`` creemos el fichero:

````csharp
c-basic-api/
    └── INE/
        └── AvailableOperations/
            └── AvailableOperationsServices.cs
            └── AvailableOperationsHttpQuery.cs
````

Y ahora vamos a añadir el siguiente código:

```csharp
namespace c_basic_api.INE.AvailableOperations;

public static class AvailableOperationsServices
{
    public static void RegisterAvailableOperations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient("QueryOperationsAvailable", client => 
            client.BaseAddress = new Uri(""));
    }
}
```

##### Entendiendo ``AddHttpClient``

Esta función lo que hace es "activar el sistema de peticiones HTTP":

> _Cuando llama a cualquiera de los métodos de extensión AddHttpClient, está agregando IHttpClientFactory y los servicios y relacionados a IServiceCollection._

Es decir, usamos un objeto ``IServiceCollection`` para poder crear otro de tipo `IHttpClientFactory` con el cual establecer la conexión `http` deseada.

Por tanto, ``AddHttpClient`` nos da lo que se llama un "cliente en blanco", y éste nos permite hacer una _preconfiguración_ **en ese mismo momento** (como hemos hecho durante el
desarrollo de nuestra aplicación 👆) o, simplemente, cogerlo sin hacer nada de esto. El lado malo de esto es que, cada vez que lo usemos, deberemos configurar los aspectos necesarios.

⚙️ Pre-configurar sería lo mismo que decir, por ejemplo: "Para esta conexión `QueryOperationsAvailable` (el nombre del cliente) quiero establecer cuál es la Uri por
defecto (`client.BaseAddress = new Uri(");`)."

> 🧑‍💻 Hay otras más opciones de preconfiguración, pero de momento no vamos a tratarlas.

> 🦄 De hecho, en el desarrollo de nuestra aplicación, por ejemplo, le pasamos 2 parámetros.

Los parámetros que puede recibir ``AddHttpClient`` son:

a) El **nombre de la conexión** mediante la variable `httpClientName`. En nuestro caso sería: `QueryOperationsAvailable`.
b) El cliente (`client`) que nos permitirá establecer los parámetros de la conexión (como los `headers`). Por ejemplo, en el
desarrollo de la aplicación, lo hemos utilizado para establecer la Uri de este cliente: ``client.BaseAddress = new Uri("")``

> 📚 Microsoft en su guía oficial utiliza la uri de jsonplaceholder: `client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");`

Por tanto, cada vez que utilicemos el cliente con nombre ``QueryOperationsAvailable``, la uri asociada sería la predefinida en la preconfiguración. Como
no lo hicimos antes, ahora que hemos explicado un poco más en profundidad ``AddHttpClient``, vamos a añadir a la preconfiguración del cliente la uri a la que queremos apuntar:

```csharp
using c_basic_api.Core;

namespace c_basic_api.INE.AvailableOperations;

public static class AvailableOperationsServices
{
    public static void RegisterAvailableOperationsOperations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IQuery<IAvailableOperationsModel[]>, AvailableOperationsHttpQuery>();
        serviceCollection.AddHttpClient("QueryOperationsAvailable", client => 
            client.BaseAddress = new Uri("https://servicios.ine.es/wstempus/js/ES/OPERACIONES_DISPONIBLES"));
    }
}
```

#### 3.2.2.3 Tercera parte del ``CQRS``: la inyección de dependencias

> 🌏 https://medium.com/@bromanv/dependency-injection-c-f73bc303b221

En este punto ya tenemos todos los **pasos previos** necesarios para poder dar carpetazo a la inyección de dependencias.
Solo nos falta **un último paso**: **definir la inyección de dependencia en sí**.

Para poder definir la inyección de dependencias, necesitamos hacer uso de la interfaz ya conocida `IServiceCollection` y
utilizar uno de los posibles
métods para ello:

- ``AddScoped``
- ``AddSingleton``
- ``AddTransient``

##### ``AddScoped`` vs ``AddTransient`` vs ``AddSingleton``:

> https://medium.com/@developerstory/addsingleton-vs-addtransient-vs-addscoped-in-net-core-9a936147c72e

Seguramente la pregunta que flote en el aire es: ¿y qué método uso? Si leemos el artículo mencionado anteriormente:

> _In ASP.NET Core’s dependency injection system, AddSingleton, AddScoped, and AddTransient are methods used to register
services with different lifetimes._

1️⃣ ``**AddSingleton**``

> _A Singleton service is instantiated the first time it is requested, and this same instance is shared across all
subsequent requests. In other words, a Singleton service is created only once per application and the same instance is
used throughout the application's lifetime._

2️⃣ ```**AddTransient**```

> _This method registers a service as Transient. A new instance of a Transient service is created every time it is
requested._

3️⃣ ```AddScoped```

> _This method registers a service as Scoped. A new instance of a Scoped service is created for each request. For
example, in a web application, a new instance is created for each HTTP request, but the same instance is reused for all
operations within that specific request. Eg: IEventWriter, or repository registration._

> 📚https://medium.com/@bromanv/dependency-injection-c-f73bc303b221

Cada uno tiene sus propias peculiaridades:

| AddSingleton                                                       | AddTransient                                                                                               | AddScoped                                                                                                               |
|--------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------|
| Se crea una única instancia que se comparte durante<br/>           | Se crea una nueva instancia del servicio cada vez que se solicita;<br/>                                    | Se crea una instancia *cada vez que llega una petición HTTP* y se reuiliza mientras que ésta dure (la petición).<br/>   |
| todo el tiempo útil de la aplicación                               | es decir, cada vez que una clase de la aplicación lo pida, esta instancia se creará y luego se destruirá   | Una vez la petición determina, ésta se destruye.                                                                        |
|                                                                    |                                                                                                            |                                                                                                                         |
| ----------------------------------------------------------         | ---------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------- |
| 📚 Uso: Caché de memoria, configuración global, servicios que sean | 📚 Uso: Para casos ligeros (por ejemplo, una calculadora) u operaciones rápidas y sencillas                | 📚 Uso: El más típico para las APIs.                                                                                    |
| costosos de crear...                                               |                                                                                                            |                                                                                                                         |


Teniendo en cuenta estos aspectos, ahora sabemos que debemos utilizar ``AddScoped`` para registrar el `DI`.

```csharp
using c_basic_api.Core;

namespace c_basic_api.INE.AvailableOperations;

public static class AvailableOperationsServices
{
    public static void RegisterAvailableOperations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IQuery<IAvailableOperationsModel[]>, AvailableOperationsHttpQuery>();
        serviceCollection.AddHttpClient("QueryOperationsAvailable", client => 
            client.BaseAddress = new Uri(""));
    }
}
```

Ahora que ya hemos hecho la **conexión** entre nuestra interfaz ``IQuery`` y la clase `AvailableOperationsHttpQuery`, podemos utilizar la `DI`.

En el fichero ``Program.cs`` tenemos la siguiente llamada que se nos hizo por defecto al iniciar el proyecto:

```csharp
app.MapGet("/",) => "Hello World");
```

Podemos modificarla para recibir por parámetro la ``DI``:

```csharp
app.MapGet("/", (IQuery<IAvailableOperationsModel[]> availableOperationsQuery, IHttpClientFactory factory) =>
{
    availableOperationsQuery.Execute(factory);
});
```

> 🦄 El código es posible mejorarlo, pero de momento no lo haremos.

Y, ¿por qué la función ``MapGet`` tiene en su poder el parámetro `IHttpClientFactory factory`?
Bueno, esto es gracias al principio de ``Inversion of control (IoC)``.

##### Inversión de control (IoC)

> 🌏 https://medium.com/@anderson.buenogod/dependency-injection-inversion-of-control-in-c-net-8-2caef0086332
> 🌏 https://learn.microsoft.com/es-es/dotnet/communitytoolkit/mvvm/ioc

Los patrones de ``IoC`` y de `DI` funcionan **increíblemente bien juntos**.

> _In software development, Dependency Injection (DI) and Inversion of Control (IoC) are key architectural patterns used for building maintainable, scalable, and testable applications._

Gracias al `IoC` y al propio funcionamiento de la función `MapGet`, tenemos acceso **en ese momento a**:

1. Todo lo que sea un **servicio registrado** (como lo es ``AvailableOperationsHttpQuery`` gracias a `AddScoped`, que se hace con el `IServiceCollection`)
2. Y todo lo que sea un **dato vinculable** (``binding``).

> ‼️ En este momento no vamos ahondar en lo que significa ``binding``pero lo veremos más adelante 🧑‍💻.

##### Resumen hasta el momento

Hasta ahora hemos hecho lo siguiente:

1. Inicializar la aplicación utilizando **Minimal API** y organizando la arquitectura mediante **VSA** (Vertical Slice Architecture).
2. Crear un modelo que replique el tipo de dato que nos va a dar la API externa que vamos a utilizar (el INE).
3. Aplicar el patrón `CQRS`.
4. Hemos utilizado ``IServiceCollection`` para registrar una inyección de dependencia, utilizando `IQuery` como interfaz y `AvailableOperationsHttpQuery` como servicio.

##### Pero, ¡no funciona!

Si tratamos de levantar la aplicación, y **no funciona** por este mensaje:

```bash
The service collection cannot be modified because it is read-only
```

Esto es fácil de arreglar 👍 y pasa a menudo. 
Dentro del archivo ``Program.cs`` tenemos esta instrucción:

```csharp
var app = builder.Build();
```

Y esto es **el cierre total de la configuración** de la aplicación. Eso significa que, tras esta instrucción,
**no es posible registrar ningún otro servicio**. Es posible que el código haya quedado de alguna manera parecida a esto:

````csharp
var app = builder.Build();

services.RegisterAvailableOperations();
````

La línea ``service.RegisterAvailableOperations();``está **registrando un servicio después** de que la aplicación se haya cerrado.
La solución es simple: hacer todos los registros **antes**:


````csharp
services.RegisterAvailableOperations();
var app = builder.Build();
````

#### 3.2.3 Recogiendo los datos

¿Recuerdas este código?

```csharp
namespace c_basic_api.INE.AvailableOperations;
using Core.IQuery;

public class AvailableOperationsHttpQuery: IQuery<IAvailableOperationsModel[]>

{
    public IAvailableOperationsModel[] Execute(IHttpClientFactory httpClientFactory)
    {
        HttpClient client = httpClientFactory.CreateClient("QueryOperationsAvailable");
    }
}
```

Lo dejamos aparcado para explicar ``IServiceCollection``, pero ahora que ya tenemos la conexión configurada, toca **utilizarla**, que es lo que nos permite
``CreateClient``.

🦄 Al haber definido la conexión previamente con `AddHttpClient`, ésta ha quedado **almacenada** en la factoría `HttpClientFactory`, y **ahora** podemos recuperarla.

Vamos a extender un poco más el código:


````csharp
namespace c_basic_api.INE.AvailableOperations;
using Core;

public class AvailableOperationsHttpQuery: IQuery<IAvailableOperationsModel[]>

{
    public IAvailableOperationsModel[] Execute(IHttpClientFactory httpClientFactory)
    {
        HttpClient client = httpClientFactory.CreateClient("QueryOperationsAvailable");
        client.GetAsync("");
        return Array.Empty<IAvailableOperationsModel>();
    }
}
````

#### 3.2.3.1 Obtener los datos de la petición

##### 3.2.3.1.1 Task, GetAsync y GetAsyncFromJson

###### GetAsync y GetAsyncFromJson

Para obtener los datos de la petición, tenemos dos posibles funciones a utilizar: ``GetAsync`` y ``GetAsyncFromJson``

Cuando creamos la configuración con `AddHttpClient`, una de las cosas que hablamos es que **podíamos establecer una preconfiguración**, y entre las posibilidades
a preconfigurar, estaba la ``uri``. 👇

> ```csharp
> client.BaseAddress = new Uri("https://servicios.ine.es/wstempus/js/ES/OPERACIONES_DISPONIBLES"));
> ```

Eso significa que cuando usemos ``client.GetAsync("")``, **se conectará directamente a la uri que ya especificamos en su momento**, devolviéndonos
los datos obtenidos en esta petición.

Ahora, vamos a *analizar* las diferencias entre utilizar ``GetAsync`` y ``GetFromJsonAsync``:

- `GetAsync`:

> 🌏 https://learn.microsoft.com/es-es/dotnet/api/system.net.http.httpclient.getasync?view=net-8.0

``GetAsync`` es la manera de afrontar el trabajo **manualmente**, lo cual tiene algunas ventajas y desventajas.

✅ **Ventajas**

- **Te permite acceder a las cabeceras** (los `headers`). Esto es útil cuando accedemos a contenido **paginado**, donde el número total lo sitúan como un parámetro del hader.
- **Te permite acceder a contenido que no sea JSON**. Aunque hoy en día lo más común es devolver los datos como ``JSON``, hay casos donde no ocurre.
- **Gestión avanzada de errores**. Mientras que ``GetFromJsonAsync`` lanza una excepción genérica si la recepción fue mala, ``GetAsync``sí que te permite tener una definición 
más exhaustiva de los errores.
- **Mejor rendimiento para un volumen de datos grandes**.

❌ **Desventajas**

- **Pesado**. Para procesos que buscamos que sean ligeros, no es una buena opción.
- **Puede ser un overkill**. Si no necesitamos ninguna de las ventajas que ofrece ``GetAsync``, no es necesario utilizarla.

> 📚 https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient

- `GetFromJsonAsync`:

✅ **Ventajas**

- **Es más ligero que ``GetAsync``**.
- **El proceso de deserialización y serialización del ``json`` lo hace automáticamente**.
- **También el proceso de validación del json**

❌ **Desventajas**

Prácticamente son lo que serían las **ventajas** de utilizar ``GetAsync``.

Dependiendo de nuestras intenciones, es mejor utilizar uno u otro, pero para este caso lo más adecuado sería utilizar
```GetFromJsonAsync```, porque lo otro sería un **overkill**.

Así que vamos a modificar un poco el código:

```csharp
namespace c_basic_api.INE.AvailableOperations;
using Core;

public class AvailableOperationsHttpQuery: IQuery<IAvailableOperationsModel[]>

{
    public IAvailableOperationsModel[] Execute(IHttpClientFactory httpClientFactory)
    {
        HttpClient client = httpClientFactory.CreateClient("QueryOperationsAvailable");
        var response = client.GetFromJsonAsync<IAvailableOperationsModel[]>("");
        return Array.Empty<IAvailableOperationsModel>();
    }
}
```

‼️Es necesario añadir entre diamantes el tipo que esperamos que se devuelva en la petición.

👀Pero es necesario que hagamos **un cambio más**, y esto tiene que ver con el siguiente punto: ``Task``

###### Task

Tanto``GetAsync`` como ``GetFromJsonAsync`` nos devuelve una ``Task``.
Explicado por la propia documentación:

Por desgracia, en la documentación oficial de Microsoft la información que se nos da es escasa para lo que necesitamos ahora mismo:

> 🌏 https://learn.microsoft.com/es-es/dotnet/api/system.threading.tasks.task?view=net-8.0

Así que veamos este otro artículo 👇:

> 🌏https://www.c-sharpcorner.com/article/task-and-thread-in-c-sharp/#:~:text=A%20Task%20represents%20some%20asynchronous,the%20use%20of%20cancellation%20tokens.

Si vamos directamente a la definición de ``Task``, nos dice:

> _ Task represents some asynchronous operation and is part of the Task Parallel Library, a set of APIs for running tasks asynchronously and in parallel._

Entonces, una ``Task`` realmente _se parece bastante_ a una ``Promise`` de javascript, ya que ambas nos permiten trabajar **asíncronamente** con datos:

| Concepto | JavaScript / TypeScript | C# |
| :--- | :--- | :--- |
| **El Objeto** | `Promise<string>` | `Task<string>` |
| **Sin retorno** | `Promise<void>` | `Task` |
| **Esperar** | `await myPromise` | `await myTask` |
| **Crear función** | `async function getName() { ... }` | `async Task<string> GetName() { ... }` |
| **Todo a la vez** | `Promise.all([p1, p2])` | `Task.WhenAll(t1, t2)` |
| **El primero** | `Promise.race([p1, p2])` | `Task.WhenAny(t1, t2)` |
| **Valor inmediato** | `Promise.resolve("Hola")` | `Task.FromResult("Hola")` |


Utilizando ``GetAsync`` no es necesario que especifiquemos entre diamantes, pero con ``GetFromJsonAsync`` sí que lo es.

##### Modificando el código

Ahora que ya sabemos que tanto ``GetAsync`` como ``GetFromJsonAsync`` nos devuelven una ``Task``, tenemos que modificar nuestra intefaz
`IQuery` para que refleje esto:

`````csharp
namespace c_basic_api.Core;

public interface IQuery<T>
{
    public Task<T> Execute(IHttpClientFactory httpClientFactory);
}
`````

Y lo mismo con `AvailableOperationsHttpQuery`:

````csharp
namespace c_basic_api.INE.AvailableOperations;
using Core;

public class AvailableOperationsHttpQuery: IQuery<IAvailableOperationsModel[]>

{
    public async Task<IAvailableOperationsModel[]> Execute(IHttpClientFactory httpClientFactory)
    {
        HttpClient client = httpClientFactory.CreateClient("QueryOperationsAvailable");
        var response = await client.GetFromJsonAsync<IAvailableOperationsModel[]>("");
        Console.WriteLine("RESPONSE =====> " + response);
        
        return Array.Empty<IAvailableOperationsModel>();
    }
}
````

Hasta ahora no nos hemos propuesto mucho levantar la aplicación, pero si lo hacemos, veremos un **error garrafal**:

````bash
NotSupportedException: Deserialization of interface or abstract types is not supported. Type 'IAvailableOperationsModel'.
````

¡Vaya! 😔 Esto ocurre porque al querer _deserializar_ el objeto, le estamos **pasando una ``interfaz``** en lugar de una clase.
Tiene fácil arreglo. Lo que necesitamos es crear, justamente, un ``DTO``.

Es decir, esta línea:

```csharp
        var response = await client.GetFromJsonAsync<IAvailableOperationsModel[]>("");
```

Es ❌ **incorrecta**. Veamos, entonces, cómo implementar el ``DTO`` para poder des-serializar el `json` correctamente. 

#### 3.2.4 El ``DTO``

> https://arquitectosinbloques.wordpress.com/2017/09/06/usando-el-patron-dto-en-net/

¿Qué es un ``DTO``? Sus siglas significan ``Data Transfer Object``, y lo que quiere decir es, básicamente, "transformar un objeto
a otro". Cuando creamos la interfaz ``IAvailableOperationsModel``, lo hicimos, precisamente, porque necesitábamos una **representación** a nivel de nuestra aplicación
de los datos que **íbamos a obtener desde fuera**, pero **no llegamos a crear el transformador en sí**.

> 🌏 https://medium.com/@20011002nimeth/understanding-data-transfer-objects-dtos-in-c-net-best-practices-examples-fe3e90238359
> 🌏 https://learn.microsoft.com/es-es/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5

Dentro de la carpeta ``AvailableOperations`` vamos a crear el fichero ``AvailableOperationsDTO``:

````csharp
c-basic-api/
    └── AvailableOperations/
        └── AvailableOperationsDTO.cs
        └── AvailableOperationsServices.cs
        └── AvailableOperationsHttpQuery.cs
[...]
````

Y vamos a añadir el siguiente código:

````csharp
namespace c_basic_api.INE.AvailableOperations;

public class AvailableOperationsDTO: IAvailableOperationsModel
{
    public string Id { get; set; } = "";
    public string Cod_IOE { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Codigo { get; set; } = "";
    
}
````

#### 3.2.5 Últimos ajustes

Antes de continuar, necesitamos hacer unos **cambios en el código**.

Una de las ventajas que tiene el ``DTO`` es que **hace automáticamente la traducción de los datos `json` al objeto DTO**, pero para ello
**las propiedades deben llamarse de la misma manera que las propiedades del objeto ``json``**.

Lo que nos devuelve la petición del INE es un objeto con estas propiedades:

````json
  {
    "Id": 4,
    "Cod_IOE": "30147",
    "Nombre": "Estadística de Efectos de Comercio Impagados",
    "Codigo": "EI"
  },
````

Por lo que debemos cambiar el nombre de las propiedades ``Name`` por `Nombre`, y `Code` por `Codigo`, quedando el resultado así:

- `DTO`
````csharp
namespace c_basic_api.INE.AvailableOperations;

public class AvailableOperationsDTO: IAvailableOperationsModel
{
    public int Id { get; set; }
    public string Cod_IOE { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Codigo { get; set; } = "";
}
````

- `Interface`

````csharp
public interface IAvailableOperationsModel
{
    public int Id { get; set; }
    public string Cod_IOE { get; set; }
    public string Nombre { get; set; }
    public string Codigo { get; set; }
}
````

Ahora, ajustamos el código del servicio:

````csharp
namespace c_basic_api.INE.AvailableOperations;
using Core;

public class AvailableOperationsHttpQuery: IQuery<IAvailableOperationsModel[]>

{
    public async Task<IAvailableOperationsModel[]> Execute(IHttpClientFactory httpClientFactory)
    {
        HttpClient client = httpClientFactory.CreateClient("QueryAvailableOperations");
        var json = await client.GetFromJsonAsync<List<AvailableOperationsDTO>>("");
        if (json is not null)
        {
            return json.ToArray<IAvailableOperationsModel>();
        }
        return Array.Empty<IAvailableOperationsModel>();
    }
}
````

☝️ Es importante que comprobemos que la respuesta entregada por la petición **no es nula**.

> 🌏https://www.thomasclaudiushuber.com/2020/03/12/c-different-ways-to-check-for-null/

> En esta pequeña API **no hemos hecho ninguna gestión de errores** a nivel de petición. Vamos a asumir únicamente el
> **happy path**. Estudiaremos las gestiones de errores en futuros tutoriales.

También es necesario que utilicemos ``ToArray<IAvailableOperationsModel>()`` porque es una manera de indicarle de manera explícita a C# que,
efectivamente, estamos creando una lista del tipo ``IAvailableOperationsModel``. Entonces funciona como una "doble aseguración" de tipos 👍.

Ahora, solo queda devolver la respuesta en la función ``MapGet``:

````csharp
app.MapGet("/", (IQuery<IAvailableOperationsModel[]> availableOperationsQuery, IHttpClientFactory factory) => availableOperationsQuery.Execute(factory));
````

¡Y listo 🥳!

Ahora, si hacemos una llamada desde Postman (o el navegador) a ```http://localhost:5124/``` veremos el resultado de la petición hecha a la misma url que ``https://servicios.ine.es/wstempus/js/ES/OPERACIONES_DISPONIBLES``.

### 4. Resumen

En esta mini-api, hemos visto:

1. Cómo crear una ``Minimal API`` con `.NET`.
2. Cómo crear rutas (concretamente, una ruta ``GET``).
3. Cómo gestionar una arquitectura basada en ``VSA`` (Vertical Slice Architecture).
4. Cómo crear un cliente con ``.NET`` y utilizarlo para realizar peticiones.
5. Cómo utilizar el patrón ``CQRS`` en `#C`.
6. Cómo registrar una inyección de dependencia (``DI``).
7. Cómo utilizar el patrón ```CQRS``` junto con ``DI`` en `C#`.
8. Cómo crear una interfaz en ``C#``.
9. Cómo crear un ``DTO`` en ``C#``.
10. Cómo recibir los datos a partir de una petición ``http`` (``GetFromJsonAsync`` y ``GetAsync``).
11. Cómo transformar los datos obtenidos de la petición ``http`` a un objeto ``DTO``.