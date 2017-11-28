using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace FinalProject
{
    class MakeMTGCard
    {
        private string _name;
        private string _set;
        private string _desc;
        private string _condition;
        private string _category;
        private string url;

        public MakeMTGCard(string name, string set, string description, string condition, string category)
        {
            _name = name;
            _set = set;
            _desc = description;
            _condition = condition;
            _category = category;
            url = "http://www.mtgprice.com/sets/" + _set + "/" + _name;
        }

        // returns the price of the MTG card
        private double pullPrice()
        {
            WebClient web = new WebClient();
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string html = String.Empty;
            using (StreamReader sr = new StreamReader(data))
            {
                html = sr.ReadToEnd();
            }
            String price = Parse(html, "&nbsp-&nbsp$", "&nbsp &nbsp");
            return Convert.ToDouble(price);
        }

        // finds the wanted info from website
        private String Parse(string text, string before, string after)
        {
            int start = text.LastIndexOf(before) + before.Length;
            int end = text.IndexOf(after);
            int length = end - start;
            string toReturn = text.Substring(start, length);
            Console.Out.WriteLine(toReturn);
            return toReturn;
        }

        public MagicCard Make()
        {
            double price = pullPrice();
            MagicCard card = new MagicCard(_name, _desc, _category, _condition, price);
            return card;
        }
    }
}
