# Base Project

## Overview
This project is a well-structured ASP.NET Core application that follows best practices in software architecture, authentication, and logging. It is designed with a multitier structure and implements key patterns such as the Repository and Services pattern, Unit of Work, and custom entity mapping.

## Features
- **Error and Trace Logging:** Comprehensive logging system for debugging and monitoring.
- **Default and Admin Areas:** Separation of concerns with different user areas.
- **Authentication Logic:** Secure authentication system for user management.
- **Multitier Architecture:** A clean separation of concerns with different application layers.
- **Repository and Services Pattern:** Implements repository and service layers for data access and business logic.
- **Unit of Work:** Ensures consistency in database transactions.
- **Entity Framework:** ORM for efficient database interaction.
- **Exception Handling Middleware:** Centralized error handling.
- **Resources Management:** Organized resource files for localization and configurations.
- **Custom Entity Mapper:** Custom mapping logic for converting between entities and DTOs.

## Technologies Used
- ASP.NET Core
- Entity Framework Core
- JWT Authentication
- Serilog/NLog (for logging)
- AutoMapper (for custom entity mapping)

## Project Structure
```
├── src  
│   ├── Project.BaseProject (Presentation Layer)  
│   │   ├── Project.BaseProject.Resources (Localization & Resources)  
│   ├── Project.Application (Business Logic Layer)  
│   ├── Project.Common (General Utilities and mapping)   
│   ├── Project.Domain (Data Access Layer)
│   ├── Project.Infrastructure (Repositories)
└── README.md  
```

## Installation & Setup
1. Clone the repository:
   ```sh
   git clone https://github.com/Melqartz/BaseProject.git
   ```
2. Navigate to the project directory:
   ```sh
   cd BaseProject
   ```
3. Restore dependencies:
   ```sh
   dotnet restore
   ```
4. Apply database migrations:
   ```sh
   dotnet ef database update
   ```
5. Run the application:
   ```sh
   dotnet run
   ```

## Usage
- The **Admin Area** is accessible only to users with administrative privileges.
- The **Exception Handling Middleware** captures all application errors and logs them.
- The **Authentication System** handles user sign-in, sign-out, and authorization.

## Contact
For any questions or suggestions, please reach out via [GitHub Issues](https://github.com/Melqartz/BaseProject/issues).

