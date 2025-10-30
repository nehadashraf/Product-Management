# Product Manager System

A simple full-stack application for managing products using **ASP.NET Core Web API (.NET 8)** and **Angular 18**.
It demonstrates clean architecture, validation, soft deletion, pagination, concurrency handling, and API security via a static token.

---

## Setup Instructions

### Prerequisites

* **.NET 8 SDK**
* **Node.js 18+**
* **Angular CLI 18+**
* **SQL Server** (or localdb)

---

### Backend (API)

1. Navigate to the API folder:

   ```bash
   cd api
   ```
2. Restore and build:

   ```bash
   dotnet restore
   dotnet build
   ```
3. Apply migrations (if applicable):

   ```bash
   dotnet ef database update
   ```
4. Run the API:

   ```bash
   dotnet run
   ```

   The API will run by default on:

   ```
   https://localhost:7264
   ```

---

### Frontend (Angular)

1. Navigate to the web folder:

   ```bash
   cd web
   ```
2. Install dependencies:

   ```bash
   npm install
   ```
3. Run the app:

   ```bash
   ng serve
   ```

   The frontend will run on:

   ```
   http://localhost:4200
   ```

---

## Authentication

All API requests require the following static header:

```
X-Auth-Token: MyStaticToken
```

Requests without this header will return `401 Unauthorized`.

---

## Example Requests

### Add a Product

`POST /api/products`

```json
{
  "sku": "SKU123",
  "name": "Laptop",
  "description": "High performance device",
  "price": 25000
}
```

### Update Product

`PUT /api/products/1`

```json
{
  "sku": "SKU123",
  "name": "Laptop Pro",
  "description": "Upgraded version",
  "price": 27000
}
```

### Get Products (with pagination & search)

`GET /api/products?search=laptop&page=1&pageSize=10&includeDeleted=false`

### Soft Delete

`DELETE /api/products/1`
Marks the product as deleted (`IsDeleted = true`) instead of removing it.

---

## Technical Notes

### Soft Delete

Products are not physically removed from the database.
Instead, `IsDeleted = true` hides them by default, using a global EF Core query filter:

```csharp
builder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
```

You can show deleted products by calling:

```
GET /api/products?includeDeleted=true
```

---

### Concurrency

Each update operation updates the `UpdatedAtUtc` field.
This helps track the last modification and can be extended for concurrency tokens if needed.

---

### Idempotency

All endpoints (especially PUT and DELETE) are **idempotent**:

* Repeating the same PUT or DELETE request gives the same result.
* No duplicate or unexpected state changes occur if the same request is re-sent.

---

## Optional Demo

Include a short screen recording (max 3 minutes) showing:

* Adding, editing, soft deleting a product
* Searching and pagination
* “Show Deleted” checkbox toggle
