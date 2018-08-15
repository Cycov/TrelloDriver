using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloDriver
{
    public class TrelloConnectionInfo
    {
        public string Token { get; protected set; }
        public string Key { get; protected set; }

        public TrelloConnectionInfo(string token, string key)
        {
            Token = token;
            Key = key;
        }
    }
}
