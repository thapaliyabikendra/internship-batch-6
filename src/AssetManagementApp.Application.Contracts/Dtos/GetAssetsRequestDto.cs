using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementApp.Dtos;

public class GetAssetsRequestDto 
{
    public int MaxResultCount { get; set; } = 0;
    public int SkipCount { get; set; } = 10;
}
