using NexTube.Application.Common.Models;

namespace NexTube.Application.Common.Interfaces {
    public interface IFileService {
        Task<(Result Result, string? FileId)> UploadFileAsync(string bucket, Stream source);
        Task<(Result Result, string Url)> GetFileUrlAsync(string bucket, string fileId, string contentType);
    }
}
