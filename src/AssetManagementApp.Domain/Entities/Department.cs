using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace AssetManagementApp.Entities;

public class Department : FullAuditedAggregateRoot<Guid>
{
    [Required]
    [MaxLength (100)]
    public string DisplayName { get; set; }
    [Required]
    [MaxLength (100)]
    public string SystemName { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Description { get; set; }
}
