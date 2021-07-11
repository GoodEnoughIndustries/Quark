using System;

namespace Quark.System
{
    [Flags]
    public enum SystemdServiceOptions
    {
        DaemonReload = 1 << 0,
        Enabled = 1 << 1,
        Disabled = 1 << 2,
        Restarted = 1 << 3,
    }
}