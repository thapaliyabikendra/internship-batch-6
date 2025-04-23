## Test (1 Hour): Task Management API
**Scenario:** Build a simple API to manage tasks and their categories.

**Tasks:**

1.  **Project Setup:**
    * Use existing ABP application:

2.  **Entity Definitions:**
    * Create two entities in your domain layer:
        * **`TaskItem`**:
            * `Guid Id`
            * `string Title` (Required, Max Length: 256)
            * `string? Description`
            * `bool IsCompleted` (Default: `false`)
            * `Guid? CategoryId` (Foreign Key to `Category`)
            * `Category? Category` (Navigation Property)
        * **`Category`**:
            * `Guid Id`
            * `string Name` (Required, Max Length: 128, Unique)
    * Both entities should inherit from `AuditedAggregateRoot<Guid>`. Briefly explain the benefit of this.
    * Configure the one-to-many relationship between `Category` and `TaskItem` in your `TaskManagementDbContext` (`OnModelCreating` method) using EF Core conventions or fluent API.

3.  **Application Layer (DTOs):**
    * Create the following DTOs (e.g., `TaskManagement.Application/Dtos`):
        * `CreateUpdateTaskDto`: `Title` (`[Required]`, `[MaxLength(256)]`), `Description` (`[MaxLength(512)]`), `CategoryId`.
        * `TaskDto`: `Id`, `Title`, `Description`, `IsCompleted`, `CategoryId`, `CategoryName` (display the category name).
        * `CategoryDto`: `Id`, `Name`.

4.  **Application Services:**
    * Create two application service interfaces: `ITaskAppService` and `ICategoryAppService`.
    * Implement `TaskAppService` (inheriting from `ApplicationService`):
        * `Task<TaskDto> CreateAsync(CreateUpdateTaskDto input)`: Creates a new task and associates it with a category. Ensure the `CategoryId` exists.
        * `Task<PagedResultDto<TaskDto>> GetListAsync(string? categoryNameFilter, bool? isCompletedFilter, PagedAndSortedResultRequestDto input)`: Retrieves a paged and sorted list of tasks, optionally filtering by `Category.Name` (contains) and `IsCompleted` status. Include the `Category.Name` in the `TaskDto` result. Use LINQ to join and filter.
        * `Task<TaskDto> GetAsync(Guid id)`: Retrieves a specific task with its category.
    * Implement `CategoryAppService` (inheriting from `ApplicationService`):
        * `Task<CategoryDto> CreateAsync(CategoryDto input)`: Creates a new category (ensure `Name` is unique).
        * `Task<List<CategoryDto>> GetListAsync()`: Retrieves all categories.

5.  **API Controllers:**
    * Verify that ABP generates API endpoints for `ITaskAppService` (likely `/api/app/task`) and `ICategoryAppService` (likely `/api/app/category`).
