using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Quark
{
    public class QuarkConfiguration : IQuarkConfiguration
    {
        public List<DirectoryInfo> FileLocations { get; } = new();
        public List<IQuarkTask> QuarkTasks { get; } = new();
        public List<IQuarkTargetGroup> TargetGroups { get; init; } = new List<IQuarkTargetGroup>();

        public QuarkConfiguration(QuarkConfigurationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            this.FileLocations = builder.FileLocations;
            this.QuarkTasks = builder.QuarkTasks;
            this.TargetGroups = builder.TargetGroups;
        }

        public QuarkResult Run(CancellationToken token)
        {
            return new QuarkResult();
        }
    }
}
