using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quark;

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

    public static IQuarkTargetBuilder WithVariable(this IQuarkTargetBuilder builder, string key, string value) => builder;
}
