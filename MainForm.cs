/*
 
 The app code must be documented adequately and correctly, including the app name, app
purpose, date, and developer names at the top. For this pair-programming app, also include
which sections of the code each developer coded vs. reviewed. The team to submit (to Moodle)
the best working solution by the due date and time, complete with all of the requirements,
wins! If there is a tie in terms of two or more teams each submitting an app with all
requirements complete, I will select the team that submitted the winning app the fastest. I will
announce the winners via Moodle or in class. Good luck, everyone!
 
 */


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
        //Declare global Lists for the sorted list and each file line (for use on display file values event handler)
        List<int> sortedList = new List<int>();
        List<string> fileLines = new List<string>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Declare string array of size 5 for each int on a file line and the StreamReader object
            String[] strFields = new string[5];
            StreamReader CalculatorReader = new StreamReader("Items.txt");

            //Read in the file content
            while (CalculatorReader.EndOfStream == false)
            {
                //Set currentLine equal to the current line in the txt file
                String currentLine = CalculatorReader.ReadLine();

                //Add the current line to the fileLines List for easier access in displayFileButton
                fileLines.Add(currentLine);

                //Add the current line to the string array, splitting values apart when a comma is read
                strFields = currentLine.Split(',');

                //For loop to add all values in the current line to the global list
                for (int i = 0; i < strFields.Length; i++)
                    sortedList.Add(int.Parse(strFields[i]));

            }

            //Call the Sort method to sort the list and close the StreamReader
            Sort(sortedList);
            CalculatorReader.Close();

            //For Debugging
            //foreach (int num in sortedList)
            //{
            //    displayListBox.Items.Add(num);
            //}

        }

        private void Sort(List<int> sortedList)
        {
            //Nested for loop which goes through each value in sorted list and compares it to the values indexed afted it
            for (int i = 0; i < sortedList.Count - 1; i++)

                for (int j = 0; j < sortedList.Count - i - 1; j++)
                    //If the current value in the loop is greater than the next value
                    if (sortedList[j] > sortedList[j + 1])
                    {
                        //Temporarily store the current value
                        int temp = sortedList[j];

                        //Set the current value to the next value
                        sortedList[j] = sortedList[j + 1];

                        //Set the next value to the temporarily stored current value
                        sortedList[j + 1] = temp;
                    }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
            //Add the verification code later
            //// Exit Confirmation
            //DialogResult dialog = MessageBox.Show("Are you sure you want to exit?",
            //                      this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            ////If answer is yes, close the app
            //if (dialog == DialogResult.Yes)
            //    this.Close();
        }

        private void countButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            displayListBox.Items.Add("Count: " + sortedList.Count);
        }

        private void meanButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            displayListBox.Items.Add("Mean: " + Math.Round(sortedList.Average(), 2));
        }

        private void rangeButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            displayListBox.Items.Add("Range: " + (sortedList.Max() - sortedList.Min()));
        }

        private void standardDeviationButton_Click(object sender, EventArgs e)
        {
            double total = 0;
            displayListBox.Items.Clear();
            foreach (int i in sortedList)
            {
                total += Math.Pow(i - sortedList.Average(), 2);
            }
            total /= sortedList.Count - 1;
            total = Math.Sqrt(total);


            displayListBox.Items.Add("Standard Deviation: " + Math.Round(total, 3));
        }

        private void medianButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            double median;
            if (sortedList.Count % 2 == 0)
            {
                median = sortedList[(sortedList.Count / 2)] + sortedList[(sortedList.Count / 2) - 1];
                median = median / 2;
                displayListBox.Items.Add("Median: " + median);

            }
            else
            {
                displayListBox.Items.Add("Median: " + sortedList[(sortedList.Count / 2) + (1 / 2)]);
            }
        }

        private void maxButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            displayListBox.Items.Add("Max: " + sortedList.Max());
        }

        private void minButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            displayListBox.Items.Add("Min: " + sortedList.Min());
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


            //If the value of the indexed mode isn't zero(empty), add the value to a string and display it
            string modes = "";
            for (int i = 0; i < multipleModes.Length; i++)
            {
                if (multipleModes[i] != 0)
                {
                    modes += multipleModes[i];
                    displayListBox.Items.Add("Mode: " + modes);
                }   
            }
            

        }

        private void sumButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            displayListBox.Items.Add("Sum: " + sortedList.Sum());
        }

        private void factorialButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            int.TryParse(xTextBox.Text, out int xVal);

            if (xVal < 0 || xVal > 10)
            {
                DialogResult dialog = MessageBox.Show("Error: Please enter a number between 0 and 10.",
                                     Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (xVal == 0)
                displayListBox.Items.Add("0!: 1");
            else
            {
                int total = xVal;

                for (int i = xVal - 1; i > 1; i--)
                    total *= i;

                displayListBox.Items.Add(xVal + "!: " + total);
            }
        }

        private void squareButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();

            int.TryParse(xTextBox.Text, out int x);
            int.TryParse(yTextBox.Text, out int y);
            int total = (int)Math.Pow(x, y);

            displayListBox.Items.Add(x + " raised to the power of " + y + ": " + total);
        }

        private void reciprocalButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            double xInt;
            double.TryParse(xTextBox.Text, out xInt);

            if (xInt == 0)
            {
                DialogResult dialog = MessageBox.Show("Error: Cannot divide by 0.",
                                     Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                displayListBox.Items.Add("Reciprocal of " + xInt + ": " + (Math.Round(1.0 / xInt, 4)));
        }


        private void primeButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();

            int.TryParse(xTextBox.Text, out int x);
            int.TryParse(yTextBox.Text, out int y);

            if (x > y)
            {
                DialogResult dialog = MessageBox.Show("Error: The value of x must be less than y. " +
                    "Please enter a valid input.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (x < 0)
            {
                DialogResult dialog = MessageBox.Show("Error: The value of x must be a positive number. " +
                    "Please enter a valid input.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (y > 500)
            {
                DialogResult dialog = MessageBox.Show("Error: The value of y must be less than 500. " +
                    "Please enter a valid input.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                for (int i = x; i <= y; i++)
                {
                    if (isPrime(i))
                    {
                        displayListBox.Items.Add(i + " is a prime number.");
                    }
                    else
                    {
                        displayListBox.Items.Add(i + " is not a prime number.");
                    }
                }
            }
        }

        private bool isPrime(int i)
        {
            if (i == 1)
            {
                return false;
            }
            for (int z = 2; z < i; z++)
            {
                if (i % z == 0)
                {
                    return false;
                }
            }
            return true;
        }

        

        private void sumSquaresButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();

            int.TryParse(xTextBox.Text, out int xInt);
            int.TryParse(yTextBox.Text, out int yInt);

            if (xInt == 0 && yInt == 0)
            {
                DialogResult dialog = MessageBox.Show("Error: Both values cannot be zero. " +
                    "Please enter in a valid input.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (xInt > yInt)
            {
                DialogResult dialog = MessageBox.Show("Error: The value of 'x' cannot be greater than 'y.' " +
                    "Please enter a valid input.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int xSquared = (int)Math.Pow(xInt, 2);
                int ySquared = (int)Math.Pow(yInt, 2);
                int sumOfNums = xInt + yInt;
                int sumOfSquares = xSquared + ySquared;

                displayListBox.Items.Add("The number is: " + xInt + " and its square is: " + xSquared);
                displayListBox.Items.Add("The number is: " + yInt + " and its square is: " + ySquared);
                displayListBox.Items.Add("Sum of numbers: " + sumOfNums + " Sum of squares: " + sumOfSquares);
            }
        }

        private void fibonacciButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();

            double xInt;
            double.TryParse(xTextBox.Text, out xInt);

            if(xTextBox.Text == "")
            {
                DialogResult dialog = MessageBox.Show("Error: Please enter a value for x.",
                                      Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (xInt <= 0 || xInt > 30)
            {
                DialogResult dialog = MessageBox.Show("Error: Please enter a number between 1 and 30.",
                                      Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Maybe Have
                //xTextBox.Text = "";
            }
            else
            {
                int first = 0;
                int second = 1;
                int third = 0;
                string sequence = "";

                if (xInt > 5)
                {
                    displayListBox.Items.Add("Fibonacci sequence for " + xInt);
                    sequence = "n = 0 through n = 5: ";
                }
                else
                    sequence = "Fibonacci sequence for " + xInt + ": ";
                    
                
                sequence += first + " " + second;

                for (int i = 2; i <= xInt; i++)
                {
                    third = first + second;
                    
                    //Maybe add commas between numbers (ask her)
                    sequence += " " + third;
                    if ((i % 5) == 0 && i != xInt)
                    {
                        displayListBox.Items.Add(sequence);
                        sequence = "n = " + (i+1) + " through n = " + (i + 5) + ":";
                    }
                    

                    first = second;
                    second = third;
                }
                displayListBox.Items.Add(sequence);
            }
            
        }

        private void displayFileButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            foreach (string line in fileLines)
                displayListBox.Items.Add(line);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            xTextBox.Clear();
            yTextBox.Clear();
            yCheckBox.Checked = false;
            xTextBox.Focus();
        }

        private void oneButton_Click(object sender, EventArgs e)
        {
            if (!yCheckBox.Checked)
                xTextBox.Text += 1;
            else
                yTextBox.Text += 1;
        }

        private void twoButton_Click(object sender, EventArgs e)
        {
            if (!yCheckBox.Checked)
                xTextBox.Text += 2;
            else
                yTextBox.Text += 2;
        }

        private void threeButton_Click(object sender, EventArgs e)
        {
            if (!yCheckBox.Checked)
                xTextBox.Text += 3;
            else
                yTextBox.Text += 3;
        }

        private void fourButton_Click(object sender, EventArgs e)
        {
            if (!yCheckBox.Checked)
                xTextBox.Text += 4;
            else
                yTextBox.Text += 4;
        }

        private void fiveButton_Click(object sender, EventArgs e)
        {
            if (!yCheckBox.Checked)
                xTextBox.Text += 5;
            else
                yTextBox.Text += 5;
        }

        private void sixButton_Click(object sender, EventArgs e)
        {
            if (!yCheckBox.Checked)
                xTextBox.Text += 6;
            else
                yTextBox.Text += 6;
        }

        private void sevenButton_Click(object sender, EventArgs e)
        {
            if (!yCheckBox.Checked)
                xTextBox.Text += 7;
            else
                yTextBox.Text += 7;
        }

        private void eightButton_Click(object sender, EventArgs e)
        {
            if (!yCheckBox.Checked)
                xTextBox.Text += 8;
            else
                yTextBox.Text += 8;
        }

        private void nineButton_Click(object sender, EventArgs e)
        {
            if (!yCheckBox.Checked)
                xTextBox.Text += 9;
            else
                yTextBox.Text += 9;
        }

        private void zeroButton_Click(object sender, EventArgs e)
        {
            if (!yCheckBox.Checked)
            {
                xTextBox.Text += 0;
                //xTextBox.Focus();
            }
            else
            {
                yTextBox.Text += 0;
                //yTextBox.Focus();
            }
        }

        private void clearLastValueButton_Click(object sender, EventArgs e)
        {
            
            if (!yCheckBox.Checked && !xTextBox.Text.Equals(""))
            {
                string clearLastEntry = xTextBox.Text.Substring(0, xTextBox.Text.Length - 1);
                xTextBox.Text = clearLastEntry;
                //xTextBox.Focus();
            }
            else if(yCheckBox.Checked && !yTextBox.Text.Equals(""))
            {
                string clearLastEntry = yTextBox.Text.Substring(0, yTextBox.Text.Length - 1);
                yTextBox.Text = clearLastEntry;
                //yTextBox.Focus();
            }
        }

        private void clearTextBoxButton_Click(object sender, EventArgs e)
        {
            if (!yCheckBox.Checked)
            {
                xTextBox.Text = "";
                xTextBox.Focus();
            }
            else
            {
                yTextBox.Text = "";
                yTextBox.Focus();
            }
        }

        private void xTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // This event handler only allows digits, control characters, and the negative symbol.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
                return;
            }
        }

        private void yTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // This event handler only allows digits, control characters, and the negative symbol.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
                return;
            }
        }

        private void xTextBox_TextChanged(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
        }

        private void yTextBox_TextChanged(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
        }

        private void xTextBox_Enter(object sender, EventArgs e)
        {
            xTextBox.SelectAll();

        }

        private void yTextBox_Enter(object sender, EventArgs e)
        {
            yTextBox.SelectAll();
        }

        private void xTextBox_Click(object sender, EventArgs e)
        {
            xTextBox.SelectAll();
        }

        private void yTextBox_Click(object sender, EventArgs e)
        {
            yTextBox.SelectAll();
        }

        //private void standardDeviationButton_MouseEnter(object sender, EventArgs e)
        //{
        //    standardDeviationButton.BackColor = Color.WhiteSmoke;
        //}


        //All MouseHover and MouseLeave Event Handlers

        private void countButton_MouseHover(object sender, EventArgs e)
        {
            countButton.BackColor = Color.WhiteSmoke;
        }

        private void countButton_MouseLeave(object sender, EventArgs e)
        {
            countButton.BackColor = Color.Gainsboro;
        }

        private void meanButton_MouseHover(object sender, EventArgs e)
        {
            meanButton.BackColor = Color.WhiteSmoke;
        }

        private void meanButton_MouseLeave(object sender, EventArgs e)
        {
            meanButton.BackColor = Color.Gainsboro;
        }

        private void rangeButton_MouseHover(object sender, EventArgs e)
        {
            rangeButton.BackColor = Color.WhiteSmoke;
        }

        private void rangeButton_MouseLeave(object sender, EventArgs e)
        {
            rangeButton.BackColor = Color.Gainsboro;
        }

        private void standardDeviationButton_MouseHover(object sender, EventArgs e)
        {
            standardDeviationButton.BackColor = Color.WhiteSmoke;
        }

        private void standardDeviationButton_MouseLeave(object sender, EventArgs e)
        {
            standardDeviationButton.BackColor = Color.Gainsboro;
        }

        private void medianButton_MouseHover(object sender, EventArgs e)
        {
            medianButton.BackColor = Color.WhiteSmoke;
        }

        private void medianButton_MouseLeave(object sender, EventArgs e)
        {
            medianButton.BackColor = Color.Gainsboro;
        }

        private void maxButton_MouseHover(object sender, EventArgs e)
        {
            maxButton.BackColor = Color.WhiteSmoke;
        }

        private void maxButton_MouseLeave(object sender, EventArgs e)
        {
            maxButton.BackColor = Color.Gainsboro;
        }

        private void minButton_MouseHover(object sender, EventArgs e)
        {
            minButton.BackColor = Color.WhiteSmoke;
        }

        private void minButton_MouseLeave(object sender, EventArgs e)
        {
            minButton.BackColor = Color.Gainsboro;
        }

        private void modeButton_MouseHover(object sender, EventArgs e)
        {
            modeButton.BackColor = Color.WhiteSmoke;
        }

        private void modeButton_MouseLeave(object sender, EventArgs e)
        {
            modeButton.BackColor = Color.Gainsboro;
        }

        private void sumButton_MouseHover(object sender, EventArgs e)
        {
            sumButton.BackColor = Color.WhiteSmoke;
        }

        private void sumButton_MouseLeave(object sender, EventArgs e)
        {
            sumButton.BackColor = Color.Gainsboro;
        }

        private void factorialButton_MouseHover(object sender, EventArgs e)
        {
            factorialButton.BackColor = Color.WhiteSmoke;
        }

        private void factorialButton_MouseLeave(object sender, EventArgs e)
        {
            factorialButton.BackColor = Color.Gainsboro;
        }

        private void squareButton_MouseHover(object sender, EventArgs e)
        {
            squareButton.BackColor = Color.WhiteSmoke;
        }

        private void squareButton_MouseLeave(object sender, EventArgs e)
        {
            squareButton.BackColor = Color.Gainsboro;
        }

        private void reciprocalButton_MouseHover(object sender, EventArgs e)
        {
            reciprocalButton.BackColor = Color.WhiteSmoke;
        }

        private void reciprocalButton_MouseLeave(object sender, EventArgs e)
        {
            reciprocalButton.BackColor = Color.Gainsboro;
        }

        private void primeButton_MouseHover(object sender, EventArgs e)
        {
            primeButton.BackColor = Color.WhiteSmoke;
        }

        private void primeButton_MouseLeave(object sender, EventArgs e)
        {
            primeButton.BackColor = Color.Gainsboro;
        }

        private void sumSquaresButton_MouseHover(object sender, EventArgs e)
        {
            sumSquaresButton.BackColor = Color.WhiteSmoke;
        }

        private void sumSquaresButton_MouseLeave(object sender, EventArgs e)
        {
            sumSquaresButton.BackColor = Color.Gainsboro;
        }

        private void fibonacciButton_MouseHover(object sender, EventArgs e)
        {
            fibonacciButton.BackColor = Color.WhiteSmoke;
        }

        private void fibonacciButton_MouseLeave(object sender, EventArgs e)
        {
            fibonacciButton.BackColor = Color.Gainsboro;
        }

        private void displayFileButton_MouseHover(object sender, EventArgs e)
        {
            displayFileButton.BackColor = Color.WhiteSmoke;
        }

        private void displayFileButton_MouseLeave(object sender, EventArgs e)
        {
            displayFileButton.BackColor = Color.Gainsboro;
        }

        private void clearButton_MouseHover(object sender, EventArgs e)
        {
            clearButton.BackColor = Color.WhiteSmoke;
        }

        private void clearButton_MouseLeave(object sender, EventArgs e)
        {
            clearButton.BackColor = Color.Gainsboro;
        }

        private void exitButton_MouseHover(object sender, EventArgs e)
        {
            exitButton.BackColor = Color.WhiteSmoke;
        }

        private void exitButton_MouseLeave(object sender, EventArgs e)
        {
            exitButton.BackColor = Color.Gainsboro;
        }
    }
}
