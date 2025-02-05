## About PS3MAPI
PS3MAPI is a Visual Studio project containing MAPILib, an open source implementation of the PlayStation 3 Manager API in .NET C#.

By communicating with a console's internal MAPI server with C# sockets, MAPILib is able to send commands and parse their responses to manipulate a console over a network connection. 

As the MAPI server is provided by webMAN MOD which comes stock with every modern custom firmware, users and developers do not have to configure additional software to make use of MAPILib's functionality.

### Built With

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white) ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white) ![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white)

### Features

* Read / write process memory
* Execute syscalls
* Fetch process information
* Fetch CPU / RSX temperatures
* Fetch firmware versions

## Getting Started

### Preqrequisites

PS3MAPI requires you to have Visual Studio installed on your machine.

### Building

The recommend way to build PS3MAPI is to simply open the solution in Visual Studio and build for `Debug` or `Release`.

However, it is possible to build the project from a `Developer Command Prompt` like so.

1. Clone the repository.

```bash
git clone https://github.com/joshuaeduque/PS3MAPI.git PS3MAPI
```

2. Navigate to the cloned project directory.

```bash
cd PS3MAPI
```

3. Build the solution using `msbuild`

```bash
msbuild
```

## Usage

Building the solution should produce an `MAPILib.dll` file that you can include as a dependency in your C# projects.

Here is a simple example showing how to connect to a console and read some memory from a process:

```C#
// Get the address of the console you'd like to connect to
string ipAddress = "192.168.1.13";

// Create an MAPI object 
MAPI api = new MAPI();

// Connect to the console
api.Connect(ipAddress);

// Get the id of the currently running process
uint? pid = api.GetCurrentProcessId();

// Determine the address and number of bytes to read 
uint address = 0x10000;
uint size = 4;

// Read four bytes from the process
byte[]? memory = api.GetMemory(pid, address, size);
```

### Documentation

If you would like to learn more about using MAPILib, you can find some documentation here:

[MAPI Methods](/docs/METHODS.md)

[Usage Examples](/docs/EXAMPLES.md)

## License

Distrubuted under the GNU General Public License v3.0. See `LICENSE` for more information.

## Contact

Joshua Duque - joshuaeduque@gmail.com