using System;
using Microsoft.Win32;

namespace SweepingBlade.WindowsRegistry.Win32;

public sealed class Win32RegistryHiveFactory : IRegistryHiveFactory
{
    private readonly IRegistry _registry;

    internal Win32RegistryHiveFactory(IRegistry registry)
    {
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
    }

    public IRegistryHive OpenBaseKey(RegistryHive registryHive)
    {
        return _registry.OpenBaseKey(registryHive);
    }

    public IRegistryHive OpenBaseKey(RegistryHive registryHive, RegistryView registryView)
    {
        var registryKey = RegistryKey.OpenBaseKey(registryHive.ToWin32(), registryView.ToWin32());
        return new Win32RegistryHive(_registry, registryKey, registryKey.Name);
    }

    public IRegistryHive OpenRemoteBaseKey(RegistryHive registryHive, string machineName)
    {
        var registryKey = RegistryKey.OpenRemoteBaseKey(registryHive.ToWin32(), machineName);
        return new Win32RegistryHive(_registry, registryKey, registryKey.Name);
    }

    public IRegistryHive OpenRemoteBaseKey(RegistryHive registryHive, string machineName, RegistryView registryView)
    {
        var registryKey = RegistryKey.OpenRemoteBaseKey(registryHive.ToWin32(), machineName, registryView.ToWin32());
        return new Win32RegistryHive(_registry, registryKey, registryKey.Name);
    }
}