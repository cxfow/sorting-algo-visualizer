using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSortingAlgos
{
    internal class SortEngineMoveToBack : ISortEngine
    {
        // variables
        private readonly int[] arr; // copy of the array
        private Graphics g; // graphics object
        private readonly int maxVal; // maximum value integer

        private int CurrentListPointer = 0;

        // using bursh to draw white and black rectangles into the graphics object
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        // construtor
        public SortEngineMoveToBack(int[] arr_In, Graphics g_In, int maxVal_In)
        {
            arr = arr_In;
            g = g_In;
            maxVal = maxVal_In;
        }

        public void NextStep()
        {
            if (CurrentListPointer >= arr.Count() - 1)
                CurrentListPointer = 0;
            if (arr[CurrentListPointer] > arr[CurrentListPointer + 1])
            {
                Rotate(CurrentListPointer);
            }
            CurrentListPointer++;
        }

        private void Rotate(int currentListPointer)
        {
            int temp = arr[CurrentListPointer];
            int EndPoint = arr.Count() - 1;

            for (int i = currentListPointer; i < EndPoint; i++)
            {
                arr[i] = arr[i + 1];
                DrawBar(i, arr[i]);
            }

            arr[EndPoint] = temp;
            DrawBar(EndPoint, arr[EndPoint]);
        }

#pragma warning disable IDE0060 // Remove unused parameter
        private void DrawBar(int position, int height)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            g.FillRectangle(BlackBrush, position, 0, 1, maxVal); // remove old values
            g.FillRectangle(WhiteBrush, position, maxVal - arr[position], 1, maxVal); // show new values
        }

        public bool IsSorted()
        {
            for (int i = 0; i < arr.Count() - 1; i++)
            {
                if (arr[i] > arr[i + 1]) // if any element is greater than the next element, return false.
                    return false;
            }
            return true;
        }

        public void ReDraw()
        {
            for (int i = 0; i < arr.Count(); i++)
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, maxVal - arr[i], 1, maxVal);
            }
        }
    }
}
