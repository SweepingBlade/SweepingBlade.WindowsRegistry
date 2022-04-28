using Microsoft.Win32;

namespace SweepingBlade.WindowsRegistry.Win32;

public class Win32RegistryKey : Win32RegistryKeyItem, IRegistryKey
{
    internal Win32RegistryKey(IRegistry registry, RegistryKey registryKey, string name)
        : base(registry, registryKey, name)
    {
    }
}