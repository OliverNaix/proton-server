using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Proton_Server_Core.Converters
{
    class JsonConvert
    {
        public static string ToString(object value)
        {
            return JsonSerializer.Serialize(value);
        }

        public static byte[] ToByteArray(object value)
        {
            return Encoding.UTF8.GetBytes(ToString(value));
        }
    }
}
