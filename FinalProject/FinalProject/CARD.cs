using System;
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

            textBox1.Text = _username;
            textBox2.Text = _email;
            textBox3.Text = _password;
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

        // change username and update database with new username
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Enabled == true)
            {
                CreateNewAccount create = new CreateNewAccount(_connectionString, _login);
                create.Visible = false;
                if (!_username.Equals(textBox1.Text)) {
                    if (create.validUsername(textBox1.Text))
                    {
                        OleDbConnection conn = new OleDbConnection(_connectionString);
                        OleDbCommand updateUsername = conn.CreateCommand();
                        conn.Open();
                        updateUsername = new OleDbCommand("UPDATE [Users] SET [Username]='" + textBox1.Text + "' WHERE [Username]='" + _username + "'", conn);
                        updateUsername.ExecuteScalar();
                        OleDbCommand updateCollection = conn.CreateCommand();
                        updateCollection = new OleDbCommand("UPDATE [Collection] SET [Username]='" + textBox1.Text + "' WHERE [Username]='" + _username + "'", conn);
                        updateCollection.ExecuteScalar();
                        conn.Close();
                        _username = textBox1.Text;
                        _user.setUsername(_username);
                    }
                }
                textBox1.Enabled = false;
                button10.Enabled = true;
                button11.Enabled = true;
                button5.Text = "Change";
            }
            else
            {
                textBox1.Enabled = true;
                button5.Text = "Confirm";
                button10.Enabled = false;
                button11.Enabled = false;
            }

        }

        // change email and update database with new email
        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox2.Enabled == true)
            {
                CreateNewAccount create = new CreateNewAccount(_connectionString, _login);
                create.Visible = false;
                if (create.validEmail(textBox2.Text))
                {
                    OleDbConnection conn = new OleDbConnection(_connectionString);
                    OleDbCommand updateUsername = conn.CreateCommand();
                    conn.Open();
                    updateUsername = new OleDbCommand("UPDATE [Users] SET [Email]='" + textBox2.Text + "' WHERE [Username]='" + _username + "'", conn);
                    updateUsername.ExecuteScalar();
                    conn.Close();
                    _email = textBox2.Text;
                    _user.setEmail(_email);
                    textBox2.Enabled = false;
                    button5.Enabled = true;
                    button11.Enabled = true;
                    button10.Text = "Change";
                }
            }
            else
            {
                textBox2.Enabled = true;
                button10.Text = "Confirm";
                button5.Enabled = false;
                button11.Enabled = false;
            }
        }

        // change password and update database with new password
        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox3.Enabled == true)
            {
                CreateNewAccount create = new CreateNewAccount(_connectionString, _login);
                create.Visible = false;
                if (create.validPassword(textBox3.Text))
                {
                    OleDbConnection conn = new OleDbConnection(_connectionString);
                    OleDbCommand updateUsername = conn.CreateCommand();
                    conn.Open();
                    updateUsername = new OleDbCommand("UPDATE [Users] SET [Password]='" + textBox3.Text + "' WHERE [Username]='" + _username + "'", conn);
                    updateUsername.ExecuteScalar();
                    conn.Close();

                    _password = textBox3.Text;
                    _user.setPassword(_password);
                    textBox3.Enabled = false;
                    button5.Enabled = true;
                    button10.Enabled = true;
                    button11.Text = "Change";
                }
            }
            else
            {
                textBox3.Enabled = true;
                button11.Text = "Confirm";
                button5.Enabled = false;
                button10.Enabled = false;
            }
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
