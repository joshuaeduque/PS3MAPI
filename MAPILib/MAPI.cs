using System.Formats.Asn1;
using System.Net.Sockets;

namespace MAPILib
{
    public class MAPI
    {
        private const int DEFAULT_PORT = 7887;

        private readonly MAPIClient Client;

        public bool Connected { get; private set; }

        public MAPI()
        {
            Client = new MAPIClient();
            Connected = false;
        }

        public MAPIResult Connect(string host, int port = DEFAULT_PORT)
        {
            MAPIResult connectResult = Client.Connect(host, port);

            if (connectResult == MAPIResult.OK)
                Connected = true;

            return connectResult;
        }

        public void Disconnect()
        {
            Client.Disconnect();
            Connected = false;
        }

        public MAPIResult GetCurrentProcessId(out uint? processId)
        {
            MAPIResult getProcIdResult = Client.GetCurrentProcessId(out uint? outProcessId);
            processId = outProcessId;
            return getProcIdResult;
        }

        public uint? GetCurrentProcessId()
        {
            GetCurrentProcessId(out uint? currentPid);
            return currentPid;
        }

        public MAPIResult GetProcessIds(out uint[]? processIds)
        {
            MAPIResult result = Client.GetProcessIds(out uint[]? outProcessIds);
            processIds = outProcessIds;
            return result;
        }

        public uint[]? GetProcessIds()
        {
            GetProcessIds(out uint[]? processIds);
            return processIds;
        }

        public MAPIResult GetMemory(uint processId, uint address, uint size, out byte[]? buffer)
        {
            MAPIResult getMemoryResult = Client.GetMemory(processId, address, size, out byte[]? outBuffer);
            buffer = outBuffer;
            return getMemoryResult;
        }

        public byte[]? GetMemory(uint processId, uint address, uint size)
        {
            GetMemory(processId, address, size, out byte[]? buffer);
            return buffer;
        }

        public MAPIResult SetMemory(uint processId, uint address, byte[] buffer)
        {
            MAPIResult setMemoryResult = Client.SetMemory(processId, address, buffer);
            return setMemoryResult;
        }

        public MAPIResult Syscall(uint number, object[] args, out ulong? result)
        {
            MAPIResult syscallResult = Client.Syscall(number, args, out ulong? outResult);
            result = outResult;
            return syscallResult;
        }

        public ulong? Syscall(uint number, object[] args)
        {
            // NOTE result is nullable because zero is an acceptable syscall result
            Syscall(number, args, out ulong? result);
            return result;
        }

        public MAPIResult GetFirmwareVersion(out string? version)
        {
            MAPIResult result = Client.GetFirmwareVersion(out string? outVersion);
            version = outVersion;
            return result;
        }

        public string? GetFirmwareVersion()
        {
            GetFirmwareVersion(out string? version);
            return version;
        }

        public MAPIResult GetTemperature(out int cpu, out int rsx)
        {
            MAPIResult result = Client.GetTemperature(out int cpuResult, out int rsxResult);
            cpu = cpuResult;
            rsx = rsxResult;
            return result;
        }

        public (int cpu, int rsx) GetTemperature()
        {
            GetTemperature(out int cpuResult, out int rsxResult);
            return (cpu: cpuResult, rsx: rsxResult);
        }
    }
}
