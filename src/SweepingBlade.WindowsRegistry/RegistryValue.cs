namespace SweepingBlade.WindowsRegistry;

public interface IRegistryValue : IRegistryItem
{
    object Data { get; }
}