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
            string cardhtml = web.DownloadString("https://deckbox.org/games/mtg/cards");
            MatchCollection m1 = Regex.Matches(cardhtml, @"<a class='simple' href=(.+?)</tr>", RegexOptions.Singleline);

            for (int i = 1; i < m1.Count - 1; i++)
            {
                string card = m1[i].Groups[1].Value;
                cards.Add(card);
            }


            for (int i = 0; i < cards.Count; i++)
            {
                //string name = Parse(cards[i], "target='_blank'>", "</a>");
                //names.Add(name);
                Console.WriteLine(cards[i]);
                //string price = Parse(cards[i], "<td class='center minimum_width'>", "</td>");
                //prices.Add(price);
                //string type = Parse(cards[i], "<td>", "</td><td class='center'>");
                //types.Add(type);
                //string link = Parse(cards[i], "href='", "'");
                //links.Add(link);
                //cards[i] = name + " - "+ price;

            }

            listBox1.DataSource = cards;
        }
    }
}
