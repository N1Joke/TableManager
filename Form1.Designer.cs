
namespace TabMenager
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.OpenFilesButton = new System.Windows.Forms.Button();
            this.SaveFilesButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.thickness_textBox = new System.Windows.Forms.TextBox();
            this.thickness_label = new System.Windows.Forms.Label();
            this.area_label = new System.Windows.Forms.Label();
            this.area_textBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.valueToSearch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TemperatureSearch = new System.Windows.Forms.CheckBox();
            this.CalculateValues = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // OpenFilesButton
            // 
            this.OpenFilesButton.Location = new System.Drawing.Point(12, 41);
            this.OpenFilesButton.Name = "OpenFilesButton";
            this.OpenFilesButton.Size = new System.Drawing.Size(164, 47);
            this.OpenFilesButton.TabIndex = 0;
            this.OpenFilesButton.Text = "Open files";
            this.OpenFilesButton.UseVisualStyleBackColor = true;
            this.OpenFilesButton.Click += new System.EventHandler(this.OpenFilesButton_Click);
            // 
            // SaveFilesButton
            // 
            this.SaveFilesButton.Location = new System.Drawing.Point(223, 41);
            this.SaveFilesButton.Name = "SaveFilesButton";
            this.SaveFilesButton.Size = new System.Drawing.Size(164, 47);
            this.SaveFilesButton.TabIndex = 1;
            this.SaveFilesButton.Text = "Save files";
            this.SaveFilesButton.UseVisualStyleBackColor = true;
            this.SaveFilesButton.Click += new System.EventHandler(this.SaveFilesButton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 103);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(65, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Tg delta";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 134);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(82, 17);
            this.checkBox2.TabIndex = 3;
            this.checkBox2.Text = "C - capacity";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 432);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 5;
            // 
            // thickness_textBox
            // 
            this.thickness_textBox.Location = new System.Drawing.Point(204, 100);
            this.thickness_textBox.Name = "thickness_textBox";
            this.thickness_textBox.Size = new System.Drawing.Size(183, 20);
            this.thickness_textBox.TabIndex = 6;
            // 
            // thickness_label
            // 
            this.thickness_label.AutoSize = true;
            this.thickness_label.Location = new System.Drawing.Point(139, 104);
            this.thickness_label.Name = "thickness_label";
            this.thickness_label.Size = new System.Drawing.Size(59, 13);
            this.thickness_label.TabIndex = 7;
            this.thickness_label.Text = "Thickness:";
            // 
            // area_label
            // 
            this.area_label.AutoSize = true;
            this.area_label.Location = new System.Drawing.Point(139, 134);
            this.area_label.Name = "area_label";
            this.area_label.Size = new System.Drawing.Size(32, 13);
            this.area_label.TabIndex = 8;
            this.area_label.Text = "Area:";
            // 
            // area_textBox
            // 
            this.area_textBox.Location = new System.Drawing.Point(204, 131);
            this.area_textBox.Name = "area_textBox";
            this.area_textBox.Size = new System.Drawing.Size(183, 20);
            this.area_textBox.TabIndex = 9;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.valueToSearch});
            this.dataGridView1.Location = new System.Drawing.Point(12, 172);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(375, 315);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView1_CellValuePushed);
            // 
            // valueToSearch
            // 
            this.valueToSearch.HeaderText = "Frequency to search";
            this.valueToSearch.Name = "valueToSearch";
            this.valueToSearch.Width = 325;
            // 
            // TemperatureSearch
            // 
            this.TemperatureSearch.AutoSize = true;
            this.TemperatureSearch.Location = new System.Drawing.Point(14, 12);
            this.TemperatureSearch.Name = "TemperatureSearch";
            this.TemperatureSearch.Size = new System.Drawing.Size(121, 17);
            this.TemperatureSearch.TabIndex = 11;
            this.TemperatureSearch.Text = "Temperature search";
            this.TemperatureSearch.UseVisualStyleBackColor = true;
            this.TemperatureSearch.CheckedChanged += new System.EventHandler(this.TemperatureSearch_CheckedChanged);
            // 
            // CalculateValues
            // 
            this.CalculateValues.AutoSize = true;
            this.CalculateValues.Location = new System.Drawing.Point(142, 12);
            this.CalculateValues.Name = "CalculateValues";
            this.CalculateValues.Size = new System.Drawing.Size(104, 17);
            this.CalculateValues.TabIndex = 12;
            this.CalculateValues.Text = "Calculate values";
            this.CalculateValues.UseVisualStyleBackColor = true;
            this.CalculateValues.CheckedChanged += new System.EventHandler(this.CalculateValues_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 499);
            this.Controls.Add(this.CalculateValues);
            this.Controls.Add(this.TemperatureSearch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.area_textBox);
            this.Controls.Add(this.area_label);
            this.Controls.Add(this.thickness_label);
            this.Controls.Add(this.thickness_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.SaveFilesButton);
            this.Controls.Add(this.OpenFilesButton);
            this.Name = "Form1";
            this.Text = "Table Manager";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button OpenFilesButton;
        private System.Windows.Forms.Button SaveFilesButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox thickness_textBox;
        private System.Windows.Forms.Label thickness_label;
        private System.Windows.Forms.Label area_label;
        private System.Windows.Forms.TextBox area_textBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueToSearch;
        private System.Windows.Forms.CheckBox TemperatureSearch;
        private System.Windows.Forms.CheckBox CalculateValues;
    }
}

