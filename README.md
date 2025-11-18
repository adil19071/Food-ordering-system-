# Online Food Ordering System - ASP.NET Core MVC

## How to run

1. Open the solution folder in Visual Studio or VS Code.
2. Ensure .NET 6 SDK is installed.
3. Update the connection string in `appsettings.json` for your SQL Server.
4. Run EF Core migrations:
   - `dotnet ef migrations add InitialCreate`
   - `dotnet ef database update`
5. Press F5 / run the project.

Default roles:
- Register a user and manually change `Role` to `Admin` in the database for admin access.

## Features

- User registration & login (simple, session-based)
- Browse restaurants & menus
- Add items to cart (session cart)
- Checkout and place orders
- Admin dashboard to manage restaurants, food items and orders
