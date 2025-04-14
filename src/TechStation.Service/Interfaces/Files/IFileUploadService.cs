using TechStation.Service.DTOs.FileUpload;

namespace TechStation.Service.Interfaces.Files;

public interface IFileUploadService
{
    public Task<bool> FileDeleteAsync(string filePath);
    public Task<FileUploadForResultDto> FileUploadAsync(FileUploadForCreationDto dto);
}
