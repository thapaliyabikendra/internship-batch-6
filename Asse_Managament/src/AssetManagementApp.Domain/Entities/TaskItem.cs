using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace AssetManagementApp.Entities;

public class TaskItem : FullAuditedAggregateRoot<Guid>
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }

    public Guid CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }

    // Consider removing this if it's redundant with Title
    // public string Name { get; set; }
}


//Guid Id
//string Title(Required, Max Length: 256)
//string? Description
//bool IsCompleted(Default: false)
//Guid? CategoryId(Foreign Key to Category)
//Category? Category