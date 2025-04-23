using AssetManagementApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementApp.Dtos.TaskManagementDtos;

public class CreateUdateTaskDto
{
    [Required]
    [MaxLength(256)]
    public string Title { get; set; }

    [MaxLength(512)]
    public string? Description { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

}
