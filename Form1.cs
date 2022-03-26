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

        private int indexTemteratureSearchColumn;

        const float EpsZero = 8.85E-12f;

        private int indexCapacity = 6;
        private int indexTgDelta = 2;

        public Form1()
        {
            InitializeComponent();

            checkBox1.Checked = true;
            checkBox2.Checked = false;

            UpdateColumnIndex();
            SetUpStartValueToSearch();

            TemperatureSearch.Checked = true;
            CalculateValues.Checked= false;
            UpdateInterface();
        }

        private void OpenFilesButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "TXT files (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _fileNames = openFileDialog1.FileNames;

                _txtFile = "";
                label2.Text = "";
            }
        }

        private void SaveFilesButton_Click(object sender, EventArgs e)
        {
            if (TemperatureSearch.Checked)
                DoTemperatureSearch();
            else
                DoCalculateValues();

            saveFileDialog1.Filter = "TXT files (*.txt)|*.txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                sw.Write(_txtFile);
                sw.Close();
            }
        }

        private void DoTemperatureSearch()
        {
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

                    if (elementsInLine[0] != "" && elementsInLine[indexTemteratureSearchColumn] != "")
                    {
                        elements.Add(StringToFloat(elementsInLine[0]));
                        elements.Add(StringToFloat(elementsInLine[indexTemteratureSearchColumn]));

                    }
                    else
                    {
                        label2.Text += "Ошибка в файлe: " + openFileDialog1.SafeFileNames[f] + "\n";
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
                    _txtFile += "," + filteredLine[1].ToString().Replace(',', '.');
                }

                _txtFile += "\n";

                sr.Close();
            }
        }

        private void DoCalculateValues()
        {
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

                    for (int el = 0; el < elementsInLine.Length; el++)
                    {
                        if (elementsInLine[el] != "")
                            elements.Add(StringToFloat(elementsInLine[el]));
                        else
                            label2.Text += "Ошибка в файлe: " + openFileDialog1.SafeFileNames[f] + "\n";
                    }

                    //Epsilon E* = (C * d) / (E0 * S)
                    float epsilon = (elements[indexCapacity] * StringToFloat(thickness_textBox.Text)) /(EpsZero * StringToFloat(area_textBox.Text));
                    elements.Add(epsilon);

                    //Real part Epsilon E' = E* / (sqrt(1 + tg^2(delta))) ---- Math.Pow(Math.Tan(delta), 2)
                    float reEpsilon = (float)(epsilon / Math.Sqrt(1 + Math.Pow(Math.Tan(elements[indexTgDelta]), 2)));
                    elements.Add(reEpsilon);

                    //Imaginary part Epsilon E'' = E' * tg(delta)
                    float imEpsilon = reEpsilon * (float)Math.Tan(elements[indexTgDelta]);
                    elements.Add(imEpsilon);

                    lines.Add(elements);

                    line = sr.ReadLine();
                }

                foreach (var l in lines)
                {
                    _txtFile += "," + l[1].ToString().Replace(',', '.');
                }

                _txtFile += "\n";

                sr.Close();
            }
        }

        private float StringToFloat(string elementStr)
        {
            elementStr = elementStr.Replace('.', ',');

            return float.Parse(elementStr);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = !checkBox1.Checked;
            UpdateColumnIndex();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox2.Checked;
            UpdateColumnIndex();
        }

        private void UpdateColumnIndex()
        {
            if (checkBox1.Checked)
            {
                indexTemteratureSearchColumn = indexTgDelta;
            }
            else
            {
                indexTemteratureSearchColumn = indexCapacity;
            }
        }

        private void SetUpStartValueToSearch()
        {
            dataGridView1.Rows.Clear();

            for (int i = 0; i < _valuesToSearch.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = _valuesToSearch[i];
            }
        }

        private void UpdateValuesToSearch(object sender, DataGridViewCellEventArgs e)
        {
            _valuesToSearch.Clear();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    int value = (int)dataGridView1.Rows[i].Cells[0].Value;
                    _valuesToSearch.Add(value);
                }
                catch
                {
                    dataGridView1.Rows[i].Cells[0].Value = "";
                }
            }
        }

        private void TemperatureSearch_CheckedChanged(object sender, EventArgs e)
        {
            CalculateValues.Checked = !TemperatureSearch.Checked;
            UpdateInterface();
        }

        private void CalculateValues_CheckedChanged(object sender, EventArgs e)
        {
            TemperatureSearch.Checked = !CalculateValues.Checked;            
            UpdateInterface();
        }

        private void UpdateInterface()
        {
            if (TemperatureSearch.Checked)
            {
                thickness_label.Visible = false;
                thickness_textBox.Visible = false;
                area_label.Visible = false;
                area_textBox.Visible = false;

                dataGridView1.Visible = true;
                checkBox1.Visible = true;
                checkBox2.Visible = true;
            }
            else
            {
                thickness_label.Visible = true;
                thickness_textBox.Visible = true;
                area_label.Visible = true;
                area_textBox.Visible = true;

                dataGridView1.Visible = false;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
            }
        }
    }
}
