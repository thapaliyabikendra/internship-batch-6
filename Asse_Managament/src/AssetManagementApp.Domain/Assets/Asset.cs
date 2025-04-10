using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace AssetManagementApp.Assets;

public class Asset : FullAuditedAggregateRoot<Guid>
{
    public string AssetName { get; set; }
    public string SerialNumber { get; set; }   

    public AssetCategory Category { get; set; }
    public Department OwnDepartment { get; set; }
    public DateTime ReceivedDate { get; set; }

}
