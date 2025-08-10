# 🚀 .NET Core Clean Architecture JWT Authentication
This is a .NET Core application built following the Clean Architecture principles. It implements JWT-based authentication with user registration and login functionality. User data is managed via Entity Framework Core migrations. Unit tests are written using xUnit.


## ✨ Features

  - 🔒 Clean Architecture: Separation of concerns across Application, Domain, Infrastructure, and Presentation layers

  - 🔐 JWT Authentication: Secure token-based authentication

  - 👥 User Registration and Login: APIs for user signup and signin

  - 🗄️ Database Migrations: EF Core code-first migrations for managing user table

  - 🧪 Unit Testing: Core service tests implemented with xUnit and Moq
    
  - 📖 Swagger UI: Interactive API documentation for easy testing and exploration



## 🛠️ Technologies

  - 💻 .NET Core

  - 🗄️ Entity Framework Core

  - 🔑 JSON Web Tokens (JWT)

  - 🧪 xUnit

  - 🎭 Moq
    
  - 📚 Swagger / Swashbuckle


## 🚦 Getting Started


  Clone the repository:


  git clone https://github.com/sepidehAkbarii/Auth-Jwt-app.git

  Configure database connection:

  Update the connection string in appsettings.json.

  Apply migrations to create/update database schema:

  Add-Migration InitialCreate

  Update-Database


  Run the API project:

  dotnet run --project src/Api

  Access Swagger UI:
  
  Navigate to https://localhost:{port}/swagger in your browser to explore the API endpoints.

  Run unit tests:

  dotnet test
  
## 📁 Project Structure (Example)


  src/
  
     ├── Application/        # Business logic and service layer
     
     ├── Domain/             # Domain models and business rules
     
     ├── Infrastructure/     # Data access, EF Core migrations, repository implementations
   
     ├── Api/       # API layer (Controllers and Endpoints)
   
  test/
  
   └── Tests/  # xUnit test project
   
## 📌 Notes
   Made with ❤️ by [sepideh]
