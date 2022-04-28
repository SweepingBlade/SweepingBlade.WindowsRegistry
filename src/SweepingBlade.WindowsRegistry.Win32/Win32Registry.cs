using System.Collections.Generic;
using Microsoft.Win32;

namespace SweepingBlade.WindowsRegistry.Win32;

public sealed class Win32Registry : IRegistry
{
    private IRegistryHive _classesRoot;
    private IRegistryHive _currentConfig;
    private IRegistryHive _currentUser;
    private IRegistryHive _localMachine;
    private IRegistryHive _performanceData;
    private IRegistryHiveFactory _registryHive;
    private IRegistryKeyWatcherFactory _registryKeyWatcher;
    private IRegistryHive _users;

    public IRegistryHive ClassesRoot => _classesRoot ??= new Win32RegistryHive(this, Registry.ClassesRoot, Registry.ClassesRoot.Name);
    public IRegistryHive CurrentConfig => _currentConfig ??= new Win32RegistryHive(this, Registry.CurrentConfig, Registry.CurrentConfig.Name);
    public IRegistryHive CurrentUser => _currentUser ??= new Win32RegistryHive(this, Registry.CurrentUser, Registry.CurrentUser.Name);
    public IRegistryHive LocalMachine => _localMachine ??= new Win32RegistryHive(this, Registry.LocalMachine, Registry.LocalMachine.Name);
    public IRegistryHive PerformanceData => _performanceData ??= new Win32RegistryHive(this, Registry.PerformanceData, Registry.PerformanceData.Name);
    public IRegistryHiveFactory RegistryHive => _registryHive ??= new Win32RegistryHiveFactory(this);
    public IRegistryKeyWatcherFactory RegistryKeyWatcher => _registryKeyWatcher ??= new Win32RegistryKeyWatcherFactory(RegistryHive);
    public IRegistryHive Users => _users ??= new Win32RegistryHive(this, Registry.Users, Registry.Users.Name);

    public IEnumerable<IRegistryHive> GetHives()
    {
        yield return ClassesRoot;
        yield return CurrentUser;
        yield return LocalMachine;
        yield return Users;
        yield return CurrentConfig;
        yield return PerformanceData;
    }

    public void Accept(IRegistryVisitor registryVisitor)
    {
        if (!registryVisitor.VisitEnterRegistry(this)) return;
        foreach (var registryHive in GetHives())
        {
            if (!registryVisitor.VisitEnterRegistryHive(registryHive)) return;
            registryVisitor.VisitRegistryHive(registryHive);
            registryVisitor.VisitLeaveRegistryHive(registryHive);
        }

        registryVisitor.VisitLeaveRegistry(this);
    }
}