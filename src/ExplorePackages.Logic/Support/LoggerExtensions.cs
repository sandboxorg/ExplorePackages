﻿namespace Knapcode.ExplorePackages.Logic
{
    public static class LoggerExtensions
    {
        public static NuGet.Common.ILogger ToNuGetLogger(this Microsoft.Extensions.Logging.ILogger logger)
        {
            return new StandardToNuGetLogger(logger);
        }
    }
}
