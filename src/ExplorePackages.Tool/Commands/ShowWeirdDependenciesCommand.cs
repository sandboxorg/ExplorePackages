﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Knapcode.ExplorePackages.Entities;
using Knapcode.ExplorePackages.Logic;
using McMaster.Extensions.CommandLineUtils;

namespace Knapcode.ExplorePackages.Tool.Commands
{
    public class ShowWeirdDependenciesCommand : ICommand
    {
        private readonly PackageQueryService _queryService;
        private readonly NuspecStore _nuspecStore;

        public ShowWeirdDependenciesCommand(
            PackageQueryService queryService,
            NuspecStore nuspecStore)
        {
            _queryService = queryService;
            _nuspecStore = nuspecStore;
        }

        public void Configure(CommandLineApplication app)
        {
        }

        public async Task ExecuteAsync(CancellationToken token)
        {
            await ShowMatchedStringCounts(
                PackageQueryNames.FindMixedDependencyGroupStylesNuspecQuery,
                (package, nuspec) =>
                {
                    if (NuspecUtility.HasMixedDependencyGroupStyles(nuspec))
                    {
                        return new[] { package.Identity };
                    }
                    else
                    {
                        return new string[0];
                    }
                });

            await ShowMatchedStringCounts(
                PackageQueryNames.FindInvalidDependencyVersionsNuspecQuery,
                (package, nuspec) => NuspecUtility.GetInvalidDependencyVersions(nuspec));

            await ShowMatchedStringCounts(
                PackageQueryNames.FindWhitespaceDependencyTargetFrameworkNuspecQuery,
                (package, nuspec) => NuspecUtility.GetWhitespaceDependencyTargetFrameworks(nuspec));

            await ShowMatchedStringCounts(
                PackageQueryNames.FindInvalidDependencyTargetFrameworkNuspecQuery,
                (package, nuspec) => NuspecUtility.GetInvalidDependencyTargetFrameworks(nuspec));

            await ShowMatchedStringCounts(
                PackageQueryNames.FindInvalidDependencyIdNuspecQuery,
                (package, nuspec) => NuspecUtility.GetInvalidDependencyIds(nuspec));

            await ShowMatchedStringCounts(
                PackageQueryNames.FindUnsupportedDependencyTargetFrameworkNuspecQuery,
                (package, nuspec) => NuspecUtility.GetUnsupportedDependencyTargetFrameworks(nuspec));

            await ShowMatchedStringCounts(
                PackageQueryNames.FindDuplicateDependencyTargetFrameworksNuspecQuery,
                (package, nuspec) => NuspecUtility
                    .GetDuplicateDependencyTargetFrameworks(nuspec)
                    .Select(x => x.Key));

            await ShowMatchedStringCounts(
                PackageQueryNames.FindDuplicateNormalizedDependencyTargetFrameworksNuspecQuery,
                (package, nuspec) => NuspecUtility
                    .GetDuplicateNormalizedDependencyTargetFrameworks(nuspec)
                    .Select(x => x.Key.ToString()));
        }

        private async Task ShowMatchedStringCounts(string queryName, Func<PackageEntity, XDocument, IEnumerable<string>> getStrings)
        {
            var matches = new Dictionary<string, int>();
            await ProcessMatchedNuspecsAsync(
                queryName,
                (package, nuspec) =>
                {
                    foreach (var value in getStrings(package, nuspec))
                    {
                        var fixedValue = value ?? "(null)";
                        if (!matches.ContainsKey(fixedValue))
                        {
                            matches[fixedValue] = 1;
                        }
                        else
                        {
                            matches[fixedValue]++;
                        }
                    }
                });
            Console.WriteLine(queryName);
            if (matches.Any())
            {
                var maxWidth = matches.Values.Max(x => x.ToString().Length);
                foreach (var pair in matches.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine($"  {pair.Value.ToString().PadLeft(maxWidth)} {pair.Key}");
                }
            }
            Console.WriteLine();
        }

        private async Task ProcessMatchedNuspecsAsync(string queryName, Action<PackageEntity, XDocument> processNuspec)
        {
            int count;
            long lastKey = 0;
            do
            {
                var matches = await _queryService.GetMatchedPackagesAsync(queryName, lastKey);
                count = matches.Packages.Count;
                lastKey = matches.LastKey;

                foreach (var match in matches.Packages)
                {
                    var nuspecContext = await _nuspecStore.GetNuspecContextAsync(match.Id, match.Version);
                    processNuspec(match, nuspecContext.Document);
                }
            }
            while (count > 0);
        }

        public bool IsDatabaseRequired()
        {
            return true;
        }
    }
}
