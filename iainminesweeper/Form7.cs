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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            this.ControlBox = false;

            textBox1.AppendText("\n");
            textBox1.AppendText("\n");
            textBox1.AppendText("Selected difficulty will vary the size of the game map and the amount of mines.\n");
            textBox1.AppendText("To clear the game, left click which have no mines in them. A blank tile has no mines, whereas a tile with a number indicates how many mines are in its immediate proximity.\n");

            textBox1.AppendText("Once you have identified a mine, right click on that space to indicate that you have found it. Find all mines to clear the game.\n");
            textBox1.AppendText("A turn counter indicates how many left clicks you have taken to search the game map. The mines number indicates how many mines you have yet to select. Do not go past zero or you risk hitting a mine!\n");
            textBox1.AppendText("Once a game is won, you will have the opportunity to save a high score, to do so, select the high scores button. These can be viewed later on using the game menu.\n");
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }
    }
}
