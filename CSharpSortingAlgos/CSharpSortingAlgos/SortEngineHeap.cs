using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSortingAlgos
{
    internal class SortEngineHeap : ISortEngine
    {
        // variables
        private int[] arr; // copy of the array
        private Graphics g; // graphics object
        private int maxVal; // maximum value integer

        // using bursh to draw white and black rectangles into the graphics object
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        // constructor
        public SortEngineHeap(int[] arr_In, Graphics g_In, int maxVal_In)
        {
            arr = arr_In;
            g = g_In;
            maxVal = maxVal_In;
        }

        // priamry sorting method
        public void NextStep()
        {
            int n = arr.Length;

            // build heap
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(arr, n, i);
            }

            // extract elements from heap 1 by 1
            for (int i = n - 1; i > 0; i--)
            {
                (arr[i], arr[0]) = (arr[0], arr[i]); // manual swap

                DrawBar(i, arr[i]);
                DrawBar(0, arr[0]);

                // call heapify on reduced heap
                Heapify(arr, i, 0);
            }

        }

        private void DrawBar(int position, int height)
        {
            g.FillRectangle(BlackBrush, position, 0, 1, maxVal); // remove old values
            g.FillRectangle(WhiteBrush, position, maxVal - arr[position], 1, maxVal); // show new values
        }

        private void Heapify(int[] arr, int n, int i)
        {
            int largest = i; // initialize largest as the root
            int l = 2 * i + 1; // left heap
            int r = 2 * i + 2; // right heap

            // if left child is larger than root
            if (l < n && arr[l] > arr[largest])
                largest = l;

            // if right child is larger than largest so far
            if (r < n && arr[r] > arr[largest])
                largest = r;

            // if the largest is not the root
            if (largest != i)
            {
                (arr[largest], arr[i]) = (arr[i], arr[largest]); // manual swap

                DrawBar(i, arr[i]);
                DrawBar(largest, arr[largest]);

                // recursively heapify the affected sub-tree
                Heapify(arr, n, largest);
            }
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
