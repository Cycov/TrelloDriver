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
    public class Board : ITrelloObject
    {
        public string Id { get; }
        public string Name
        {
            get => m_name;
            set => throw new NotImplementedException();
        }
        private string m_name;

        public Member AssignedMember
        {
            get
            {
                if (m_member == null)
                    throw new Exceptions.TrelloDriverConfigurationException("The user is not set");
                else
                    return m_member;
            }

            protected set
            {
                m_member = value;
            }
        }
        private Member m_member;

        public Dictionary<string, List> Lists;

        public TrelloConnectionInfo ConnectionInfo { get; protected set; }

        public Board(string id, TrelloConnectionInfo connectionInfo)
        {
            Id = id;
            ConnectionInfo = connectionInfo;
            m_name = GetBoardName(id);
            Refresh();
        }
        public Board(string id, string memberId, TrelloConnectionInfo connectionInfo)
        {
            Id = id;
            ConnectionInfo = connectionInfo;
            m_name = GetBoardName(id);
            m_member = new Member(memberId, connectionInfo);
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
            Lists = new Dictionary<string, List>();
            using (HttpClient http = new HttpClient())
            {
                //Get all lists
                // TODO: Make asyncronous and have events
                string data = http.GetStringAsync(String.Format("https://api.trello.com/1/boards/{0}/lists?key={1}&token={2}", Id, ConnectionInfo.Key, ConnectionInfo.Token)).Result;

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
                        currentList.Cards.Add(new Card(id, name, currentList, this));
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
