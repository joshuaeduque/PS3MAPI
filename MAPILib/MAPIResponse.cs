using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MAPILib
{
    public enum ResponseCode : int
    {
        UNKNOWN = -1,
        OK_150 = 150,
        OK_200 = 200,
        OK_220 = 220,
        OK_221 = 221,
        OK_226 = 226,
        OK_227 = 227,
        OK_230 = 230,
        ERROR_425 = 425,
        ERROR_451 = 451,
        ERROR_501 = 501,
        ERROR_502 = 502,
        ERROR_550 = 550
    }

    internal class MAPIResponse
    {
        public readonly string Response; // Truncated response string 
        public readonly int Code;        // Response code 
        public readonly bool Ok;         // Whether response code is ok 

        // Empty constructor 
        private MAPIResponse()
        {
            Response = string.Empty;
            Code = (int)ResponseCode.UNKNOWN;
            Ok = false;
        }

        public MAPIResponse(string rawResponse)
        {
            ParseResponse(rawResponse, out string response, out int code, out bool ok);
            Response = response;
            Code = code;
            Ok = ok;
        }

        // Just return empty constructor 
        public static MAPIResponse EmptyResponse()
        {
            MAPIResponse response = new();
            return response;
        }

        private void ParseResponse(string rawResponse, out string response, out int code, out bool ok)
        {
            // Try parsing response
            try
            {
                string[] splitRawResponse = rawResponse.Split(' ');

                // Get response 
                if (splitRawResponse[1] == "OK:" || splitRawResponse[1] == "Error:")    // <code> <OK/Error>: <response>
                    response = string.Join(' ', splitRawResponse.Skip(2));
                else                                                                    // <code>: <response> 
                    response = string.Join(' ', splitRawResponse.Skip(1));

                // Get response code 
                code = int.Parse(splitRawResponse[0]);

                // Get ok 
                ok = (code >= (int)ResponseCode.OK_150 && code <= (int)ResponseCode.OK_230);
            }
            catch(Exception)
            {
                response = string.Empty;
                code = (int)ResponseCode.UNKNOWN;
                ok = false;
            }
        }
    }
}
