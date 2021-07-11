using System;

namespace Quark.Abstractions
{
    [Flags]
    public enum QuarkTargetTypes
    {
        Unknown = 0,
        Windows = 1 << 0,
        Linux = 1 << 1,
    }
}