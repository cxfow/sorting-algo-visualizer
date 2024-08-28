using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSharpSortingAlgos
{
    public partial class mainWindow : Form
    {
        int[] arr;
        Graphics g;

        BackgroundWorker bgw = null; // running sorting algorithm on a different thread

        bool Paused = false;

        public mainWindow()
        {
            InitializeComponent();
            PopulateDrodown();
        }

        private void PopulateDrodown()
        {
            // getting a list of the names of classes that implement the interface, exclude the interface itself and any abstrac classes,
            // then get the names of those canidates, cast them as a list, and place them in the variable ClassList.
            List<string> ClassList = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => typeof(ISortEngine).IsAssignableFrom(x)
             && !x.IsAbstract).Select(x => x.Name).ToList();

            ClassList.Sort(); // sort the list alphabetically

            // populate the drop down with the names of those classes
            foreach (string entry in ClassList)
            {
                comboBox1.Items.Add(entry);
            }

            comboBox1.SelectedIndex = 0;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (arr == null)
                btnReset_Click(null, null);

            bgw = new BackgroundWorker(); // create background worker object
            bgw.WorkerSupportsCancellation = true; // allowing you to cancel the background workers task
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork); // add an event handler that add to a list of things that happen when the event fires
            bgw.RunWorkerAsync(argument: comboBox1.SelectedItem); // run the background worker on whichever sorting algo is selected
        }


        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!Paused)
            {
                bgw.CancelAsync(); // pause worker
                Paused = true;
            }
            else
            {
                // resume worker if background worker isnt busy
                if (bgw.IsBusy)
                    return;

                int NumEntries = panel1.Width;
                int MaxVal = panel1.Height;

                Paused = false;

                for (int i = 0; i < NumEntries; i++)
                {
                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), i, 0, 1, MaxVal);
                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, MaxVal - arr[i], 1, MaxVal);
                }
                bgw.RunWorkerAsync(argument: comboBox1.SelectedItem);
            }

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics(); // creating graphics object

            // creating variables to hold the currents state of the array based on the height and width of the pannel
            int NumEntries = panel1.Width;
            int maxVal = panel1.Height;

            arr = new int[NumEntries]; // creating the array

            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), 0, 0, NumEntries, maxVal); // creating a black panel background
            Random rand = new Random(); // creating a random number generator

            // initializing each memeber of the array to a random number between 0 and the max number
            for (int i = 0; i < NumEntries; i++)
            {
                arr[i] = rand.Next(0, maxVal);
            }

            // drawing bars to represent the integers
            for (int i = 0; i < NumEntries; i++)
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, maxVal - arr[i], 1, maxVal);
            }
        }

        public void bgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // explicitly identify the sender as a background worker
            BackgroundWorker bgw = sender as BackgroundWorker;

            // extract the name of the sorting algo to use
            string SortEgineName = (string)e.Argument;

            // figure out the type of the class that will implement the algo
            Type type = Type.GetType("CSharpSortingAlgos." + SortEgineName);

            // get the constructors of that type
            var ctors = type.GetConstructors();

            try
            {
                // creating a sort engine, create an instance of the constructor, and pass the 3 parameters they need
                ISortEngine se = (ISortEngine)ctors[0].Invoke(new object[] { arr, g, panel1.Height});

                while (!se.IsSorted() && (!bgw.CancellationPending))
                {
                    se.NextStep();
                }
            }
            catch (Exception  ex)
            {


            }
        }
    }
}
