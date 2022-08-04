using Serilog;
using Quark.Systems;

namespace Quark;

public static class SerilogExtensions
{
    public static LoggerConfiguration DestructureQuark(this LoggerConfiguration configuration)
    {
        configuration.Destructure
            .ByTransforming<PackageDescription>(pd => new
            {
                InstallPath = pd.GetInstallerPath(),
            });
        return configuration;
    }
}
