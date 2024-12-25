# MAPI Methods
MAPILib provides two types of methods for most operations. 

## MAPIResult Methods
Methods that return an MAPIResult are useful for debugging or if you're concerned with specific errors.
```C#
MAPIResult Connect(string host, int port = 7887)
MAPIResult GetCurrentProcessId(out uint? processId)
MAPIResult GetProcessIds(out uint[]? processIds)
MAPIResult GetMemory(uint processId, uint address, uint size, out byte[]? buffer)
MAPIResult SetMemory(uint processId, uint address, byte[] buffer)
MAPIResult Syscall(uint number, object[] args, out ulong? result)
MAPIResult GetFirmwareVersion(out string? version)
MAPIResult GetTemperature(out int cpu, out int rsx)
```

## Convenience Methods
If you're less concerned with error types, you can instead use convenience methods that return values directly.
```C#
uint? GetCurrentProcessId()
uint[]? GetProcessIds()
byte[]? GetMemory(uint processId, uint address, uint size)
ulong? Syscall(uint number, object[] args)
string? GetFirmwareVersion()
(int cpu, int rsx) GetTemperature()
```

Some methods like `void Disconnect()` and `MAPIResult SetMemory(uint processId, uint address, byte[] buffer)` do not have MAPIResult / convenience counterparts.