using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zee.Sample.CaveatEmptor.Model
{
    public class Item
    {
        public virtual String Name { get; set; }
        public virtual String Description { get; set; }
        public virtual Double initialPrice { get; set; }
        public virtual Double ReservePrice { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual DateTime Created { get; set; }

        /// <summary> No-arg constructor for tools.</summary>
        protected Item() { }
    }
}
