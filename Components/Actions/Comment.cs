using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloDriver.Components.Actions
{
    class Comment : Action
    {
        public string Text { get; protected set; }

        public Comment(string id, Member memberCreator, Card containerCard, DateTime dateTimeCreated, string text) : base(id, memberCreator, containerCard, dateTimeCreated)
        {
            this.Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public Comment(string id, string key, string token) : base(id, key, token) { }
        public Comment(string id, TrelloConnectionInfo connectionInfo) : base(id, connectionInfo) { }

        public override void Refresh()
        {
            BsonDocument actionData = Utils.GetBsonDocument(String.Format("https://api.trello.com/1/actions/{0}/?limit=5&key={1}&token={2}", Id, key, token));
            Member member = new Member(actionData.GetValue("idMemberCreator").AsString, key);
        }
    }
}
