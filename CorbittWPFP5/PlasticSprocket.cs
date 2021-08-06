using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorbittWPFP5
{
    class PlasticSprocket:Sprocket
    {
        public PlasticSprocket() : this(0, 0, 0)
        {

        }
        public PlasticSprocket(int newItemID, int newNumItems, int newNumTeeth) : base(newItemID, newNumItems, newNumTeeth)
        {

        }

        protected override void Calc()
        {
            Price = NumItems * NumTeeth * .02M;
        }

        public override string ToString()
        {
            return base.ToString() + " Material: plastic";
        }

    }
}
