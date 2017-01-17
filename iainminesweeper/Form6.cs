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

namespace iainmorton_coursework1
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            this.ControlBox = false;
            textBox1.Text = "PLAYER\tTURNS\tDIFFICULTY";
            displayScore();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void displayScore()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                               "HighScores.txt");
            StreamReader s = File.OpenText(fileName);
            string read = null;
            while ((read = s.ReadLine())!=null)
            {
                textBox2.AppendText(read+"\n");
            }
            s.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
