using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace MAPILib
{
    /// <summary>
    /// Enum describing the result of an MAPI operation state.
    /// </summary>
    public enum MAPIResult
    {
        /// <summary>
        /// Operation completed successfully.
        /// </summary>
        OK,
        /// <summary>
        /// Connect operation failed.
        /// </summary>
        CONNECT_FAILED,
        /// <summary>
        /// Retrieval of a data socket for data operations failed.
        /// </summary>
        GET_DATASOCKET_FAILED,
        /// <summary>
        /// Connection to a data socket for data operations failed.
        /// </summary>
        CONNECT_DATASOCKET_FAILED,
        /// <summary>
        /// Failed to send a command to the host.
        /// </summary>
        SEND_COMMAND_FAILED,
        /// <summary>
        /// Host returned an unexpected response code for a command.
        /// </summary>
        WRONG_RESPONSE_CODE,
        /// <summary>
        /// Data socket failed to retrieve data buffer.
        /// </summary>
        GET_RESPONSE_BUFFER_FAILED,
        /// <summary>
        /// Data socket failed to send data buffer.
        /// </summary>
        DATASOCKET_SEND_FAILED,
        /// <summary>
        /// Failed to parse a process ID command response.
        /// </summary>
        PARSE_PID_FAILED,
        /// <summary>
        /// Failed to parse a syscall command response.
        /// </summary>
        PARSE_SYSCALL_FAILED,
        /// <summary>
        /// Failed to parse a temperature command response.
        /// </summary>
        PARSE_TEMPERATURE_FAILED
    }

    // NOTE values from PS3 syscalls wiki
    public enum LEDColor
    {
        Red = 0,
        Green = 1,
        Yellow = 2
    }

    // NOTE values from PS3 syscalls wiki
    public enum LEDMode
    {
        Off = 0,
        On = 1,
        BlinkFast = 2,
        BlinkSlow = 3
    }

    internal class MAPIClient
    {
        private Socket MainSocket;

        public MAPIClient()
        {
            MainSocket = CreateSocket();
        }

        private static Socket CreateSocket()
        {
            Socket socket = new(SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = 5000,
                SendTimeout = 5000
            };

            return socket;
        }

        // Send command to main socket 
        private bool SendCommand(string command)
        {
            // Check if main socket connected 
            if (!MainSocket.Connected)
                return false;

            // Get command buffer 
            byte[] cmdBuffer = Encoding.ASCII.GetBytes(command + "\r\n");

            // Try sending command
            try
            {
                MainSocket.Send(cmdBuffer);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        // Get response from main socket 
        private MAPIResponse GetResponse()
        {
            // Check if main socket connected 
            if (!MainSocket.Connected)
                return MAPIResponse.EmptyResponse();

            // Try getting response buffer 
            byte[] responseBuffer = new byte[2048];
            int bytesReceived = 0;
            try
            {
                while (bytesReceived <= 0)
                    bytesReceived = MainSocket.Receive(responseBuffer);
            }
            catch (Exception)
            {
                return MAPIResponse.EmptyResponse();
            }

            // Try getting response string 
            string responseString;
            try
            {
                responseString = Encoding.ASCII.GetString(responseBuffer);
                responseString = responseString.Trim('\0');
                responseString = responseString.Replace("\r\n", string.Empty);
            }
            catch (Exception)
            {
                return MAPIResponse.EmptyResponse();
            }

            // Try getting response 
            MAPIResponse response;
            try
            {
                response = new MAPIResponse(responseString);
            }
            catch (Exception)
            {
                return MAPIResponse.EmptyResponse();
            }

            // Return response 
            return response;
        }

        private static bool ParsePASV(string response, out string? pasvHost, out int? pasvPort)
        {
            // Default out params 
            pasvHost = null;
            pasvPort = null;

            // Try parsing host and port 
            string parsedHost;
            int parsedPort;
            try
            {
                MatchCollection matches = Regex.Matches(response, @"\d+");
                parsedHost = $"{matches[0].Value}.{matches[1].Value}.{matches[2].Value}.{matches[3].Value}";
                parsedPort = ((int.Parse(matches[4].Value) * 256) + int.Parse(matches[5].Value));
            }
            catch (Exception)
            {
                return false;
            }

            // Assign out params 
            pasvHost = parsedHost;
            pasvPort = parsedPort;

            return true;
        }

        private Socket? GetDataSocket(out string? host, out int? port)
        {
            // Default out params 
            host = null;
            port = null;

            // Send PASV command 
            bool pasvSent = SendCommand("PASV");
            if (!pasvSent)
                return null;

            // Get PASV response 
            MAPIResponse pasvResponse = GetResponse();
            if (pasvResponse.Code != 227)
                return null;

            // Get host and port 
            bool pasvParsed = ParsePASV(pasvResponse.Response, out string? pasvHost, out int? pasvPort);
            if (!pasvParsed)
                return null;

            // Create data socket 
            Socket? dataSocket = new(SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = 5000,
                SendTimeout = 5000
            };

            // Assign out params 
            host = pasvHost;
            port = pasvPort;

            // Return data socket 
            return dataSocket;
        }

        private static bool ConnectDataSocket(Socket dataSocket, string host, int port)
        {
            // Try connecting data socket 
            try
            {
                dataSocket.Connect(host, port);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private static byte[]? GetResponseBuffer(Socket dataSocket)
        {
            // Check if data socket connected 
            if (!dataSocket.Connected)
                return null;

            // Try getting data from socket 
            MemoryStream responseStream = new();
            int bytesReceived;
            try
            {
                do
                {
                    byte[] receiveBuffer = new byte[2048];
                    bytesReceived = dataSocket.Receive(receiveBuffer);
                    responseStream.Write(receiveBuffer, 0, bytesReceived);
                }
                while (bytesReceived > 0);
            }
            catch (Exception)
            {
                return null;
            }

            // Return response buffer 
            byte[] responseBuffer = responseStream.ToArray();
            return responseBuffer;
        }

        public MAPIResult Connect(string host, int port)
        {
            // Return OK if already connected 
            if (MainSocket.Connected)
                return MAPIResult.OK;

            // Try connecting main socket 
            try
            {
                MainSocket.Connect(host, port);
            }
            catch (Exception)
            {
                return MAPIResult.CONNECT_FAILED;
            }

            // Catch 220 
            MAPIResponse catch220 = GetResponse();
            if (catch220.Code != 220)
                return MAPIResult.CONNECT_FAILED;

            // Catch 230 
            MAPIResponse catch230 = GetResponse();
            if (catch230.Code != 230)
                return MAPIResult.CONNECT_FAILED;

            return MAPIResult.OK;
        }

        public void Disconnect()
        {
            // Close old socket  
            MainSocket.Close();
            // Overwrite old socket 
            MainSocket = CreateSocket();
        }

        public MAPIResult GetCurrentProcessId(out uint? processId)
        {
            // Default out params  
            processId = null;

            // Send get pid command 
            bool pidCommandSent = SendCommand("PROCESS GETCURRENTPID");
            if (!pidCommandSent)
                return MAPIResult.SEND_COMMAND_FAILED;

            // Get pid response 
            MAPIResponse getPidResponse = GetResponse();
            if (getPidResponse.Code != 200)
                return MAPIResult.WRONG_RESPONSE_CODE;

            // Try parsing pid 
            try
            {
                uint pid = uint.Parse(getPidResponse.Response);
                processId = pid;
            }
            catch (Exception)
            {
                return MAPIResult.PARSE_PID_FAILED;
            }

            return MAPIResult.OK;
        }

        public MAPIResult GetProcessIds(out uint[]? processIds)
        {
            // Default out params
            processIds = null;

            // Send command
            bool commandSent = SendCommand("PROCESS GETALLPID");
            if (!commandSent)
                return MAPIResult.SEND_COMMAND_FAILED;

            // Get response
            // NOTE response should be in format pid|pid|...|pid|
            MAPIResponse response = GetResponse();
            if (response.Code != 200)
                return MAPIResult.WRONG_RESPONSE_CODE;

            // Parse response
            List<uint> parsedPids = new List<uint>();
            string[] responsePidSplit = response.Response.Split('|');
            for (int i = 0; i < responsePidSplit.Length; i++)
            {
                bool pidParsed = uint.TryParse(responsePidSplit[i], out uint pid);
                if (!pidParsed)
                    break;
                parsedPids.Add(pid);
            }

            // Assign out param as parsed pids array
            processIds = parsedPids.ToArray();

            return MAPIResult.OK;
        }

        public MAPIResult GetMemory(uint processId, uint address, uint size, out byte[]? buffer)
        {
            // Default out params 
            buffer = null;

            // Get data socket
            Socket? dataSocket = GetDataSocket(out string? host, out int? port);
            if (dataSocket == null || host == null || port == null)
                return MAPIResult.GET_DATASOCKET_FAILED;

            // Connect data socket
            bool dataSocketConnected = ConnectDataSocket(dataSocket, host, (int)port);
            if (!dataSocketConnected)
                return MAPIResult.CONNECT_DATASOCKET_FAILED;

            // Send memory get command 
            bool memoryGetSent = SendCommand($"MEMORY GET {processId} {address:X} {size}");
            if (!memoryGetSent)
                return MAPIResult.SEND_COMMAND_FAILED;

            // Catch 150 OK 
            MAPIResponse catch150Response = GetResponse();
            if (catch150Response.Code != 150)
                return MAPIResult.WRONG_RESPONSE_CODE;

            // Catch 226 OK 
            MAPIResponse catch226Response = GetResponse();
            if (catch226Response.Code != 226)
                return MAPIResult.WRONG_RESPONSE_CODE;

            // Get response buffer 
            byte[]? responseBuffer = GetResponseBuffer(dataSocket);
            if (responseBuffer == null)
                return MAPIResult.GET_RESPONSE_BUFFER_FAILED;

            // Assign out params and return 
            buffer = responseBuffer;
            return MAPIResult.OK;
        }

        public MAPIResult SetMemory(uint processId, uint address, byte[] buffer)
        {
            // Get data socket
            Socket? dataSocket = GetDataSocket(out string? host, out int? port);
            if (dataSocket == null || host == null || port == null)
                return MAPIResult.GET_DATASOCKET_FAILED;

            // Connect data socket
            bool dataSocketConnected = ConnectDataSocket(dataSocket, host, (int)port);
            if (!dataSocketConnected)
                return MAPIResult.CONNECT_DATASOCKET_FAILED;

            // Send memory set command 
            bool memoryGetSent = SendCommand($"MEMORY SET {processId} {address:X}");
            if (!memoryGetSent)
                return MAPIResult.SEND_COMMAND_FAILED;

            // Catch 150 OK 
            MAPIResponse catch150Response = GetResponse();
            if (catch150Response.Code != 150)
                return MAPIResult.WRONG_RESPONSE_CODE;

            // Send buffer 
            try
            {
                dataSocket.Send(buffer);
                dataSocket.Close();
            }
            catch (Exception)
            {
                return MAPIResult.DATASOCKET_SEND_FAILED;
            }

            // Catch 226 OK 
            MAPIResponse catch226Response = GetResponse();
            if (catch226Response.Code != 226)
                return MAPIResult.WRONG_RESPONSE_CODE;

            return MAPIResult.OK;
        }

        public MAPIResult Syscall(uint number, object[] args, out ulong? result)
        {
            // Default out params
            result = null;

            // Send syscall command
            string syscallCommand = $"SYSCALL {number} {string.Join(" ", args.Select(o => o.ToString()))}";

            bool syscallCommandSent = SendCommand(syscallCommand);
            if (!syscallCommandSent)
                return MAPIResult.SEND_COMMAND_FAILED;

            // Get syscall response
            MAPIResponse getSyscallResponse = GetResponse();
            if (getSyscallResponse.Code != 200)
                return MAPIResult.WRONG_RESPONSE_CODE;

            // Try parsing result
            try
            {
                ulong syscallResult = ulong.Parse(getSyscallResponse.Response);
                result = syscallResult;
            }
            catch (Exception)
            {
                return MAPIResult.PARSE_SYSCALL_FAILED;
            }

            return MAPIResult.OK;
        }

        public MAPIResult GetFirmwareVersion(out string? version)
        {
            // Default out params
            version = null;

            // Send command
            bool commandSent = SendCommand("PS3 GETFWVERSION");
            if (!commandSent)
                return MAPIResult.SEND_COMMAND_FAILED;

            // Get response
            MAPIResponse response = GetResponse();
            if (response.Code != 200)
                return MAPIResult.WRONG_RESPONSE_CODE;

            // Assign out params
            version = response.Response;

            // Return ok
            return MAPIResult.OK;
        }

        // UNTESTED
        public MAPIResult GetFirmwareType(out string? fwtype)
        {
            // Default out params
            fwtype = null;

            // Send command
            bool commandSent = SendCommand("PS3 GETFWTYPE");
            if (!commandSent)
                return MAPIResult.SEND_COMMAND_FAILED;

            // Get response
            MAPIResponse response = GetResponse();
            if (response.Code != 200)
                return MAPIResult.WRONG_RESPONSE_CODE;

            // Assign out params
            fwtype = response.Response;

            // Return ok
            return MAPIResult.OK;
        }

        public MAPIResult GetTemperature(out int? cpu, out int? rsx)
        {
            // Default out params
            cpu = null;
            rsx = null;

            // Send command
            bool commandSent = SendCommand("PS3 GETTEMP");
            if (!commandSent)
                return MAPIResult.SEND_COMMAND_FAILED;

            // Get response
            MAPIResponse response = GetResponse();
            if (response.Code != 200)
                return MAPIResult.WRONG_RESPONSE_CODE;

            // Parse response
            string[] split = response.Response.Split('|');
            if (!int.TryParse(split[0], out int parsedCpu) || !int.TryParse(split[1], out int parsedRsx))
                return MAPIResult.PARSE_TEMPERATURE_FAILED;

            // Assign out params
            cpu = parsedCpu;
            rsx = parsedRsx;

            // Return ok
            return MAPIResult.OK;
        }

        // UNTESTED
        // TODO create enums for icon and sound values
        public MAPIResult Notify(string message, int icon, int sound)
        {
            // Send command
            bool commandSent = SendCommand($"PS3 NOTIFY {message}&icon={icon}&snd={sound}");
            if (!commandSent)
                return MAPIResult.SEND_COMMAND_FAILED;

            // Get response
            MAPIResponse response = GetResponse();
            if (response.Code != 200)
                return MAPIResult.WRONG_RESPONSE_CODE;

            // Return ok
            return MAPIResult.OK;
        }

        // UNTESTED
        // TODO research buzzer syscall and create enum from possible values
        public MAPIResult Buzzer(int mode)
        {
            // Send command
            bool commandSent = SendCommand($"PS3 BUZZER{mode}");
            if (!commandSent)
                return MAPIResult.SEND_COMMAND_FAILED;

            // Get response
            MAPIResponse response = GetResponse();
            if (response.Code != 200)
                return MAPIResult.WRONG_RESPONSE_CODE;

            // Return ok
            return MAPIResult.OK;
        }

        // UNTESTED
        // TODO create enums for the color and mode values
        public MAPIResult LED(LEDColor color, LEDMode mode)
        {
            // Send command
            // NOTE MAPI server parses color and mode in command as u64
            bool commandSent = SendCommand($"PS3 LED {(uint)color} {(uint)mode}");
            if (!commandSent)
                return MAPIResult.SEND_COMMAND_FAILED;

            // Get response
            MAPIResponse response = GetResponse();
            if (response.Code != 200)
                return MAPIResult.WRONG_RESPONSE_CODE;

            // Return ok
            return MAPIResult.OK;
        }
    }
}
