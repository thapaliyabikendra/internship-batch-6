using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace TaskManagement.Entities;

public class TaskItem: AuditedAggregateRoot<Guid>
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = false;
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
}
