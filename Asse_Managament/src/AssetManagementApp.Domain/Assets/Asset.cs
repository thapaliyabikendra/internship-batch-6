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

    [Required]
    public AssetCategory CategoryName { get; set; }

    // foreign key
    [ForeignKey(nameof(DeprtmentId))]
    public Guid DeprtmentId { get; set; }

    [Required]
    public Department OwnByDepartment { get; set; }

    [DataType(DataType.Date)]
    public DateTime ReceivedDate { get; set; }

}
