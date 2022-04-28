using System;

namespace SweepingBlade.WindowsRegistry.Win32;

internal static class RegistryKeyPermissionCheckExtensions
{
    public static Microsoft.Win32.RegistryKeyPermissionCheck ToWin32(this RegistryKeyPermissionCheck permissionCheck)
    {
        return permissionCheck switch
        {
            RegistryKeyPermissionCheck.Default => Microsoft.Win32.RegistryKeyPermissionCheck.Default,
            RegistryKeyPermissionCheck.ReadSubTree => Microsoft.Win32.RegistryKeyPermissionCheck.ReadSubTree,
            RegistryKeyPermissionCheck.ReadWriteSubTree => Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree,
            _ => throw new ArgumentOutOfRangeException(nameof(permissionCheck), permissionCheck, null)
        };
    }
}