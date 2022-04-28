using System;

namespace SweepingBlade.WindowsRegistry;

[Flags]
public enum RegistryValueOptions
{
    None = 0x0,
    DoNotExpandEnvironmentNames = 0x1
}