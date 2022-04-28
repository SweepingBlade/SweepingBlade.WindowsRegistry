using System;
using System.IO;

namespace SweepingBlade.WindowsRegistry;

public interface IRegistryKeyWatcher
{
    event EventHandler Changed;
    event EventHandler<ErrorEventArgs> Error;
    bool EnableRaisingEvents { get; set; }
    bool IncludeSubTree { get; set; }
    NotifyFilters NotifyFilters { get; set; }
}