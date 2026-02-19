# Udemy 6th Project – Restaurants API (Clean Architecture + CQRS + EF Core + Azure)

🎓 **Udemy Course:** https://www.udemy.com/course/aspnet-core-web-api-clean-architecture-azure/  
🧪 **Development (Swagger):** https://restaurants-api-dev-app-cqdpgmcgejf6cwhh.polandcentral-01.azurewebsites.net/swagger  
🚀 **Production (Swagger):** https://restaurants-api-prod-app-ane0fthtaue2daet.polandcentral-01.azurewebsites.net/swagger  

---

## 🍽️ Overview

A layered **ASP.NET Core Web API** built with **Clean Architecture**, **CQRS (MediatR)**, **EF Core**, **FluentValidation**, **AutoMapper**, and a complete set of **unit/integration tests**.

Includes **Seeding**, **Pagination/Sorting/Filtering**, **Authorization** (roles/claims/policies/requirements/resource-based), **Serilog logging** (Console/File/Application Insights), **Azure SQL**, and **Azure Blob Storage**.

---

## ✨ Features

- ✅ Clean Architecture (Domain / Application / Infrastructure / IoC / API)
- ✅ CQRS with MediatR (Commands + Queries per entity)
- ✅ EF Core + Migrations + Configurations + Seeders
- ✅ FluentValidation + Validation Behavior pipeline
- ✅ AutoMapper Profiles + Mapping tests
- ✅ Unit of Work + Repository pattern (refactored for CQRS)
- ✅ Authentication + Authorization:
  - Roles & Claims
  - Policy-based authorization
  - Requirements + Resource-based authorization  
    *(e.g., `RestaurantAuthorizationService`)*
- ✅ Logging:
  - Serilog Console
  - Serilog File
  - Serilog Application Insights
- ✅ Azure:
  - App Service deployments (Dev/Prod)
  - Azure SQL
  - Application Insights telemetry
  - Azure Blob Storage + SAS URL generation
- ✅ Testing:
  - Domain / Application / Infrastructure unit tests
  - API integration tests (controllers + middleware + fake auth policy evaluator)

---

## 🧱 Architecture (5 Layers)

### 1) Domain
**Pure domain logic**:
- Entities
- Value Objects
- Constants (shared by Application & Infrastructure)
- Exceptions
- Interfaces (e.g., Blob Service & Authorization contracts)
- Repository interfaces

### 2) Application
Use-cases and business workflows:
- CQRS (MediatR): Commands & Queries per entity (DTOs + Handlers + Validators)
- AutoMapper profiles
- FluentValidation + `ValidationBehavior`
- Pagination (`PagedResult`)
- User/Identity:
  - `UserContext`
  - `CurrentUser`

### 3) Infrastructure
Implementation details:
- Persistence (`DbContext`)
- Repository implementations
- Migrations + EF configurations
- Seeding:
  - `IEntitySeeder`
  - `IDbInitializer`
- Storage:
  - Azure Blob implementation
- Authorization:
  - Policy names, requirements, claims principal factory
- Internal visibility + `RegisterInfrastructureServices`

### 4) IoC
Centralized service registration for all layers.

### 5) API
Delivery layer:
- Endpoints (Controllers / Minimal APIs)
- Middlewares (ErrorHandling, RequestTiming, etc.)
- Swagger + Identity endpoints
- Auth setup (Bearer tokens)
- Serilog config (Console/File/AppInsights)
- Authorization policies + requirements

---

## 📁 Solution Structure (Simplified)

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
