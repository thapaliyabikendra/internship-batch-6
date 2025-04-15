using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementApp.DepartmentDtos;

public class CreateDepartmentResponseDto
{
    public Guid Id { get; set; }
    public string DepartmentName { get; set; }
    public string DepartmentSystemName { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; }

}
