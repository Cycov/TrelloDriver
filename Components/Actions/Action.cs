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
using TrelloDriver.Components;

namespace TrelloDriver.Components.Actions
{
    public abstract class Action : ITrelloObject
    {
        public string Id { get; }
        public Member MemberCreator { get; protected set; }
        public Card ContainerCard { get; protected set; }
        public DateTime DateTimeCreated { get; protected set; }

        protected string key, token;

        protected Action(string id, Member memberCreator, Card containerCard, DateTime dateTimeCreated)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            MemberCreator = memberCreator ?? throw new ArgumentNullException(nameof(memberCreator));
            ContainerCard = containerCard ?? throw new ArgumentNullException(nameof(containerCard));
            DateTimeCreated = dateTimeCreated;
        }

        protected Action(string id, TrelloConnectionInfo connectionInfo) : this(id, connectionInfo.Key, connectionInfo.Token)
        {

        }

        protected Action(string id, string key, string token)
        {
            this.key = key;
            this.token = token;
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public abstract void Refresh();
    }
}
