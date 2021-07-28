# WEB-API
## _Asp.net core server-side application_
## Used technologies:
1. ASP.NET Core WEB API
2. EF Core
3. MS SQL
4. Automapper
5. Serilog
6. Identity

This education project is a prototype with basic functionality for e-commerce.
Designed with usage of 3-Layer architecture for separation DB-logic from business logic and http-related methods in controllers.
## Currently implemented features:
- Auth logic (sign in, sign up) - related to AuthController
  - Roles separation
  - Email Confirmation service sending confirmation token
  - Server-side validation of forms (email, password validation by regex)
  - Password changing
  - Email changin with resending confirmation message to email
  - Get/Update methods
- Validation errors logic - BaseController
  - Added method to base controller to display validation errors in pretty formatting
- System of products - Product controller
  - CRUD methods
  - Working rating system (many-to-many relation, one rate from one user to one item)
  - Filtering, Sorting, Pagination by almost all implemented fields.
- Order system
  - Smart logic of order adding (don't sure it was neccessary): adding to existing order and creating new if not existed.
  - Execute/Discard order methods with different logic in Business logic layer.
  - Used cookie for making anonymous orders, otherwise if logged in orders will be stored in db with userId - so you can check history of orders;
  - Unique constraints only for one active order (basket) per user
  - DB-calculated fields of item price and order total sum.
- Logging system
  - Serilog connected to project
  - Separation by log level to different files
- Object mapping
  - AutoMapper used for mapping between DAL-objects/business models/view models classes
  - Separate profiles in business project and web
  - Implemented null resolver for partially updating products (patch)
- Optimization
  - DAL-level optimization by using IQueryable instead of IEnumerable where possible
  - AsNoTracking() for read-only methods
  - DB Indexes
  - Response compression
  - Using EF Core caching where possible
  ## DB relations diagram
  ![DB](https://i.imgur.com/DiLpUmx.png)
  ## How to run
 1. Change connection string in appsettings.json
  ```sh
    "ConnectionStrings": {
    "DefaultConnection": "Your conn string"
  }
  ```
 2. Change credentials of your SMTP account in appsettings.json
  ```sh
  "EmailService":
  {
    "Email":"aboba@m.ru",
    "Password":"Amogus",
    "SmtpUrl": "smtp.gmail.com",
    "SmtpPort": 465,
    "UseSsl": true
  }
 ```
 3. Apply all migrations (startup project - Web, call update method from DAL folder)
 4. Call api/home/init GET method if you want to init your db with admin user.
 5. Run app
 6. If errors occured check dependencies or DB-schema (stored procedures created on standart dbo scheme)
 7. Have fun :)
   
  ## In progress:
   - Client app for connecting API
   - Swagger documentation
   - Code simplification and rewriting some methods according REST architecture
   - Some other optimization/functionality features
   - Bugfixes
  
  
Thanks @surinov for help
