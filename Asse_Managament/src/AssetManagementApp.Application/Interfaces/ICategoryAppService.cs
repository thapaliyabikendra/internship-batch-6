using AssetManagementApp.Dtos.TaskManagementDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementApp.Interfaces;

public interface ICategoryAppService
{
    Task<CategoryDto> CreateAsync(CategoryDto input);

    Task<List<CategoryDto>> GetListAsync();
}
