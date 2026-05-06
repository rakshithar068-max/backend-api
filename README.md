# EmployeeApi

This is a .NET 8 Web API project that exposes employee CRUD endpoints matching the requested names.

## Endpoints

- `GET /api/employees/GetEmployees`
- `GET /api/employees/GetEmployeesById/{id}`
- `POST /api/employees/CreateEmployee`
- `PUT /api/employees/UpdateEmployee/{id}`
- `DELETE /api/employees/DeleteEmployee/{id}`

## Models

The `Employee` model includes:
- `Id`
- `FirstName`
- `LastName`
- `Email`
- `Phone`
- `Department`
- `Salary`
- `JoinDate`

## Run

To run this API, use:

```bash
cd EmployeeApi
dotnet run
```

Then open Swagger at `https://localhost:7249/swagger` (development only).

## Notes

- This project uses an in-memory data store.
- Seed employee data is already included in `EmployeeService`.
