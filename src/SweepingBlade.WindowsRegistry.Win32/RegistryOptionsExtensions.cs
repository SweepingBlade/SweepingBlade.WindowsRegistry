namespace SweepingBlade.WindowsRegistry.Win32;

internal static class RegistryOptionsExtensions
{
    public static Microsoft.Win32.RegistryOptions ToWin32(this RegistryOptions options)
    {
        var result = Microsoft.Win32.RegistryOptions.None;

        if (options.HasFlag(RegistryOptions.Volatile))
        {
            result |= Microsoft.Win32.RegistryOptions.Volatile;
        }

        return result;
    }
}