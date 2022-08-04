using Microsoft.Extensions.DependencyInjection;
using Quark.Abstractions;

namespace Quark.Linux;

public static class QuarkRunnerBuilderExtensions
{
    public static QuarkRunnerBuilder ManageLinux(this QuarkRunnerBuilder builder)
    {
        builder.ConfigureServices((c, sc) =>
        {
            sc.AddSingleton<IQuarkSecurityProvider, LinuxSecurityProvider>();
        });

        return builder;
    }
}
