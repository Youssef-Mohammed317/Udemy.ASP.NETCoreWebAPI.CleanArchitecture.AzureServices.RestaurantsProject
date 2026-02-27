# 🍽️ Restaurants API  
Clean Architecture + CQRS + EF Core + Azure

🎓 **Udemy Course:**  
https://www.udemy.com/course/aspnet-core-web-api-clean-architecture-azure/

🧪 **Development (Swagger):**  
https://restaurants-api-dev-app-cqdpgmcgejf6cwhh.polandcentral-01.azurewebsites.net/swagger

🚀 **Production (Swagger):**  
https://restaurants-api-prod-app-ane0fthtaue2daet.polandcentral-01.azurewebsites.net/swagger

📜 **Certificate:**  
https://drive.google.com/file/d/11CF_e1657bjhhq0HsWZyEIH0lMH6ANH2/view?usp=drive_link

---

## 📸 Swagger Preview

![Swagger Screenshot](./docs/swagger.png)

---

## 🍽️ Overview

A production-ready **ASP.NET Core Web API** built using:

- Clean Architecture  
- CQRS (MediatR)  
- EF Core  
- FluentValidation  
- AutoMapper  
- Serilog  
- Azure Services  

The project demonstrates real-world backend practices including layered architecture, advanced authorization, logging, cloud deployment, and structured testing.

---

## ✨ Features

- ✅ Clean Architecture (Domain / Application / Infrastructure / IoC / API)
- ✅ CQRS with MediatR (Commands & Queries per entity)
- ✅ EF Core + Configurations + Migrations + Seeding
- ✅ FluentValidation + ValidationBehavior pipeline
- ✅ AutoMapper Profiles + Mapping tests
- ✅ Authentication + Authorization:
  - Roles & Claims
  - Policy-based authorization
  - Requirements + Resource-based authorization
- ✅ Serilog logging (Console / File / Application Insights)
- ✅ Azure App Service (Dev & Prod)
- ✅ Azure SQL
- ✅ Azure Blob Storage (SAS URL generation)
- ✅ Application Insights telemetry
- ✅ Unit & Integration Testing

---

## 🔐 Roles & Access (Admin / Owner / User)

### 🛡️ Admin Role

- Can assign and upgrade user roles (e.g., promote User → Owner)
- Has full system-level access
- The Admin account is inserted **directly via SQL into the database**
- Only Admin can elevate user roles

---

### 🧑‍🍳 Owner Role

- Can create restaurants
- Can manage owned resources via resource-based authorization

#### How to Become an Owner

1. Register normally as a **User**
2. The **Admin upgrades your role to Owner**
3. After upgrade, you can create your restaurant

This demonstrates real-world role management and authorization control.

---

### 🔑 Demo Credentials

Admin and Owner accounts can be provided after connecting with me (e.g., via LinkedIn).

---

## 🧱 Architecture (5 Layers)

### 1️⃣ Domain
- Entities
- Value Objects
- Constants
- Exceptions
- Interfaces
- Repository Contracts

### 2️⃣ Application
- CQRS (Commands + Queries)
- MediatR Handlers
- FluentValidation
- AutoMapper Profiles
- Pagination (`PagedResult`)
- UserContext / CurrentUser

### 3️⃣ Infrastructure
- DbContext
- Repository Implementations
- EF Configurations
- Migrations
- Seeding
- Azure Blob Implementation
- Authorization Requirements

### 4️⃣ IoC
Centralized service registration.

### 5️⃣ API
- Controllers / Endpoints
- Middlewares
- Swagger
- Authentication setup
- Serilog configuration
- Authorization policies

---

## 🧪 Testing

### 📸 Test Results

![Test Results](./docs/tests.png)

The solution includes structured tests across multiple layers.

### ✅ Covered Areas

- API Controller tests  
- Middleware tests (Error handling behavior)  
- Application Command & Query handler tests  
- FluentValidation validator tests  
- AutoMapper mapping tests  
- Authorization requirement handler tests  
- CurrentUser / UserContext tests  
- Infrastructure authorization tests  

### 🧠 Testing Approach

- Focused on behavior & business logic
- Arrange / Act / Assert structure
- Mocking with Moq
- Authorization testing with fake policy evaluators
- No unnecessary framework-internal testing

---

## 📁 Solution Structure

```txt
src/
  Restaurants.Domain/
  Restaurants.Application/
  Restaurants.Infrastructure/
  Restaurants.IoC/
  Restaurants.API/

tests/
  Restaurants.Domain.UnitTests/
  Restaurants.Application.UnitTests/
  Restaurants.InfrastructureTests/
  Restaurants.APITests/
