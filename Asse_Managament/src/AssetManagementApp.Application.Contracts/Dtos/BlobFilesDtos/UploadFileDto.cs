using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementApp.Dtos.BlobFilesDtos;

public class UploadFileDto
{
    public string Name { get; set; }

    public byte[] FileContent { get; set; }
}
