using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;

namespace FinalProject
{
    public partial class Verify : Form
    {
        private String _username;
        private String _email;
        private String _password;
        private Login _login;
        private String _connectionString;

        private static String _code;

        public Verify(String username, String email, String password, Login login, String connectionString)
        {
            _username = username;
            _email = email;
            _password = password;
            _login = login;
            _connectionString = connectionString;
            InitializeComponent();
            generateCode();
            sendCode();
        }

        // Generates the random integer to be sent to the user
        public static void generateCode()
        {
            Random rand = new Random();
            _code = Convert.ToString(rand.Next(100000, 1000000));
        }

        private void verCodeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        // Sends the verification code to the user's registered email
        private void sendCode()
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("therealcardsystem@gmail.com");
            msg.To.Add(_email);
            msg.Subject = "Verification Code";
            msg.Body = "Verification Code: " + _code;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("therealcardsystem@gmail.com", "18241918");
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
                Console.WriteLine("Mail has been successfully sent!");
            }
            catch (Exception ex)
            {

                Console.WriteLine("Fail Has error" + ex.Message);
            }
            finally
            {
                msg.Dispose();
            }
        }

        // if the user has inputted the correct verification code, CARD is opened with the user's information
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
