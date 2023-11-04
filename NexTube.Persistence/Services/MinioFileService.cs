﻿using Microsoft.Extensions.Configuration;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minio;
using Minio.DataModel.Args;
using Microsoft.AspNetCore.Http.HttpResults;

namespace NexTube.Persistence.Services {
    public class MinioFileService : IFileService {
        private readonly IMinioClient minioClient;

        public MinioFileService(IMinioClient minioClient) {
            this.minioClient = minioClient;
        }

        public async Task<(Result Result, string Url)> GetFileUrlAsync(string bucket, string fileId)
        {
            var argsGetUrl = new PresignedGetObjectArgs()
                .WithBucket(bucket)
                .WithObject(fileId)
                .WithExpiry(60 * 60);

            var url = await minioClient.PresignedGetObjectAsync(argsGetUrl);
            return (Result.Success(), url);
        }

        public async Task<(Result Result, string? FileId)> UploadFileAsync(string bucket, Stream source) {
            var putObjArgs = new PutObjectArgs()
                .WithBucket(bucket)
                .WithObject(Guid.NewGuid().ToString())
                .WithObjectSize(source.Length)
                .WithStreamData(source);

            var obj = await minioClient.PutObjectAsync(putObjArgs);
            
            return (Result.Success(), obj.ObjectName);
        }
    }
}
