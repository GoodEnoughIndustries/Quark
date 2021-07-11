using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Quark
{
    public class QuarkConfigurationBuilder
    {
        public List<string> TargetNames { get; } = new List<string>();

        public List<DirectoryInfo> FileLocations { get; } = new List<DirectoryInfo>();
        public List<IQuarkTask> QuarkTasks { get; } = new List<IQuarkTask>();

        public IQuarkConfiguration Build() => new QuarkConfiguration(this);

        public QuarkConfigurationBuilder AddTarget(string target)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                throw new ArgumentNullException(nameof(target));
            }

            this.TargetNames.Add(target);

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

        public QuarkConfigurationBuilder AddQuarkTask(IQuarkTask task)
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            this.QuarkTasks.Add(task);

            return this;
        }

        public QuarkConfigurationBuilder ConfigureTelegraf(List<IQuarkTargetGroup> elasticSearchNodes) => throw new NotImplementedException();
        public QuarkConfigurationBuilder ConfigureSnmp(List<IQuarkTargetGroup> elasticSearchNodes) => throw new NotImplementedException();
    }
}
