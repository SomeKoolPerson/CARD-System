using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Verify : Form
    {
        private String _username;
        private String _email;
        private String _password;
        private Login _login;
        private String _connectionString;

        private String _code = "666";

        public Verify(String username, String email, String password, Login login, String connectionString)
        {
            _username = username;
            _email = email;
            _password = password;
            _login = login;
            _connectionString = connectionString;
            InitializeComponent();
        }

        private void verCodeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            bool isValidCode = verCodeTextBox.Text.Equals(_code);
            if (isValidCode)
            {
                
                CARD card = new CARD(_username, _email, _password, _login, _connectionString);
                card.Show();
                this.Close();
            }
        }
    }
}
