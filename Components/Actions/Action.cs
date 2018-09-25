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
