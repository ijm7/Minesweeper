﻿using System;
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
    public partial class Form4 : Form
    {
        string name;
        public Form4()
        {
            InitializeComponent();
            this.ControlBox = false;

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = textBox2.Text;
            Close();
        }

        public string getName()
        {
            return name;
        }
    }
}
