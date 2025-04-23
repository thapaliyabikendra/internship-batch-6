using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace TaskManagement.Entities;

    public class Category : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
    }

