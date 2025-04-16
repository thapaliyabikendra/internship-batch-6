using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementApp.DepartmentDtos;

public class CreateDepartmentRequestDto
{
    public string DisplayName { get; set; }
    public String SystemName { get; set; }
    public bool IsActive { get; set; }
    public String Description { get; set; }
}
