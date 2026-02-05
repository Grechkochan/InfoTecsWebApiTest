using Microsoft.AspNetCore.Mvc;
using InfotecsTestWebApi.Services;
using InfotecsTestWebApi.Models;

namespace InfotecsTestWebApi.Controllers;

[ApiController]
[Route("api/files")]
public class FilesController : ControllerBase
{
    private readonly FileProcessingService _fileProcessingService;

    public FilesController(FileProcessingService fileProcessingService)
    {
        _fileProcessingService = fileProcessingService;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload([FromForm] UploadFileRequest request)
    {
        var file = request.File;

        if (file == null || file.Length == 0)
            return BadRequest("Файл не передан");

        if (!Path.GetExtension(file.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            return BadRequest("Допускаются только CSV файлы");

        var tempFilePath = Path.GetTempFileName();

        try
        {
            using (var stream = new FileStream(tempFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var resultMessage = _fileProcessingService.ProcessFile(
                tempFilePath,
                file.FileName
            );

            return Ok(new
            {
                Message = resultMessage
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Error = ex.Message
            });
        }
        finally
        {
            if (System.IO.File.Exists(tempFilePath))
                System.IO.File.Delete(tempFilePath);
        }
    }
}
