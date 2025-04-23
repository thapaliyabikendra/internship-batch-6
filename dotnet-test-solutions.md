# Solutions

## Core Concepts
1. Abstract class: can have implementation, one inheritance. Interface: only signatures, multiple implementations.
2. Async: marks methods that use await, enables non-blocking code execution for I/O operations.
3. Empty catch: hides errors, makes debugging impossible, leads to unexpected behavior.
4. DI: provides dependencies instead of creating them; configured in Program.cs with AddScoped, AddSingleton, etc.

## ASP.NET API
```csharp
[HttpGet("{id}")]
public IActionResult GetItem(int id)
{
    if (id <= 0) return NotFound();
    return Ok(new { id, name = "Test" });
}
```

## EF & LINQ
1. `var employees = context.Employees.Where(e => e.Salary > 50000).ToList();`
2. `Add-Migration MigrationName`

## SQL
1. `SELECT * FROM Products WHERE Price > 100;`
2. WHERE: filters rows before grouping; HAVING: filters grouped results after aggregation.

## Debugging
1. Bug: IndexOutOfRangeException - loop starts at input.Length instead of input.Length-1
2. Fix:
```csharp
public string Reverse(string input)
{
    if (input == null) return null;
    string result = "";
    for (int i = input.Length - 1; i >= 0; i--)
    {
        result += input[i];
    }
    return result;
}
```

## Palindrome
```csharp
public bool IsPalindrome(string input)
{
    if (string.IsNullOrEmpty(input)) return true;
    input = input.ToLower();
    int left = 0, right = input.Length - 1;
    while (left < right)
    {
        if (input[left] != input[right]) return false;
        left++; right--;
    }
    return true;
}
```