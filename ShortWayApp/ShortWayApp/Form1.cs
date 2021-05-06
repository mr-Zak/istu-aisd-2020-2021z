using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortWayApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Controls.Add(checkBox3);
            groupBox1.Controls.Add(checkBox4);
            groupBox1.Controls.Add(checkBox5);
            groupBox1.Controls.Add(checkBox6);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] checkedCitys = GetCheckedCity();
            int min = int.MaxValue;
            int count = 0;
            string result = textBox1.Text.ToUpper();
            GraphVertex X = null;

            textBox2.Text = Program.dijkstra.FindShortestPath(textBox1.Text.ToUpper(), checkedCitys, result, count);
            


        }

        private  string GetResultIfTwoVertex(string[] checkedCitys)
        {
            return Program.dijkstra.FindShortestPath(checkedCitys[0], checkedCitys[1]);
        }

        private string GetResultIfThreeVertex(string[] checkedCitys)
        {
            string path = "";
            int min;
            foreach(var city in checkedCitys)
            {
                foreach (var item in checkedCitys)
                {
                    if(city != item)
                    {
                       path += Program.dijkstra.FindShortestPath(city, item) ;
                    }
                }
            }
            return path;
        }

        private string[] GetCheckedCity()
        {
            int count = 0;
            foreach(CheckBox chb in groupBox1.Controls)
            {
                if (chb.Checked)
                {
                    var elem = Program.g.FindVertex(chb.Text);
                    if(elem != null)
                    {
                        count++;
                    }
                    
                }
            }
            string[] arr = new string[count+1];
            arr[0] = textBox1.Text.ToUpper();
            if (count == 0)
            {
                MessageBox.Show("Не выбран пункт назначения");
                return arr;
            }
            else
            {
                int i = 1;
                foreach (CheckBox chb in groupBox1.Controls)
                {
                    if (chb.Checked)
                    {
                        var elem = Program.g.FindVertex(chb.Text);
                        if (elem != null)
                        {
                            arr[i] = chb.Text;
                            i++;
                        }
                    }
                }
                return arr;
            }
        }
    }
}
