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
