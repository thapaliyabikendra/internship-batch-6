using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementApp.AssetDtos;

public class UpdateAssetDto
{
    public string AssetName { get; set; }
    public string SerialNumber { get; set; }
    public Guid AssetCategoryId { get; set; }
    public Guid DepartmentId { get; set; }
    public DateTime PurchaseDate { get; set; }
}
