using AssetManagementApp.Dtos.TaskManagementDtos;
using AssetManagementApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementApp.AppServices;

public class CategotyAppService : ICategoryAppService
{
    public Task<CategoryDto> CreateAsync(CategoryDto input)
    {
        throw new NotImplementedException();
    }

    public Task<List<CategoryDto>> GetListAsync()
    {
        throw new NotImplementedException();
    }
}
