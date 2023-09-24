using Quark.Abstractions;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Quark.Systems;

public class MachineFactsProvider : IQuarkFactProvider
{
    public Task<IQuarkResult> ExecuteAsync(IQuarkExecutionContext context, IQuarkTarget target)
    {
        const string ComputerName = "computer.name";
        const string MachineName = "machine.name";
        const string OSArchitecture = "os.arch";
        const string OSPlatform = "os.platform";
        const string UserName = "user.name";

        target.Facts.Add(OSArchitecture, RuntimeInformation.OSArchitecture);
        target.Facts.Add(OSPlatform, Environment.OSVersion.Platform);
        target.Facts.Add(ComputerName, Environment.MachineName);
        target.Facts.Add(MachineName, Environment.MachineName);
        target.Facts.Add(UserName, Environment.UserName);

        return Task.FromResult((IQuarkResult)new QuarkResult
        {
            QuarkRunResult = QuarkRunResult.Success,
            QuarkTarget = target,
        });
    }
}
