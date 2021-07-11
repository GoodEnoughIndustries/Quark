using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quark
{
    public static class QuarkConfigurationBuilderExtensions
    {
        public static QuarkConfigurationBuilder WithTargets(
            this QuarkConfigurationBuilder builder,
            IEnumerable<IQuarkTargetGroup> targets)
            => throw new Exception();

        public static QuarkConfigurationBuilder WithTargets(
            this QuarkConfigurationBuilder builder,
            string pattern,
            params string[] tags)
            => throw new Exception();

        public static QuarkConfigurationBuilder WithTargets(
            this QuarkConfigurationBuilder builder,
            string filePath)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("message", nameof(filePath));
            }

            builder.AddTarget(filePath);

            return builder;
        }

        public static QuarkConfigurationBuilder WithTarget(
            this QuarkConfigurationBuilder builder,
            string target)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrWhiteSpace(target))
            {
                throw new ArgumentException("message", nameof(target));
            }

            builder.AddTarget(target);

            return builder;
        }

        public static QuarkConfigurationBuilder WithVariable(this QuarkConfigurationBuilder builder, string key, string value) => builder;

        public static QuarkConfigurationBuilder WithJinja2Template(this QuarkConfigurationBuilder builder, string localPath, string remotePath) => builder;

        public static QuarkConfigurationBuilder WithQuarkFiles(
            this QuarkConfigurationBuilder builder,
            string path)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("message", nameof(path));
            }

            builder.AddFileLocation(path);

            return builder;
        }

        public static QuarkConfigurationBuilder WithTask(
            this QuarkConfigurationBuilder builder,
            IQuarkTask task)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            builder.AddQuarkTask(task);

            return builder;
        }
    }
}
