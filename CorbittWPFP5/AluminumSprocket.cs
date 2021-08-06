using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorbittWPFP5
{
  
        public class AluminumSprocket : Sprocket
        {
            public AluminumSprocket() : this(0, 0, 0)
            {

            }
            public AluminumSprocket(int newItemID, int newNumItems, int newNumTeeth) : base(newItemID, newNumItems, newNumTeeth)
            {

            }

            protected override void Calc()
            {
                Price = NumItems * NumTeeth * .04M;
            }

            public override string ToString()
            {
                return base.ToString() + " Material: aluminum";
            }


        }
    
}
