# 1-Hour .NET Intern Skills Test

## Section 1: Core Concepts (15 minutes)
**Answer briefly:**  
1. What is the difference between `abstract class` and `interface` in C#?  
2. What does the `async` keyword do, and when would you use it?  
3. Why should you avoid catching exceptions with an empty `catch` block?  
4. What is Dependency Injection, and how is it configured in ASP.NET Core?  

## Section 2: ASP.NET Core API (15 minutes)
**Task:**  
Create an HTTP `GET` endpoint that:  
- Accepts an `id` parameter.  
- Returns a `200 OK` with a JSON response `{ "id": 5, "name": "Test" }` if the `id` is valid (assume `id > 0` is valid).  
- Returns `404 Not Found` if the `id` is invalid.  

*(Write code for the Controller action only.)*

## Section 3: Entity Framework & LINQ (10 minutes)
1. Write a LINQ query to fetch all `Employees` from a `DbContext` where `Salary > 50000`.  
2. What command would you run in the Package Manager Console to create a new EF Core migration?  

## Section 4: SQL (5 minutes)
Write a SQL query to:  
- Retrieve all columns from the `Products` table where `Price > 100`.  
- Explain the difference between `WHERE` and `HAVING`.  

## Section 5: Debugging (10 minutes)
**Fix this code:**  
```csharp
public class StringFormatter
{
    public string Reverse(string input)
    {
        string result = "";
        for (int i = input.Length; i >= 0; i--)
        {
            result += input[i];
        }
        return result;
    }
}
```
1. Identify the bug.  
2. Correct the code.  

## Section 6: Problem-Solving (5 minutes)
Write a C# method to check if a string is a palindrome (case-insensitive).  
Example:  
- Input: `"Racecar"` → Output: `true`  

## Answer Sheet Template
[Provide space for answers]