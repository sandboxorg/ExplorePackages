﻿using System.Threading.Tasks;

namespace Knapcode.ExplorePackages.Logic
{
    public class HasCrossCheckDiscrepancyPackageQuery : IPackageQuery
    {
        private readonly CrossCheckConsistencyService _service;

        public HasCrossCheckDiscrepancyPackageQuery(CrossCheckConsistencyService service)
        {
            _service = service;
        }

        public string Name => PackageQueryNames.HasCrossCheckDiscrepancyPackageQuery;
        public string CursorName => CursorNames.HasCrossCheckDiscrepancyPackageQuery;

        public async Task<bool> IsMatchAsync(PackageQueryContext context, PackageConsistencyState state)
        {
            var isConsistent = await _service.IsConsistentAsync(context, state, NullProgressReporter.Instance);
            return !isConsistent;
        }
    }
}
