using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace AssetManagementApp.Dtos.AssetDtos;

public class GetAssetList : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
