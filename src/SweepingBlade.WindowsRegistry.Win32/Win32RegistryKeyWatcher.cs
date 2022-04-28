using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using SweepingBlade.WindowsRegistry.Win32.Interop;

namespace SweepingBlade.WindowsRegistry.Win32;

public sealed class Win32RegistryKeyWatcher : IRegistryKeyWatcher, IDisposable
{
    public event EventHandler Changed;
    public event EventHandler<ErrorEventArgs> Error;

    private readonly ManualResetEvent _eventTerminate = new ManualResetEvent(false);
    private readonly IRegistryHive _registryHive;
    private readonly string _subKey;
    private readonly object _threadLock = new object();
    private bool _disposed;
    private bool _enabled;
    private bool _includeSubtree;

    private NotifyFilters _notifyFilters = NotifyFilters.Key | NotifyFilters.Attribute |
                                           NotifyFilters.Value | NotifyFilters.Security;

    private Thread _thread;

    public bool IsMonitoring => _thread is not null;

    internal Win32RegistryKeyWatcher(IRegistryHive registryHive, string subKey = null)
    {
        _registryHive = registryHive ?? throw new ArgumentNullException(nameof(registryHive));
        _subKey = subKey;
    }

    ~Win32RegistryKeyWatcher()
    {
        Dispose();
    }

    public void Dispose()
    {
        StopRaisingEvents();
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    public bool EnableRaisingEvents
    {
        get => _enabled;
        set
        {
            if (_enabled == value) return;

            _enabled = value;

            if (_enabled)
            {
                StartRaisingEvents();
            }
            else
            {
                StopRaisingEvents();
            }
        }
    }

    public bool IncludeSubTree
    {
        get => _includeSubtree;
        set
        {
            lock (_threadLock)
            {
                if (IsMonitoring)
                {
                    throw new InvalidOperationException("Monitoring thread is already running");
                }

                _includeSubtree = value;
            }
        }
    }

    public NotifyFilters NotifyFilters
    {
        get => _notifyFilters;
        set
        {
            lock (_threadLock)
            {
                if (IsMonitoring)
                {
                    throw new InvalidOperationException("Monitoring thread is already running");
                }

                _notifyFilters = value;
            }
        }
    }

    private void MonitorThread()
    {
        try
        {
            ThreadLoop();
        }
        catch (Exception e)
        {
            OnError(e);
        }

        _thread = null;
    }

    private void OnChanged()
    {
        Changed?.Invoke(this, EventArgs.Empty);
    }

    private void OnError(Exception exception)
    {
        Error?.Invoke(this, new ErrorEventArgs(exception));
    }

    private void StartRaisingEvents()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(null, "This instance is already disposed");
        }

        lock (_threadLock)
        {
            if (IsMonitoring) return;

            _eventTerminate.Reset();
            _thread = new Thread(MonitorThread);
            _thread.IsBackground = true;
            _thread.Start();
        }
    }

    private void StopRaisingEvents()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(null, "This instance is already disposed");
        }

        lock (_threadLock)
        {
            if (!IsMonitoring) return;

            _eventTerminate.Set();
            _thread.Join();
        }
    }

    private void ThreadLoop()
    {
        var result = NativeMethods.RegistryOpenKey(_registryHive.Handle, _subKey, REG_OPTION.OPEN_LINK, SECURITY_AND_ACCESS_RIGHTS_MASK, out var registryKeyHandle);
        if (result != 0)
        {
            throw new Win32Exception(result);
        }

        try
        {
            var eventNotify = new AutoResetEvent(false);
            var waitHandles = new WaitHandle[] { eventNotify, _eventTerminate };
            while (!_eventTerminate.WaitOne(0, true))
            {
                result = NativeMethods.RegisterKeyChangeListener(registryKeyHandle, true, _notifyFilters, eventNotify.SafeWaitHandle);
                if (result != 0)
                {
                    throw new Win32Exception(result);
                }

                if (WaitHandle.WaitAny(waitHandles) == 0)
                {
                    OnChanged();
                }
            }
        }
        finally
        {
            if (!registryKeyHandle.IsClosed)
            {
                var closeResult = NativeMethods.RegistryCloseKey(registryKeyHandle);

                if (closeResult != 0)
                {
                    throw new Win32Exception(closeResult);
                }
            }
        }
    }

    // ReSharper disable InconsistentNaming
    private const int KEY_NOTIFY = 0x0010;
    private const int KEY_QUERY_VALUE = 0x0001;
    private const int SECURITY_AND_ACCESS_RIGHTS_MASK = STANDARD_RIGHTS_READ | KEY_QUERY_VALUE | KEY_NOTIFY;
    private const int STANDARD_RIGHTS_READ = 0x00020000;
    // ReSharper restore InconsistentNaming
}