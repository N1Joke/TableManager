using System;
using System.Collections.Generic;
using System.Text;
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
        private int indexEpsilon = 8;
        private int indexEpsilon1 = 9;
        private int indexEpsilon2 = 10;

        private bool resetValuesToSearch = true;

        public Form1()
        {
            InitializeComponent();

            _valuesToSearch = new List<int>
        {5, 10, 100, 1000, 10000, 100000, 500000, 1000000, 5000000, 10000000, 15000000, 20000000};

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
            {
                DoTemperatureSearch();

                saveFileDialog1.Filter = "TXT files (*.txt)|*.txt";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                    sw.Write(_txtFile);
                    sw.Close();

                    MessageBox.Show("File was successfully created!!!");
                }
            }
            else
                DoCalculateValues();
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
                        label2.Text += "Error in file: " + openFileDialog1.SafeFileNames[f] + "\n";
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

                onlyName = onlyName.Remove(onlyName.IndexOf('.'), 4);

                if (onlyName.Contains(" calculated"))
                {
                    int r = onlyName.IndexOf(" calculated");
                    onlyName = onlyName.Remove(r, 11);
                }

                _txtFile += onlyName;

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
            if (_fileNames == null)
                return;

            for (int f = 0; f < _fileNames.Length; f++)
            {
                _txtFile = "";

                string name = openFileDialog1.SafeFileNames[f];

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
                            label2.Text += "Error in file: " + openFileDialog1.SafeFileNames[f] + "\n";
                    }

                    //Epsilon E* = (C * d) / (E0 * S)
                    //float epsilon = (elements[indexCapacity] * StringToFloat(thickness_textBox.Text)) /(EpsZero * StringToFloat(area_textBox.Text));
                    float epsilon = elements[indexCapacity] * StringToFloat(multiplier_textBox.Text);
                    elements.Add(epsilon);

                    //Real part Epsilon E' = E* / (sqrt(1 + tg^2(delta))) ---- Math.Pow(Math.Tan(delta), 2)
                    float reEpsilon = (float)(epsilon / Math.Sqrt(1 + Math.Pow(elements[indexTgDelta], 2)));
                    elements.Add(reEpsilon);

                    //Imaginary part Epsilon E'' = E' * tg(delta)
                    float imEpsilon = reEpsilon * (float)elements[indexTgDelta];
                    elements.Add(imEpsilon);

                    lines.Add(elements);

                    line = sr.ReadLine();
                }

                foreach (var l in lines)
                {
                    foreach (var l2 in l)
                    {                        
                        _txtFile += l2.ToString().Replace(',', '.') + ",";
                    }

                    _txtFile += "\n";
                }

                sr.Close();

                name = name.Replace(".txt", "");

                string path = _fileNames[f].Replace(name, name + " calculated");

                FileStream fs = File.Create(path);

                byte[] info = new UTF8Encoding(true).GetBytes(_txtFile);

                fs.Write(info, 0, info.Length);

                fs.Close();
            }

            MessageBox.Show("Files were successfully recalculated and saved!!!");
        }

        private float StringToFloat(string elementStr)
        {
            elementStr = elementStr.Replace('.', ',');

            return float.Parse(elementStr);
        }
                
        private void UpdateColumnIndex()
        {
            if (checkBox1.Checked)
            {
                indexTemteratureSearchColumn = indexTgDelta;
            }
            else if (checkBox2.Checked)
            {
                indexTemteratureSearchColumn = 1;
            }
            else if (EpsilonChB.Checked)
            {
                indexTemteratureSearchColumn = indexEpsilon;
            }
            else if (Epsilon1ChB.Checked)
            {
                indexTemteratureSearchColumn = indexEpsilon1;
            }
            else if (Epsilon2ChB.Checked)
            {
                indexTemteratureSearchColumn = indexEpsilon2;
            }
        }

        private bool canClearValuesToSearch = true;

        private void SetUpStartValueToSearch()
        {
            if (!resetValuesToSearch)
                return;

            canClearValuesToSearch = false;

            dataGridView1.Rows.Clear();

            for (int i = 0; i < _valuesToSearch.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = _valuesToSearch[i];
            }

            canClearValuesToSearch = true;
            resetValuesToSearch = false;
        }

        private void UpdateValuesToSearch()
        {
            if (!canClearValuesToSearch)
                return;

            _valuesToSearch.Clear();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value == null)
                    continue;

                try
                {
                    int value = 0;
                    if (dataGridView1.Rows[i].Cells[0].Value is string)
                        value = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                    else
                        value = (int)dataGridView1.Rows[i].Cells[0].Value;
                    _valuesToSearch.Add(value);
                }
                catch
                {
                    dataGridView1.Rows[i].Cells[0].Value = null;
                }
            }

            SetUpStartValueToSearch();
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
                EpsilonChB.Visible = true;
                Epsilon1ChB.Visible = true;
                Epsilon2ChB.Visible = true;
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
                EpsilonChB.Visible = false;
                Epsilon1ChB.Visible = false;
                Epsilon2ChB.Visible = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                EpsilonChB.Checked = false;
                Epsilon1ChB.Checked = false;
                Epsilon2ChB.Checked = false;

                UpdateColumnIndex();
            }            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                EpsilonChB.Checked = false;
                Epsilon1ChB.Checked = false;
                Epsilon2ChB.Checked = false;

                UpdateColumnIndex();
            }
        }

        private void EpsilonChB_CheckedChanged(object sender, EventArgs e)
        {
            if (EpsilonChB.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                Epsilon1ChB.Checked = false;
                Epsilon2ChB.Checked = false;

                UpdateColumnIndex();
            }
        }

        private void Epsilon1ChB_CheckedChanged(object sender, EventArgs e)
        {
            if (Epsilon1ChB.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                EpsilonChB.Checked = false;
                Epsilon2ChB.Checked = false;

                UpdateColumnIndex();
            }
        }

        private void Epsilon2ChB_CheckedChanged(object sender, EventArgs e)
        {
            if (Epsilon2ChB.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                EpsilonChB.Checked = false;
                Epsilon1ChB.Checked = false;

                UpdateColumnIndex();
            }            
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateValuesToSearch();
            resetValuesToSearch = true;
        }
    }
}
