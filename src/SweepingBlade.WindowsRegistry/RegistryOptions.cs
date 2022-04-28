using System;

namespace SweepingBlade.WindowsRegistry;

[Flags]
public enum RegistryOptions
{
    None = 0x0,
    Volatile = 0x1
}