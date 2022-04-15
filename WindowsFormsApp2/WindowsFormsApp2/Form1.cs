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
            string folder = "";
            string result = "";
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
                                result = AddFunction(block.blockData).ToString();
                                listBox1.Items.Add(result);
                                break;
                            case "subtract":
                                result = SubtractFunction(block.blockData).ToString();
                                listBox1.Items.Add(result);
                                break;
                            case "multiply":
                                result = MultplyFunction(block.blockData).ToString();
                                listBox1.Items.Add(result);
                                break;
                            case "devide":
                                result = DevideFunction(block.blockData).ToString();
                                listBox1.Items.Add(result);
                                break;
                        }
                        #endregion
                        break;
                    case "text":
                        switch (block.FunctionInBlock)
                        {
                            case"write":
                                listBox1.Items.Add(WriteOnSame(block.blockData));
                                break;
                            case "writeline":
                                listBox1.Items.Add(WriteOnNew(block.blockData));
                                break;
                        }
                        break;
                    case "loops":
                        break;
                }
            }
            //foreach (var item in blocks[0].blockData)
            //{
            //    listBox1.Items.Add(item);
            //}
            //foreach (var item in GetDataArr(blocks[0].blockData))
            //{
            //    listBox1.Items.Add(item);
            //}
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
                    newBlock.End = allLines.Count - 1;
                    List<string> data = new List<string>();
                    int j = i;
                    while (j < allLines.Count)
                    {
                        if (allLines[j] == string.Empty)
                        {
                            newBlock.End = j - 1;
                            break;
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
            else if (functionLine == "hoi4")
            {
                return "hoi4";
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
            double temp = data[1].Split(' ').Length;
            output = temp / (data[2].Split(' ').Length);
            return output;
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
        private string[] WriteOnNew(List<string> initialList)
        {
            string[] data = GetDataArr(initialList);
            string[] output = new string[data.Length-1];
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i].Split(' ').Length == i + 1)
                {
                    output[i-2] = letters[i-1];
                }
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
    }
}

