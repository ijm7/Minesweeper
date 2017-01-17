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
    public partial class Form3 : Form
    {
        string user, diff;
        int turn;

        public Form3(string userName,int turns, string difficulty)
        {
            InitializeComponent();
            user = userName;
            turn = turns;
            diff = difficulty;

            this.ControlBox = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                               "HighScores.txt");
            if (!File.Exists(fileName))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine(user + "\t" + Convert.ToString(turn) +"\t" + diff);
                    
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine(user + "\t" + Convert.ToString(turn) + "\t" + diff);

                }	
            }
            textBox2.Text = "Game Saved!";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
