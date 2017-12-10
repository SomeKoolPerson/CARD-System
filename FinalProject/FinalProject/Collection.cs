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
            foreach (Item item in _collection)
            {
                String name2 = item.getName();
                if (name2.Equals(name))
                {
                    return item;
                }
            }
            return null;
        }
        public void AddItem(Item item) { _collection.Add(item); calcTotalValue(); }
        public void RemoveItem(String itemName)
        {
            Boolean removed =_collection.Remove(getItem(itemName));
            if (removed == true)
            {
                calcTotalValue();
            } 
        }
        public void RemoveItems(String itemName)
        {
            foreach (Item eachitem in _collection)
            {
                if (getItem(itemName).getName().Equals(eachitem.getName()))
                {
                    _collection.Remove(eachitem);
                    break;
                }
            }
            calcTotalValue();
        }

        // updates the total value of the collection
        public void calcTotalValue()
        {
            double total = 0;
            foreach (Item eachitemm in _collection)
            {
                total += (eachitemm.getPrice() * eachitemm.getCount());
            }
            _totalValue = total;
        }
        public void addToTotal(double toAdd)
        {
            _totalValue += toAdd;
        }
    }
}
