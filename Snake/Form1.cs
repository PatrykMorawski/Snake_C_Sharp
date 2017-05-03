using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {

        Random randFood = new Random();
        Graphics paper;
        Snake snake = new Snake();
        Food food;

        bool left = false;
        bool right = false;
        bool down = false;
        bool up = false;

        bool pause = false;

        int score = 0;

        public Form1()
        {
            InitializeComponent();
            food = new Food(randFood);
            timer1.Enabled = false;
            odczyt_hightscore();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            paper = e.Graphics;
            food.drawFood(paper);
            snake.drawSnake(paper);

            
        }//Rysowanie

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.S)
            {
                timer1.Enabled = true;
                pause = true;
                lblRestart.Text = "";
                down = false;
                up = false;
                right = true;
                left = false;
            }


            // Podwójny Check

            if (e.KeyData == Keys.Left && e.KeyData == Keys.Up) {  }
            else
            {
                if(e.KeyData == Keys.Left && e.KeyData == Keys.Down) {  }
                else
                {
                    if (e.KeyData == Keys.Right && e.KeyData == Keys.Up) {  }
                    else
                    {
                        if (e.KeyData == Keys.Right && e.KeyData == Keys.Down) {  }
                        else
                        {
                            if (e.KeyData == Keys.Down && up == false && down == false)
                            {
                                left = false;
                                right = false;
                                down = true;
                                up = false;
                            } 
                            else
                            {
                                if (e.KeyData == Keys.Up && down == false && up == false)
                                {
                                    left = false;
                                    right = false;
                                    down = false;
                                    up = true;
                                }
                                else
                                {
                                    if (e.KeyData == Keys.Right && left == false && right == false)
                                    {
                                        left = false;
                                        right = true;
                                        down = false;
                                        up = false;
                                    }
                                    else
                                    {
                                        if (e.KeyData == Keys.Left && right == false && left == false)
                                        {
                                            left = true;
                                            right = false;
                                            down = false;
                                            up = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            // Klawisze systemowe

            if (e.KeyData == Keys.P && pause == true)
            {
                if (timer1.Enabled == false)
                {
                    timer1.Enabled = true;
                }   
                else
                {
                    timer1.Enabled = false;
                }      
            }
            if (e.KeyData == Keys.Escape)
            {
                Close();
            }

        }//Klawisze

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblsnakeScore.Text = Convert.ToString(score);

            if (down && !up && !right && !left) { snake.moveDown(); }
            if (up && !down && !right && !left) { snake.moveUp(); }
            if (right && !up && !down && !left) { snake.moveRight(); }
            if (left && !up && !right && !down) { snake.moveLeft();  }

            for (int i = 0; i < snake.snakeRec.Length; i++)
            {
                if (snake.snakeRec[i].IntersectsWith(food.foodRec))
                {
                    score += 10;
                    snake.growSnake();
                    food.foodLocation(randFood);
                    try{timer1.Interval -= 1;}
                        catch{}
                }
            }

            collision();

            this.Invalidate(); // Rysowanie, odświeżanie
        }

        public void collision()
        {
            for (int i = 1; i < snake.SnakeRec.Length; i++)
            {
                if ( snake.SnakeRec[0].IntersectsWith(snake.snakeRec[i]))
                {
                    restart();
                }
            }

            if (snake.SnakeRec[0].X < 0 || snake.SnakeRec[0].X > 270)
            {
                restart();
            }

            if (snake.SnakeRec[0].Y < 0 || snake.SnakeRec[0].Y > 230)
            {
                restart();
            }
        }

        public void odczyt_hightscore()
        {

            lblNajScore.Text = System.IO.File.ReadAllText(@"E:\Kopia Zapasowa Programów\Gry\Snake - C#\Hightscore.txt");
           
        }

        public void zapis_hightscore()
        {
                int WysokiScore = Int32.Parse(lblNajScore.Text);
            if ( WysokiScore < score)
            {
                string[] lines = { lblsnakeScore.Text };
                System.IO.File.WriteAllLines(@"E:\Kopia Zapasowa Programów\Gry\Snake - C#\Hightscore.txt", lines);
            }
        }

        public void restart()
        {
            timer1.Enabled = false;
            pause = false;
            timer1.Interval = 50;
            MessageBox.Show("Snake is dead. You scored: " + score);
            zapis_hightscore();
            lblsnakeScore.Text = "0";
            odczyt_hightscore();
            score = 0;
            lblRestart.Text = "Press S to start\nPress Esc to quit";
            snake = new Snake();
        }
    }
}
