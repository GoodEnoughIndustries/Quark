using Quark.Abstractions;

namespace Quark;

public static class QuarkTargetBuilderExtensions
{
    public static IQuarkTargetBuilder ElevateAs(this IQuarkTargetBuilder builder, IQuarkCredential credential)
    {
        builder.Credentials.Add(credential);

        return builder;
    }
}
