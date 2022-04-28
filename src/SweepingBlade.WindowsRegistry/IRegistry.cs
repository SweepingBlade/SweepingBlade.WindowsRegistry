using System.Collections.Generic;

namespace SweepingBlade.WindowsRegistry;

public interface IRegistry : IRegistryVisitable
{
    IEnumerable<IRegistryHive> GetHives();
    IRegistryHive ClassesRoot { get; }
    IRegistryHive CurrentConfig { get; }
    IRegistryHive CurrentUser { get; }
    IRegistryHive LocalMachine { get; }
    IRegistryHive PerformanceData { get; }
    IRegistryHiveFactory RegistryHive { get; }
    IRegistryKeyWatcherFactory RegistryKeyWatcher { get; }
    IRegistryHive Users { get; }
}