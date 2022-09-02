using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensensionTypeProvider;
        private readonly ILogger<FilesController> _logger;

        public FilesController(FileExtensionContentTypeProvider fileExtensensionTypeProvider, ILogger<FilesController> logger)
        {
            _fileExtensensionTypeProvider = fileExtensensionTypeProvider;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("fileId")]
        public ActionResult GetFile(string fileId)
        {
            var pathToFile = "Files/Recap.pdf";

            if (!System.IO.File.Exists(pathToFile))
            {
                _logger.LogInformation($"File not found", DateTime.UtcNow.ToLongTimeString());
                return NotFound();


            }

            if (!_fileExtensensionTypeProvider.TryGetContentType(pathToFile, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, contentType, Path.GetFileName(pathToFile));
        }
    }
}
