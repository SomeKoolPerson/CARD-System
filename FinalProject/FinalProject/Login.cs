using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Login : Form
    {
        List<User> database = new List<User>();
        private String _connectionString;

        private String _username;
        private String _email;
        private String _password;

        public Login()
        {
            // The commented out code below is to be used to generate "dummy" users with "dummy" collections. Only run if database is clear.
            /*OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\beklybor\Documents\Semester 5 Fall 2017\CS_275\Final Project\FinalProject\CARD.accdb");
            OleDbCommand addUsers = conn.CreateCommand();

            // for simulating a database
            for (int i = 0; i < 20; i++)
            {
                addUsers.CommandText = "INSERT INTO Users (Username, Email, [Password]) VALUES('User " + i + "', 'User_" + i + "@CARD.com', '200000" + i + "')";
                addUsers.CommandType = CommandType.Text;
                conn.Open();

                addUsers.ExecuteNonQuery();
                conn.Close();
            }
            OleDbCommand addItems = conn.CreateCommand();
            for (int i=0;i<20;i++) {
                for (int j = 0; j < 30; j++)
                {
                    addItems.CommandText = "INSERT INTO Collection (Username, ItemName, Description, Category, Condition, Price) VALUES('User " + i + "', 'Card " + j + "', 'The best that ever was.', 'Magic: The Gathering Card', 'New', '10.00')";
                    addItems.CommandType = CommandType.Text;
                    conn.Open();

                    addItems.ExecuteNonQuery();
                    conn.Close();
                }
            }*/
            _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\nitulio\Documents\Final Project SE\the older one\FinalProject\CARD.accdb";
            InitializeComponent();
        }

        private void userName_Click(object sender, EventArgs e)
        {

        }

        private void userBox_TextChanged(object sender, EventArgs e)
        {

        }

        // returns true if we can 1.) find the user by either name or email in the database and 2.) the inputted password
        // matches that in the database
        private bool validateUser()
        {
            // Do something here to find the user in the database and fetch the missing information (uername or email)
            // If username or email can't be found in database, set the appropriate value to empty string

            OleDbConnection conn = new OleDbConnection(_connectionString);
            OleDbCommand getUser1 = conn.CreateCommand(); // if an email is entered
            OleDbCommand getUser2 = conn.CreateCommand(); // if a username is entered

            if (userBox.Text.Contains("@"))
            {
                _email = userBox.Text;
                _username = "";
                _password = textBox1.Text;
                try
                {
                    conn.Open();
                    // searches the database for an entry with the correct email and password and retrieves the associated username
                    getUser1 = new OleDbCommand("SELECT Username FROM Users WHERE Users.Email = '" + _email+ "' AND Users.[Password] = '" + _password + "'", conn);
                    _username = getUser1.ExecuteScalar().ToString();
                    //Console.WriteLine(_username);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            } else
            {
                _username = userBox.Text;
                _email = "";
                _password = textBox1.Text;
                try
                {
                    conn.Open();
                    // searches the database for an entry with the correct username and password and retrieves the associated email
                    getUser2 = new OleDbCommand("SELECT Email FROM Users WHERE Users.Username = '" + _username + "' AND Users.Password = '" + _password  + "'", conn);
                    _email = getUser2.ExecuteScalar().ToString();
                    // Console.WriteLine(_email);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (validateUser())
            {
                Verify verify = new Verify(_username,_email,_password,this, _connectionString);
                verify.Show();
                this.Hide();
            }
            else
            {
                label1.Visible = true;
                label1.Enabled = true;
            }
        }

        public void clear()
        {
            userBox.Clear();
            textBox1.Clear();
        }

        private void passwordLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Enabled = false;
            label1.Visible = false;
        }

        private void createAccButton_Click(object sender, EventArgs e)
        {
            CreateNewAccount acc = new CreateNewAccount(_connectionString, this);
            acc.Show();
            this.Hide();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
