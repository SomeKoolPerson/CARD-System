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
using System.Data.OleDb;
using FinalProject.Properties;

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
        private int _count;
        private Collection _collection;
        private CARD _card;
        private Button _button;
        private User _user;
        private String _connectionString;
        public AddItem(User user, CARD card, Button button, String connectionString)
        {
            _card = card;
            _collection = user.GetCollection();
            _button = button;
            _connectionString = connectionString;
            _user = user;
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
            //_name = _name.Replace(' ', '_');
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
            Boolean inCollection = false;
            _count = Int32.Parse(textBox3.Text);
            List<Item> items = _collection.getCollection();
            foreach (Item item in items)
            {
                if(item.getName().Equals(_name))
                {
                    inCollection = true;
                }
            }
            // when the item is already in the collection
            if (inCollection == true)
            {
                _name = textBox1.Text;
                //_name = _name.Replace(' ', '_');
                string userName = _user.getUsername();
                int addNumber;
                try
                {
                    addNumber = Int32.Parse(textBox3.Text);
                }
                catch
                {
                    addNumber = 1;
                }
                foreach (Item item in items)
                {
                    Console.WriteLine(item.getName());
                }
                Item temp = _collection.getItem(_name);
                temp.IncrementCount();
                OleDbConnection conn = new OleDbConnection(_connectionString);
                OleDbCommand increment = conn.CreateCommand();
                conn.Open();
                increment = new OleDbCommand("SELECT Count FROM Collection WHERE Username = '" + _user.getUsername() + "' AND ItemName = '" + _name + "'", conn);
                OleDbDataReader reader = increment.ExecuteReader();
                reader.Read();
                int count = Int32.Parse(reader[0].ToString());
                count += addNumber;
                reader.Close();
                OleDbCommand repush = conn.CreateCommand();
                repush = new OleDbCommand("UPDATE [Collection] SET [Count]='" + count + "' WHERE Username='" + _user.getUsername() + "' AND ItemName='" + _name + "'", conn);
                repush.ExecuteScalar();
                conn.Close();
                _collection.getItem(_name).setCount(count);
                
                _collection.addToTotal(_collection.getItem(_name).getPrice());
                _collection.getItem(_name).setName(_name.Replace('_', ' '));
                _card.calcTotValue();
                _card.initializeCollection();
                this.Close();

            }
            else // when adding a new item
            {
                _name = _name.Replace(' ','_');
                MakeMTGCard make = new MakeMTGCard(_name, _set, _desc, _condition, _category, _count);
                string price = make.pullPrice();
                //validates that a price was found and throws a spelling error if not
                if (price.Equals(""))
                {
                    label7.Visible = true;
                    label8.Visible = true;
                }
                else
                {
                    MagicCard card = make.Make(Convert.ToDouble(price));
                    _price = card.getPrice();
                    _name = _name.Replace('_', ' ');
                    card.setName(_name);
                    _collection.AddItem(card);
                    _card.initializeCollection();
                    _card.calcTotValue();
                    //_collection.getItem(_name).setName(_name.Replace('_', ' '));
                    string username = _user.getUsername();
                    OleDbConnection conn = new OleDbConnection(_connectionString);
                    OleDbCommand addItem = conn.CreateCommand();
                    conn.Open();
                    addItem = new OleDbCommand("INSERT INTO [Collection] ([Username], [ItemName], [Description], [Category], [Condition], [Price], [Count]) VALUES('" + username + "', '" + _name + "', '" + _desc + "', '" + _category + "', '" + _condition + "', '" + _price + "', '" + _count + "')", conn);
                    addItem.ExecuteScalar();
                    conn.Close();
                    this.Close();

                }
   
            }
            
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
            _set = _set.Replace(' ', '_');
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        public Label getLabel7() { return label7; }
        public Label getLabel8() { return label8; }

    }


}
