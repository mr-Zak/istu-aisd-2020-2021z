using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ShortWayApp
{
    public partial class Form1 : Form
    {
        public static string[] checkedCitys;
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
            textBox2.Clear();
            checkedCitys = GetCheckedCity();
            string result = "";
            string startVert = textBox1.Text.ToUpper();
            string st = textBox1.Text.ToUpper();
            string previousVert = "";
            if(checkedCitys.Length < 2)
            {
                st = Program.dijkstra.FindShortestPath(startVert, checkedCitys[0]);

            }
            else
            {
                for(int i = 0; i < checkedCitys.Length; i++) 
                {
                    if(startVert.Length > 1)
                    {
                        startVert = startVert[startVert.Length - 1].ToString();
                    }
                    result = Program.dijkstra.FindShortestPath(startVert, checkedCitys, previousVert);
                    st += result;
                    previousVert = startVert;
                    startVert = result;
                }
            }
                string[] returnedArr = st.ToCharArray().Select(i => (i.ToString())).ToArray();
                returnedArr[0] += " (начальный город)";
                for (int i = 1; i < returnedArr.Length; i++)
                {
                    int indexF = st.IndexOf(returnedArr[i][0].ToString());
                    int indexL = st.LastIndexOf(returnedArr[i][0].ToString());
                    int indexOfArr = Array.IndexOf(checkedCitys, returnedArr[i][0].ToString());
                    if (indexOfArr < 0)
                    {
                        returnedArr[i] += " (транзитный город)";
                    }
                    else if (indexF >= 0 && indexF == indexL)
                    {
                        returnedArr[i] += " ( город адресат )";
                    }
                    else if (indexF >= 0 && indexL >= 0 && indexF != indexL)
                    {
                        returnedArr[indexF] += " ( город адресат )";
                        returnedArr[indexL] += " (транзитный город)";
                    }
                }
                for(int i = 0; i < returnedArr.Length; i++)
                {
                    if(returnedArr[i].Length >= 20)
                    {
                        returnedArr[i] = returnedArr[i].Substring(0, 20);
                    }
                    textBox2.Text += returnedArr[i] + Environment.NewLine;
                }

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
            string[] arr = new string[count];
            arr[0] = textBox1.Text.ToUpper();
            if (count == 0)
            {
                MessageBox.Show("Не выбран пункт назначения");
                return arr;
            }
            else
            {
                int i = 0;
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
