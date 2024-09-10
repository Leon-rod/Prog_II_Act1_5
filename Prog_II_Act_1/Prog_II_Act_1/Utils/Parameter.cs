using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_II_Act_1.Utils
{
    public class Parameter
    {
        public String Name { get; set; }
        public Object Value{ get; set; }

        public Parameter(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
