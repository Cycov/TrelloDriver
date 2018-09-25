using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloDriver
{
    public class TrelloConnectionInfo : IDisposable
    {
        public string Token { get; protected set; }
        public string Key { get; protected set; }
        public string UserId { get; protected set; }

        public TrelloConnectionInfo(string token, string key)
        {
            Token = token;
            Key = key;
        }
        public TrelloConnectionInfo(string token, string key, string userId)
        {
            Token = token;
            Key = key;
            UserId = userId;
        }

        public void Dispose()
        {
            Token = null;
            Key = null;
            UserId = null;
        }
    }
}
