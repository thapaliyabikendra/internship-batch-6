using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementApp.AssetDtos;

public class AssetResponseDto
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public string SystemName { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
    public Guid AssetCategoryId { get; set; }
    public Guid DepartmentId { get; set; }
}
