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
            Item test = new Item("Black Lotus", "Adds 3 mana of any single color of your choice" +
                " to your mana pool,then is dicarded. Tapping this aftifact can be played as an interrupt.","Mint","Artifact",7499.99,1);
            var c1 = new Tuple<String, double>("Sep-07", 7394.65);
            var c3 = new Tuple<String, double>("Oct-17", 7142.75);
            var c6 = new Tuple<String, double>("Dec-03", 7578.47);
            var c7 = new Tuple<String, double>("Dec-11", 7499.99);
            test.addChange(c1);
            test.addChange(c3);
            test.addChange(c6);
            test.addChange(c7);

            _collection.Add(test);
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
