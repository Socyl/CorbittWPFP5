using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorbittWPFP5
{
    public class SteelSprocket : Sprocket
    {
       public SteelSprocket():this(0,0,0)
        {

        }
       public SteelSprocket(int newItemID, int newNumItems, int newNumTeeth):base(newItemID, newNumItems, newNumTeeth)
        {

        }

        protected override void Calc()
        {
            Price = NumItems * NumTeeth * .05M;
        }

        public override string ToString()
        {
            return base.ToString()+" Material: steel";
        }


    }
 
}
