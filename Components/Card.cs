using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TrelloDriver.Components
{
    public partial class Card : ITrelloObject
    {
        public Card(string id)
        {
            Id = id;

            Refresh();
        }
        public Card(string id, string name, List parentList, Board parentBoard)
        {
            Id = id;
            m_name = name;
            this.ParentList = parentList;
            this.ParentBoard = parentBoard;

            Refresh();
        }

        public void Refresh()
        {
            if (m_members != null)
                m_members.Clear();
            m_members = new Dictionary<string, Member>();
            using (HttpClient http = new HttpClient())
            {
                string uri = String.Format("https://api.trello.com/1/cards/{0}/?key={1}&token={2}", Id, ParentBoard.ConnectionInfo.Key, ParentBoard.ConnectionInfo.Token);
                string data = http.GetStringAsync(uri).Result;

                BsonDocument elements = BsonDocument.Parse(data);

                m_name = elements.GetValue("name").AsString;
                ShortLink = elements.GetValue("shortLink").AsString;
                Url = elements.GetValue("url").AsString;
                Description = elements.GetValue("desc").AsString;

                BsonArray members = elements.GetValue("idMembers").AsBsonArray;
                foreach (BsonValue member in members)
                {
                    m_members.Add(member.AsString, ParentBoard.Members[member.AsString]);
                }
            }
        }

        public async Task<HttpResponseMessage> MoveCard(List newList)
        {
            using (HttpClient http = new HttpClient())
            {
                var httpContent = new StringContent("", Encoding.UTF8, "application/json");
                string uri = String.Format("https://api.trello.com/1/cards/{0}?idList={1}&key={2}&token={3}", Id, newList.Id, ParentBoard.ConnectionInfo.Key, ParentBoard.ConnectionInfo.Token);
                HttpResponseMessage responseMessage = await http.PutAsync(uri, httpContent);
                ParentList.Cards.Remove(this);
                this.ParentList = newList;
                newList.Cards.Add(this);
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    return responseMessage;
                else if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exceptions.TrelloDriverRequestException("Card or list id invalid", null, responseMessage);
                else
                    throw new Exceptions.TrelloDriverRequestException(Utils.ProcessHttpResponse(responseMessage.StatusCode), null, responseMessage);
            }
        }

        public async Task<HttpResponseMessage> AddComment(string comment)
        {
            if (String.IsNullOrEmpty(comment) || String.IsNullOrWhiteSpace(comment))
                throw new System.ArgumentNullException("The comment cannont be null, empty or white space");
            var httpContent = new StringContent("", Encoding.UTF8, "application/json");
            var uri = String.Format("https://api.trello.com/1/cards/{0}/actions/comments?text={1}&key={2}&token={3}", Id, System.Web.HttpUtility.HtmlEncode(comment), ParentBoard.ConnectionInfo.Key, ParentBoard.ConnectionInfo.Token);
            HttpResponseMessage responseMessage = await Trello.ExtensionWideClient.PostAsync(
                uri,
                httpContent);
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                return responseMessage;
            else if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new Exceptions.TrelloDriverRequestException("Card id invalid", null, responseMessage);
            else
                throw new Exceptions.TrelloDriverRequestException(Utils.ProcessHttpResponse(responseMessage.StatusCode), null, responseMessage);
                 
        }

        public override string ToString()
        {
            return String.Format("Trello card, named {0} containing {1} actions", m_name, 0);
        }
    }
}
