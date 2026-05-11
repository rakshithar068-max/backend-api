using EmployeeApi.Models;
using EmployeeApi.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:5001","https://react-rakshitha-czhwfef3hce2fjaf.southindia-01.azurewebsites.net");

builder.Services.AddSingleton<EmployeeService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee API", Version = "v1" }));

builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDevCors", policy =>
    {
        policy.WithOrigins("https://react-rakshitha-czhwfef3hce2fjaf.southindia-01.azurewebsites.net","http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API v1"));

app.UseCors("LocalDevCors");
app.UseHttpsRedirection();

app.MapGet("/api/employees/GetEmployees", (EmployeeService service) => Results.Ok(service.GetAll()));

app.MapGet("/api/employees/GetEmployeesById/{id}", (int id, EmployeeService service) =>
{
    var employee = service.GetById(id);
    return employee is not null ? Results.Ok(employee) : Results.NotFound();
});

app.MapPost("/api/employees/CreateEmployee", (Employee employee, EmployeeService service) =>
{
    var created = service.Create(employee);
    return Results.Created($"/api/employees/GetEmployeesById/{created.Id}", created);
});

app.MapPut("/api/employees/UpdateEmployee/{id}", (int id, Employee employee, EmployeeService service) =>
{
    var updated = service.Update(id, employee);
    return updated is not null ? Results.Ok(updated) : Results.NotFound();
});

app.MapDelete("/api/employees/DeleteEmployee/{id}", (int id, EmployeeService service) =>
{
    return service.Delete(id) ? Results.NoContent() : Results.NotFound();
});

app.MapGet("/", () => "Employee API is running!");

app.Run();
