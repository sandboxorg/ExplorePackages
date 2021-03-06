﻿using Knapcode.ExplorePackages.Entities;
using Microsoft.Extensions.Logging;

namespace Knapcode.ExplorePackages.Logic
{
    public class DependencyPackagesToDatabaseCommitCollector : CommitCollector<PackageRegistrationEntity, PackageDependencyEntity>
    {
        public DependencyPackagesToDatabaseCommitCollector(
            CursorService cursorService,
            ICommitEnumerator<PackageRegistrationEntity> enumerator,
            DependencyPackagesToDatabaseCommitProcessor processor,
            ILogger<DependencyPackagesToDatabaseCommitCollector> logger) : base(cursorService, enumerator, processor, logger)
        {
        }
    }
}
