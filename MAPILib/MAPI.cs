namespace MAPILib
{
    /// <summary>
    /// Class for general MAPI host connection and operations.
    /// </summary>
    public class MAPI
    {
        private const int DEFAULT_PORT = 7887;

        private readonly MAPIClient Client;

        /// <summary>
        /// State of connection to an MAPI host.
        /// </summary>
        public bool Connected { get; private set; }

        /// <summary>
        /// Sole constructor for an MAPI object.
        /// </summary>
        public MAPI()
        {
            Client = new MAPIClient();
            Connected = false;
        }

        /// <summary>Connect to an MAPI server host.</summary>
        /// <param name="host">The IP address of the MAPI server host.</param>
        /// <param name="port">The port number of the MAPI server (7887 by default).</param>
        /// <returns>An MAPIResult for the API call.</returns>
        public MAPIResult Connect(string host, int port = DEFAULT_PORT)
        {
            MAPIResult connectResult = Client.Connect(host, port);

            if (connectResult == MAPIResult.OK)
                Connected = true;

            return connectResult;
        }

        /// <summary>
        /// Disconnect from the host.
        /// </summary>
        public void Disconnect()
        {
            Client.Disconnect();
            Connected = false;
        }

        /// <summary>
        /// Get the currently running process ID of the host.
        /// </summary>
        /// <param name="processId">A uint of the currently running process ID on the host or null upon failure.</param>
        /// <returns>An MAPIResult for the API call.</returns>
        public MAPIResult GetCurrentProcessId(out uint? processId)
        {
            MAPIResult getProcIdResult = Client.GetCurrentProcessId(out uint? outProcessId);
            processId = outProcessId;
            return getProcIdResult;
        }

        /// <summary>
        /// Get the currently running process ID of the host.
        /// </summary>
        /// <returns>A uint of the currently running process ID on the host or null upon failure.</returns>
        public uint? GetCurrentProcessId()
        {
            GetCurrentProcessId(out uint? currentPid);
            return currentPid;
        }

        /// <summary>
        /// Get an array of running process IDs on the host.
        /// </summary>
        /// <param name="processIds">An array of uints containing process IDs running on the host or null upon failure.</param>
        /// <returns>An MAPIResult for the API call.</returns>
        public MAPIResult GetProcessIds(out uint[]? processIds)
        {
            MAPIResult result = Client.GetProcessIds(out uint[]? outProcessIds);
            processIds = outProcessIds;
            return result;
        }

        /// <summary>
        /// Get an array of running process IDs on the host.
        /// </summary>
        /// <returns>An array of uints containing process IDs running on the host or null upon failure.</returns>
        public uint[]? GetProcessIds()
        {
            GetProcessIds(out uint[]? processIds);
            return processIds;
        }

        /// <summary>
        /// Read an array of bytes from a host process.
        /// </summary>
        /// <param name="processId">The ID of the process.</param>
        /// <param name="address">The address in the process.</param>
        /// <param name="size">The number of bytes to read.</param>
        /// <param name="buffer">A byte array containing memory read from the process or null upon failure.</param>
        /// <returns>An MAPIResult for the API call.</returns>
        public MAPIResult GetMemory(uint processId, uint address, uint size, out byte[]? buffer)
        {
            MAPIResult getMemoryResult = Client.GetMemory(processId, address, size, out byte[]? outBuffer);
            buffer = outBuffer;
            return getMemoryResult;
        }

        /// <summary>
        /// Read an array of bytes from a host process.
        /// </summary>
        /// <param name="processId">The ID of the process.</param>
        /// <param name="address">The address in the process.</param>
        /// <param name="size">The number of bytes to read.</param>
        /// <returns>A byte array containing memory read from the process or null upon failure.</returns>
        public byte[]? GetMemory(uint processId, uint address, uint size)
        {
            GetMemory(processId, address, size, out byte[]? buffer);
            return buffer;
        }

        /// <summary>
        /// Write an array of bytes into a host process.
        /// </summary>
        /// <param name="processId">The ID of the process.</param>
        /// <param name="address">The address in the process.</param>
        /// <param name="buffer">The array of bytes to write.</param>
        /// <returns>An MAPIResult for the API call.</returns>
        public MAPIResult SetMemory(uint processId, uint address, byte[] buffer)
        {
            MAPIResult setMemoryResult = Client.SetMemory(processId, address, buffer);
            return setMemoryResult;
        }

        /// <summary>
        /// Execute a syscall on the host.
        /// </summary>
        /// <param name="number">The syscall number.</param>
        /// <param name="args">The arguments of the syscall.</param>
        /// <param name="result">A ulong of the syscall's execution return value or null upon failure.</param>
        /// <returns>An MAPIResult for the API call.</returns>
        public MAPIResult Syscall(uint number, object[] args, out ulong? result)
        {
            MAPIResult syscallResult = Client.Syscall(number, args, out ulong? outResult);
            result = outResult;
            return syscallResult;
        }

        /// <summary>
        /// Execute a syscall on the host.
        /// </summary>
        /// <param name="number">The syscall number.</param>
        /// <param name="args">The arguments of the syscall.</param>
        /// <returns>A ulong of the syscall's execution return value or null upon failure.</returns>
        public ulong? Syscall(uint number, object[] args)
        {
            // NOTE result is nullable because zero is an acceptable syscall result
            Syscall(number, args, out ulong? result);
            return result;
        }

        /// <summary>
        /// Get the firmware version of the host.
        /// </summary>
        /// <param name="version">A string containing the host's firmware version or null upon failure.</param>
        /// <returns>An MAPIResult for the API call.</returns>
        public MAPIResult GetFirmwareVersion(out string? version)
        {
            MAPIResult result = Client.GetFirmwareVersion(out string? outVersion);
            version = outVersion;
            return result;
        }

        /// <summary>
        /// Get the firmware version of the host.
        /// </summary>
        /// <returns>A string containing the host's firmware version or null upon failure.</returns>
        public string? GetFirmwareVersion()
        {
            GetFirmwareVersion(out string? version);
            return version;
        }

        // UNTESTED
        public MAPIResult GetFirmwareType(out string? fwtype)
        {
            MAPIResult result = Client.GetFirmwareType(out string? outFwtype);
            fwtype = outFwtype;
            return result;
        }

        // UNTESTED
        public string? GetFirmwareType()
        {
            GetFirmwareType(out string? fwtype);
            return fwtype;
        }

        /// <summary>
        /// Get the CPU and RSX temperature of the host.
        /// </summary>
        /// <param name="cpu"></param>
        /// <param name="rsx"></param>
        /// <returns>An MAPIResult for the API call.</returns>
        public MAPIResult GetTemperature(out int? cpu, out int? rsx)
        {
            MAPIResult result = Client.GetTemperature(out int? cpuResult, out int? rsxResult);
            cpu = cpuResult;
            rsx = rsxResult;
            return result;
        }

        /// <summary>
        /// Get the CPU and RSX temperature of the host.
        /// </summary>
        /// <returns>A tuple containing the CPU and RSX temperature of the host or null upon failure.</returns>
        public (int? cpu, int? rsx) GetTemperature()
        {
            GetTemperature(out int? cpuResult, out int? rsxResult);
            return (cpu: cpuResult, rsx: rsxResult);
        }

        // UNTESTED
        public MAPIResult Notify(string message)
        {
            // TODO check the default sound and icon params
            MAPIResult result = Client.Notify(message, 0, 0);
            return result;
        }

        // UNTESTED
        public MAPIResult Notify(string message, int icon, int sound)
        {
            MAPIResult result = Client.Notify(message, icon, sound);
            return result;
        }
    }
}
