namespace SweepingBlade.WindowsRegistry;

public interface IRegistryKeyWatcherFactory
{
    IRegistryKeyWatcher CreateNew(RegistryHive registryHive);
    IRegistryKeyWatcher CreateNew(RegistryHive registryHive, string subKey);
    IRegistryKeyWatcher CreateNew(RegistryHive registryHive, RegistryView registryView, string subKey);
}