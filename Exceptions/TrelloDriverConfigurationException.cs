using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloDriver.Exceptions
{
    [Serializable]
    public class TrelloDriverConfigurationException : Exception
    {
        public TrelloDriverConfigurationException() { }
        public TrelloDriverConfigurationException(string message) : base(message) { }
        public TrelloDriverConfigurationException(string message, Exception inner) : base(message, inner) { }
        protected TrelloDriverConfigurationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
