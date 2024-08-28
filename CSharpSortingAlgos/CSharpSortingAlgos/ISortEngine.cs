using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSortingAlgos
{
    internal interface ISortEngine
    {
        // making the sorting run on a background thread
        void NextStep();
        bool IsSorted();
        void ReDraw();
    }
}
