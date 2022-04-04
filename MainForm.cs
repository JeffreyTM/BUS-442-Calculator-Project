/*
    Calculator Application
    Date Submitted:

    Developers: Jeffrey Michaels and William Shomo

    This application has two types of calculators built in: a statistical 
    calculator and a scientific calculator. The statistical calculator reads 
    in a file of data points and has buttons that displays various evaluations 
    on the data points, such as the mean, median, mode, range, etc. The scientific 
    calculator reads input for x and y (if y is needed) that is either typed in 
    directly by the user, or inputted by pushing the keypad buttons, and evaluates 
    various scientific calculations such as the factorial, fibonacci sequence, x^y, etc.

    Coded by William: MainForm_Load, Sort, countButton_Click, meanButton_Click, rangeButton_Click, 
        standardDeviationButton_Click, medianButton_Click, maxButton_Click, minButton_Click, 
        sumButton_Click, factorialButton_Click, squareButton_Click, reciprocalButton_Click, 
        primeButton_Click, fibonacciButton_Click, clearButton_Click

    Coded by Jeffrey: exitButton_Click, modeButton_Click, sumSquaresButton_Click, fibonacciButton_Click, 
        displayFileButton_Click, zero-nineButton_Click, clearLastValueButton_Click, clearTextBoxButton_Click, 
        xTextBox_KeyPress, yTextBox_KeyPress, x and yTextBox_Click and _Enter, all MouseHover and 
        MouseLeave event handlers
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

            //Call the Sort method to sort the list 
            Sort(sortedList);

            //Close the StreamReader
            CalculatorReader.Close();

        }

        private void Sort(List<int> sortedList)
        {
            //Nested for loop which goes through each value in sorted list and compares it to the values indexed after it
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
            // Exit Confirmation
            DialogResult dialog = MessageBox.Show("Are you sure you want to exit?",
                                  this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //If answer is yes, close the app
            if (dialog == DialogResult.Yes)
                this.Close();
        }

        private void countButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();

            //Call the count method to get the total amount of values in the data set
            int count = sortedList.Count;

            //Display the value in the listbox
            displayListBox.Items.Add("Count: " + count);
        }

        private void meanButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();

            //Call the Average method to get the mean and the Math.Round method to round the value to 2 decimal places
            double mean = Math.Round(sortedList.Average(), 2);

            //Display the value in the listbox
            displayListBox.Items.Add("Mean: " + mean);
        }

        private void rangeButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();

            //Call the Max and Min methods and subtract the values
            int range = sortedList.Max() - sortedList.Min();

            //Display the value in the listbox
            displayListBox.Items.Add("Range: " + range);
        }

        private void standardDeviationButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();

            double total = 0;

            foreach (int i in sortedList)
            {
                //Evaluate the current value minus the mean of the list and raise the value to the power of two  
                total += Math.Pow(i - sortedList.Average(), 2);
            }
            //Divide the total by the count - 1
            total /= sortedList.Count - 1;

            //Use Math.Sqrt to get the square root of the total
            total = Math.Sqrt(total);

            //Display the value in the listbox rounded to 3 decimal points
            displayListBox.Items.Add("Standard Deviation: " + Math.Round(total, 3));
        }

        private void medianButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();

            double median;
            if (sortedList.Count % 2 == 0) //If the amount of values in the list is even
            {
                //Index the middle two numbers in the list, add them together, and divide them by 2
                median = sortedList[(sortedList.Count / 2)] + sortedList[(sortedList.Count / 2) - 1];
                median = median / 2;

                //Display the value in the listbox
                displayListBox.Items.Add("Median: " + median);

            }
            else //If the amount of values in the list is odd
            {
                //Index the middle number in the list by subtracting the count by 1 and dividing it by 2
                median = sortedList[(sortedList.Count - 1) / 2];

                //Display the value in the listbox
                displayListBox.Items.Add("Median: " + median);
            }
        }

        private void maxButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();

            //Display the max value in the listbox by calling the Max method
            displayListBox.Items.Add("Max: " + sortedList.Max());
        }

        private void minButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();

            //Display the min value in the listbox by calling the Min method
            displayListBox.Items.Add("Min: " + sortedList.Min());
        }

        private void modeButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();

            //Declare a dictionary with key values of the unique values and normal values of the count of values
            Dictionary<int, int> counts = new Dictionary<int, int>();

            
            foreach (int i in sortedList)
            {
                if (counts.ContainsKey(i)) //If the value i is already a key value
                {
                    //Increment the key value's count by 1
                    counts[i]++;
                }
                else //If the value i is not already a key value
                {
                    //Add the key value and set the count equal to 1
                    counts[i] = 1;
                }
            }

            int max = int.MinValue;
            int count = 0;

            //List that can store multiple modes if more than one exists
            List<int> modes = new List<int>();


            //For each key value in the counts Dictionary
            foreach (int key in counts.Keys)
            {
                //if The count of the current key > max and the count is greater than 1
                if (counts[key] > max && counts[key] > 1) 
                {
                    //Set the new max to the counts of the current key
                    max = counts[key];

                    //Clear out the current modes list
                    modes.Clear();

                    //Add the key value to the modes list
                    modes.Add(key);

                }
                //If the count of the key value = max and the count is greater than 1
                else if (counts[key] == max && counts[key] > 1) 
                {
                    //Add the key value to the modes list
                    modes.Add(key);
                }

                count++;
            }


            //Display the mode 
            if (modes.Count == 1) //If there is only one mode
            {
                //Display the mode in the listbox
                displayListBox.Items.Add("Mode: " + modes[0]);
            }
            else if (modes.Count > 1) //If there is more than one mode
            {
                //Declare a string that tells the user how many modes are in the data set
                string multipleModes = "There are " + modes.Count + " Modes in this data set: ";

                foreach (int mode in modes)
                {
                    //Concatenate each mode into the string
                    multipleModes += mode + " ";
                }

                //Display the modes in the listbox
                displayListBox.Items.Add(multipleModes);

            }
            else if (modes.Count == 0) //If there is no mode
            {
                //Display the information in the listbox
                displayListBox.Items.Add("There are no modes in this data set.");
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

            if (xTextBox.Text == "")
            {
                //Display error message if the text box is empty
                DialogResult dialog = MessageBox.Show("Error: Please enter a value for x.",
                                      Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (xVal < 0 || xVal > 10)
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


            if (xTextBox.Text == "" || yTextBox.Text == "")
            {
                //Display error message if one or both of the text boxes is/are empty
                DialogResult dialog = MessageBox.Show("Error: Please enter a value for x and y.",
                                      Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int total = (int)Math.Pow(x, y);
                displayListBox.Items.Add(x + " raised to the power of " + y + ": " + total);
            }
            
        }

        private void reciprocalButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();
            double xInt;
            double.TryParse(xTextBox.Text, out xInt);

            //Error Handlers
            if (xTextBox.Text == "")
            {
                //Display error message if one or both of the text boxes is/are empty
                DialogResult dialog = MessageBox.Show("Error: Please enter a value for x.",
                                      Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (xInt == 0)
            {
                DialogResult dialog = MessageBox.Show("Error: Cannot divide by 0.",
                                     Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                displayListBox.Items.Add("Reciprocal of " + xInt + ": " + (Math.Round(1.0 / xInt, 4)));
            }

        }


        private void primeButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();

            int.TryParse(xTextBox.Text, out int x);
            int.TryParse(yTextBox.Text, out int y);

            //Error Handlers
            if (xTextBox.Text == "" || yTextBox.Text == "")
            {
                //Display error message if one or both of the text boxes is/are empty
                DialogResult dialog = MessageBox.Show("Error: Please enter a value for x and y.",
                                      Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (x > y)
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
                //For loop that iterates from the value of x to the value of y
                for (int i = x; i <= y; i++)
                {
                    //Declare bool to store the result of the for loop
                    bool valueIsPrime = true;

                    if (i == 1)
                        //If i = 1, set the bool to false
                        valueIsPrime = false;
                    else
                    {
                        //Nested for loop that compares i to every int before i starting at 2
                        for (int j = 2; j < i; j++)
                        {
                            if (i % j == 0)
                            {
                                //If i % j == 0, then i divides evenly into a number before itself starting at 2
                                valueIsPrime = false;
                            }
                        }
                    }
                        //After the nested for loop, display whether i is a prime number or not
                        if (valueIsPrime)
                            displayListBox.Items.Add(i + " is a prime number.");
                        else
                            displayListBox.Items.Add(i + " is not a prime number.");
                    
                }
            }

        }

        private void sumSquaresButton_Click(object sender, EventArgs e)
        {
            displayListBox.Items.Clear();

            int.TryParse(xTextBox.Text, out int xInt);
            int.TryParse(yTextBox.Text, out int yInt);

            //Error Handlers
            if(xTextBox.Text == "" || yTextBox.Text == "")
            {
                //Display error message if one or both of the text boxes is/are empty
                DialogResult dialog = MessageBox.Show("Error: Please enter a value for x and y.",
                                      Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (xInt == 0 && yInt == 0)
            {
                //Display error message if x and y both equal 0
                DialogResult dialog = MessageBox.Show("Error: Both values cannot be zero. " +
                    "Please enter in a valid input.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (xInt > yInt)
            {
                //Display error message if x is greater than y
                DialogResult dialog = MessageBox.Show("Error: The value of 'x' cannot be greater than 'y.' " +
                    "Please enter a valid input.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Find x and y squared
                int xSquared = (int)Math.Pow(xInt, 2);
                int ySquared = (int)Math.Pow(yInt, 2);

                //Add x and y together and x squared and y squared together
                int sumOfNums = xInt + yInt;
                int sumOfSquares = xSquared + ySquared;

                //Display x and y, x squared and y squared, and the sum of numbers and squares
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

            //Error Handlers
            if(xTextBox.Text == "")
            {
                //Display error message if the text box is empty
                DialogResult dialog = MessageBox.Show("Error: Please enter a value for x.",
                                      Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (xInt <= 0 || xInt > 30)
            {
                //Display error message if the value entered is not between 1 and 30
                DialogResult dialog = MessageBox.Show("Error: Please enter a number between 1 and 30.",
                                      Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Maybe Have
                //xTextBox.Text = "";
            }

            else
            {
                //Declare variables
                int first = 0;
                int second = 1;
                int third = 0;

                //String sequence will be used to display 5 values at a time
                string sequence = "";

                
                if (xInt > 5)
                {
                    //If the x value is greater than 5, display this in the list box
                    displayListBox.Items.Add("Fibonacci sequence for " + xInt);

                    //Set sequence equal to the start of the first line of values
                    sequence = "Values 1 through 5: ";
                }
                else if (xInt == 1)
                {
                    //Display the fibonacci sequence and return out of the event handler if the x value is 1
                    displayListBox.Items.Add("Fibonacci sequence for 1: " + first);
                    return;
                }
                else
                {
                    //If x is less than or equal to 5, display everything on one line
                    sequence = "Fibonacci sequence for " + xInt + ": ";
                }

                //Maybe add commas between numbers (ask her)
                sequence += first + ", " + second;  //comma goes before whitespace here


                //For loop that starts at the current Fibonacci number and iterates until i > x
                for (int i = 3; i <= xInt; i++)
                {
                    //Evaluate the Fibonacci number and add it to the sequence string
                    third = first + second;
                    sequence += ", " + third;        //comma goes before whitespace here

                    //If we are on the i % 5th number (not including 5) and this is not the last line needed,
                    //we start a new line
                    if ((i % 5) == 0 && i != 5 && i != xInt)
                    {
                        //If the next line is less than 5 digits
                        if(xInt - i < 5)
                        {
                            //Add the current line to the list box
                            displayListBox.Items.Add(sequence);

                            //Reset the sequence variable to show the next x values
                            sequence = "Values " + (i + 1) + " through " + (i + (xInt - i)) + ":";
                        }
                        else
                        {
                            //Add the current line to the list box
                            displayListBox.Items.Add(sequence);

                            //Reset the sequence variable to show the next 5 values
                            sequence = "Values " + (i + 1) + " through " + (i + 5) + ":";
                        }
                        
                    }
                    //If we are on the 5th number but the 5th number isn't the last number
                    else if (i == 5 && i != xInt)
                    {
                        //If there are less than 5 numbers left
                        if (xInt - i < 5)
                        {
                            //Add the current line to the list box
                            displayListBox.Items.Add(sequence);

                            //Reset the sequence to show the next x values starting from 6
                            sequence = "Values 6 through " + xInt + ":";
                        }
                        else
                        {
                            //Add the current line to the list box
                            displayListBox.Items.Add(sequence);

                            //Reset the sequence to show values 6 through 10
                            sequence = "Values 6 through 10:";
                        }
                    }

                    //Set first and second equal to second and third for the next iteration
                    first = second;
                    second = third;
                }

                //Once out of the for loop, add the final sequence to the list box
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

        //private void oneButton_MouseHover(object sender, EventArgs e)
        //{
        //    oneButton.BackColor = SystemColors.WindowFrame;
        //}

        //private void oneButton_MouseLeave(object sender, EventArgs e)
        //{
        //    oneButton.BackColor = Color.Black;
        //}
    }
}
