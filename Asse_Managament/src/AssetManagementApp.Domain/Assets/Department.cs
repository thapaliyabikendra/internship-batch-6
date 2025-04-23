using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace AssetManagementApp.Assets;

public class Department : FullAuditedAggregateRoot<Guid>
{
    [Required]
    public string DepartmentName { get; set; }
    public string DepartmentSystemName { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
}
