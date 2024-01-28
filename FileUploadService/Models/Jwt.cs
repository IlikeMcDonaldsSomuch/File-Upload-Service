using System;
namespace FileUploadService.Models
{
	public class Jwt
	{
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string Key { get; set; }
    }
}

