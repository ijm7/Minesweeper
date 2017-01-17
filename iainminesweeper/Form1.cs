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
    //IAIN MORTON, 11/02/2016
    //MINESWEEPER IMPLEMENTATION IN C#
    public partial class Form1 : Form
    {
        Button[,] btn;
        Panel dynamicPanel,backPanel;
        int difficultyLevel,turns;
        int[,] hidden;
        bool[,] revealed;
        bool fillingBorders;
        int noMines,minesFound,time;
        Random random1;
        string sunk,userName;
        int xRange, yRange;
        
        
        public Form1()
        {
            InitializeComponent();
            initialise();
            
        }

        //  Method that takes a clicked button and produces the result of that click
        void btnEvent_MouseDown(object sender, MouseEventArgs e)
        {
            int foundX = -1;
            int foundY = -1;
                            for (int i = 0; i < btn.GetLength(0) && foundX < 0; ++i)
                            {
                                for (int j = 0; j < btn.GetLength(1); ++j)
                                {
                                    if (btn[i, j] == (object)sender) 
                                    {
                                        foundX = i;
                                        foundY = j;
                                        break;
                                    }
                                }
                            }
                            if (e.Button == MouseButtons.Right)
                            {
                                if (btn[foundX, foundY].Text == "M" && minesFound>0)
                                {
                                    btn[foundX, foundY].Text = "";
                                    noMines++;
                                }
                                
                                else
                                {

                                    noMines--;
                                    btn[foundX, foundY].Text = "M";
                                    if (noMines < 0)
                                    {
                                        textBox6.Text = "WARNING MINES";
                                        int risk = randCoord(10);
                                        if (risk == 1)
                                        {
                                            failure(sunk);
                                        }
                                    }
                                }
                                if (hidden[foundX, foundY] == 9)
                                {
                                    minesFound--;
                                }
                                if (minesFound==0)
                                {
                                    displayAll();
                                    victory();
                                }
                                countingMines();
                            }
                            else
                            {
                                //btn[foundX, foundY].BackColor = Color.LightGray;
                                
                                if (hidden[foundX, foundY]==9)
                                {
                                    
                                        btn[foundX, foundY].BackColor = Color.Red;
                                        btn[foundX, foundY].Text = "M";
                                        textBox6.Text = "MINE HIT";
                                        sunk = "You Hit a mine";
                                        displayAll();
                                        failure(sunk);
                                    
                                }
                                else if (hidden[foundX, foundY] == 0)
                                {
                                    textBox6.Text = "NO MINES IN THIS SPACE";
                                    int whiteX, whiteY, checker;
                                    whiteX = foundX;
                                    whiteY = foundY;
                                    floodFill(whiteX,whiteY);
                                    fillingBorders = true;
                                    findBorders();
                                    fillingBorders = false;
                                    floodFill(whiteX, whiteY);
                                    
                                   

                                }
                                else
                                {
                                    btn[foundX, foundY].BackColor = Color.LightGray;
                                    btn[foundX, foundY].Text = Convert.ToString(hidden[foundX, foundY]);
                                    textBox6.Text = hidden[foundX, foundY] + " MINES NEARBY";
                                }
                                ((Button)sender).Enabled = false;
                                turns++;
                                if (turns < 10)
                                {
                                    textBox2.Text = "00" + Convert.ToString(turns);
                                }
                                else if (turns>9 && turns<100)
                                {
                                    textBox2.Text = "0" + Convert.ToString(turns);
                                }
                                else if (turns>99 && turns<1000)
                                {
                                    textBox2.Text = Convert.ToString(turns);
                                }
                                else
                                {
                                    sunk = "You exceeded the turn limit";
                                    failure(sunk);
                                }



                            }
                    }

        // Implementation of an 8-way flood fill algorithm to fill out blank spaces
        private void floodFill(int x, int y)
        {

            if (x >= 0 && x <= xRange && y >= 0 && y <= yRange)
            {

                if (hidden[x, y] < 1 && btn[x, y].BackColor != Color.LightGray)
                {
                    //This algorithm adapted from http://lodev.org/cgtutor/floodfill.html#8-Way_Recursive_Method_floodFill8
                    fillButton(x, y);
                    floodFill(x + 1, y);
                    floodFill(x - 1, y);
                    floodFill(x, y + 1);
                    floodFill(x, y - 1);
                    floodFill(x + 1, y + 1);
                    floodFill(x + 1, y - 1);
                    floodFill(x - 1, y + 1);
                    floodFill(x - 1, y - 1);
                   
                    
                }
                else
                {
                    return;
                }
            }
        }

        //Augmentation of the flood fill algorithm to allow the border of the filled area to be produced
        private void findBorders()
        {
            for (int x = 0; x <= xRange; x++)         // Loop for x
            {
                for (int y = 0; y <= yRange; y++)     // Loop for y
                {
                        if (revealed[x,y]==true )
                        {
                            if (x != 0 && x != xRange && y != 0 && y != yRange) //NO EDGES
                            {
                                fillButton(x + 1, y + 1);
                                fillButton(x + 1, y - 1);
                                fillButton(x - 1, y - 1);
                                fillButton(x - 1, y + 1);
                                fillButton(x, y + 1);
                                fillButton(x, y - 1);
                                fillButton(x + 1, y);
                                fillButton(x - 1, y);
                            }
                            else if (x == 0 && x != xRange && y == 0 && y != yRange) //TOP LEFT
                            {
                                fillButton(x + 1, y);
                                fillButton(x + 1, y + 1);
                                fillButton(x, y + 1);
                               
                                
                            }
                            else if (x == 0 && x != xRange && y != 0 && y != yRange) //LEFT
                            {
                                fillButton(x, y - 1);
                                fillButton(x + 1, y - 1);
                                fillButton(x + 1, y);
                                fillButton(x + 1, y + 1);
                                fillButton(x, y + 1);
                                
                            }
                            else if (x != 0 && x != xRange && y == 0 && y != yRange) //TOP
                            {
                                fillButton(x + 1, y);
                                fillButton(x + 1, y + 1);
                                fillButton(x, y + 1);
                                fillButton(x - 1, y + 1);
                                fillButton(x - 1, y);

                            }
                            else if (x != 0 && x == xRange && y == 0 && y != yRange) //TOP RIGHT
                            {
                                fillButton(x, y + 1);
                                fillButton(x - 1, y + 1);
                                fillButton(x - 1, y);
                            }
                            else if (x != 0 && x == xRange && y != 0 && y != yRange) // RIGHT
                            {
                                fillButton(x, y + 1);
                                fillButton(x - 1, y + 1);
                                fillButton(x - 1, y);
                                fillButton(x - 1, y - 1);
                                fillButton(x, y - 1);

                            }
                            else if (x != 0 && x == xRange && y != 0 && y == yRange) // BOTTOM RIGHT
                            {
                                fillButton(x - 1, y);
                                fillButton(x - 1, y - 1);
                                fillButton(x, y - 1);

                            }
                            else if (x != 0 && x != xRange && y != 0 && y == yRange) // BOTTOM
                            {
                                fillButton(x - 1, y);
                                fillButton(x - 1, y - 1);
                                fillButton(x, y - 1);
                                fillButton(x + 1, y - 1);
                                fillButton(x + 1, y);
                            }
                            else if (x == 0 && x != xRange && y != 0 && y == yRange) // BOTTOM LEFT
                            {
                                fillButton(x, y - 1);
                                fillButton(x + 1, y - 1);
                                fillButton(x + 1, y);

                            }
                        }
                }
            }
        }

        //Method that assigns properties to a defined button 
        private void fillButton(int x, int y)
        {
            btn[x, y].Enabled = false;
            btn[x, y].BackColor = Color.LightGray;
            
            if (fillingBorders==true)
            {
                if (hidden[x, y] > 0)
                {
                    btn[x, y].Text = Convert.ToString(hidden[x, y]);
                }
                else
                {
                    //btn[x, y].Text = "";
                }
            }
            else
            {
                btn[x, y].Text = "";
                revealed[x, y] = true;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Minesweeper Implementation in C# by Iain Morton (c) 2016", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //initialise();
            //Application.Restart();
            //Used this http://stackoverflow.com/questions/95098/why-is-application-restart-not-reliable
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        // Prepares the GUI and game rules
        private void initialise()
        {
            time = 0;
            turns = 0;
            //int promptValue = Prompt.ShowDialog("Test", "123");
            random1 = new Random();
            Form2 settingsForm = new Form2();
            settingsForm.ShowDialog();
            difficultyLevel=settingsForm.diffReturn();
            Form4 nameGetter = new Form4();
            nameGetter.ShowDialog();
            userName = nameGetter.getName();
            textBox7.Text = userName;
            backPanel = new Panel();
            backPanel.Location = new System.Drawing.Point(140, 39);
            backPanel.Name = "Panel2";
            backPanel.Size = new System.Drawing.Size(400, 400);
            backPanel.BorderStyle = BorderStyle.Fixed3D;
            backPanel.BackColor = Color.LightGray;
            backPanel.SendToBack();
            Controls.Add(backPanel);
            dynamicPanel = new Panel();
            
            
            int generalx=0, generaly=0;
            fillingBorders = false;
            if (difficultyLevel==1)
            {
                hidden = new int[9, 9];
                btn = new Button[9, 9];
                revealed = new bool[9, 9];
                //hideAll();
                generalx = 8;
                generaly = 8;
                generateMines(generalx,generaly);
                dynamicPanel.Location = new System.Drawing.Point(243, 135);
                dynamicPanel.Name = "Panel1";
                dynamicPanel.Size = new System.Drawing.Size(190, 190);
                dynamicPanel.BackColor = Color.Black;
                Controls.Add(dynamicPanel);

            }
            else if (difficultyLevel==2)
            {
                hidden = new int[15, 15];
                btn = new Button[15, 15];
                revealed = new bool[15, 15];
                generalx = 14;
                generaly = 14;
                generateMines(generalx, generaly);
                dynamicPanel.Location = new System.Drawing.Point(180, 75);
                dynamicPanel.Name = "Panel1";
                dynamicPanel.Size = new System.Drawing.Size(316, 316);
                dynamicPanel.BackColor = Color.Black;
                Controls.Add(dynamicPanel);
            }
            else if (difficultyLevel==3)
            {
                hidden = new int[19, 19];
                btn = new Button[19, 19];
                revealed = new bool[19, 19];
                generalx = 18;
                generaly = 18;
                generateMines(generalx, generaly);
                dynamicPanel.Location = new System.Drawing.Point(140, 39);
                dynamicPanel.Name = "Panel1";
                dynamicPanel.Size = new System.Drawing.Size(400, 400);
                dynamicPanel.BackColor = Color.Black;
                Controls.Add(dynamicPanel);
            }
            xRange = generalx;
            yRange = generaly;
            /*
            hidden = new int[19, 19];
            btn = new Button[19, 19];
            
            */

            for (int x = 0; x < btn.GetLength(0); x++)         // Loop for x
            {
                for (int y = 0; y < btn.GetLength(1); y++)     // Loop for y
                {
                    btn[x, y] = new Button();
                    this.btn[x, y].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btn[x, y].SetBounds(21 * x, 21 * y, 22, 22);
                    btn[x, y].BackColor = Color.SlateGray;
                    btn[x, y].MouseDown += new MouseEventHandler(this.btnEvent_MouseDown);
                    dynamicPanel.Controls.Add(btn[x, y]);
                }
            }
            backPanel.SendToBack();
            countingMines();
            minesFound = noMines;
            xRange = generalx;
            yRange = generaly;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)  //REQUIRED
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        // Generates all mines
        private void generateMines(int generalx,int generaly)
        {
            int a, b;
            
            int mineCount=0;
            a = 0;
            b = 0;
            if (difficultyLevel==1)
            {
                noMines = 10;
            }
            else if (difficultyLevel == 2)
            {
                noMines = 28;
            }
            else if (difficultyLevel == 3)
            {
                noMines = 45;
            }
            for (int i = 0; i < noMines; i++)
            {
                a = randCoord(generalx+1);
                b = randCoord(generaly+1);
                
                
                if(hidden[a,b]!=9)
                {
                    hidden[a, b] = 9;
                }
                else
                {
                    i--;
                }
                
            }
            for (int x = 0; x <= generalx; x++)         // Loop for x
            {
                for (int y = 0; y <= generaly; y++)     // Loop for y
                {
                    revealed[x,y]=false;
                    if(hidden[x,y]!=9)
                    {
                        mineCount = 0;
                        if (x != 0 && x != generalx && y != 0 && y != generaly) //NO EDGES
                        {
                            mineCount=retTopLeft(x,y)+retTop(x,y)+retTopRight(x,y)+retRight(x,y)
                                      +retBottomRight(x,y)+retBottom(x,y)+retBottomLeft(x,y)+retLeft(x,y);
                        }
                        else if (x == 0 && x != generalx && y == 0 && y != generaly) //TOP LEFT
                        {
                            mineCount=retRight(x,y)+retBottomRight(x,y)+retBottom(x,y); 
                        }
                        else if (x == 0 && x != generalx && y != 0 && y != generaly) //LEFT
                        {
                            mineCount = retTop(x, y) + retTopRight(x, y) + retRight(x, y) 
                                + retBottomRight(x, y) + retBottom(x, y);
                        }
                        else if (x != 0 && x != generalx && y == 0 && y != generaly) //TOP
                        {
                            mineCount = retRight(x, y) + retBottomRight(x, y) + retBottom(x, y)
                                + retBottomLeft(x, y) + retLeft(x, y);
                        }
                        else if (x != 0 && x == generalx && y == 0 && y != generaly) //TOP RIGHT
                        {
                            mineCount = retLeft(x, y) + retBottomLeft(x, y) + retBottom(x, y);
                        }
                        else if (x != 0 && x == generalx && y != 0 && y != generaly) // RIGHT
                        {
                            mineCount = retTop(x, y) + retTopLeft(x, y) + retLeft(x, y)
                                + retBottomLeft(x, y) + retBottom(x, y);
                        }
                        else if (x != 0 && x == generalx && y != 0 && y == generaly) // BOTTOM RIGHT
                        {
                            mineCount = retTop(x, y) + retTopLeft(x, y) + retLeft(x, y);
                        }
                        else if (x != 0 && x != generalx && y != 0 && y == generaly) // BOTTOM
                        {
                            mineCount = retTop(x, y) + retTopLeft(x, y) + retLeft(x, y)
                                + retTopRight(x,y)+retRight(x,y);
                        }
                        else if (x == 0 && x != generalx && y != 0 && y == generaly) // BOTTOM LEFT
                        {
                            mineCount = retTop(x, y) + retTopRight(x, y) + retRight(x, y);

                        }
                        hidden[x, y] = mineCount;
                    }
                   
                   
                    
                }
            }
        }


        // Returns the amount of mines in the top left corner
        private int retTopLeft(int x, int y)
        {
            int mineCount=0;
                if (hidden[x-1 , y - 1] == 9)
                 {
                      mineCount++;
                      return mineCount;
                 }
                else
                {
                    return mineCount;
                }
        }

        //Returns mines at the top
        private int retTop(int x, int y)
        {
            int mineCount = 0;
            if (hidden[x, y - 1] == 9)
            {
                mineCount++;
                return mineCount;
            }
            else
            {
                return mineCount;
            }
        }

        //Returns mines in the top right
        private int retTopRight(int x, int y)
        {
            int mineCount = 0;
            if (hidden[x + 1, y - 1] == 9)
            {
                mineCount++;
                return mineCount;
            }
            else
            {
                return mineCount;
            }
        }

        //Returns mines on the right
        private int retRight(int x, int y)
        {
            int mineCount = 0;
            if (hidden[x + 1, y] == 9)
            {
                mineCount++;
                return mineCount;
            }
            else
            {
                return mineCount;
            }
        }

        //Returns mines on the bottom right
        private int retBottomRight(int x, int y)
        {
            int mineCount = 0;
            if (hidden[x + 1, y + 1] == 9)
            {
                mineCount++;
                return mineCount;
            }
            else
            {
                return mineCount;
            }
        }

        //Returns mines on the bottom
        private int retBottom(int x, int y)
        {
            int mineCount = 0;
            if (hidden[x, y + 1] == 9)
            {
                mineCount++;
                return mineCount;
            }
            else
            {
                return mineCount;
            }
        }

        //Returns mines on the bottom left
        private int retBottomLeft(int x, int y)
        {
            int mineCount = 0;
            if (hidden[x - 1, y + 1] == 9)
            {
                mineCount++;
                return mineCount;
            }
            else
            {
                return mineCount;
            }
        }

        //Returns mines on the left
        private int retLeft(int x, int y)
        {
            int mineCount = 0;
            if (hidden[x - 1, y] == 9)
            {
                mineCount++;
                return mineCount;
            }
            else
            {
                return mineCount;
            }
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            difficultyLevel = 1;
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            difficultyLevel = 2;
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            difficultyLevel = 3;
        }

        //Generates a random number
        private int randCoord(int a)
        {
            return random1.Next(a);
        }

        private void displayAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string temp="";
            displayAll();
            failure(temp);
        }

        //Displays all parts of the grid
        private void displayAll()
        {
            for (int x = 0; x < btn.GetLength(0); x++)         // Loop for x
            {
                for (int y = 0; y < btn.GetLength(1); y++)     // Loop for y
                {
                    btn[x, y].Enabled = false;
                    
                    if (hidden[x, y] > 0 && hidden[x,y]<9)
                    {
                        btn[x, y].BackColor = Color.LightGray;
                        btn[x, y].Text = Convert.ToString(hidden[x, y]);
                    }
                    else if (hidden[x,y]>8)
                    {
                        btn[x, y].Text = "M";
                        btn[x, y].BackColor = Color.Red;
                    }
                    else
                    {
                        btn[x, y].BackColor = Color.LightGray;
                    }
                }
            }
        }

        //Hides all points on the grid
        private void hideAll()
        {
            for (int x = 0; x < btn.GetLength(0); x++)         // Loop for x
            {
                for (int y = 0; y < btn.GetLength(1); y++)     // Loop for y
                {
                    btn[x, y].Enabled = true;
                }
            }
        }

        //Counts how many mines are remaining
        private void countingMines()
        {
            
            if (noMines > 9)
            {
                textBox4.Text = "0" + Convert.ToString(noMines);
            }
            else if (noMines<10 && noMines>=0)
            {
                
                textBox4.Text = "00" + Convert.ToString(noMines);
            }
            else 
            {
                textBox4.Text = "000";
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //Generates the victory screen if the game is won
        private void victory()
        {
            Form3 win = new Form3(userName, turns, getDifficulty(difficultyLevel));
            win.ShowDialog();
            Close();
        }

        //Generates a loss screen if the game is lost
        private void failure(string reason)
        {
            Form5 loss = new Form5();
            loss.ShowDialog();
            Close();
            
        }

        //Returns how many turns the player has taken
        public int getTurns(int turns)
        {
            return turns;
        }

        //Returns the difficulty of the game
        public string getDifficulty(int difficultyLevel)
        {
            string convDiff=null;
            if (difficultyLevel==1)
            {
                convDiff = "Easy";
                
            }
            else if (difficultyLevel == 2)
            {
                convDiff = "Medium";

            }
            else if (difficultyLevel == 3)
            {
                convDiff = "Hard";

            }
            return convDiff;
        }

        /*private void gameTimer()
        {
            Timer MyTimer = new Timer();
            while (minesFound != 0)
            {
                
                MyTimer.Interval = 1000;
                MyTimer.Tick += new EventHandler(MyTimer_Tick);
                textBox2.Text = time.ToString();
                MyTimer.Start();
            }
            MyTimer.Stop();
        }*/

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void highScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 highForm = new Form6();
            highForm.ShowDialog();
        }

        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 howToPlay = new Form7();
            howToPlay.ShowDialog();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

       /* private void MyTimer_Tick(object sender, EventArgs e)
        {
            textBox2.Text = time.ToString();
            time++;
        }*/


        
    }
}
