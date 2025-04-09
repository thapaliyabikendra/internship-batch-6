using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Asset_Management_App.Asset
{
    class Asset : AuditedAggregateRoot<Guid>
    {
        public string AssetName { get; set; }
        public string SerialNUmber { get; set; }
        public AssetCategoryList Category { get; set; }

        public DepartmentsList Department { get; set; }

        public DateTime ReceivedDate { get; set; }
    }

}
