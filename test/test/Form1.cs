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


        private void WebGet(string site, List<string> cards, int iteration)
        {
            WebClient web = new WebClient();
            string cardhtml = web.DownloadString(site);
            MatchCollection m1 = Regex.Matches(cardhtml, @"<td class='card_name'>(.+?)r>", RegexOptions.Singleline);

            for (int i = 0; i < m1.Count; i++)
            {
                string card = m1[i].Groups[1].Value;
                cards.Add(card);
            }

            for (int i = iteration; i < (iteration + 30); i++)
            {
                Console.Write(cards[i]);
                string link = Parse(cards[i], "<a class='simple' href='", "' target='_blank'>");
                string name = Parse(cards[i], "target='_blank'>", "</a>\n");
                string price = Parse(cards[i], "<td class='center minimum_width'>", "</td>\n</t");
                string type = Parse(cards[i], "</a>\n</td>\n<td>", "</td>\n<td class='center'>");

                cards[i] = name + " - " + type + " - " + price + " - " + link;

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> cards = new List<string>();

            for (int i = 1; i < 100; i++)
            {
                string url = "https://deckbox.org/games/mtg/cards?f=g5MC4wMQ%2A%2A&p=" + i.ToString();
                int iteration = ((i - 1) * 30);
                WebGet(url, cards, iteration);
            }
            
           
            listBox1.DataSource = cards;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
