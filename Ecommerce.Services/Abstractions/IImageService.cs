using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Abstractions
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
