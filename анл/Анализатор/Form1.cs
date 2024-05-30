namespace Анализатор
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog.FileName;

                string text = File.ReadAllText(file);

                textBox1.Text = text;



            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string text = RemoveEmptyLines(textBox1.Text);

                List<Token> tokens = LexicalAnalyzer.LexAnalizing(text);
                int i = 1;
                textBox2.Clear();
                textBox3.Clear();
                errors.Clear();
                foreach (Token token in tokens)
                {
                    token.index = i;
                    if (token.Value == token.Type.ToString())
                    {
                        textBox2.Text += $"{i++}. {token.Value} \r\n";
                    }
                    textBox2.Text += $"{i++}. {token} \r\n";
                }
                new Parser(tokens).Program();
                textBox3.Text += $"Обратная польская запись выражений\r\n{Parser.res}\r\nМатрицы выражений{Parser.matr}";
            }
            catch (Exception ex) { errors.Text = ex.Message; }
        }

        
        public string RemoveEmptyLines(string input)
        {
            var lines = input.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var nonEmptyLines = lines.Where(line => !string.IsNullOrWhiteSpace(line));
            return string.Join("\r\n", nonEmptyLines);
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
