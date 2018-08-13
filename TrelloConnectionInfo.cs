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
        public string UserID
        {
            get
            {
                if (String.IsNullOrWhiteSpace(m_userId))
                    throw new Exceptions.TrelloDriverConfigurationException("The user id is not set");
                else
                    return m_userId;
            }

            protected set
            {
                m_userId = value;
            }
        }
        private string m_userId;


        public TrelloConnectionInfo(string token, string key)
        {
            Token = token;
            Key = key;
        }
        public TrelloConnectionInfo(string token, string key, string userId)
        {
            Token = token;
            Key = key;
            UserID = userId;
        }
    }
}
