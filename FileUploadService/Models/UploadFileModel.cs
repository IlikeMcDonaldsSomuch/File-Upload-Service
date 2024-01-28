using System;
namespace FileUploadService.Models
{
	public class UploadFileModel
	{
        public string Image { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Folder { get; set; } = string.Empty;
    }
}

