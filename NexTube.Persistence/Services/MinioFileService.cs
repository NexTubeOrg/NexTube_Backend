using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using Minio;
using Minio.DataModel.Args;

namespace NexTube.Persistence.Services {
    public class MinioFileService : IFileService {
        private readonly IMinioClient minioClient;

        public MinioFileService(IMinioClient minioClient) {
            this.minioClient = minioClient;
        }

        public async Task<(Result Result, string Url)> GetFileUrlAsync(string bucket, string fileId, string contentType)
        {
            var argsGetUrl = new PresignedGetObjectArgs()
                .WithBucket(bucket)
                .WithObject(fileId)
                .WithHeaders(new Dictionary<string, string> {
                    { "response-content-type", contentType },
                } )
                .WithExpiry(60 * 60);
            var url = await minioClient.PresignedGetObjectAsync(argsGetUrl);
            return (Result.Success(), url);
        }

        public async Task<(Result Result, string? FileId)> UploadFileAsync(string bucket, Stream source) {
            return await UploadFileAsync(bucket, source, Guid.NewGuid().ToString());
        }

        public async Task<(Result Result, string? FileId)> UploadFileAsync(string bucket, Stream source, string filename) {
            var putObjArgs = new PutObjectArgs()
                .WithBucket(bucket)
                .WithObject(filename)
                .WithObjectSize(source.Length)
                .WithStreamData(source);

            var obj = await minioClient.PutObjectAsync(putObjArgs);

            return (Result.Success(), obj.ObjectName);
        }
    }
}
