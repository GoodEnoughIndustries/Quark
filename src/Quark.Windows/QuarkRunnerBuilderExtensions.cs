using Microsoft.Extensions.DependencyInjection;
using Quark.Abstractions;
using Quark.Windows;

namespace Quark;

public static class QuarkRunnerBuilderExtensions
{
    public static QuarkRunnerBuilder ManageWindows(this QuarkRunnerBuilder builder)
    {
        builder.ConfigureServices((c, sc) =>
        {
            sc.AddSingleton<IQuarkSecurityProvider, WindowsSecurityProvider>();
            sc.AddSingleton<IQuarkFactProvider, WindowsWMIFactsProvider>();
        });

        return builder;
    }
}
