using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSortingAlgos
{
    public class BarDrawer
    {
        private readonly Graphics _graphics;
        private readonly int _maxVal;
        private readonly Brush _whiteBrush;
        private readonly Brush _blackBrush;

        public BarDrawer(Graphics graphics, int maxVal)
        {
            _graphics = graphics;
            _maxVal = maxVal;
            _whiteBrush = new SolidBrush(Color.White);
            _blackBrush = new SolidBrush(Color.Black);
        }

        public void DrawBar(int position, int height)
        {
            _graphics.FillRectangle(_blackBrush, position, 0, 1, _maxVal); // remove old values
            _graphics.FillRectangle(_whiteBrush, position, _maxVal - height, 1, _maxVal); // show new values
        }

        public void RedrawAllBars(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                DrawBar(i, array[i]);
            }
        }
    }
}

