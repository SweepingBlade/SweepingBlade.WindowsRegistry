namespace SweepingBlade.WindowsRegistry;

public interface IRegistryVisitable
{
    void Accept(IRegistryVisitor registryVisitor);
}