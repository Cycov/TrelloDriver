using System;
using System.Collections.Generic;
using System.Net.Http;
using MongoDB.Bson;
using TrelloDriver.Exceptions;

namespace TrelloDriver.Components
{
    public class List : ITrelloObject
    {
        public string Id { get; }
        public string Name
        {
            get => m_name;
            set => throw new NotImplementedException();
        }
        private string m_name;

        public List<Card> Cards;
        public event EventHandler OnInitialised;

        private Board parent;

        public List(string name, string id)
        {
            m_name = name;
            Id = id;
            parent = null;
            Cards = new List<Card>();
            OnInitialised?.Invoke(this, EventArgs.Empty);
        }
        public List(string name, string id, Board parent)
        {
            m_name = name;
            Id = id;
            this.parent = parent;
            Cards = new List<Card>();
            OnInitialised?.Invoke(this, EventArgs.Empty);
        }

        public override string ToString()
        {
            return String.Format("Trello list, named {0} containing {1} cards", m_name, Cards.Count);
        }
    }
}
