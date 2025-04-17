using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementApp.AssetDtos;

public class CreateAssetDto
{
    [Required]
    public string AssetName { get; set; }
    public string SerialNumber { get; set; }

    [Required]
    public Guid AssetCategoryId { get; set; }

    [Required]
    public Guid DepartmentId { get; set; }

    [DataType(DataType.Date)]
    public DateTime PurchaseDate { get; set; }
}
