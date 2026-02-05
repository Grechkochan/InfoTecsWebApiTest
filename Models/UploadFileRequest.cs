using Microsoft.AspNetCore.Http;

namespace InfotecsTestWebApi.Models;

public class UploadFileRequest
{
    public IFormFile File { get; set; } = null!;
}