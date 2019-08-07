using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ImageResizeWebApp.Models;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using ImageResizeWebApp.Helpers;

namespace ImageResizeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        // make sure that appsettings.json is filled with the necessary details of the azure storage
        private readonly AzureStorageConfig _storageConfig = null;

        public ImagesController(IOptions<AzureStorageConfig> config)
        {
            _storageConfig = config.Value;
        }


        public IActionResult ValidateUpload(IFormFile files)
        {
            if (files == null)
                return BadRequest("No files received from the upload");
            if (_storageConfig.AccountKey == string.Empty || _storageConfig.AccountName == string.Empty)
                return BadRequest(
                    "sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");
            if (_storageConfig.ImageContainer == string.Empty)
                return BadRequest("Please provide a name for your image container in the azure blob storage");
            if (!StorageHelper.IsImage(files))
                return new UnsupportedMediaTypeResult();
            return null;
        }

        // POST /api/images/upload
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            var file = files.FirstOrDefault();
            var validated = ValidateUpload(file);
            if (validated != null) return validated;
            try
            {
                StorageUri isUploaded;
                using (Stream stream = file.OpenReadStream())
                {
                    isUploaded =
                        await StorageHelper.UploadFileToStorage(stream, file.FileName, _storageConfig);
                }
                if (isUploaded == null) return BadRequest("Looks like the image couldnt upload to the storage");
                return Json(isUploaded.PrimaryUri);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}