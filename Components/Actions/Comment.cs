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
