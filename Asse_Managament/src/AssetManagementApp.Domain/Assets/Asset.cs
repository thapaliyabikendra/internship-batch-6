using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace AssetManagementApp.Assets
{
    class Asset : FullAuditedAggregateRoot<Guid>
    {
        public string Asset_Name { get; set; }
        public string Serial_Number { get; set; }   

        public AssetCategoty Category { get; set; }
        public Department OwnDepartment { get; set; }
        public DateTime ReceivedDate { get; set; }

    }

    public enum AssetCategoty
    {
        Hardware,
        Software,
        Network,
        Furniture,

    }

    public enum Department
    {
        IT,
        HR,
        Finance,
        Marketing,
        Sales,
        Operations,
    }
}
