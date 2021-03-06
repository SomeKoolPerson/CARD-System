﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace FinalProject
{
    public partial class CARD : Form
    {

        private List<ListViewItem>  fullItems= new List<ListViewItem>();
        private String _username = "username";
        private String _email = "email";
        private String _password = "password";

        private String _connectionString;

        private List<String> _categories;

        private User _user;
        private Collection _collection;
        private Login _login;
        private Verify _verify;

        public User getUser()
        {
            return _user;
        }

        public CARD(String username, String email, String password, Login login, String connectionString)
        {
            _login = login;
            _connectionString = connectionString;
            _username = username;
            _email = email;
            _password = password;
            _user = new User(username, password, email);
            _collection = new Collection();

            pullCollection();
            _user.setCollection(_collection);
            InitializeComponent();
            initializeCollection();
            calcTotValue();
            initializeAccount();

        }

        public void pullCollection()
        {
            OleDbConnection conn = new OleDbConnection(_connectionString);
            OleDbCommand getCollection = conn.CreateCommand();
            getCollection = new OleDbCommand("SELECT ItemName, Description, Category, Condition, Price, Count FROM Collection WHERE Collection.Username = '" + _username + "'", conn);
            conn.Open();
            OleDbDataReader reader = getCollection.ExecuteReader();

            while (reader.Read())
            {
                String itemName = reader[0].ToString();
                // Console.WriteLine(itemName);
                String description = reader[1].ToString();
                String category = reader[2].ToString();
                String condition = reader[3].ToString();
                String sPrice = reader[4].ToString();
                double price = Convert.ToDouble(sPrice);
                int count = Int32.Parse(reader[5].ToString());

                Item item = new Item(itemName, description, condition, category, price, count);

                _collection.AddItem(item);
            }
            conn.Close();
        }

        // get the Collection associated with the current user
        public void initializeCollection() {
            List<Item> col = _collection.getCollection();
            listView3.Items.Clear();
            listView1.Items.Clear();
            foreach (Item eachitem in col)
            {
                ListViewItem fullitem = new ListViewItem(eachitem.getName());
                fullitem.SubItems.Add(eachitem.getDesc());
                fullitem.SubItems.Add("$" + Convert.ToString(eachitem.getPrice()));
                fullitem.SubItems.Add(Convert.ToString(eachitem.getCount()));
                listView3.Items.Add(fullitem);

                ListViewItem shortItem = new ListViewItem(eachitem.getName());
                shortItem.SubItems.Add("$" + Convert.ToString(eachitem.getPrice()));
                shortItem.SubItems.Add(Convert.ToString(eachitem.getCount()));
                listView1.Items.Add(shortItem);
            }
            listView3.Columns[0].TextAlign = HorizontalAlignment.Left;
            listView3.Columns[1].TextAlign = HorizontalAlignment.Left;
            listView3.Columns[2].TextAlign = HorizontalAlignment.Left;
            listView3.Columns[3].TextAlign = HorizontalAlignment.Left;
        }

        private void initializeAccount()
        {
            _username = _user.getUsername();
            _email = _user.getEmail();
            _password = _user.getPassword();
            label7.Text = _username ;
            label8.Text = _email;
            label9.Text = _password;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        // removes selected items from the collection
        private void button9_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = listView3.SelectedItems;
            foreach (ListViewItem eachitem in items){
                String itemName = eachitem.Text;
                OleDbConnection conn = new OleDbConnection(_connectionString);
                conn.Open();
                OleDbCommand find = new OleDbCommand("SELECT Count FROM  Collection WHERE Username ='" + _username + "' AND ItemName='" + itemName + "'",conn);
                OleDbDataReader reader = find.ExecuteReader();
                reader.Read();
                int count = Int32.Parse(reader[0].ToString());
                if (count>1)
                {
                    _collection.getItem(itemName).DecrementCount();
                    count--;
                    OleDbCommand repush = conn.CreateCommand();
                    repush = new OleDbCommand("UPDATE [Collection] SET [Count]='" + count + "' WHERE Username='" + _user.getUsername() + "' AND ItemName='" + itemName + "'", conn);
                    repush.ExecuteScalar();
                    reader.Close();
                }
                else
                {
                    _collection.RemoveItem(itemName);
                    OleDbCommand remove = conn.CreateCommand();
                    remove = new OleDbCommand("DELETE FROM Collection Where Username='" + _username + "' AND ItemName= '" + itemName + "'", conn);
                    remove.ExecuteScalar();
                    reader.Close();
                }
            }
            initializeCollection();
            calcTotValue();

        }

        public void calcTotValue() {
            _collection.calcTotalValue();
            double totalValue = _collection.getTotalValue();
            label5.Text = "$" + Convert.ToString(totalValue);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        // searches the collection for the item with the entered name and only displays the item with the same name
        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            ListView.ListViewItemCollection temp = listView3.Items;
            if (e.KeyCode == Keys.Enter)
            {
                if (!textBox4.Text.Equals(""))
                {
                    foreach (ListViewItem eachItem in listView3.Items)
                    {
                        if (!textBox4.Text.Equals(listView3.Items[eachItem.Index].SubItems[0].Text))
                        {
                            listView3.Items.Remove(eachItem);
                        }
                    }
                    // calcTotValue();
                }
                else
                {
                    listView3.Items.Clear();
                    foreach (ListViewItem item in temp)
                    {
                        listView3.Items.Add(item);
                    }
                }
            }
        }

        // change username
        private void button5_Click(object sender, EventArgs e)
        {
            button10.Enabled = false;
            button11.Enabled = false;
            label1.Text = "New Username:";
            label7.Visible = false;
            textBox1.Visible = true;
            textBox1.Enabled = true;
            textBox1.Text = label7.Text;
            button5.Visible = false;
            button5.Enabled = false;
            button6.Visible = true;
            button6.Enabled = true;
        }
        // confirm username
        private void button6_Click(object sender, EventArgs e)
        {
            label1.Text = "Username:";
            _username = textBox1.Text;
            label7.Text = _username;
            label7.Visible = true;
            textBox1.Visible = false;
            textBox1.Enabled = false;
            textBox1.Text = "";
            button5.Visible = true;
            button5.Enabled = true;
            button6.Visible = false;
            button6.Enabled = false;
            button10.Enabled = true;
            button11.Enabled = true;
        }
        // change email
        private void button10_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            button11.Enabled = false;
            label2.Text = "New Email:";
            label8.Visible = false;
            textBox2.Visible = true;
            textBox2.Enabled = true;
            textBox2.Text = label8.Text;
            button10.Visible = false;
            button10.Enabled = false;
            button12.Visible = true;
            button12.Enabled = true;
        }
        // confirm email
        private void button12_Click(object sender, EventArgs e)
        {
            label2.Text = "Email:";
            _email = textBox2.Text;
            label8.Text = _email;
            label8.Visible = true;
            textBox2.Visible = false;
            textBox2.Enabled = false;
            textBox2.Text = "";
            button10.Visible = true;
            button10.Enabled = true;
            button12.Visible = false;
            button12.Enabled = false;
            button5.Enabled = true;
            button11.Enabled = true;
        }
        // change password
        private void button11_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            button10.Enabled = false;
            label3.Text = "New Password:";
            label9.Visible = false;
            textBox3.Visible = true;
            textBox3.Enabled = true;
            textBox3.Text = label9.Text;
            button11.Visible = false;
            button11.Enabled = false;
            button13.Visible = true;
            button13.Enabled = true;

        }
        // confirm password
        private void button13_Click(object sender, EventArgs e)
        {
            label3.Text = "Password:";
            _password = textBox3.Text;
            label9.Text = _password;
            label9.Visible = true;
            textBox3.Visible = false;
            textBox3.Enabled = false;
            textBox3.Text = "";
            button11.Visible = true;
            button11.Enabled = true;
            button13.Visible = false;
            button13.Enabled = false;
            button5.Enabled = true;
            button10.Enabled = true;
        }
        /**
        // adds a new item to the collection
        private void button7_Click(object sender, EventArgs e)
        {
            AddItem addItem = new AddItem(_user.GetCollection(), this, button7);
            addItem.Show();
            button7.Enabled = false;
        }*/
        private void CARD_Closed(object sender, FormClosedEventArgs e)
        {
            _login.clear();
            _login.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddItem AddItem = new AddItem(_user,this, button7, _connectionString);
            AddItem.Show();
            button7.Enabled = false;
        }

        private void CARD_Load(object sender, EventArgs e)
        {

        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
        // updates the prices of the items in the collection in the Collection object and on the database
        private void button2_Click(object sender, EventArgs e)
        {
            /**List<Item> col = _collection.getCollection();
            MakeMTGCard make = new MakeMTGCard();
            foreach (Item item in col)
            {
                if (item.getCategory().Equals("Magic: The Gathering Card"))
                {
                    
                }
            }*/
        }

        private void button14_Click(object sender, EventArgs e)
        {
            String name = listView3.SelectedItems[0].SubItems[0].Text;
            Item item = _collection.getItem(name);
            item.open_view();
        }

        //Opens View Item
        /*private void listView3_SelectedIndexChanged(object sender, EventArgs e)
         {
             //only one item at a time can be selected
             listView3.MultiSelect = false;
             string name = listView3.SelectedItems[0].SubItems[0].Text;
             //search collection for item and retrieve it
             Item item = _collection.getItem(name);
             item.open_view();

         }*/
    }
}
