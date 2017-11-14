using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Item
    {
        private String _name;
        private String _desc;
        private double _price;
        private String _condition;
        private String _category;

        public Item(String name, String desc, String condition, String category, double price)
        {
            _name = name;
            _desc = desc;
            _condition = condition;
            _category = category;
            _price = price;
        }
        public String getName() { return _name; }
        public String getDesc() { return _desc; }
        public String getCondition() { return _condition; }
        public String getCategory() { return _category; }
        public double getPrice() { return _price; }

    }
}
