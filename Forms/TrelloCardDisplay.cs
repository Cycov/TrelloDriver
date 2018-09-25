using System;
using System.Diagnostics;
using System.Windows.Forms;
using TrelloDriver.Components;

namespace TrelloDriver.Forms
{
    public partial class TrelloCardDisplay : Form
    {
        public Card Card;
        protected TrelloConnectionInfo connectionInfo;

        public TrelloCardDisplay(string id) : this(new Card(id)) { }
        public TrelloCardDisplay(Card card)
        {
            InitializeComponent();
            Card = card;
        }

        public static DialogResult ShowDialog(string id, string title, TrelloConnectionInfo connectionInfo)
        {
            TrelloCardDisplay cardDisplay = new TrelloCardDisplay(id);
            cardDisplay.Text = title;
            cardDisplay.connectionInfo = connectionInfo;
            return cardDisplay.ShowDialog();
        }
        public static DialogResult ShowDialog(Card card, string title, TrelloConnectionInfo connectionInfo)
        {
            TrelloCardDisplay cardDisplay = new TrelloCardDisplay(card);
            cardDisplay.Text = title;
            cardDisplay.connectionInfo = connectionInfo;
            return cardDisplay.ShowDialog();
        }

        private void TrelloCardDisplay_Load(object sender, EventArgs e)
        {
            idTb.Text = Card.Id;
            nameTb.Text = Card.Name;
            listIdTb.Text = Card.ParentList.Id;
            boardIdTb.Text = Card.ParentBoard.Id;
            shortLinkTb.Text = Card.ShortLink;
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = Card.Url;
            cardUrl.Links.Add(link);
            cardUrl.Text = Card.Url;
            foreach (var member in Card.Members)
            {
                listBox1.Items.Add(member.Value.FullName + " (id:" + member.Key + ")");
            }
        }

        private void cardUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }
}
