using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Imaging;
using WWW.Domain.Entity;
using Microsoft.Extensions.Logging;
using WWW.Service.Interfaces;
using static System.Net.Mime.MediaTypeNames;
using NuGet.Protocol.Core.Types;
using WWW.Domain.Api;
using WWW.DAL.Repositories;
using WWW.DAL.Interfaces;

namespace WWW.Controllers
{
    public class HelpController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ILogger<HelpController> _logger;  
        private readonly DownloadService _downloadService;
        private readonly IUserRepository _accountRepository;
        public HelpController(IArticleService articleService, ILogger<HelpController> logger, DownloadService downloadService, IUserRepository accountRepository)
        {
            _articleService = articleService;
            _logger = logger;
            _downloadService = downloadService;
            _accountRepository = accountRepository;
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
            var db = (await _articleService.GetById(id)).Data.Picture;
            if (db == null)
            {
                string fileWay = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "base-article-photo.jpg");
                var noPhoto = System.IO.File.ReadAllBytes(fileWay);
                return File(noPhoto, "image/jpeg");
            }
            var db_image = db.picture;
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

            

            return File(db_image, contentType);

        }



        public async Task<IActionResult> GetAvatarById(int id)
        {
            var user = await _accountRepository.GetALL().FirstAsync(u => u.Id == id);
            string contentType = "image/jpeg";
            var pict = user.Avatar;
            if(pict == null)
            {
                    string fileway = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "base-avatar.jpg");
                    pict = System.IO.File.ReadAllBytes(fileway);
            }
            return File(pict, contentType);
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
