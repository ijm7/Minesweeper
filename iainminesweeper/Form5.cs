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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
