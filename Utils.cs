using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TrelloDriver
{
    static class Utils
    {
        public static string ProcessHttpResponse(HttpStatusCode httpResponse)
        {
            switch (httpResponse)
            {
                case HttpStatusCode.Unauthorized: return "Token or key invalid.";
                default: return "Unhandled error occured: " + httpResponse.ToString();
            }
        }

        public static BsonArray GetBsonArray(string uri)
        {
            using (HttpClient http = new HttpClient())
            {
                string data = http.GetStringAsync(uri).Result;
                if (data == "unauthorized permission requested")
                    throw new Exceptions.TrelloDriverRequestException("Key or token invalid");
                else if (data == "The requested resource was not found.")
                    throw new Exceptions.TrelloDriverRequestException("Invalid Action id");

                using (var jsonReader = new JsonReader(data))
                {
                    var serializer = new BsonArraySerializer();
                    try
                    {
                        BsonArray array = serializer.Deserialize(BsonDeserializationContext.CreateRoot(jsonReader));
                        return array;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        throw;
                    }
                }
            }
        }

        public static BsonDocument GetBsonDocument(string uri)
        {
            using (HttpClient http = new HttpClient())
            {
                string data = http.GetStringAsync(uri).Result;

                if (data == "unauthorized permission requested")
                    throw new Exceptions.TrelloDriverRequestException("Key or token invalid");
                else if (data == "The requested resource was not found.")
                    throw new Exceptions.TrelloDriverRequestException("Invalid Action id");

                try
                {
                    BsonDocument bsonElements = BsonDocument.Parse(data);
                    return bsonElements;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    throw;
                }
            }
        }
    }
}
