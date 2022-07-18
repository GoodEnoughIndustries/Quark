using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quark
{
    public static class QuarkTargetBuilderExtensions
    {
        public static IEnumerable<IQuarkTargetGroup> WithTag(this IEnumerable<IQuarkTargetGroup> targetGroups, object tag)
        {
            return targetGroups.Select(tg => tg.WithTag(tag));
        }

        public static IEnumerable<IQuarkTargetGroup> WithTags(this IEnumerable<IQuarkTargetGroup> targetGroups, params object[] tags)
        {
            return targetGroups.Select(tg => tg.WithTags(tags));
        }

        public static IQuarkTargetBuilder WithTask(
            this IQuarkTargetBuilder builder,
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

        public static IQuarkTargetBuilder WithVariable(this IQuarkTargetBuilder builder, string key, string value) => builder;

        public static IQuarkTargetBuilder WithJinja2Template(this IQuarkTargetBuilder builder, string localPath, string remotePath) => builder;
    }
}
