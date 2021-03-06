﻿using System.Threading;
using System.Threading.Tasks;
using Knapcode.ExplorePackages.Logic;
using McMaster.Extensions.CommandLineUtils;

namespace Knapcode.ExplorePackages.Tool.Commands
{
    public class DependencyPackagesToDatabaseCommand : ICommand
    {
        private readonly DependencyPackagesToDatabaseCommitCollector _collector;

        public DependencyPackagesToDatabaseCommand(
            DependencyPackagesToDatabaseCommitCollector collector)
        {
            _collector = collector;
        }

        public void Configure(CommandLineApplication app)
        {
        }

        public async Task ExecuteAsync(CancellationToken token)
        {
            await _collector.ProcessAsync(ProcessMode.Sequentially, token);
        }

        public bool IsDatabaseRequired()
        {
            return true;
        }
    }
}
