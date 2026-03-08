namespace domain.interfaces
{
    // lưu ảnh và trả về đường dẫn tương đối
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(Stream fileStream, string fileName);
        Task DeleteFileAsync(string filePath);
    }
}