using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zee.Sample.CaveatEmptor.Model
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Category ParentCategory { get; set; }



    }
}
