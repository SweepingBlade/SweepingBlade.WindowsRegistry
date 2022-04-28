using System;

namespace SweepingBlade.WindowsRegistry.Win32;

internal static class RegistryViewExtensions
{
    public static Microsoft.Win32.RegistryView ToWin32(this RegistryView registryView)
    {
        return registryView switch
        {
            RegistryView.Default => Microsoft.Win32.RegistryView.Default,
            RegistryView.Registry64 => Microsoft.Win32.RegistryView.Registry64,
            RegistryView.Registry32 => Microsoft.Win32.RegistryView.Registry32,
            _ => throw new ArgumentOutOfRangeException(nameof(registryView), registryView, null)
        };
    }
}