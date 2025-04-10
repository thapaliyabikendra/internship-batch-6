using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementApp.AssetsDtos;

public class CreateAssetCategoryDto
{
    public string DisplayName { get; set; }
    public string SystemName { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
}
