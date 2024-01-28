using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileUploadService.Models;
using FileUploadService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadService.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class UploadFileController : Controller
    {
        private readonly IUploadServcie _uploadServcie;
        private readonly IRabitMQProducer _rabitMQProducer;
        public UploadFileController(IUploadServcie uploadServcie, IRabitMQProducer rabitMQProducer)
        {
            _uploadServcie = uploadServcie;
            _rabitMQProducer = rabitMQProducer;
        }

        [Authorize]
        [HttpPost("uploadFile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseResult))]
        public async Task<IActionResult> UploadImage([FromBody] UploadFileModel request)
        {
            try
            {

                string result = string.Empty;
                if (ValidateHelper.ValidateWithOutNull(request.Folder) && ValidateHelper.ValidateWithOutNull(request.Name) && ValidateHelper.ValidateWithOutNull(request.Type)
                    && ValidateHelper.ValidateWithOutNull(request.Image))
                {
                    var upload = await _uploadServcie.PostImage(request);
                    string uploadSuccess = $"Upload File {upload} is complete";
                    _rabitMQProducer.SendProductMessage(uploadSuccess);
                    result = uploadSuccess;
                }
                return Ok(new ResponseResult
                {
                    status = true,
                    statusCode = StatusCodes.Status200OK,
                    message = "Success",
                    data = result,
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseResult
                {
                    status = true,
                    statusCode = StatusCodes.Status500InternalServerError,
                    message = ex.Message.ToString(),
                    data = string.Empty,
                });
            }
        }

        [AllowAnonymous]
        [HttpGet("GeneratToken")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseResult))]
        public async Task<IActionResult> GeneratToken()
        {
            try
            {
                var generateToken = await _uploadServcie.GenerateJSONWebToken();
                return Ok(new ResponseResult
                {
                    status = true,
                    statusCode = StatusCodes.Status200OK,
                    message = "Success",
                    data = generateToken
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseResult
                {
                    status = true,
                    statusCode = StatusCodes.Status500InternalServerError,
                    message = ex.Message.ToString(),
                    data = string.Empty,
                });
            }
        }

    }
}

