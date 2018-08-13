using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using TrelloDriver.Exceptions;

namespace TrelloDriver
{
    public class Trello
    {
        public static HttpClient ExtensionWideClient
        {
            get
            {
                if (httpClient == null)
                    httpClient = new HttpClient();
                return httpClient;
            }
        }
        private static HttpClient httpClient;

        public static string GetMemberID(string memberName, string key)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.GetStringAsync(String.Format("https://api.trello.com/1/members/{0}?key={1}",memberName,key)).Result;
                    string id = BsonDocument.Parse(response).GetValue("id").AsString;
                    return id;
                }
                catch (AggregateException ex)
                {
                    if (ex.InnerException is HttpRequestException)
                    throw new TrelloDriverRequestException("The request key is invalid", ex.InnerException);
                    return string.Empty;
                }
            }
        }
    }
}
