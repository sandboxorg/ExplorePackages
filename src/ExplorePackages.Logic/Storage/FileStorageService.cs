﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Knapcode.ExplorePackages.Logic
{
    public class FileStorageService : IFileStorageService
    {
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30);

        private readonly PackageBlobNameProvider _blobNameProvider;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<FileStorageService> _logger;

        public FileStorageService(
            PackageBlobNameProvider blobNameProvider,
            IBlobStorageService blobStorageService,
            IMemoryCache memoryCache,
            ILogger<FileStorageService> logger)
        {
            _blobNameProvider = blobNameProvider;
            _blobStorageService = blobStorageService;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task StoreStreamAsync(string id, string version, FileArtifactType type, Func<Stream, Task> writeAsync)
        {
            using (var memoryStream = new MemoryStream())
            {
                await writeAsync(memoryStream);

                var blobName = _blobNameProvider.GetLatestBlobName(id, version, type);
                var contentType = GetContentType(type);
                memoryStream.Position = 0;

                var cacheKey = GetCacheKey(id, version, type);
                try
                {
                    await _blobStorageService.UploadStreamAsync(blobName, contentType, memoryStream);
                }
                catch
                {
                    // Clear the cache since we don't know what happened.
                    _memoryCache.Remove(cacheKey);
                    throw;
                }

                CacheMemoryStream(cacheKey, memoryStream);
            }
        }

        public async Task<Stream> GetStreamOrNullAsync(string id, string version, FileArtifactType type)
        {
            var cacheKey = GetCacheKey(id, version, type);
            if (_memoryCache.TryGetValue<byte[]>(cacheKey, out var cachedValue))
            {
                return new MemoryStream(cachedValue);
            }

            var blobName = _blobNameProvider.GetLatestBlobName(id, version, type);
            var outputStream = new MemoryStream();
            using (var blobStream = await _blobStorageService.GetStreamOrNullAsync(blobName))
            {
                if (blobStream == null)
                {
                    return null;
                }

                await blobStream.CopyToAsync(outputStream);
            }

            CacheMemoryStream(cacheKey, outputStream);

            outputStream.Position = 0;
            return outputStream;
        }

        private void CacheMemoryStream(string cacheKey, MemoryStream memoryStream)
        {
            var cacheValue = memoryStream.ToArray();
            _memoryCache.Set(
                cacheKey,
                cacheValue,
                new MemoryCacheEntryOptions
                {
                    Size = cacheValue.Length,
                    SlidingExpiration = CacheDuration,
                });
        }

        private static string GetCacheKey(string id, string version, FileArtifactType type)
        {
            return $"{id}/{version}/{type}".ToLowerInvariant();
        }

        private static string GetContentType(FileArtifactType type)
        {
            string contentType;
            switch (type)
            {
                case FileArtifactType.Nuspec:
                    contentType = "application/xml";
                    break;
                case FileArtifactType.MZip:
                    contentType = "application/octet-stream";
                    break;
                default:
                    throw new NotSupportedException($"The file artifact type {type} is not supported.");
            }

            return contentType;
        }
    }
}
