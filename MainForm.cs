using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CalculatorProject
{
    public partial class MainForm : Form
    {
        public List<int> sortedList = new List<int>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            int[] fields = new int[5];
            String[] strFields = new string[5];
            StreamReader CalculatorReader = new StreamReader("Items.txt");
            while (CalculatorReader.EndOfStream == false)
            {
                String currentLine = CalculatorReader.ReadLine();
                strFields = currentLine.Split(',');
                for (int i = 0; i < strFields.Length; i++)
                {
                    int.TryParse(strFields[i], out fields[i]);
                }
                for (int i = 0; i < fields.Length; i++)
                {
                    sortedList.Add(fields[i]);
                    Sort(sortedList);
                }
            }
            //foreach (int num in sortedList)
            //{
            //    displayListBox.Items.Add(num);
            //}
            CalculatorReader.Close();
        }

        private void Sort(List<int> sortedList)
        {
            int i, j;
            for (i = 0; i < sortedList.Count - 1; i++)

                for (j = 0; j < sortedList.Count - i - 1; j++)
                    if (sortedList[j] > sortedList[j + 1])
                    {
                        int temp = sortedList[j];
                        sortedList[j] = sortedList[j + 1];
                        sortedList[j + 1] = temp;
                    }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
            //Add the verification code later
        }

        private void countButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            displayListBox.Items.Add(sortedList.Count);
        }

        private void meanButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            displayListBox.Items.Add(Math.Round(sortedList.Average(), 2));
        }

        private void rangeButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            displayListBox.Items.Add(sortedList.Max() - sortedList.Min());
        }

        private void standardDeviationButton_Click(object sender, EventArgs e)
        {
            double total = 0;
            displayListBox.Items.Clear();
            foreach (int i in sortedList)
            {
                total += Math.Pow(i - sortedList.Average(), 2);
            }
            total = total / sortedList.Count - 1;
            total = Math.Sqrt(total);
            displayListBox.Items.Add(total);
        }

        private void medianButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            double median;
            if (sortedList.Count % 2 == 0)
            {
                median = sortedList[(sortedList.Count / 2)] + sortedList[(sortedList.Count / 2) - 1];
                median = median / 2;
                displayListBox.Items.Add(median);

            }
            else
            {
                displayListBox.Items.Add(sortedList[(sortedList.Count / 2) + (1 / 2)]);
            }
        }

        private void maxButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            displayListBox.Items.Add(sortedList.Max());
        }

        private void minButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            displayListBox.Items.Add(sortedList.Min());
        }

        private void modeButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            //Makes Dictionary with Key Values of the unique ints and normal Values of the count of ints
            Dictionary<int, int> counts = new Dictionary<int, int>();
            foreach (int i in sortedList)
            {
                if (counts.ContainsKey(i))
                    counts[i]++;
                else
                    counts[i] = 1;
            }

            int max = int.MinValue;
            int count = 0;
            int[] multipleModes = new int[counts.Count()];


            //For each key value in the counts Dictionary
            foreach (int key in counts.Keys)
            {
                //if The count of the current key > max
                if (counts[key] > max)
                {
                    //Set the new max to the counts of the current key
                    max = counts[key];
                    for (int i = 0; i < multipleModes.Length; i++)
                        multipleModes[i] = 0;

                    //Set the value of multipleModes[count] to the value of the key
                    multipleModes[count] = key;

                }
                //If the count of the key value = max, add the key value to multiple modes array
                else if (counts[key] == max)
                {
                    multipleModes[count] = key;
                }
                count++;
            }


            //If the value of the indexed mode isn't zero(empty), display the value
            Console.Write(" The mode(s) is/are: ");
            for (int i = 0; i < multipleModes.Length; i++)
            {
                if (multipleModes[i] != 0)
                    displayListBox.Items.Add(multipleModes[i]);
            }

        }

        private void sumButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            displayListBox.Items.Add(sortedList.Sum());
        }

        private void factorialButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            int.TryParse(xTextBox.Text, out int xVal);
            int total = xVal;

            for (int i = xVal - 1; i > 1; i--)
                total *= i;

            displayListBox.Items.Add(total);
        }

        private void reciprocalButton_Click(object sender, EventArgs e)
        {

        }

        private void fibonacciButton_Click(object sender, EventArgs e)
        {

        }
    }
}