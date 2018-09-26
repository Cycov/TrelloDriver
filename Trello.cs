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
using System;
using System.Net.Http;
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
