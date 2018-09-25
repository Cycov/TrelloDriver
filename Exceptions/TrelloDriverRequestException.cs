using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TrelloDriver.Exceptions
{

    [Serializable]
    public class TrelloDriverRequestException : Exception
    {
        public HttpResponseMessage HttpResponse;

        public TrelloDriverRequestException() { }
        public TrelloDriverRequestException(string message) : base(message) { }
        public TrelloDriverRequestException(string message, Exception inner) : base(message, inner) { }
        public TrelloDriverRequestException(string message, Exception inner, HttpResponseMessage httpResponseMessage) : this(message, inner)
        {
            HttpResponse = httpResponseMessage;
        }
        protected TrelloDriverRequestException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
