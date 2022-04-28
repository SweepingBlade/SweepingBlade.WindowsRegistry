using System;

namespace SweepingBlade.WindowsRegistry.Win32;

public sealed class Win32RegistryKeyWatcherFactory : IRegistryKeyWatcherFactory
{
    private readonly IRegistryHiveFactory _registryHiveFactory;

    public Win32RegistryKeyWatcherFactory(IRegistryHiveFactory registryHiveFactory)
    {
        _registryHiveFactory = registryHiveFactory ?? throw new ArgumentNullException(nameof(registryHiveFactory));
    }

    public IRegistryKeyWatcher CreateNew(RegistryHive registryHive)
    {
        return new Win32RegistryKeyWatcher(_registryHiveFactory.OpenBaseKey(registryHive));
    }

    public IRegistryKeyWatcher CreateNew(RegistryHive registryHive, string subKey)
    {
        return new Win32RegistryKeyWatcher(_registryHiveFactory.OpenBaseKey(registryHive), subKey);
    }

    public IRegistryKeyWatcher CreateNew(RegistryHive registryHive, RegistryView registryView, string subKey)
    {
        return new Win32RegistryKeyWatcher(_registryHiveFactory.OpenBaseKey(registryHive, registryView), subKey);
    }
}