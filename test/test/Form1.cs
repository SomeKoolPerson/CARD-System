using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace test
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        private string Parse(string text, string before, string after)
        {
            int start = text.LastIndexOf(before) + before.Length;
            int end = text.IndexOf(after);
            int length = end - start;
            return text.Substring(start, length);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> cards = new List<string>();
            List<string> names = new List<string>();
            List<string> prices = new List<string>();
            List<string> links = new List<string>();
            List<string> types = new List<string>();
            WebClient web = new WebClient();
            string cardhtml = web.DownloadString("https://deckbox.org/games/mtg/cards?f=g5MC4wMQ**");
            MatchCollection m1 = Regex.Matches(cardhtml, @"<td class='card_name'>(.+?)r>", RegexOptions.Singleline);

            for (int i = 0; i < m1.Count; i++)
            {
                string card = m1[i].Groups[1].Value;
                cards.Add(card);
            }

            Console.WriteLine(cards[0]);

            for (int i = 0; i < cards.Count; i++)
            {
                string link = Parse(cards[i], "<a class='simple' href='", "' target='_blank'>");
                links.Add(link);
                string name = Parse(cards[i], "target='_blank'>", "</a>");
                names.Add(name);
                string price = Parse(cards[i], "<td class='center minimum_width'>", "</td>\n</t");
                prices.Add(price);
                string type = Parse(cards[i], "</a>\n</td>\n<td>", "</td>\n<td class='center'>");
                types.Add(type);

                cards[i] = name + " - " + type + " - " + price + " - " + link;

            }

            listBox1.DataSource = cards;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
