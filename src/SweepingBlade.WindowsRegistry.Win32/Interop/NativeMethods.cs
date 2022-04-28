using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace SweepingBlade.WindowsRegistry.Win32.Interop;

internal static class NativeMethods
{
    private const string DllName = "advapi32.dll";

    [DllImport(DllName, SetLastError = true)]
    private static extern int RegCloseKey(IntPtr hKey);

    [DllImport(DllName, SetLastError = true)]
    private static extern int RegNotifyChangeKeyValue(IntPtr hKey, bool bWatchSubtree, NotifyFilters dwNotifyFilters, IntPtr hEvent, bool fAsynchronous);

    [DllImport(DllName, SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern int RegOpenKeyEx(IntPtr hKey, string subKey, REG_OPTION options, int samDesired, out IntPtr phkResult);

    public static int RegisterKeyChangeListener(SafeHandle registryHiveHandle, bool includeSubTree, NotifyFilters notifyFilters, SafeWaitHandle eventHandle)
    {
        var registryHiveHandleAddRef = false;
        var eventHandleAddRef = false;
        try
        {
            IntPtr registryKeyHandleLocal;
            if (registryHiveHandle is not null)
            {
                registryHiveHandle.DangerousAddRef(ref registryHiveHandleAddRef);
                registryKeyHandleLocal = registryHiveHandle.DangerousGetHandle();
            }
            else
            {
                registryKeyHandleLocal = default;
            }

            IntPtr eventHandleLocal;
            if (eventHandle is not null)
            {
                eventHandle.DangerousAddRef(ref eventHandleAddRef);
                eventHandleLocal = eventHandle.DangerousGetHandle();
            }
            else
            {
                eventHandleLocal = default;
            }

            var result = RegNotifyChangeKeyValue(registryKeyHandleLocal, includeSubTree, notifyFilters, eventHandleLocal, true);
            return result;
        }
        finally
        {
            if (registryHiveHandleAddRef)
            {
                registryHiveHandle.DangerousRelease();
            }

            if (eventHandleAddRef)
            {
                eventHandle.DangerousRelease();
            }
        }
    }

    public static int RegistryCloseKey(SafeHandle registryHiveHandle)
    {
        var registryHiveHandleAddRef = false;
        try
        {
            IntPtr registryKeyHandleLocal;
            if (registryHiveHandle is not null)
            {
                registryHiveHandle.DangerousAddRef(ref registryHiveHandleAddRef);
                registryKeyHandleLocal = registryHiveHandle.DangerousGetHandle();
            }
            else
            {
                registryKeyHandleLocal = default;
            }

            var result = RegCloseKey(registryKeyHandleLocal);
            return result;
        }
        finally
        {
            if (registryHiveHandleAddRef)
            {
                registryHiveHandle.DangerousRelease();
            }
        }
    }

    public static int RegistryOpenKey(SafeHandle registryHiveHandle, string subKey, REG_OPTION options, int samDesired, out SafeHandle safeHandle)
    {
        var registryHiveHandleAddRef = false;
        try
        {
            IntPtr registryKeyHandleLocal;
            if (registryHiveHandle is not null)
            {
                registryHiveHandle.DangerousAddRef(ref registryHiveHandleAddRef);
                registryKeyHandleLocal = registryHiveHandle.DangerousGetHandle();
            }
            else
            {
                registryKeyHandleLocal = default;
            }

            var result = RegOpenKeyEx(registryKeyHandleLocal, subKey, options, samDesired, out var handle);
            safeHandle = new SafeFileHandle(handle, true);
            return result;
        }
        finally
        {
            if (registryHiveHandleAddRef)
            {
                registryHiveHandle.DangerousRelease();
            }
        }
    }
}