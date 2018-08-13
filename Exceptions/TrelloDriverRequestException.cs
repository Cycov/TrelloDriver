using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloDriver.Exceptions
{

    [Serializable]
    public class TrelloDriverRequestException : Exception
    {
        public TrelloDriverRequestException() { }
        public TrelloDriverRequestException(string message) : base(message) { }
        public TrelloDriverRequestException(string message, Exception inner) : base(message, inner) { }
        protected TrelloDriverRequestException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
