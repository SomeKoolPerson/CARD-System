using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class MagicCard: Item
    {
        private String _name;
        private String _desc;
        private String _category;
        private String _condition;
        private double _price;
        private String _set;
        private String[] _color;
        private bool _isMultiColor;
        private String _power;
        private String _strength;
        private String _type;
        private String _releaseYear;
        public MagicCard(String name, String desc, String category, String condition, double price, int count)
            :base(name, desc, category, condition, price, count)
        {
            _name = name;
            _desc = desc;
            _category = category;
            _condition = condition;
            _price = price;
            _color = new String[5];
        }

        public String getSet() { return _set; }
    }
}
