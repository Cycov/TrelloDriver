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

namespace TrelloDriver
{
    public class TrelloConnectionInfo : IDisposable
    {
        public string Token { get; protected set; }
        public string Key { get; protected set; }
        public string UserId { get; protected set; }

        public TrelloConnectionInfo(string token, string key)
        {
            Token = token;
            Key = key;
        }
        public TrelloConnectionInfo(string token, string key, string userId)
        {
            Token = token;
            Key = key;
            UserId = userId;
        }

        public void Dispose()
        {
            Token = null;
            Key = null;
            UserId = null;
        }
    }
}
