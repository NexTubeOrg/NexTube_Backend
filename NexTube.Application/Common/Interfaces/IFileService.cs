using NexTube.Application.Common.Models;

namespace NexTube.Application.Common.Interfaces {
    public interface IFileService {
        Task<(Result Result, string? FileId)> UploadFileAsync(string bucket, Stream source);
        Task<(Result Result, Stream FileStream, string ContentType)> GetFileStreamAsync(string bucket, string fileId);
    }
}
