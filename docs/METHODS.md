# MAPILib Methods

Created by [mddox](https://github.com/loxsmoke/mddox) on 12/28/2024

MAPILib provides two types of methods for most operations:

1. Methods that return an `MAPIResult`.
2. Methods that return values directly for convenience.

Methods that return an `MAPIResult` are useful for debugging or testing.

Convenience methods are useful if you're only concerned with success / failure. If a returned value is null or default, it means that the call failed.

# All types

|   |   |   |
|---|---|---|
| [MAPI Class](#mapi-class) | [MAPIResult Enum](#mapiresult-enum) |   |
# MAPI Class

Namespace: MAPILib

Class for general MAPI host connection and operations.

## Properties

| Name | Type | Summary |
|---|---|---|
| **Connected** | bool | State of connection to an MAPI host. |
## Constructors

| Name | Summary |
|---|---|
| [MAPI()](#mapi) | Sole constructor for an MAPI object. |
## Methods

| Name | Returns | Summary |
|---|---|---|
| [Connect(string host, int port)](#connectstring-host-int-port) | [MAPIResult](#mapiresult-enum) | Connect to an MAPI server host. |
| [Disconnect()](#disconnect) | void | Disconnect from the host. |
| [GetCurrentProcessId()](#getcurrentprocessid) | uint? | Get the currently running process ID of the host. |
| [GetCurrentProcessId(out uint? processId)](#getcurrentprocessidout-uint--processid) | [MAPIResult](#mapiresult-enum) | Get the currently running process ID of the host. |
| [GetFirmwareVersion()](#getfirmwareversion) | string | Get the firmware version of the host. |
| [GetFirmwareVersion(out string version)](#getfirmwareversionout-string-version) | [MAPIResult](#mapiresult-enum) | Get the firmware version of the host. |
| [GetMemory(uint processId, uint address, uint size)](#getmemoryuint-processid-uint-address-uint-size) | byte[] | Read an array of bytes from a host process. |
| [GetMemory(uint processId, uint address, uint size, out byte[] buffer)](#getmemoryuint-processid-uint-address-uint-size-out-byte-buffer) | [MAPIResult](#mapiresult-enum) | Read an array of bytes from a host process. |
| [GetProcessIds()](#getprocessids) | uint[] | Get an array of running process IDs on the host. |
| [GetProcessIds(out uint[] processIds)](#getprocessidsout-uint-processids) | [MAPIResult](#mapiresult-enum) | Get an array of running process IDs on the host. |
| [GetTemperature()](#gettemperature) | (int? cpu, int? rsx) | Get the CPU and RSX temperature of the host. |
| [GetTemperature(out int? cpu, out int? rsx)](#gettemperatureout-int--cpu-out-int--rsx) | [MAPIResult](#mapiresult-enum) | Get the CPU and RSX temperature of the host. |
| [SetMemory(uint processId, uint address, byte[] buffer)](#setmemoryuint-processid-uint-address-byte-buffer) | [MAPIResult](#mapiresult-enum) | Write an array of bytes into a host process. |
| [Syscall(uint number, Object[] args)](#syscalluint-number-object-args) | ulong? | Execute a syscall on the host. |
| [Syscall(uint number, Object[] args, out ulong? result)](#syscalluint-number-object-args-out-ulong--result) | [MAPIResult](#mapiresult-enum) | Execute a syscall on the host. |
## Constructors

### MAPI()

Sole constructor for an MAPI object.



## Methods

### Connect(string host, int port)

Connect to an MAPI server host.

| Parameter | Type | Description |
|---|---|---|
| host | string | The IP address of the MAPI server host. |
| port | int | The port number of the MAPI server (7887 by default). |


### Returns

[MAPIResult](#mapiresult-enum)

An MAPIResult for the API call.

### Disconnect()

Disconnect from the host.



### GetCurrentProcessId()

Get the currently running process ID of the host.



### Returns

uint?

A uint of the currently running process ID on the host or null upon failure.

### GetCurrentProcessId(out uint? processId)

Get the currently running process ID of the host.

| Parameter | Type | Description |
|---|---|---|
| processId | out uint? | A uint of the currently running process ID on the host or null upon failure. |


### Returns

[MAPIResult](#mapiresult-enum)

An MAPIResult for the API call.

### GetFirmwareVersion()

Get the firmware version of the host.



### Returns

string

A string containing the host's firmware version or null upon failure.

### GetFirmwareVersion(out string version)

Get the firmware version of the host.

| Parameter | Type | Description |
|---|---|---|
| version | out string | A string containing the host's firmware version or null upon failure. |


### Returns

[MAPIResult](#mapiresult-enum)

An MAPIResult for the API call.

### GetMemory(uint processId, uint address, uint size)

Read an array of bytes from a host process.

| Parameter | Type | Description |
|---|---|---|
| processId | uint | The ID of the process. |
| address | uint | The address in the process. |
| size | uint | The number of bytes to read. |


### Returns

byte[]

A byte array containing memory read from the process or null upon failure.

### GetMemory(uint processId, uint address, uint size, out byte[] buffer)

Read an array of bytes from a host process.

| Parameter | Type | Description |
|---|---|---|
| processId | uint | The ID of the process. |
| address | uint | The address in the process. |
| size | uint | The number of bytes to read. |
| buffer | out byte[] | A byte array containing memory read from the process or null upon failure. |


### Returns

[MAPIResult](#mapiresult-enum)

An MAPIResult for the API call.

### GetProcessIds()

Get an array of running process IDs on the host.



### Returns

uint[]

An array of uints containing process IDs running on the host or null upon failure.

### GetProcessIds(out uint[] processIds)

Get an array of running process IDs on the host.

| Parameter | Type | Description |
|---|---|---|
| processIds | out uint[] | An array of uints containing process IDs running on the host or null upon failure. |


### Returns

[MAPIResult](#mapiresult-enum)

An MAPIResult for the API call.

### GetTemperature()

Get the CPU and RSX temperature of the host.



### Returns

(int? cpu, int? rsx)

A tuple containing the CPU and RSX temperature of the host or null upon failure.

### GetTemperature(out int? cpu, out int? rsx)

Get the CPU and RSX temperature of the host.

| Parameter | Type | Description |
|---|---|---|
| cpu | out int? |  |
| rsx | out int? |  |


### Returns

[MAPIResult](#mapiresult-enum)

An MAPIResult for the API call.

### SetMemory(uint processId, uint address, byte[] buffer)

Write an array of bytes into a host process.

| Parameter | Type | Description |
|---|---|---|
| processId | uint | The ID of the process. |
| address | uint | The address in the process. |
| buffer | byte[] | The array of bytes to write. |


### Returns

[MAPIResult](#mapiresult-enum)

An MAPIResult for the API call.

### Syscall(uint number, Object[] args)

Execute a syscall on the host.

| Parameter | Type | Description |
|---|---|---|
| number | uint | The syscall number. |
| args | Object[] | The arguments of the syscall. |


### Returns

ulong?

A ulong of the syscall's execution return value or null upon failure.

### Syscall(uint number, Object[] args, out ulong? result)

Execute a syscall on the host.

| Parameter | Type | Description |
|---|---|---|
| number | uint | The syscall number. |
| args | Object[] | The arguments of the syscall. |
| result | out ulong? | A ulong of the syscall's execution return value or null upon failure. |


### Returns

[MAPIResult](#mapiresult-enum)

An MAPIResult for the API call.

# MAPIResult Enum

Namespace: MAPILib

Enum describing the result of an MAPI operation state.

## Values

| Name | Summary |
|---|---|
| **OK** | Operation completed successfully. |
| **CONNECT_FAILED** | Connect operation failed. |
| **GET_DATASOCKET_FAILED** | Retrieval of a data socket for data operations failed. |
| **CONNECT_DATASOCKET_FAILED** | Connection to a data socket for data operations failed. |
| **SEND_COMMAND_FAILED** | Failed to send a command to the host. |
| **WRONG_RESPONSE_CODE** | Host returned an unexpected response code for a command. |
| **GET_RESPONSE_BUFFER_FAILED** | Data socket failed to retrieve data buffer. |
| **DATASOCKET_SEND_FAILED** | Data socket failed to send data buffer. |
| **PARSE_PID_FAILED** | Failed to parse a process ID command response. |
| **PARSE_SYSCALL_FAILED** | Failed to parse a syscall command response. |
| **PARSE_TEMPERATURE_FAILED** | Failed to parse a temperature command response. |

## Usage Examples

If you'd like to see some usage examples of these methods, you can find them here:

[Usage Examples](/docs/EXAMPLES.md)