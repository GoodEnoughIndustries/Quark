using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Quark
{
    public class QuarkConfigurationBuilder : QuarkTargetBuilder
    {
        public List<DirectoryInfo> FileLocations { get; } = new List<DirectoryInfo>();

        public IQuarkConfiguration Build() => new QuarkConfiguration(this);

        public QuarkConfigurationBuilder AddTargetGroups(IEnumerable<IQuarkTargetGroup> targetGroups)
        {
            this.TargetGroups.AddRange(targetGroups);

            return this;
        }

        public QuarkConfigurationBuilder AddTargetGroup(IQuarkTargetGroup targetGroup)
        {
            this.TargetGroups.Add(targetGroup);

            return this;
        }

        public QuarkConfigurationBuilder AddFileLocation(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                throw new ArgumentNullException(nameof(folderPath));
            }

            this.FileLocations.Add(new DirectoryInfo(folderPath));

            return this;
        }
    }
}
