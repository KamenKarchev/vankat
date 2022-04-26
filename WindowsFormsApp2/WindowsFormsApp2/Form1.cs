using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        string[] lines;
        int _var = 0;

        List<string> allLines = new List<string>();
        List<Block> blocks = new List<Block>();
        string[] letters = {"e","t","a","o","i","n","s","r","h","l","d","c","u","m","f","p","g","w","y","b","v","k","x","j","q","z"};

        public Form1()
        {
            InitializeComponent();
            richTextBox1.Text = "cat\ncat cat\ncat cat\ncat cat\ncat cat cat cat cat\ncat";
            _var = 0;

            allLines = richTextBox1.Lines.ToList();
        }

        private void compile_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            DetermineBlocks();
            foreach (var block in blocks)
            {
                switch (block.FunctionFolder)
                {
                    default:
                        break;
                    case "math":
                        #region math
                         switch (block.FunctionInBlock)
                        {
                            default:
                                break;
                            case "add":
                                listBox1.Items.Add(AddFunction(block.blockData).ToString());
                                break;
                            case "subtract":
                                listBox1.Items.Add(SubtractFunction(block.blockData).ToString());
                                break;
                            case "multiply":
                                listBox1.Items.Add(MultplyFunction(block.blockData).ToString());
                                break;
                            case "devide":
                                listBox1.Items.Add(DevideFunction(block.blockData).ToString());
                                break;
                        }
                        #endregion
                        break;
                    case "text":
                        #region text
                        switch (block.FunctionInBlock)
                        {
                            case"write":
                                listBox1.Items.Add(WriteOnSame(block.blockData));
                                break;
                            case "writeline":
                                foreach (var line in WriteOnNew(block.blockData))
                                {
                                    listBox1.Items.Add(line);
                                }
                                break;
                        }
                        #endregion
                        break;
                    case "loops":
                        #region loops
                        switch (block.FunctionInBlock)
                        {
                            case "loop":
                                foreach (var line in Loop(block.blockData))
                                {
                                    listBox1.Items.Add(line);
                                }
                                break;
                            case "reverse loop":
                                foreach (var line in ReverseLoop(block.blockData))
                                {
                                    listBox1.Items.Add(line);
                                }
                                break;
                            case "random":
                                listBox1.Items.Add(RandomFunction(block.blockData).ToString());
                                break;
                        }
                        #endregion
                        break;
                }
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
        #region block fromation
        private void DetermineBlocks()
        {
            blocks.Clear();
            //get text
            allLines = richTextBox1.Lines.ToList();
            //find blocks
            int i = 0;
            Block newBlock;
            while (i < allLines.Count)
            {
                if (allLines[i] == "cat")
                {
                    newBlock = new Block();
                    newBlock.Start = i;
                    if (allLines[allLines.Count-1] != "cat")
                    {
                        throw new Exception($"You didn close one of your functions. Stopped at line {allLines.Count -1}");
                    }
                    else
                    {
                        newBlock.End = allLines.Count - 1;
                    }
                    List<string> data = new List<string>();
                    int j = i;
                    while (j < allLines.Count)
                    {
                        if (allLines[j] == string.Empty)
                        {
                            if (allLines[j-1]!= "cat")
                            {
                                throw new Exception($"You didn close one of your functions. Stopped at line {j - 1}");
                            }
                            else
                            {
                                newBlock.End = j - 1;
                                break;
                            }
                        }
                        j++;
                    }
                    for (int l = newBlock.Start + 1; l < newBlock.End; l++)
                    {
                        data.Add(allLines[l]);
                    }
                    newBlock.blockData = data;
                    newBlock.FunctionFolder = DetermenBlockFunctionFolder(newBlock.blockData);
                    switch (newBlock.FunctionFolder)
                    {
                        case "math":
                            newBlock.FunctionInBlock = DetermenBlockFunction_Math(newBlock.blockData);
                            break;
                        case "text":
                            newBlock.FunctionInBlock = DetermenBlockFunction_Text(newBlock.blockData);
                            break;
                        case "loops":
                            newBlock.FunctionInBlock = DetermenBlockFunction_Loop(newBlock.blockData);
                            break;
                    }
                    blocks.Add(newBlock);
                    i = j;
                }
                else
                {
                    i++;
                }
            }
        }
        private string[] GetDataArr(List<string> initialList)
        {

            string[] data = new string[initialList.Count - 1];
            int transferIndex = 1;
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = initialList[transferIndex];
                transferIndex++;
            }
            return data;
        }
        private string DetermenBlockFunction_Math(List<string> initialList)
        {
            string functionLine = initialList[1];
            if (functionLine == "cat cat")
            {
                return "add";
            }
            else if(functionLine == "cat cat cat")
            {
                return "subtract";
            }
            else if (functionLine == "cat cat cat cat")
            {
                return "multiply";
            }
            else if (functionLine == "cat cat cat cat cat")
            {
                return "devide";
            }
            return string.Empty;
        }
        private string DetermenBlockFunction_Text(List<string> initialList)
        {
            string functionLine = initialList[1];
            if (functionLine == "cat cat")
            {
                return "write";
            }
            else if (functionLine == "cat cat cat")
            {
                return "writeline";
            }
            return string.Empty;
        }
        private string DetermenBlockFunction_Loop(List<string> initialList)
        {
            string functionLine = initialList[1];
            if (functionLine == "cat cat")
            {
                return "loop";
            }
            else if (functionLine == "cat cat cat")
            {
                return "reverse loop";
            }

            else if (functionLine == "cat cat cat cat")
            {
                return "random";
            }
            return string.Empty;
        }
        private string DetermenBlockFunctionFolder(List<string> initialList)
        {
            string functionLine = initialList[0];
            if (functionLine == "cat cat")
            {
                return "math";
            }
            else if (functionLine == "cat cat cat")
            {
                return "text";
            }
            else if (functionLine == "cat cat cat cat")
            {
                return "loops";
            }
            return string.Empty;
        }
        #endregion
        #region math functions
        private int AddFunction(List<string> initialList)
        {
            int output = 0;
            string[] data = GetDataArr(initialList);
            for (int i = 1; i < data.Length; i++)
            {
                string[] temp = data[i].Split(' ');
                output = output + temp.Length;
            }
            return output;
        }
        private int SubtractFunction(List<string> initialList)
        {
            int output = 0;
            string[] data = GetDataArr(initialList);
            int temp = data[1].Split(' ').Length;
            for (int i = 1; i < data.Length; i++)
            {
                output = temp - data[i].Split(' ').Length;
            }
            return output;
            
        }
        private int MultplyFunction(List<string> initialList)
        {
            int output = 1;
            string[] data = GetDataArr(initialList);
            for (int i = 1; i < data.Length; i++)
            {
                string[] temp = data[i].Split(' ');
                output = output * temp.Length;
            }
            return output;
        }
        private double DevideFunction(List<string> initialList)
        {
            double output = 0;
            string[] data = GetDataArr(initialList);
            if (data.Length > 3)
            {
                throw new Exception($"Devide functions can only have tow parametars.");
            }
            else
            {
                double temp = data[1].Split(' ').Length;
                output = temp / (data[2].Split(' ').Length);
                return output;
            }
        }

        private int RandomFunction(List<string> initialList)
        {
            int output = 0;
            Random random = new Random();
            string[] data = GetDataArr(initialList);
            if (data.Length > 3)
            {
                throw new Exception($"Random functions can only have tow parametars.");
            }
            else
            {
                int start = data[1].Split(' ').Length;
                int end = data[2].Split(' ').Length;
                output = random.Next(start, end);
                return output;
            }
        }
        #endregion
        #region text functions
        private string WriteOnSame(List<string> initialList)
        {
            string output = "";
            string[] data = GetDataArr(initialList);
            for (int i = 1; i < data.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (data[i].Split(' ').Length == j+2)
                    {
                        output = output + letters[j];
                    }
                }
                
            }
            return output;
        }
        private List<string> WriteOnNew(List<string> initialList)
        {
            string[] data = GetDataArr(initialList);
            List<string> output = new List<string>();
            string lettersLine = WriteOnSame(initialList);
            foreach (char letter in lettersLine)
            {
                output.Add(letter.ToString());
            }
            return output;
        }
        //private int AddFunction(List<string> initialList)
        //{
        //    int output = 0;
        //    string[] data = GetDataArr(initialList);
        //    for (int i = 1; i < data.Length; i++)
        //    {
        //        string[] temp = data[i].Split(' ');
        //        output = output + temp.Length;
        //    }
        //    return output;
        //}
        #endregion
        #region loops functions
        private List<string> Loop(List<string> initialList)
        {
            List<string> output = new List<string>();
            string[] data = GetDataArr(initialList);
            int start = data[1].Split(' ').Length;
            int end = data[2].Split(' ').Length;
            for (int i = start; i < end; i++)
            {
                output.Add(i.ToString());
            }
            return output;
        }
        private List<string> ReverseLoop(List<string> initialList)
        {
            List<string> output = new List<string>();
            string[] data = GetDataArr(initialList);
            int start = data[2].Split(' ').Length;
            int end = data[1].Split(' ').Length;
            for (int i = end; i > start; i--)
            {
                output.Add(i.ToString());
            }
            return output;
        }
        #endregion
    }
}

