using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iainmorton_coursework1
{
    public partial class Form2 : Form
    {
        int diff;
        public Form2()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            diff=1;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            diff=2;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            diff=3;
            Close();
        }

        public int diffReturn()
        {
            return diff;
        }
    }
}
