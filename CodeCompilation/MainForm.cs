using System;
using System.IO;
using System.Windows.Forms;
using ActiveMesa.R2P.Infrastructure;

namespace CodeCompilation
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonWork_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = new Worker().Work();
        }

        private int _countAssemblies = 1;
        private void buttonWork2_Click(object sender, EventArgs e)
        {
            textBoxResult.Text =
                ((IWorker)
                    new CSharpInProcessCompiler().CompileAndInstantiate(
                        File.ReadAllText("..\\..\\Worker.cs")
                            .Replace("return \"Work\";", "return \"Work replace=" + _countAssemblies++ + "\";"))).Work();
        }
    }
}
