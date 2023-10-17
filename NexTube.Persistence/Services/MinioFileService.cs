using Microsoft.Extensions.Configuration;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minio;
using Minio.DataModel.Args;

namespace NexTube.Persistence.Services {
    public class MinioFileService : IFileService {
        private readonly IMinioClient minioClient;

        public MinioFileService(IMinioClient minioClient) {
            this.minioClient = minioClient;
        }
        public async Task<(Result Result, Stream FileStream)> GetFileStreamAsync(string bucket, string fileId) {
            Stream? stream = null;
            var args_get = new GetObjectArgs()
                .WithBucket(bucket)
                .WithObject(fileId)
                .WithCallbackStream(s=> {
                    var ms = new MemoryStream();
                    s.CopyTo(ms);
                    stream = ms;
                });

            var res = await minioClient.GetObjectAsync(args_get);

            return (Result.Success(), stream);
        }

        public async Task<(Result Result, string? FileId)> UploadFileAsync(string bucket, Stream source) {
            throw new NotImplementedException();
        }
    }
}
