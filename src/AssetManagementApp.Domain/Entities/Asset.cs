using AssetManagementApp.Assets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace AssetManagementApp.Entities;

public class Asset : FullAuditedAggregateRoot<Guid>
{
    [Required]
    [MaxLength(100)]
    public string DisplayName { get; set; }
    [Required]
    [MaxLength(100)]
    public string SystemName { get; set; }
    public bool IsActive { get; set; } = true;
    [MaxLength(500)]
    public string? Description { get; set; }
    [ForeignKey("AssetCategory")]
    public Guid AssetCategoryId { get; set; }
    public  AssetCategory AssetCategory { get; set; }

    [ForeignKey("Department")]
    public Guid DepartmentId { get; set; }
    public  Department Department { get; set; }
}