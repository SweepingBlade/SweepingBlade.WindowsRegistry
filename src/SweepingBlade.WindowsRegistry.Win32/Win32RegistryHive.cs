using Microsoft.Win32;

namespace SweepingBlade.WindowsRegistry.Win32;

public sealed class Win32RegistryHive : Win32RegistryKeyItem, IRegistryHive
{
    internal Win32RegistryHive(IRegistry registry, RegistryKey registryKey, string name)
        : base(registry, registryKey, name)
    {
    }
}