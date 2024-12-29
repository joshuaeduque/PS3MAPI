# Usage Examples

Here's a few simple examples showing how you may want to use MAPILib.

* [Connect to a console](#connect-to-a-console)
* [Read / write memory](#read--write-memory)
* [Execute a syscall](#execute-a-syscall)
* [Get firmware version and temperature](#get-firmware-version-and-temperature)

## Connect to a console
```C#
// Get the ip address of the console you'd like to connect to
string ipAddress = "192.168.1.13";

// Create an MAPI object
MAPI api = new MAPI();

// Try connecting to the console
MAPIResult connectRes = api.Connect(ipAddress);

// Check the result of the connect call
if(connectRes != MAPIResult.OK) {
    // If the MAPIResult returned by Connect() is not OK, connection must have failed
    Console.WriteLine($"Failed to connect to {ipAddress}");
    return;
}

Console.WriteLine($"Connected to {ipAddress}");
```

## Read / write memory

```C#
// Connect to a console
...

// Try getting the current process id
uint? pid = api.GetCurrentProcessId();

// Check the result of the get process id call
if(pid == null) {
    // If the returned process id is null, the call must have failed
    // Note that it is nullable because a process id can actually be zero
    Console.WriteLine("Failed to get the current process ID");
    return;
}

Console.WriteLine($"Got current process ID {pid}");

// Read four bytes of memory from the process
uint address = 0x10000;
uint size = 4;

// Try reading from the process
byte[]? readBuf = api.GetMemory(pid, address, size);

// Check the result of the read call
if(readBuf == null) {
    // If the read buffer is null, the call must have failed
    Console.WriteLine("Failed to read memory");
    return;
}

Console.WriteLine($"Read {readBuf.Length} bytes of memory");

// Write four bytes of memory to the process
byte[] writeBuf = new byte[] { 0xDE, 0xAD, 0xBE, 0xEF }; // 🐄 -> 🍔 😔

// Try writing to the process
MAPIResult writeRes = api.SetMemory(pid, address, writeBuf);

// Check the result of the write call
if(writeRes != MAPIResult.OK) {
    // If the call result is not OK, it must have failed
    Console.WriteLine("Failed to write memory");
    return;
}

Console.WriteLine("Wrote to memory");

// We're done!
```

## Execute a syscall

```C#
// Connect to a console
...

// Execute a syscall to make the console's power button blink green
uint syscall = 386; // sys_sm_control_led
uint ledColor = 1;  // Green
uint ledMode = 2;   // Blink fast

object[] syscallArgs = new object[] { ledColor, ledMode };

// Try executing the syscall
uint? syscallRes = api.Syscall(syscall, syscallArgs);

// Check the result of the syscall call
if(syscallRes == null) {
    Console.WriteLine("Failed to execute syscall");
    return;
}

Console.WriteLine("Executed syscall");
```

## Get firmware version and temperature

```C#
// Connect to a console
...

// Try getting the console's firmware version
string? firmwareVer = api.GetFirmwareVersion();

// Check the result of the call
if(firmwareVer == null) {
    Console.WriteLine("Failed to get firmware version");
    return;
}

Console.WriteLine($"Got firmware version {firmwareVer}");

// Try getting the console's temperature
MAPIResult temperatureRes = api.GetTemperature(out int? cpu, out int? rsx);

// Check the result of the call
if(temperatureRes != MAPIResult.OK) {
    Console.WriteLine("Failed to get temperature");
    return;
}

Console.WriteLine($"Console temperature cpu {cpu} rsx {rsx}");
```