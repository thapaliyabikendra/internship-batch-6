using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Dtos;

public class CreateUpdateTaskDto
{
    [Required]
    [MaxLength(256)]
    public string Title { get; set; }

    [MaxLength(512)]
    public string? Description { get; set; }

    public Guid? CategoryId { get; set; }
}

