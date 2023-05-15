using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Imaging;
using WWW.Domain.Entity;
using Microsoft.Extensions.Logging;
using WWW.Service.Interfaces;
using static System.Net.Mime.MediaTypeNames;


namespace WWW.Controllers
{
    public class HelpController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ILogger<HelpController> _logger;  
        private readonly DownloadService _downloadService;
        public HelpController(IArticleService articleService, ILogger<HelpController> logger, DownloadService downloadService)
        {
            _articleService = articleService;
            _logger = logger;
            _downloadService = downloadService;
        }

        public async Task<IActionResult> GetImageByUrl(string url)
        {
            try
            {
                Picture data = (await _downloadService.DownloadJpgPictAsync(url));
                //byte[] image_arrow = data.picture;
                //string contentType;
                //using (var ms = new MemoryStream(image_arrow)) { 
                //    using (var img = System.Drawing.Image.FromStream(ms))
                //        {
                //            contentType = GetContentType(img.ToString());
                //        }
                //}
                return File(data.picture, data.Type);
            }
            catch {
                return NotFound();
            }
        }

        public async Task<IActionResult> GetImageById(int id)
        {
            var db_image = (await _articleService.GetById(id)).Data.Picture.picture;

            string contentType = "";
            using (var ms = new MemoryStream(db_image))
            {
                try
                {
                    using (var img = System.Drawing.Image.FromStream(ms))
                    {
                        contentType = GetContentType(img.RawFormat.ToString());
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            if (db_image == null)
            {
                return NotFound();
            }

            return File(db_image, contentType);

        }

        public static string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".gif":
                    return "image/gif";
                case ".png":
                    return "image/png";
                case ".bmp":
                    return "image/bmp";
                case ".svg":
                    return "image/svg+xml";
                case ".pdf":
                    return "application/pdf";
                case ".doc":
                case ".docx":
                    return "application/msword";
                case ".xls":
                case ".xlsx":
                    return "application/vnd.ms-excel";
                case ".ppt":
                case ".pptx":
                    return "application/vnd.ms-powerpoint";
                case ".mp3":
                    return "audio/mpeg";
                case ".wav":
                    return "audio/wav";
                case ".mp4":
                    return "video/mp4";
                case ".avi":
                    return "video/x-msvideo";
                default:
                    return "application/octet-stream";
            }
        }

    }
}
