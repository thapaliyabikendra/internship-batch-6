using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace AssetManagementApp.Assets;

public class Asset : FullAuditedAggregateRoot<Guid>
{
    public string AssetName { get; set; }
    public string SerialNumber { get; set; }

    // foreign key 

    [ForeignKey(nameof(AssetCategoryId))]
    public Guid AssetCategoryId { get; set; }

    public AssetCategory AssetCategory { get; set; }

    // foreign key
    [ForeignKey(nameof(DepartmentId))]
    public Guid DepartmentId { get; set; }

    public Department Department { get; set; }

    [DataType(DataType.Date)]
    public DateTime ReceivedDate { get; set; }

}
