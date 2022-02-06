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

namespace TabMenager
{
    public partial class Form1 : Form
    {
        private string[] _fileNames;

        private string _txtFile;

        private List<int> _valuesToSearch = new List<int>
        {5, 10, 100, 1000, 10000, 100000, 500000, 1000000, 5000000, 10000000, 15000000, 20000000};

        public Form1()
        {
            InitializeComponent();

            checkBox1.Checked = true;
            checkBox2.Checked = false;
        }

        private void OpenFilesButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "TXT files (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                int indexColumn = 0;

                if (checkBox1.Checked)
                {
                    indexColumn = 2;
                }
                else
                {
                    indexColumn = 6;
                }
                
                _fileNames = openFileDialog1.FileNames;

                _txtFile = "";
                label2.Text = "";


                for (int f = 0; f < _fileNames.Length; f++)
                {
                    StreamReader sr = new StreamReader(_fileNames[f]);

                    List<List<float>> lines = new List<List<float>>();

                    string line = sr.ReadLine();

                    while (line != null)
                    {
                        if (line == "")
                        {
                            line = sr.ReadLine();
                            continue;
                        }

                        string[] elementsInLine = line.Split(',');

                        List<float> elements = new List<float>();

                        if (elementsInLine[0] != "" && elementsInLine[indexColumn] != "")
                        {
                            elements.Add(StringToFloat(elementsInLine[0]));
                            elements.Add(StringToFloat(elementsInLine[indexColumn]));
                        }
                        else
                        {
                            label2.Text += "Ошибка в одном из файлов: " + openFileDialog1.SafeFileNames[f] + "\n";
                        }

                        lines.Add(elements);

                        line = sr.ReadLine();
                    }

                    List<List<float>> filteredLines = new List<List<float>>();

                    for (int i = 0; i < _valuesToSearch.Count; i++)
                    {
                        var closest = float.MaxValue;
                        var minDifference = float.MaxValue;

                        int indexClosestLine = 0;

                        for (int j = 0; j < lines.Count; j++)
                        {
                            var difference = Math.Abs((float)lines[j][0] - _valuesToSearch[i]);
                            if (minDifference > difference)
                            {
                                minDifference = (float)difference;
                                closest = lines[j][0];
                                indexClosestLine = j;
                            }
                        }

                        filteredLines.Add(lines[indexClosestLine]);
                    }

                    string onlyName = openFileDialog1.SafeFileNames[f];

                    _txtFile += onlyName.Remove(onlyName.IndexOf('.'), 4);

                    foreach (var filteredLine in filteredLines)
                    {
                        _txtFile += "," + filteredLine[1].ToString().Replace(',','.');
                    }

                    _txtFile += "\n";

                    sr.Close();
                }
            }
        }

        private void SaveFilesButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "TXT files (*.txt)|*.txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                sw.Write(_txtFile);
                sw.Close();
            }
        }

        private float StringToFloat(string elementStr)
        {
            elementStr = elementStr.Replace('.',',');

            return float.Parse(elementStr);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = !checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox2.Checked;
        }
    }
}
