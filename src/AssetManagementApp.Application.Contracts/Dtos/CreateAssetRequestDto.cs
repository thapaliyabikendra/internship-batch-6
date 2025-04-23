using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementApp.AssetDtos;

public class CreateAssetRequestDto
{
    [Required]
    [MaxLength(100)]
    public string DisplayName { get; set; }
    [Required]
    [MaxLength(100)]
    public string SystemName { get; set; }
    public bool IsActive { get; set; } = true;
    [MaxLength(500)]
    public string? Description { get; set; }
    [Required]
    public Guid AssetCategoryId { get; set; }

    [Required]
    public Guid DepartmentId { get; set; }
}
