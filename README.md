
# ClearStay Web API

**ClearStay** is an enterprise-grade apartment and housing society management system. This repository contains the core **Web API** backend, responsible for handling all business logic, data persistence, and serving endpoints for both web and mobile client applications.

## 🏗 Architecture & Tech Stack

This backend adheres strictly to **Clean Architecture** and **SOLID** principles, utilizing the **CQRS** pattern to scale read and write operations independently.

* **Framework:** .NET Core Web API
* **Language:** C#
* **Architecture:** Clean Architecture + CQRS (via MediatR)
* **Data Access:** Entity Framework Core (Code-First)
* **Database:** PostgreSQL

## 🏢 Core API Domains

1. **Identity & Occupancy:** Role-based access (Admin, Owner, Tenant), unit allocations, and occupancy history.
2. **Financial Ledger:** Consolidated monthly invoicing, line-item billing (Maintenance, Gas, Electricity), and payment processing.
3. **Association Operations:** Outbound association expense tracking and reporting.

## 📂 Solution Structure

Dependencies flow inward toward the Domain layer. Outer layers can reference inner layers, but inner layers have no knowledge of the outside framework.

```text
ClearStayWebApi/
├── src/
│   ├── Core/
│   │   ├── ClearStay.Domain        # Pure C# Entities, Enums, Interfaces
│   │   └── ClearStay.Application   # CQRS Handlers, Validation, DTOs
│   ├── Infrastructure/
│   │   └── ClearStay.Infrastructure # EF Core DbContext, PostgreSQL mappings
│   └── Presentation/
│       └── ClearStay.WebApi        # REST Endpoints, Middleware, JWT Auth
