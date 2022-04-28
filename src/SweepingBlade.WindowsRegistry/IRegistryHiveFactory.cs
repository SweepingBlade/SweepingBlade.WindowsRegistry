namespace SweepingBlade.WindowsRegistry;

public interface IRegistryHiveFactory
{
    IRegistryHive OpenBaseKey(RegistryHive registryHive);
    IRegistryHive OpenBaseKey(RegistryHive registryHive, RegistryView registryView);
    IRegistryHive OpenRemoteBaseKey(RegistryHive registryHive, string machineName);
    IRegistryHive OpenRemoteBaseKey(RegistryHive registryHive, string machineName, RegistryView registryView);
}