﻿namespace Knapcode.ExplorePackages.Logic
{
    public class PackagesContainerConsistencyReport : IConsistencyReport
    {
        public PackagesContainerConsistencyReport(
            bool isConsistent,
            BlobMetadata packageContentMetadata)
        {
            IsConsistent = isConsistent;
            PackageContentMetadata = packageContentMetadata;
        }

        public bool IsConsistent { get; }
        public BlobMetadata PackageContentMetadata { get; }
    }
}
