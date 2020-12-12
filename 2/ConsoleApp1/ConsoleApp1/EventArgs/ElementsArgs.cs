using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ElementsArgs:EventArgs
    {
        public bool StopSearch { get; set; } = true;
        public bool RemoveItem { get; } = true;
    }
}
