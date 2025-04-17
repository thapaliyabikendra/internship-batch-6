using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace AssetManagementApp.Assets;

public class AssetCategory : FullAuditedAggregateRoot<Guid>
{
    [Required]
     public string DisplayName { get; set; }
    [Required]
    public string SystemName { get; set; }
     public bool IsActive { get; set; }
     public string? Description { get; set; }

    public ICollection<Asset> Asset { get; set; }
}
