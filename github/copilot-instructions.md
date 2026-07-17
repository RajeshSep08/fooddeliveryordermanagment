# GitHub Copilot Instructions

## Project Overview

This project is a Food Delivery Order Management System built as a full-stack application using ASP.NET Core Web API and Angular.

The goal is to create a clean, maintainable, and production-style application while leveraging GitHub Copilot as an AI-assisted development tool.

---

## General Coding Standards

- Generate clean, readable, and maintainable code.
- Follow SOLID principles where applicable.
- Use meaningful class, method, and variable names.
- Keep methods small and focused on a single responsibility.
- Add XML documentation for public classes and methods in C#.
- Use async/await for database and API operations.
- Avoid duplicate code.
- Generate comments only where they add value.

---

## Backend Instructions

### Technology

- ASP.NET Core Web API
- Entity Framework Core
- In-Memory Database
- Repository Pattern
- Dependency Injection

### Architecture

Use the following structure:

FoodDelivery.Api
- Controllers
- Models
- Data
- Repositories
- Interfaces
- Services (if required)

### API Guidelines

- Return proper HTTP status codes.
- Validate request models.
- Handle exceptions gracefully.
- Return meaningful error messages.
- Keep controllers thin.
- Move business logic to the repository layer.

### Repository Pattern

Generate:

- Interface
- Repository Implementation
- Async CRUD methods

Avoid placing database logic inside controllers.

---

## Entity Framework Guidelines

- Use DbContext.
- Seed at least two sample orders.
- Use asynchronous database operations.
- Keep entity configuration simple.

---

## Angular Instructions

Use:

- Angular
- Standalone Components (if supported)
- Reactive Forms
- HttpClient
- TypeScript

Generate:

- Order Model
- Order Service
- Order List Component
- Order Form Component
- Dashboard Summary Component

---

## UI Guidelines

- Keep UI simple and clean.
- Display validation messages.
- Handle API errors gracefully.
- Use Bootstrap for responsive layout.
- Use cards for dashboard summary.
- Use tables for displaying orders.

---

## Validation Rules

CustomerName is required.

CustomerPhone is required.

FoodItem is required.

DeliveryAddress is required.

Quantity must be greater than zero.

Price must be greater than zero.

Status must be one of:

- Placed
- Preparing
- OutForDelivery
- Delivered
- Cancelled

---

## Search and Filter

Support filtering by:

- Customer Name
- Order Status

Search should be case-insensitive.

---

## Dashboard

Generate a dashboard showing:

- Total Orders
- Placed Orders
- Preparing Orders
- Out For Delivery
- Delivered Orders
- Cancelled Orders
- Total Revenue

---

## Code Quality

Generate code that is:

- Modular
- Reusable
- Easy to understand
- Easy to test

Prefer interfaces over concrete implementations.

---

## GitHub Copilot Behaviour

Before generating code:

- Explain the approach.
- Follow the project architecture.
- Reuse existing classes where possible.
- Avoid creating duplicate functionality.

After generating code:

- Explain what was created.
- Mention modified files.
- Suggest improvements where applicable.

---

## Do Not Generate

Do not generate:

- JWT Authentication
- Login functionality
- Role-based Authorization
- Payment Gateway
- Docker
- Kubernetes
- Cloud Deployment
- Email Notifications
- SMS Notifications

These are intentionally out of scope for this assessment.