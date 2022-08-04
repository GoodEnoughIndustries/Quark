using Quark.Abstractions;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Quark.Windows;

public class WindowsSecurityProvider : IQuarkSecurityProvider
{
    public bool IsAdministrator()
        => RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            && new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
}
