using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace FinalProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*String _connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\beklybor\Documents\Semester 5 Fall 2017\CS_275\Final Project\FinalProject\CARD.accdb";
            OleDbConnection conn = new OleDbConnection(_connectionString);
            OleDbCommand getUser1 = conn.CreateCommand(); // if an email is entered
            OleDbCommand getUser2 = conn.CreateCommand(); // if a username is entered

            getUser1 = new OleDbCommand("SELECT Username FROM Users WHERE Users.Email = 'bklybor@gmail.com' AND Users.[Password] = '12345'", conn);
            conn.Open();
            OleDbDataReader reader = getUser1.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader[0].ToString());
            }
            String _username = getUser1.ExecuteScalar().ToString();
            Console.WriteLine(_username);
            conn.Close();*/

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
