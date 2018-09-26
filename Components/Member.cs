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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using MongoDB.Bson;

namespace TrelloDriver.Components
{
    public class Member : ITrelloObject, ICloneable
    {
        public string Id { get; }
        public string AvatarHash { get; }
        public string AvatarUrl { get; }
        public string FullName { get; }
        public string Initials { get; }
        public string Username { get; set; }


        public event EventHandler OnInitialised;

        public Member() { }
        public Member(string memberUsername, string key)
        {
            // TODO: Make asyncronous
            string uri = String.Format("https://api.trello.com/1/members/{0}?key={1}", memberUsername, key);
            string data = Trello.ExtensionWideClient.GetStringAsync(uri).Result;

            try
            {
                BsonDocument memberInfo = BsonDocument.Parse(data);
                Id = memberInfo.GetValue("id").AsString;
                AvatarHash = memberInfo.GetValue("avatarHash") == null? null : memberInfo.GetValue("avatarHash").AsString;
                AvatarUrl = memberInfo.GetValue("avatarUrl") == null ? null : memberInfo.GetValue("avatarUrl").AsString;
                Initials = memberInfo.GetValue("initials").AsString;
                FullName = memberInfo.GetValue("fullName").AsString;
                Username = memberInfo.GetValue("username").AsString;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            OnInitialised?.Invoke(this, EventArgs.Empty);
        }
        public Member(TrelloConnectionInfo connectionInfo) : this(connectionInfo.UserId, connectionInfo.Key) { }
        public Member(string id, string avatarHash, string avatarUrl, string fullName, string initials, string username)
        {
            Id = id;
            AvatarHash = avatarHash;
            AvatarUrl = avatarUrl;
            FullName = fullName;
            Initials = initials;
            Username = username;
        }

        public object Clone()
        {
            return new Member(Id, AvatarHash, AvatarUrl, FullName, Initials, Username);
        }
    }
}
