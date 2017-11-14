using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Collection
    {
        private List<Item> _collection;
        private double _totalValue;

        public Collection()
        {
            _collection = new List<Item>();
        }

        public Collection(Collection col) { _collection = col.getCollection(); calcTotalValue(); }

        public List<Item> getCollection() { return _collection; }
        public double getTotalValue() { return _totalValue; }
        // returns the Item object associated with the given name
        public Item getItem(String name)
        {
            Item toReturn = null;
            foreach (Item item in _collection)
            {
                if (item.getName().Equals(name))
                {
                    toReturn = item;
                }
                break;
            }
            return toReturn;
        }
        public void AddItem(Item item) { _collection.Add(item); calcTotalValue(); }
        public void RemoveItem(Item item)
        {
            foreach (Item eachitem in _collection)
            {
                if (item.getName().Equals(eachitem.getName()))
                {
                    _collection.Remove(eachitem);
                }
                break;
            }
            calcTotalValue();
        }
        public void RemoveItems(Item item)
        {
            foreach (Item eachitem in _collection)
            {
                if (item.getName().Equals(eachitem.getName()))
                {
                    _collection.Remove(eachitem);
                }
            }
            calcTotalValue();
        }
        public void calcTotalValue()
        {
            double total = 0;
            foreach (Item eachitemm in _collection)
            {
                total += eachitemm.getPrice();
            }
            _totalValue = total;
        }
    }
}
