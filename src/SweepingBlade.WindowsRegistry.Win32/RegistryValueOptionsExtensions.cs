namespace SweepingBlade.WindowsRegistry.Win32;

internal static class RegistryValueOptionsExtensions
{
    public static Microsoft.Win32.RegistryValueOptions ToWin32(this RegistryValueOptions options)
    {
        var result = Microsoft.Win32.RegistryValueOptions.None;

        if (options.HasFlag(RegistryValueOptions.DoNotExpandEnvironmentNames))
        {
            result |= Microsoft.Win32.RegistryValueOptions.DoNotExpandEnvironmentNames;
        }

        return result;
    }
}