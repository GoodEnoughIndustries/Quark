using Quark.Abstractions;
using System.Runtime.InteropServices;
using Mono.Unix.Native;

namespace Quark.Linux;

public class LinuxSecurityProvider : IQuarkSecurityProvider
{
    public bool IsAdministrator()
        => RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
            && Syscall.geteuid() == 0;
}
