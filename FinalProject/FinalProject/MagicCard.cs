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
        public MagicCard(String name, String desc, String category, String condition, double price)
            :base(name, desc, category, condition, price)
        {
            _name = name;
            _desc = desc;
            _category = category;
            _condition = condition;
            _price = price;
            _color = new String[5];
        }

        public string Name { get => _name; set => _name = value; }
        public string Desc { get => _desc; set => _desc = value; }
        public string Category { get => _category; set => _category = value; }
        public string Condition { get => _condition; set => _condition = value; }
        public double Price { get => _price; set => _price = value; }
        public string Set { get => _set; set => _set = value; }
        public string[] Color { get => _color; set => _color = value; }
        public bool IsMultiColor { get => _isMultiColor; set => _isMultiColor = value; }
        public string Power { get => _power; set => _power = value; }
        public string Strength { get => _strength; set => _strength = value; }
        public string Type { get => _type; set => _type = value; }
        public string ReleaseYear { get => _releaseYear; set => _releaseYear = value; }
    }
}
