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
        private int _count;
        private List<Tuple<String, double>> changeLog;

        public Item(String name, String desc, String condition, String category, double price, int count)
        {
            _name = name;
            _desc = desc;
            _condition = condition;
            _category = category;
            _price = price;
            _count = count;
        }
        public String getName() { return _name; }
        public String getDesc() { return _desc; }
        public String getCondition() { return _condition; }
        public String getCategory() { return _category; }
        public double getPrice() { return _price; }
        public List<Tuple<String, double>> getLog() {return changeLog;}
        public int getCount() { return _count; }
        public void setCount(int count) { _count =count; }
        public void setName(String name) { _name = name; }
        public void setDesc(String desc) {_desc = desc;}
        public void setPrice(double price) { _price = price; }
        public void addChange(Tuple<String, double> change) { changeLog.Add(change); }

        public void open_view()
        {
            View_Item view = new View_Item(this);
            view.Show();
        }

        public void IncrementCount()
        {
            _count++; 
        }
        public void DecrementCount()
        {
            _count--;
        }
    }
}
