using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class SearchField
    {
        public string Name { get; set; }
        public string @Value { get; set; }
        public string Operator { get; set; }

        public SearchField(string Name, string @Value,string @Operator="==")
        {
            this.Name = Name;
            this.@Value = @Value;
            this.Operator = @Operator;
        }
    }

}
