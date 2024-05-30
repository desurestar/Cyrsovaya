namespace Анализатор
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            read_file = new Button();
            textBox2 = new TextBox();
            button1 = new Button();
            textBox3 = new TextBox();
            errors = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(38, 23);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(336, 380);
            textBox1.TabIndex = 0;
            // 
            // read_file
            // 
            read_file.Location = new Point(380, 23);
            read_file.Name = "read_file";
            read_file.Size = new Size(168, 94);
            read_file.TabIndex = 1;
            read_file.Text = "Открыть файл";
            read_file.UseVisualStyleBackColor = true;
            read_file.Click += button1_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(554, 23);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.Size = new Size(336, 380);
            textBox2.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(380, 137);
            button1.Name = "button1";
            button1.Size = new Size(168, 94);
            button1.TabIndex = 3;
            button1.Text = "Анализ";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(896, 23);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.ScrollBars = ScrollBars.Vertical;
            textBox3.Size = new Size(336, 380);
            textBox3.TabIndex = 4;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // errors
            // 
            errors.Location = new Point(554, 453);
            errors.Multiline = true;
            errors.Name = "errors";
            errors.ReadOnly = true;
            errors.Size = new Size(678, 138);
            errors.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(554, 435);
            label1.Name = "label1";
            label1.Size = new Size(127, 15);
            label1.TabIndex = 6;
            label1.Text = "Окно вывода ошибок";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 9);
            label2.Name = "label2";
            label2.Size = new Size(86, 15);
            label2.TabIndex = 7;
            label2.Text = "Окно для кода";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(554, 9);
            label3.Name = "label3";
            label3.Size = new Size(230, 15);
            label3.TabIndex = 8;
            label3.Text = "Вывод результата лексического анализа";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(896, 5);
            label4.Name = "label4";
            label4.Size = new Size(212, 15);
            label4.TabIndex = 9;
            label4.Text = "Вывод промежуточной информации";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1275, 639);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(errors);
            Controls.Add(textBox3);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(read_file);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button read_file;
        private TextBox textBox2;
        private Button button1;
        private TextBox textBox3;
        private TextBox errors;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
