
# Employee API

## Descripción

Esta es una API RESTful desarrollada con .NET Core que gestiona una base de datos de empleados. Permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar, buscar empleados que fueron contratados después de una fecha específica.) en los registros de empleados.

## Requisitos

- [.NET Core SDK](https://dotnet.microsoft.com/download) (versión 8.0 o superior)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) o [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-editions)
- [Visual Studio](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/) (opcional, pero recomendado para desarrollo)

### 1. Base de Datos en SQL Server

Para esta API, la base de datos fue creada en SQL Server y consta de una tabla principal llamada `Employees` con los siguientes campos:

- `EmployeeId` (Primary Key, int, Identity)
- `FirstName` (nvarchar(50))
- `LastName` (nvarchar(50))
- `Email` (nvarchar(100))
- `PhoneNumber` (nvarchar(15))
- `HireDate` (datetime)

### 2. Procedimientos Almacenados

Se han creado los siguientes procedimientos almacenados para gestionar las operaciones en la base de datos:

### `sp_listaEmpleados`

```sql
    CREATE proc sp_listaEmpleados
    AS 
    BEGIN
        SELECT
            EmployeeId,
            FirstName,
            LastName,
            Email,
            PhoneNumber,
            HireDate
        FROM Employees
    END
  ```

### `sp_crearEmpleado`

```sql
  CREATE proc sp_crearEmpleado
      @FirstName NVARCHAR(50),
      @LastName NVARCHAR(50),
      @Email NVARCHAR(100),
      @PhoneNumber NVARCHAR(15),
      @HireDate DATETIME
  AS 
  BEGIN
      INSERT INTO Employees (
          FirstName,
          LastName,
          Email,
          PhoneNumber,
          HireDate
      )
      VALUES (
          @FirstName,
          @LastName,
          @Email,
          @PhoneNumber,
          @HireDate
      )
  END
```

### `sp_EditarEmpleado`

```sql
  CREATE proc sp_EditarEmpleado
      @EmployeeId INT,
      @FirstName NVARCHAR(50),
      @LastName NVARCHAR(50),
      @Email NVARCHAR(100),
      @PhoneNumber NVARCHAR(15),
      @HireDate DATETIME
  AS 
  BEGIN
      UPDATE Employees
      SET
          FirstName = @FirstName,
          LastName = @LastName,
          Email = @Email,
          PhoneNumber = @PhoneNumber,
          HireDate = @HireDate
      WHERE EmployeeId = @EmployeeId
  END
```

### `sp_EliminarEmpleado`


```sql
CREATE proc sp_EliminarEmpleado
    @EmployeeId INT
AS 
BEGIN
    DELETE FROM Employees
    WHERE EmployeeId = @EmployeeId
END
```


### `sp_ObtenerEmpleados`

```sql
CREATE proc sp_ObtenerEmpleados
    @EmployeeId INT
AS 
BEGIN
    SELECT
        EmployeeId,
        FirstName,
        LastName,
        Email,
        PhoneNumber,
        HireDate
    FROM Employees
    WHERE EmployeeId = @EmployeeId
END
```


### `sp_FechaContratoEmpleados`

```sql
CREATE proc sp_FechaContratoEmpleados
    @EmployeeId INT
AS 
BEGIN
    SELECT
        EmployeeId,
        FirstName,
        LastName,
        Email,
        PhoneNumber,
        HireDate
    FROM Employees
    WHERE EmployeeId = @EmployeeId
END
```


1. **`sp_crearEmpleado`**: Inserta un nuevo empleado en la tabla `Employees`.
2. **`sp_editarEmpleado`**: Actualiza la información de un empleado existente.
3. **`sp_EliminarEmpleado`**: Elimina un empleado de la base de datos basado en su ID.
4. **`sp_listaEmpleados`**: Obtiene la lista completa de empleados.
5. **`sp_obtenerEmpleados`**: Obtiene los detalles de un empleado específico basado en su ID.
6. **`sP_FechaContratoEmpleados`**: Obtiene los empleados contratados después de una fecha específica.
   


## Instalación y Configuración

1. **Instalación de Paquetes:**
   - Utiliza NuGet Package Manager para instalar los siguientes paquetes:
     - `Microsoft.EntityFrameworkCore` (versión 8.0.8)
     - `Microsoft.EntityFrameworkCore.SqlServer` (versión 8.0.8)
     - `Pomelo.EntityFrameworkCore.MySql` (versión 8.0.2)

2. **Configuración de la Cadena de Conexión:**
   - Abre el archivo `appsettings.json` y agrega tu cadena de conexión a la base de datos SQL Server:

     ```json
     {
       "ConnectionStrings": {
         "ConexionSQL": "Data Source=(local)\\SQLEXPRESS;Initial Catalog=DBPrueba;Trusted_Connection=True;TrustServerCertificate=True;"
       }
     }
     ```

3. **Configuración del Contexto de la Base de Datos:**
   - Crea una clase llamada `ApplicationDbContext` que configura la conexión a la base de datos y define la tabla `Employees` para que se pueda utilizar para consultar y guardar los datos de los empleados.

     ```csharp
     public class ApplicationDbContext : DbContext
     {
         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
         {
         }

         public DbSet<Employee> Employees { get; set; }
     }
     ```

4. **Configuración en `Program.cs`:**
   - Agrega el siguiente código para configurar el contexto de la base de datos en el archivo `Program.cs`:

     ```csharp
     builder.Services.AddDbContext<ApplicationDbContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));
     ```

5. **Modelo de Empleado:**
   - Crea una carpeta llamada `Models` y dentro de ella un archivo llamado `Employee.cs`. Define la clase `Employee` para representar un empleado y cada propiedad o atributo representará una columna en la tabla `Employees` de la base de datos.

     ```csharp
     public class Employee
     {
         public int EmployeeId { get; set; }
         public string FirstName { get; set; }
         public string LastName { get; set; }
         public string Email { get; set; }
         public string PhoneNumber { get; set; }
         public DateTime HireDate { get; set; }
     }
     ```
     
 6. **Controlador de Empleados:**
   - Crea un controlador llamado `EmployeeController.cs` para manejar las operaciones CRUD de la entidad `Employee`. Incluye los siguientes métodos:
   - seleccione 2 que usarían los procesos almacenados anteriormente creados.

   - ### Métodos del Controlador

#### `Get()`
- **Descripción**: Recupera la lista completa de empleados.
- **Ruta**: `GET /api/Employee`
- **Procedimiento Almacenado Utilizado**: `sp_listaEmpleados`
- **Respuesta**: Retorna una lista de empleados en formato JSON.

#### `ObtenerEmpleado(int id)`
- **Descripción**: Recupera un empleado específico basado en su ID.
- **Ruta**: `GET /api/Employee/{id}`
- **Parámetro**: `id` (ID del empleado a recuperar)
- **Respuesta**: Retorna los detalles del empleado en formato JSON, o un error 404 si el empleado no se encuentra.

#### `Post([FromBody] Employee newEmployee)`
- **Descripción**: Agrega un nuevo empleado a la base de datos.
- **Ruta**: `POST /api/Employee`
- **Cuerpo de la Solicitud**: Objeto `Employee` en formato JSON con los datos del nuevo empleado.
- **Respuesta**: Retorna el nuevo empleado con un estado 201 (Creado) y una ubicación para acceder al nuevo recurso.

#### `Put(int id, [FromBody] Employee updateEmployee)`
- **Descripción**: Actualiza la información de un empleado existente.
- **Ruta**: `PUT /api/Employee/{id}`
- **Parámetro**: `id` (ID del empleado a actualizar)
- **Cuerpo de la Solicitud**: Objeto `Employee` en formato JSON con los datos actualizados del empleado.
- **Respuesta**: Retorna un estado 400 si el ID no coincide con el ID del empleado en el cuerpo de la solicitud, o un estado 200 si se actualiza correctamente.

#### `Delete(int id)`
- **Descripción**: Elimina un empleado de la base de datos.
- **Ruta**: `DELETE /api/Employee/{id}`
- **Parámetro**: `id` (ID del empleado a eliminar)
- **Respuesta**: Retorna un estado 404 si el empleado no se encuentra, o un estado 200 si se elimina correctamente.

#### `ObtenerFechaDespuesContratoEmpleados([FromQuery] DateTime hireDate)`
- **Descripción**: Recupera empleados contratados después de una fecha específica.
- **Ruta**: `GET /api/Employee/fechaContrato`
- **Procedimiento Almacenado Utilizado**: `sP_FechaContratoEmpleados`
- **Parámetro**: `hireDate` (Fecha para filtrar los empleados contratados después de esta fecha)
- **Respuesta**: Retorna una lista de empleados en formato JSON que fueron contratados después de la fecha especificada.










