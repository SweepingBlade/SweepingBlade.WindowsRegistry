using System;

namespace SweepingBlade.WindowsRegistry.Win32;

internal static class RegistryHiveExtensions
{
    public static Microsoft.Win32.RegistryHive ToWin32(this RegistryHive registryHive)
    {
        return registryHive switch
        {
            RegistryHive.ClassesRoot => Microsoft.Win32.RegistryHive.ClassesRoot,
            RegistryHive.CurrentUser => Microsoft.Win32.RegistryHive.CurrentUser,
            RegistryHive.LocalMachine => Microsoft.Win32.RegistryHive.LocalMachine,
            RegistryHive.Users => Microsoft.Win32.RegistryHive.Users,
            RegistryHive.PerformanceData => Microsoft.Win32.RegistryHive.PerformanceData,
            RegistryHive.CurrentConfig => Microsoft.Win32.RegistryHive.CurrentConfig,
            _ => throw new ArgumentOutOfRangeException(nameof(registryHive), registryHive, null)
        };
    }
}