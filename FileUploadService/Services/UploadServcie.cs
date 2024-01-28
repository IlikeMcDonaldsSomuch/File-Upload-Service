using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FileUploadService.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace FileUploadService.Services
{
	public interface IUploadServcie
	{
        Task<string> PostImage(UploadFileModel request);
        Task<AuthResponseModel> GenerateJSONWebToken();
    }

    public class UploadServcie : IUploadServcie
    {
        private readonly ConstantConfig _constantConfig;
        private readonly Jwt _jwtSetting;
        public UploadServcie(IConfiguration configuration)
		{
            _constantConfig = configuration.GetSection("ConstantConfig").Get<ConstantConfig>();
            _jwtSetting = configuration.GetSection("Jwt").Get<Jwt>();

        }

        public async Task<string> PostImage(UploadFileModel request)
        {
            string name = $"{request.Name}_{DateTime.Now.ToString("ddMMyyyyHHmmmi")}";
            string trimRequest = request.Image.Trim();
            byte[] bytes = Convert.FromBase64String(trimRequest);
            string path = $@"{_constantConfig.Path}{request.Folder}\\{name}.{request.Type}";
            using (FileStream SourceStream = File.Open(path, FileMode.OpenOrCreate))
            {
                SourceStream.Seek(0, SeekOrigin.End);
                await SourceStream.WriteAsync(bytes, 0, bytes.Length);
            }
            return await Task.FromResult($"{name}.{request.Type}");
        }

        public async Task<AuthResponseModel> GenerateJSONWebToken()
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var _expired = DateTime.Now.AddMinutes(1);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Thananun Saelim"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                  _jwtSetting.Issuer,
                  _jwtSetting.Issuer,
                  authClaims,
                  expires: _expired,
                  signingCredentials: credentials);

                return new AuthResponseModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expired = _expired
                };
            }
            catch (Exception ex)
            {
                return null;
            }
          
        }

    }
}

