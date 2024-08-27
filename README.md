
# Employee API

## Descripción

Esta es una API RESTful desarrollada con .NET Core que gestiona una base de datos de empleados. Permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar, buscar empleados que fueron contratados después de una fecha específica.) en los registros de empleados.

## Requisitos

- [.NET Core SDK](https://dotnet.microsoft.com/download) (versión 6.0 o superior)
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

1. **`sp_crearEmpleado`**: Inserta un nuevo empleado en la tabla `Employees`.
2. **`sp_editarEmpleado`**: Actualiza la información de un empleado existente.
3. **`sp_EliminarEmpleado`**: Elimina un empleado de la base de datos basado en su ID.
4. **`sp_listaEmpleados`**: Obtiene la lista completa de empleados.
5. **`sp_obtenerEmpleados`**: Obtiene los detalles de un empleado específico basado en su ID.
6. **`sP_FechaContratoEmpleados`**: Obtiene los empleados contratados después de una fecha específica.


## Instalación y Configuración de la API



