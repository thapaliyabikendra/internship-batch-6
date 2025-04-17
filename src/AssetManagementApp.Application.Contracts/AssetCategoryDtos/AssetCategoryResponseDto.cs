using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementApp.AssetsDtos;

public class AssetCategoryResponseDto
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
    public string DisplayName { get; set; }
}
