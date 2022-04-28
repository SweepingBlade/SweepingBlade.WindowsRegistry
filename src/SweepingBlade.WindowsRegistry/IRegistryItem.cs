namespace SweepingBlade.WindowsRegistry;

public interface IRegistryItem : IRegistryVisitable
{
    string Name { get; }
}