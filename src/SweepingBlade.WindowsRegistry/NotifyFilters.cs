using System;

namespace SweepingBlade.WindowsRegistry;

[Flags]
public enum NotifyFilters
{
    Key = 0x1,
    Attribute = 0x2,
    Value = 0x4,
    Security = 0x8
}