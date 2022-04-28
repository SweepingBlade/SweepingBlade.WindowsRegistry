namespace SweepingBlade.WindowsRegistry;

public enum RegistryHive : uint
{
    ClassesRoot = 0x80000000,
    CurrentUser = 0x80000001,
    LocalMachine = 0x80000002,
    Users = 0x80000003,
    PerformanceData = 0x80000004,
    CurrentConfig = 0x80000005
}