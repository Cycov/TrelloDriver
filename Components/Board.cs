using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;

namespace TrelloDriver.Components
{
    public partial class Board : ITrelloObject
    {
        public Board(string id, TrelloConnectionInfo connectionInfo)
        {
            Id = id;
            ConnectionInfo = connectionInfo;
            m_name = GetBoardName(id);
            m_member = new Member(connectionInfo);
            Refresh();
        }
        public Board(string id, Member member, TrelloConnectionInfo connectionInfo)
        {
            Id = id;
            ConnectionInfo = connectionInfo;
            m_name = GetBoardName(id);
            m_member = member;
            Refresh();
        }

        private string GetBoardName(string id)
        {
            string data = Trello.ExtensionWideClient.GetStringAsync(String.Format("https://api.trello.com/1/boards/{0}?key={1}&token={2}", Id, ConnectionInfo.Key, ConnectionInfo.Token)).Result;
            return BsonDocument.Parse(data).GetValue("id").AsString;
        }

        public void Refresh()
        {
            if (Lists != null)
                Lists.Clear();
            if (Members != null)
                Members.Clear();
            if (Cards != null)
                Cards.Clear();
            Lists = new Dictionary<string, List>();
            Members = new Dictionary<string, Member>();
            Cards = new Dictionary<string, Card>();

            using (HttpClient http = new HttpClient())
            {
                //Get all members
                string data = http.GetStringAsync(String.Format("https://api.trello.com/1/boards/{0}/members/?key={1}&token={2}", Id, ConnectionInfo.Key, ConnectionInfo.Token)).Result;
                using (var jsonReader = new JsonReader(data))
                {
                    var serializer = new BsonArraySerializer();
                    try
                    {
                        BsonArray lists = serializer.Deserialize(BsonDeserializationContext.CreateRoot(jsonReader));
                        foreach (var it in lists)
                        {
                            BsonDocument item = it.AsBsonDocument;
                            string username = item.GetValue("username").AsString;
                            string id = item.GetValue("id").AsString;
                            Member member = new Member(username, ConnectionInfo.Key);
                            Members.Add(id, member);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        throw;
                    }
                }

                //Get all lists
                // TODO: Make asyncronous and have events
                data = http.GetStringAsync(String.Format("https://api.trello.com/1/boards/{0}/lists?key={1}&token={2}", Id, ConnectionInfo.Key, ConnectionInfo.Token)).Result;

                using (var jsonReader = new JsonReader(data))
                {
                    var serializer = new BsonArraySerializer();
                    try
                    {
                        BsonArray lists = serializer.Deserialize(BsonDeserializationContext.CreateRoot(jsonReader));
                        foreach (var it in lists)
                        {
                            BsonDocument item = it.AsBsonDocument;
                            var name = item.GetValue("name").AsString;
                            var id = item.GetValue("id").AsString;
                            var list = new List(name, id, this);
                            Lists.Add(name, list);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        throw;
                    }
                }

                //Sort cards into lists
                // TODO: Make asyncronous and have events
                string uri = String.Format("https://api.trello.com/1/search?query=member:{0}%20board:{1}%20is:open%20sort:edited&card_fields=name,shortLink,idList&cards_limit=100&key={3}&token={4}", m_member.Id, Id, m_name, ConnectionInfo.Key, ConnectionInfo.Token);
                data = http.GetStringAsync(uri).Result;

                try
                {
                    BsonArray cards = BsonDocument.Parse(data).GetValue("cards").AsBsonArray;
                    foreach (var it in cards)
                    {
                        BsonDocument item = it.AsBsonDocument;
                        var name = item.GetValue("name").AsString;
                        var id = item.GetValue("id").AsString;
                        var idList = item.GetValue("idList").AsString;
                        List currentList = Lists.Where(z => z.Value.Id == idList).FirstOrDefault().Value;
                        Card newCard = new Card(id, name, currentList, this);
                        Cards.Add(id, newCard);
                        currentList.Cards.Add(newCard);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        public override string ToString()
        {
            return String.Format("Trello board, named {0} containing {1} lists", m_name, Lists.Count);
        }
    }
}
