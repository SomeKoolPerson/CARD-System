using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class User
    {
        private String _username;
        private String _password;
        private String _email;
        private Collection _collection;
        public User(String name, String password, String email)
        {
            _username = name;
            _password = password;
            _email = email;
            _collection = new Collection();
        }
        public User(String name, String password, String email, Collection collection)
        {
            _username = name;
            _password = password;
            _email = email;
            _collection = collection;
        }

        public void setUsename(String username) { _username = username; }
        public void setEmail(String email) { _email = email; }
        public void setPassword(String password) { _password = password; }
        public void setCollection(Collection col) { _collection = col; }

        public String getUsername() { return _username; }
        public String getEmail() { return _email; }
        public String getPassword() { return _password; }
        public Collection GetCollection() { return _collection; }
    }
}
