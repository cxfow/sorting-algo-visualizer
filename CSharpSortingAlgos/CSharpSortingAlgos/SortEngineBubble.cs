using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CSharpSortingAlgos
{
    internal class SortEngineBubble : ISortEngine
    {
        // variables
        private int[] copyArr; // copy of the array
        private Graphics gfx; // graphics object
        private int maxVal; // maximum value integer

        // using bursh to draw white and black rectangles into the graphics object
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        // construtor
        public SortEngineBubble(int[] arrIn, Graphics graphicsIn, int maxValIn)
        {
            copyArr = arrIn;
            gfx = graphicsIn;
            maxVal = maxValIn;
        }

        public void NextStep()
        {
            int n = copyArr.Length;
            for (int i = 0; i < n - 1; i++) // step through array
            {
                if (copyArr[i] > copyArr[i + 1]) // compare each element to the one after
                {
                    Swap(i, i + 1);
                }
            }
        }

        private void Swap(int i, int p)
        {
            // swap variables with tuples
            (copyArr[i + 1], copyArr[i]) = (copyArr[i], copyArr[i + 1]);

            DrawBar(i, copyArr[i]);
            DrawBar(p, copyArr[p]);
        }

        private void DrawBar(int position, int height)
        {
            gfx.FillRectangle(BlackBrush, position, 0, 1, maxVal); // remove old values
            gfx.FillRectangle(WhiteBrush, position, maxVal - copyArr[position], 1, maxVal); // show new values
        }

        public bool IsSorted()
        {
            for (int i = 0; i < copyArr.Count() - 1; i++)
            {
                if (copyArr[i] > copyArr[i + 1]) // if any element is greater than the next element, return false.
                    return false;
            }
            return true;
        }

        public void ReDraw()
        {
            for (int i = 0; i < copyArr.Count(); i++)
            {
                gfx.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, maxVal - copyArr[i], 1, maxVal);
            }
        }
    }
}
