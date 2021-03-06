﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Knapcode.ExplorePackages.Entities;
using Knapcode.MiniZip;

namespace Knapcode.ExplorePackages.Logic
{
    public class MZipToDatabaseCommitProcessor : ICommitProcessor<PackageEntity, PackageArchiveMetadata>
    {
        private readonly MZipStore _mZipStore;
        private readonly IPackageService _packageService;

        public MZipToDatabaseCommitProcessor(
            MZipStore mZipStore,
            IPackageService packageService)
        {
            _mZipStore = mZipStore;
            _packageService = packageService;
        }

        public string CursorName => CursorNames.MZipToDatabase;

        public IReadOnlyList<string> DependencyCursorNames { get; } = new[]
        {
            CursorNames.CatalogToDatabase,
            CursorNames.MZip,
        };

        public int BatchSize => 5000;

        public async Task<ItemBatch<PackageArchiveMetadata>> InitializeItemsAsync(
            IReadOnlyList<PackageEntity> packages,
            int skip,
            CancellationToken token)
        {
            var output = new List<PackageArchiveMetadata>();

            foreach (var package in packages)
            {
                var item = await InitializeItemAsync(package, token);
                if (item == null)
                {
                    continue;
                }

                output.Add(item);
            }

            return new ItemBatch<PackageArchiveMetadata>(output, hasMoreItems: false);
        }

        private async Task<PackageArchiveMetadata> InitializeItemAsync(PackageEntity package, CancellationToken token)
        {
            // Read the .zip directory.
            ZipDirectory zipDirectory;
            long size;
            using (var stream = await _mZipStore.GetMZipStreamOrNullAsync(
                package.PackageRegistration.Id,
                package.Version,
                token))
            {
                if (stream == null)
                {
                    if (!package.CatalogPackage.Deleted)
                    {
                        throw new InvalidOperationException($"Could not find .mzip for {package.PackageRegistration.Id} {package.Version}.");
                    }

                    return null;
                }

                using (var reader = new ZipDirectoryReader(stream))
                {
                    zipDirectory = await reader.ReadAsync();
                    size = stream.Length;
                }
            }

            // Gather the metadata.
            var metadata = new PackageArchiveMetadata
            {
                Id = package.PackageRegistration.Id,
                Version = package.Version,
                Size = size,
                ZipDirectory = zipDirectory,
            };

            return metadata;
        }

        public async Task ProcessBatchAsync(IReadOnlyList<PackageArchiveMetadata> batch)
        {
            await _packageService.AddOrUpdatePackagesAsync(batch);
        }
    }
}
