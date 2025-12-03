using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SwitchCMS.Component.Auth
{
    class TokenHandler
    {
        public string TokenDecoding(string Base64String)
        {
            string[] part = Base64String.Split('.');
            string body = part[1].PadRight(4 * ((part[1].Length + 3) / 4), '=');
            var data = Convert.FromBase64String(body);
            string decodedString = Encoding.UTF8.GetString(data);
            var jsonfile = JsonSerializer.Deserialize<Dictionary<string, object>>(decodedString);
            return decodedString;
        }
    }
}
