using System;
using Newtonsoft.Json;

namespace FileUploadService.Models
{
	public class AuthResponseModel
	{
        public string Token { get; set; }
        public DateTime Expired { get; set; }
    }
}

