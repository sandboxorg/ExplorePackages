﻿using System.Linq;
using System.Xml.Linq;

namespace Knapcode.ExplorePackages.Logic
{
    public class FindCaseInsensitiveDuplicateMetadataElementsNuspecQuery : INuspecQuery
    {
        public string Name => PackageQueryNames.FindCaseInsensitiveDuplicateMetadataElementsNuspecQuery;
        public string CursorName => CursorNames.FindCaseInsensitiveDuplicateMetadataElementsNuspecQuery;

        public bool IsMatch(XDocument nuspec)
        {
            return NuspecUtility
                .GetDuplicateMetadataElements(nuspec, caseSensitive: false)
                .Any();
        }
    }
}
