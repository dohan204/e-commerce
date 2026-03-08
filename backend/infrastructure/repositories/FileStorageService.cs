using domain.interfaces;
using Microsoft.AspNetCore.Hosting;
namespace infrastructure.repositories
{

    public class FileStorageService : IFileStorageService
    {
        private readonly string _filePaht;
        public FileStorageService(IWebHostEnvironment web)
        {
            _filePaht = Path.Combine(web.WebRootPath, "images");
            Directory.CreateDirectory(_filePaht);
        }

        public async Task<string> SaveFileAsync(Stream streamFile, string fileName)
        {
            // Lấy phần mở rộng của file
            var extension = Path.GetExtension(fileName);

            // tạo tên unique 
            var uniqueName = $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(_filePaht, uniqueName);

            // ghi file vào ổ đĩa
            using var output = new FileStream(filePath, FileMode.Create);
            await streamFile.CopyToAsync(output);

            // trả về đường dẫn tương đối để lưu db
            return $"images/{uniqueName}";
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            if(File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}