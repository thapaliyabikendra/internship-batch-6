using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace AssetManagementApp.Entities
{
    public class Asset : AuditedAggregateRoot
    {
        public override object?[] GetKeys()
        {
            throw new NotImplementedException();
        }

    }
}
