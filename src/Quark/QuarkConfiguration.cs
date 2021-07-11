using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Quark
{
    public class QuarkConfiguration : IQuarkConfiguration
    {
        public List<string> TargetNames { get; } = new();
        public List<DirectoryInfo> FileLocations { get; } = new();
        public List<IQuarkTask> QuarkTasks { get; } = new();

        public IQuarkTargetGroup Targets { get; } = new QuarkTargetGroup();

        public QuarkConfiguration(QuarkConfigurationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            this.TargetNames = builder.TargetNames;
            this.FileLocations = builder.FileLocations;
            this.QuarkTasks = builder.QuarkTasks;
        }

        public QuarkResult Run(CancellationToken token = default) => throw new NotImplementedException();
    }
}
