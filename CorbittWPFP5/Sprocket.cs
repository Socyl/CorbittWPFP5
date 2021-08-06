using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorbittWPFP5
{
    public abstract class Sprocket
    {

        private int numItems;
        private int numTeeth;
        private decimal price;

        public int NumItems
        {
            get
            {
                return numItems;
            }
            set
            {
                numItems = value;
                Calc();
            }
        }
        public int NumTeeth 
        {
            get 
            {
                return numTeeth;
            }
            set 
            {
                numTeeth = value;
                Calc();
            }
        }

        public int ItemID { get;  set; }
        public decimal Price 
        { 
            get 
            {
                return price;
            }  
            protected set
            {
                price = value;
                
            } 
        }
         
        public Sprocket():this(0,0,0)
        {

        }
        public Sprocket(int newItemID,int newNumItems, int newNumTeeth)
        {
            ItemID = newItemID;
            NumItems = newNumItems;
            NumTeeth = newNumTeeth;
        }

        protected abstract void Calc();

        public override string ToString() 
        {
            return "Item: "+ItemID + " Number: " + NumItems + " Teeth: " + numTeeth + " Price: " + Price;
        }
    }
}
