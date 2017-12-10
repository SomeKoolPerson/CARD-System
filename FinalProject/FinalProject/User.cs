using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class User
    {
        private string _username;
        private string _password;
        private string _email;
        private Collection _collection;
        public User(string name, string password, string email)
        {
            _username = name;
            _password = password;
            _email = email;
            _collection = new Collection();
        }
        public User(string name, string password, string email, Collection collection)
        {
            _username = name;
            _password = password;
            _email = email;
            _collection = collection;
        }

        public void setUsename(string username) { _username = username; }
        public void setEmail(string email) { _email = email; }
        public void setPassword(string password) { _password = password; }
        public void setCollection(Collection col) { _collection = col; }

        public string getUsername() { return _username; }
        public string getEmail() { return _email; }
        public string getPassword() { return _password; }
        public Collection GetCollection() { return _collection; }
    }
}
