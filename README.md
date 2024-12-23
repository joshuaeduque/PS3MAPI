# PS3MAPI
PS3MAPI is a repository for the MAPILib C# library and some sample applications.

## MAPILib
MAPILib is an open source implementation of the PS3 Manager API as a C# library. It functions by communicating with the internal MAPI server provided by webMAN MOD which comes stock with nearly every modern custom firmware. This means that users do not have to install or configure additional software to make use of tools developed with MAPILib.

### MAPI Methods
```C#
// Connect to a console
MAPIResult Connect(string host, int port = 7887)

// Get the current process id
MAPIResult GetCurrentProcessId(out uint? processId)
uint? GetCurrentProcessId()

// Get all process ids
MAPIResult GetProcessIds(out uint[]? processIds)
uint[]? GetProcessIds()

// Get process memory
MAPIResult GetMemory(uint processId, uint address, uint size, out byte[]? buffer)
byte[]? GetMemory(uint processId, uint address, uint size);

// Set process memory
MAPIResult SetMemory(uint processId, uint address, byte[] buffer)

// Execute a syscall
MAPIResult Syscall(uint number, object[] args, out ulong? result)
ulong? Syscall(uint number, object[] args)

// Get the console's firmware version
MAPIResult GetFirmwareVersion(out string? version)
string? GetFirmwareVersion()

// Get the console's CPU and RSX temperature
MAPIResult GetTemperature(out int cpu, out int rsx)
(int cpu, int rsx) GetTemperature()

// Disconnect a console
void Disconnect();
```

### Usage Examples
Here's an example of creating an MAPI object and connecting it to a console via IP address:
```C#
// Get the ip address of the console you'd like to connect to
string ipAddress = "192.168.1.13";

// Create an MAPI object
MAPI api = new MAPI();

// Most MAPI methods will return an MAPIResult
// An MAPIResult other than OK will detail a specific error

// Try connecting to the console
MAPIResult connectResult = api.Connect(ipAddress);

if(connectResult != MAPIResult.OK) {
    Console.WriteLine($"Failed to connect to {ipAddress}!");
    return;
}

Console.WriteLine($"Connected to {ipAddress}!");
```

Here's how to read and write memory to a process:
```C#
// Most MAPI methods also have convenient alternatives
// for when you are only concerned with success / failure

// The alternative method to get an MAPIResult would be:
// MAPIResult GetCurrentProcessId(out uint? pid)

// Get the current process id
uint? pid = api.GetCurrentProcessId();

if(pid == null) {
    Console.WriteLine("Failed to get current process ID!");
    return;
}

// Read two bytes from address 0x10000 in the current process
uint address = 0x10000;
uint size = 0x02;

byte[]? memory = api.GetMemory(pid, address, size);
if(memory == null) {
    Console.WriteLine("Failed to get memory!");
    return;
}

...

// Write two bytes from address 0x10000 to the current process
byte[] buffer = new byte[] { 0xFA, 0xCE };
MAPIResullt writeResult = api.SetMemory(pid, address, buffer);
if(writeResult != MAPIResult.OK) {
    Console.WriteLine("Failed to write memory!");
    return;
}

```