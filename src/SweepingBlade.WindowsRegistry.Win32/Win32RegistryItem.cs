using System;

namespace SweepingBlade.WindowsRegistry.Win32;

public abstract class Win32RegistryItem : IRegistryItem
{
    protected internal Win32RegistryItem(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public string Name { get; }

    public abstract void Accept(IRegistryVisitor registryVisitor);
}