using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorbittWPFP5
{
    class SprocketOrder
    {
        private List<Sprocket> items;
        public Address Address { get; set;}
        public string CustomerName { get; set; }
        public List<Sprocket> Items
        {
            get 
            {
                return items;
            }
            set 
            {
                items = value;
                Calculate();
            }
        }
        public decimal TotalPrice { get; private set; }

        public SprocketOrder() 
        {
        
        }
        private void Calculate()
        {
            foreach(Sprocket sprocket in Items)
            {
                TotalPrice += sprocket.Price;
            }

        }
  
        public override string ToString()
        {
            string response = CustomerName + ": "+ Items.Count + " items, Total Price:" + TotalPrice +"\n\n";

            if (Address.Street.Equals("Local pickup"))
            {
                response += "Local Pickup\n\n\n";
            }
            else
            {
                response += "Ship to: "+ Address.Street + "\n" +
                            Address.City + " " + Address.State + " " + Address.ZipCode+"\n\n\n";    
            }
            foreach(Sprocket sprocket in Items)
            {
                response += sprocket.ToString() + "\n";
            }
            return response;
        }
    }

}
