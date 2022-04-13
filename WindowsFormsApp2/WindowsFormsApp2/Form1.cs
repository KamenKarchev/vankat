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


        public Form1()
        {
            InitializeComponent();
            richTextBox1.Text = "cat\ncat cat\ncat cat\ncat cat cat cat cat\ncat";
            _var = 0;

            allLines = richTextBox1.Lines.ToList();
        }

        private void compile_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            DetermineBlocks();
            foreach (var block in blocks)
            {
                var number = DevideFunction(block.blockData);
                listBox1.Items.Add(String.Format("{0:0.##}", number));
            }
            listBox1.Items.Add(1940.308);
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
        private int FunctionDeterminar(List<string> initialList)
        {
            return 0;
        }
        private int AddFunction(List<string> initialList)
        {
            int output = 0;
            string[] data = GetDataArr(initialList);
            for (int i = 0; i < data.Length; i++)
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
            int temp = data[0].Split(' ').Length;
            for (int i = 0; i < data.Length; i++)
            {
                output = temp - data[i].Split(' ').Length;
            }
            return output;
            
        }
        private int MultplyFunction(List<string> initialList)
        {
            int output = 0;
            string[] data = GetDataArr(initialList);
            for (int i = 0; i < data.Length; i++)
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
            int temp = data[0].Split(' ').Length;
            //for (int i = 0; i < data.Length; i++)
            //{
            //    output = temp / (data[i].Split(' ').Length);
            //}
            output = temp / (data[1].Split(' ').Length);
            return output;
        }
    }
}

