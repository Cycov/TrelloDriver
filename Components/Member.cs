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
                BsonDocument memeberInfo = BsonDocument.Parse(data).GetValue("cards").AsBsonDocument;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        public Member(string name, TrelloConnectionInfo connectionInfo) : this(name, connectionInfo.Key) { }
        public Member(string id, string avatarHash, string fullName, string initials, string username)
        {
            Id = id;
            AvatarHash = avatarHash;
            FullName = fullName;
            Initials = initials;
            Username = username;
        }

        public object Clone()
        {
            return new Member(Id, AvatarHash, FullName, Initials, Username);
        }
    }
}
