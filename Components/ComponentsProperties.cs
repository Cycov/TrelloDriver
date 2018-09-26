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

namespace TrelloDriver.Components
{
    public partial class Card : ITrelloObject
    {
        public List ParentList;
        public Board ParentBoard;

        public string Id { get; protected set; }
        public string ShortLink { get; protected set; }
        public string Url { get; protected set; }
        public string Description { get; protected set; }

        public string Name
        {
            get => m_name;
            set => throw new NotImplementedException();
        }
        private string m_name;

        public Dictionary<string,Member> Members
        {
            get
            {
                if (m_members == null)
                    throw new Exceptions.TrelloDriverConfigurationException("The user is not set");
                else
                    return m_members;
            }

            protected set
            {
                m_members = value;
            }
        }
        private Dictionary<string, Member> m_members;
    }

    public partial class Board : ITrelloObject
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

        public Dictionary<string, Member> Members { get; protected set; }
        public Dictionary<string, List> Lists { get; protected set; }
        public Dictionary<string, Card> Cards { get; protected set; }

        public TrelloConnectionInfo ConnectionInfo { get; protected set; }
    }
}
