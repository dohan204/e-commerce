using application.interfaces;
using Microsoft.AspNetCore.Http;

namespace infrastructure.repositories
{
    public class UploadRepository : IUploadRepository
    {
        public UploadRepository(){}
        public async Task Upload(IFormFile file)
        {
            
        }
    }
}