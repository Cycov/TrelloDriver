//    TrelloDriver - .NET library to interact with Trello using HTTP requests
//
//    Copyright(C) 2018  Dragos Circa
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.If not, see http://www.gnu.org/licenses.
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
