using Quark.Abstractions;
using System.Management;
using System.Runtime.InteropServices;

namespace Quark.Windows;

public class WindowsWMIFactsProvider : IQuarkFactProvider
{
    private readonly Dictionary<string, string> _properties = new()
    {
        { "TotalVirtualMemorySize", "wmi.os.total_virtual_mem"  },
        { "FreePhysicalMemory",     "wmi.os.free_physical_mem"  },
        { "FreeVirtualMemory",      "wmi.os.free_virtual_mem"   },
        { "TotalVisibleMemorySize", "wmi.os.total_mem"          },
        { "WindowsDirectory",       "wmi.os.windows_dir"        },
        { "BuildNumber",            "wmi.os.build"              },
        { "Version",                "wmi.os.version"            },
    };

    public async Task<IQuarkResult> ExecuteAsync(IQuarkExecutionContext context, IQuarkTarget target)
    {
        QuarkRunResult result = QuarkRunResult.Unknown;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            && target.TargetType == QuarkTargetTypes.Windows)
        {
            result = await GatherWindowsFacts(target)
                ? QuarkRunResult.Success
                : QuarkRunResult.Fail;
        }
        else
        {
            result = QuarkRunResult.NotImplemented;
        }

        // Even if task says it succeeded for some reason,
        // no facts were stored, fail this.
        // If this is intentional, store a single value in there.
        if (!target.Facts.Any())
        {
            result = QuarkRunResult.Fail;
        }

        return new QuarkResult
        {
            QuarkRunResult = result,
            QuarkTarget = target,
        };
    }

    private async Task<bool> GatherWindowsFacts(IQuarkTarget target)
    {
        // TODO: All facts are copied from https://github.com/cupboard-project/cupboard/tree/main/src/Cupboard.Providers.Windows
        // Rewrite to fit into Quark style.

        var wmiResult = await GatherWMIFacts(target);

        var isSandboxUser = Environment.UserName.Equals("WDAGUtilityAccount", StringComparison.OrdinalIgnoreCase);
        target.Facts.Add("windows.sandbox", isSandboxUser);

        return wmiResult;
    }

    private Task<bool> GatherWMIFacts(IQuarkTarget target)
    {
        if (!OperatingSystem.IsWindows())
        {
            return Task.FromResult(false);
        }

        var keys = string.Join(',', _properties.Keys);
        var searcher = new ManagementObjectSearcher($"SELECT {keys} FROM Win32_OperatingSystem");
        var osDetailsCollection = searcher.Get();
        foreach (var prop in osDetailsCollection)
        {
            foreach (var property in _properties)
            {
                target.Facts.Add(property.Value, prop[property.Key]);
            }
        }

        if (osDetailsCollection.Count > 0)
        {
            target.Facts.Add("wmi.os", true);
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}
