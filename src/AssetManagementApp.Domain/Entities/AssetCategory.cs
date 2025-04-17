using AssetManagementApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace AssetManagementApp.Assets;
public class AssetCategory : FullAuditedAggregateRoot<Guid>
{
    public string DisplayName { get; set; }
    public string SystemName { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
}