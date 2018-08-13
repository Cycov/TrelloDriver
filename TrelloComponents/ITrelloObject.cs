using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrelloDriver.Components
{
    interface ITrelloObject
    {
        event EventHandler OnInitialised;
        string Id
        {
            get;
        }
    }
}
