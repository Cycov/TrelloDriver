using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TrelloDriver.Components
{
    public class Card : ITrelloObject
    {
        public event EventHandler OnInitialised;
        public string Id { get; }
        public string Name {
            get => m_name;
            set => throw new NotImplementedException();
        }
        private string m_name;

        private List parentList;
        private Board parentBoard;

        public Card(string id, string name)
        {
            Id = id;
            m_name = name;
        }
        public Card(string id, string name, List parentList, Board parentBoard)
        {
            Id = id;
            m_name = name;
            this.parentList = parentList;
            this.parentBoard = parentBoard;
        }

        public void Refresh()
        {
            OnInitialised?.Invoke(this, EventArgs.Empty);
        }

        public async void MoveCard(List newList)
        {
            var httpContent = new StringContent("", Encoding.UTF8, "application/json");
            await Trello.ExtensionWideClient.PutAsync(String.Format("https://api.trello.com/1/cards/{0}?idList={1}&key={2}&token={3}", Id, newList.Id, parentBoard.ConnectionInfo.Key, parentBoard.ConnectionInfo.Token), httpContent);
            parentList.Cards.Remove(this);
            this.parentList = newList;
            newList.Cards.Add(this);
        }
        public override string ToString()
        {
            return String.Format("Trello card, named {0} containing {1} actions", m_name, 0);
        }
    }
}
