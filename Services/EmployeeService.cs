using EmployeeApi.Models;

namespace EmployeeApi.Services;

public class EmployeeService
{
    private readonly List<Employee> _employees = new();
    private int _nextId = 1;

    public EmployeeService()
    {
        // Seed data
        _employees.AddRange(new[]
        {
            new Employee { Id = _nextId++, FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", Phone = "555-0100", Department = "IT", Salary = 65000, JoinDate = DateTime.Parse("2023-01-05") },
            new Employee { Id = _nextId++, FirstName = "Michael", LastName = "Lee", Email = "michael.lee@example.com", Phone = "555-0110", Department = "Sales", Salary = 58000, JoinDate = DateTime.Parse("2022-09-12") },
            new Employee { Id = _nextId++, FirstName = "Priya", LastName = "Patel", Email = "priya.patel@example.com", Phone = "555-0120", Department = "HR", Salary = 60000, JoinDate = DateTime.Parse("2024-02-14") }
        });
    }

    public IEnumerable<Employee> GetAll() => _employees.OrderBy(e => e.Id);

    public Employee? GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);

    public Employee Create(Employee employee)
    {
        employee.Id = _nextId++;
        _employees.Add(employee);
        return employee;
    }

    public Employee? Update(int id, Employee updated)
    {
        var existing = GetById(id);
        if (existing is null) return null;

        existing.FirstName = updated.FirstName;
        existing.LastName = updated.LastName;
        existing.Email = updated.Email;
        existing.Phone = updated.Phone;
        existing.Department = updated.Department;
        existing.Salary = updated.Salary;
        existing.JoinDate = updated.JoinDate;

        return existing;
    }

    public bool Delete(int id)
    {
        var existing = GetById(id);
        if (existing is null) return false;
        _employees.Remove(existing);
        return true;
    }
}
