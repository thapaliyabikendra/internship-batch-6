# 1-Hour .NET Intern Skills Test - Solutions

## Section 1: Core Concepts
1. **Abstract class vs Interface:**
   - Abstract classes can contain implementation, constructors, fields, and non-public members
   - Interfaces contain only method signatures, properties, events, and indexers (until C# 8.0)
   - A class can implement multiple interfaces but inherit from only one abstract class
   - Abstract classes are for "is-a" relationships while interfaces are for "can-do" relationships

2. **Async keyword:**
   - The `async` keyword enables the `await` keyword within a method and changes how method results are handled
   - Use it when performing I/O-bound operations (file/network/database) to avoid blocking threads
   - It allows writing asynchronous code that looks like synchronous code, improving readability

3. **Empty catch blocks:**
   - They silently swallow exceptions, hiding errors
   - Make debugging difficult as problems occur without any indication
   - Can lead to unexpected application behavior and data corruption
   - Better to log exceptions or handle them appropriately

4. **Dependency Injection in ASP.NET Core:**
   - A design pattern where dependencies are provided to a class rather than created within it
   - Configured in ASP.NET Core using the built-in IoC container in Program.cs or Startup.cs
   - Services are registered with different lifetimes (Singleton, Scoped, Transient)
   - Improves testability, flexibility, and decouples components

## Section 2: ASP.NET Core API
```csharp
[HttpGet("{id}")]
public IActionResult GetItem(int id)
{
    if (id <= 0)
    {
        return NotFound();
    }
    
    var item = new { id = id, name = "Test" };
    return Ok(item);
}
```

## Section 3: Entity Framework & LINQ
1. LINQ query:
```csharp
var highPaidEmployees = context.Employees
    .Where(e => e.Salary > 50000)
    .ToList();
```

2. EF Core migration command:
```
Add-Migration MigrationName
```

## Section 4: SQL
1. SQL query:
```sql
SELECT * FROM Products WHERE Price > 100;
```

2. WHERE vs HAVING:
   - WHERE filters individual rows before they are grouped
   - HAVING filters groups after GROUP BY is applied
   - WHERE works with non-aggregated columns
   - HAVING works with aggregated values (SUM, COUNT, AVG, etc.)

## Section 5: Debugging
1. Bug identified:
   - Array index out of bounds exception will occur
   - The loop starts at `input.Length` which is 1 past the last valid index
   - Should start at `input.Length - 1`

2. Corrected code:
```csharp
public class StringFormatter
{
    public string Reverse(string input)
    {
        if (input == null)
            return null;
            
        string result = "";
        for (int i = input.Length - 1; i >= 0; i--)
        {
            result += input[i];
        }
        return result;
    }
}
```

## Section 6: Problem-Solving
```csharp
public bool IsPalindrome(string input)
{
    if (string.IsNullOrEmpty(input))
        return true;
        
    input = input.ToLower();
    
    int left = 0;
    int right = input.Length - 1;
    
    while (left < right)
    {
        if (input[left] != input[right])
            return false;
            
        left++;
        right--;
    }
    
    return true;
}
```

Alternative solution using LINQ:
```csharp
public bool IsPalindrome(string input)
{
    if (string.IsNullOrEmpty(input))
        return true;
        
    string normalized = input.ToLower();
    return normalized.SequenceEqual(normalized.Reverse());
}
```