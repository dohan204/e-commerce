using Microsoft.AspNetCore.Http;

namespace application.interfaces
{
    public interface IUploadRepository
    {
        Task Upload(IFormFile formfile);
    }
}