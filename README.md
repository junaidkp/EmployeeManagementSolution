### Setup Instructions

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Create a new SQL Server database (`EmployeeDB`).
4. Execute the SQL script located in the root folder (`script.sql`). This script will create the required tables and insert the sample data.
5. Update the `DefaultConnection` connection string in `appsettings.json` with your SQL Server instance details.
6. Build and run the application.
7. Open Swagger to test the APIs.
8. Use the below user credentials to obtain a JWT token and access the secured Employee APIs. (User Name : admin , Password = 123456).

The solution includes:

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Repository Pattern
* Dependency Injection
* Global Exception Handling
* FluentValidation
* JWT Authentication
* Swagger Documentation
* Unit Tests

Junaid
