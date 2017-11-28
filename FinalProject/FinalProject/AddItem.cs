using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

namespace FinalProject
{
    public partial class AddItem : Form
    {
        private String _name;
        private String _desc;
        private double _price;
        private String _condition;
        private String _set;
        private String _category;
        private Collection _collection;
        private CARD _card;
        private Button _button;
        public AddItem(Collection collection, CARD card, Button button)
        {
            _card = card;
            _collection = collection;
            _button = button;
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _category = comboBox1.SelectedItem.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _name = textBox1.Text;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            _desc = richTextBox1.Text;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            _condition = comboBox4.SelectedItem.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MakeMTGCard make = new MakeMTGCard(_name, _set, _desc, _condition, _category);
            MagicCard card = make.Make();
            _collection.AddItem(card);
            _card.initializeCollection();
            _card.calcTotValue();
            _button.Enabled = true;
            this.Close();
        }
        private void AddItem_Closed(object sender, FormClosedEventArgs e)
        {
            _button.Enabled = true;
        }

        private void AddItem_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            _set = textBox2.Text;
        }
    }
}
