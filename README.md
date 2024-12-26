# PS3MAPI
PS3MAPI is a repository for the MAPILib C# library and some documentation / sample applications.

## MAPILib
MAPILib is an open source implementation of the PlayStation 3 Manager API as a C# library. It functions by communicating over a network with the internal MAPI server provided by webMAN MOD which comes stock with nearly every modern custom firmware. This means that users do not have to install or configure additional software to make use of tools developed with MAPILib.

**Library Features**
* Read / write process memory
* Get process info.
* Execute syscalls
* Read CPU / RSX temperature
* Get firmware version

### Usage Example
Here's a quick example showing how easy it is to develop applications using MAPILib. In this example, we'll connect to a console and read some memory from the currently running process.
```C#
// Get the ip address of the console you'd like to connect to
string ipAddress = "192.168.1.13";

// Create an MAPI object 
MAPI api = new MAPI();

// Connect the api to the console with the ip address
api.Connect(ipAddress);

// Get the current process id
uint? pid = api.GetCurrentProcessId();

// Read four bytes from the current process
uint address = 0x10000;
uint size = 4;

byte[]? memory = api.GetMemory(pid, address, size);
```

### Documentation
If you'd like to learn more about using MAPILib, you can find some documentation here:

* [MAPI Methods](/docs/METHODS.md)
* [Usage Examples](/docs/EXAMPLES.md)