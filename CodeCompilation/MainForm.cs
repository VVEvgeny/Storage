using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ActiveMesa.R2P.Infrastructure;
using Microsoft.CSharp;

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
