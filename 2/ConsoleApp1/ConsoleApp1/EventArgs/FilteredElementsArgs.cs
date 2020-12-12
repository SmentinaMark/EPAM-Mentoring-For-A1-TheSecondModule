using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class FilteredElementsArgs:EventArgs
    {
        public bool FilteredStopSearch { get; set; } = true;
        public bool FilteredRemoveItem { get; } = true;
    }
}
