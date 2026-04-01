using domain.interfaces;
using Microsoft.AspNetCore.Hosting;
namespace infrastructure.repositories
{

    public class FileStorageService : IFileStorageService
    {
        private readonly string _rootPath;
        public FileStorageService(IWebHostEnvironment web)
        {
            // Dùng ContentRootPath cho an toàn, hoặc check null WebRootPath
            var baseRoot = web.WebRootPath ?? web.ContentRootPath;
            _rootPath = Path.Combine(baseRoot, "uploads");

            // Đừng quên tạo thư mục nếu nó chưa tồn tại nhé
            if (!Directory.Exists(_rootPath))
            {
                Directory.CreateDirectory(_rootPath);
            }
        }

        public async Task<string> SaveFileAsync(Stream streamFile, string fileName)
        {
            if (streamFile == null) throw new ArgumentNullException(nameof(streamFile));
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
            if (string.IsNullOrEmpty(_rootPath)) throw new Exception("Root path is not initialized!");

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
            var filePath = Path.Combine(_rootPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await streamFile.CopyToAsync(fileStream);
            }

            return $"/uploads/{uniqueFileName}";
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var actualFileName = Path.GetFileName(fileName);
            var fullPath = Path.Combine(_rootPath, actualFileName);

            if (File.Exists(fullPath))
            {
                await Task.Run(() => File.Delete(fullPath));
            }
        }
    }
}