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
using System.Net.Mail;

namespace FinalProject
{
    public partial class CreateNewAccount : Form
    {
        private String _connectionString;
        private Login _login;

        private String _username;
        private String _email;
        private String _password;

        private bool moveOn = false;

        public CreateNewAccount(String connectionString, Login login)
        {
            _connectionString = connectionString;
            _login = login;

            InitializeComponent();
        }

        // username text box
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // email text box
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }
        
        // password text box
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        // check username availability
        private void button1_Click(object sender, EventArgs e)
        {
            if (availableUsername())
            {
                label9.Visible = true;
                label9.Text = "Unavailable";
            }
            else
            {
                label9.Visible = true;
                label9.Text = "Available";
            }
        }

        // checks to see if the current inputted username already exists in the database
        private bool availableUsername()
        {
            bool toReturn = false;
            String username = textBox1.Text;
            OleDbConnection conn = new OleDbConnection(_connectionString);
            OleDbCommand findExistingUser = conn.CreateCommand();
            try
            {
                conn.Open();
                findExistingUser = new OleDbCommand("SELECT Username FROM Users WHERE Users.Username ='" + username + "'", conn);
                String username2 = findExistingUser.ExecuteScalar().ToString();
                if (!username2.Equals(""))
                {
                    toReturn =  true;
                }
                conn.Close();
            }
            catch
            {

            }

            return toReturn;
        }

        // checks to see if email is available
        private bool availableEmail()
        {
            bool toReturn = false;
            OleDbConnection conn = new OleDbConnection(_connectionString);
            OleDbCommand findExistingEmail = conn.CreateCommand();
            try
            {
                conn.Open();
                String email = textBox2.Text;
                findExistingEmail = new OleDbCommand("SELECT Email FROM Users WHERE Users.Email ='" + email + "'", conn);
                String email2 = findExistingEmail.ExecuteScalar().ToString();
                if (!email2.Equals(""))
                {
                    toReturn = true;
                    label11.Visible = true;
                }
                conn.Close();
            }
            catch
            {
                label11.Visible = false;
            }

            return toReturn;
        }

        // checks to see if username is in proper format and available
        private bool validUsername()
        {
            String username = textBox1.Text;
            bool containsAt = username.Contains('@');
            bool available = availableUsername();
            if (!available && !containsAt)
            {
                _username = username;
                label6.Visible = false;
                return true;
            }
            else
            {
                label6.Visible = true;
                return false;
            }
            
        }

        // checks to see if password is in proper format: contains at least one digit, one uppercase letter, and is 
        // longer than six characters
        private bool validPassword()
        {
            String password = textBox3.Text;
            bool containsUpper = password.Any(c => char.IsUpper(c));
            bool containsInt = password.Any(c => char.IsDigit(c));
            bool isProperLength = password.Length >= 6;
            bool isValid = containsInt && containsUpper && isProperLength;
            if (isValid)
            {
                label8.Visible = false;
                return true;
            }
            else
            {
                label8.Visible = true;
                return false;
            }
        }

        // checks to see if email is in proper format and available
        private bool validEmail()
        {
            string email = textBox2.Text;
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email; //&& availableEmail();
            }
            catch
            {
                return false;
            }
        }

        // checks to see if username, email, and password ar valid. adds new user to database and opens
        // CARD with the new user information
        private void button2_Click(object sender, EventArgs e)
        {
            bool validName = validUsername();
            bool validEm = validEmail();
            bool validPass = validPassword();
            if (validName && validEm && validPass)
            {
                _username = textBox1.Text;
                _email = textBox2.Text;
                _password = textBox3.Text;
                OleDbConnection conn = new OleDbConnection(_connectionString);
                OleDbCommand addUser = conn.CreateCommand();
                conn.Open();
                addUser = new OleDbCommand("INSERT INTO  Users (Username, Email, [Password]) VALUES('" + _username+ "','" + _email + "','" + _password + "')",conn);
                addUser.ExecuteScalar();
                conn.Close();
                CARD card = new CARD(_username, _email, _password, _login, _connectionString);
                card.Show();
                moveOn = true;
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _login.Show();
            this.Close();
        }

        private void CreateNewAccount_Closed(object sender, FormClosedEventArgs e)
        {
            if (!moveOn) {
                _login.Show();
            }
        }

    }
}
