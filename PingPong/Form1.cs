using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingPong
{
    public partial class Form1 : Form
    {

        int ballXSpeed = 4; //ball speed on X axis
        int ballYSpeed = 4; //ball speed on Y axis
        int speed = 2; //computer speed
        int playerspeed = 8;  //player speed

        //IMPORTANT!!!!!!!
        Random rand = new Random(); //for selecting random speed for ball and computer


        bool goDown, goUp; //controls when player moves up or down 
        int computer_speed_change = 50; //interval for how often computer speed changes to d next random speed
        int playerScore = 0; //scores of player
        int computerScore = 0; //scores of computer
        int[] i = { 5, 6, 8, 9 }; //the random numbers for computer speed will be taken from here, and assigned to "int speed = 2"
        int[] j = {10, 9, 8, 11, 12};//random numbers for ballXSpeed & ballYspeed will be taken from here



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            Ball.Top -= ballYSpeed;
            Ball.Left -= ballXSpeed;

            this.Text = "Player Score: " + playerScore + "-- Computer Score: " + computerScore;

            if(Ball.Top < 0 || Ball.Bottom > this.ClientSize.Height) //if the ball has reached the top || bottom of the screen 
            {
                ballYSpeed = -ballYSpeed; //ballYspeed should reverse and go opposite direction

            } 
            if(Ball.Left < -2)
            {
                Ball.Left = 300; //reset the ball back to 300
                ballXSpeed = -ballXSpeed; //move ball in opposite direction 
                computerScore++; //add 1 to the computer score (computer WON)
            }
            if(Ball.Right > this.ClientSize.Width + 2)
            {
                Ball.Left = 300;
                ballXSpeed = -ballXSpeed;
                playerScore++; //add 1 to the player's score (Player WON)
            }

            if(Computer.Top <= 1)
            {
                Computer.Top = 0; //reset to 0
            }
            else if(Computer.Bottom >= this.ClientSize.Height) 
            {
                Computer.Top = this.ClientSize.Height - Computer.Height; //computer.Height is the height of d computer picture box

            }

            if(Ball.Top < Computer.Top + (Computer.Height / 2 ) && Ball.Left > 300)
            {//Computer.Top + (Computer.Height / 2) will make the computer follow the ball from the center of the computer paddle
                
                Computer.Top -= speed; 
            }
            if(Ball.Top > Computer.Top + (Computer.Height / 2 ) && Ball.Left > 300)
            {
                Computer.Top += speed; //increase the speed
            }

            computer_speed_change -= 1; //reducing computer speed change by 1

            if(computer_speed_change < 0)
            {
                speed = i[rand.Next(i.Length)];
                computer_speed_change = 50;

            }

            if(goDown && player.Top + player.Height < this.ClientSize.Height) //if goDown is true and player is still within the height of the ball
            {
                player.Top += playerspeed; //get the player to continue to move down till it reaches the bottom
            }

            if(goUp && player.Top > 0) 
            {
                player.Top -= playerspeed;  //move the player to the top by reducing the player speed
            }

            //checking collission by passing the ball in pic1, player in pic2, and offset value is playerRight + 5
            CheckCollision(Ball, player, player.Right + 5);

            //checking collission by passing the ball in pic1, computer in pic2, and offset value is computerLeft - 35
            CheckCollision(Ball, Computer, Computer.Left - 35); 

            if(computerScore > 5)
            {
                GameOver("You Lost");
            }
            else if(playerScore > 5)
            {
                GameOver("You WON!!");
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e) //pressing down a key
        {
            if(e.KeyCode == Keys.Down) //if the down key is pressed
            {
                goDown = true; //make the goDown bool to be true
            }
            if(e.KeyCode == Keys.Up) //if the up key is pressed
            {
                goUp = true; //make the goUp bool to be true
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e) //not pressing down a key(Key is Up)
        {
            if(e.KeyCode == Keys.Down) //if down key is released
            {
                goDown = false; //goDown bool will be false
            }
            if(e.KeyCode == Keys.Up) //if up key is released
            {
                goUp = false;  //goUp bool will be false
            }
        }



        private void CheckCollision(PictureBox PicOne, PictureBox PicTwo, int offset)
        {
            if (PicOne.Bounds.IntersectsWith(PicTwo.Bounds))//if PicOne is colliding with PicTwo
            {
                PicOne.Left = offset;
                int x = j[rand.Next(j.Length)]; //select one of the random values from the j arrays and assign to x
                int y = j[rand.Next(j.Length)];

                if (ballXSpeed < 0) 
                {
                    ballXSpeed = x; //set the number from negative to positive (x stands for any random number from j array)
                }
                else
                {
                    ballXSpeed = -x; //set the number from positive to negative(move the ball from right to left)
                }

                if (ballYSpeed < 0) //if Y value is negative(less than 0)
                {
                    ballYSpeed = -y; //new value should also be negative
                }
                else
                {
                    ballYSpeed = y; //new value should be positive(faster or slower depending on the random array number)
                }
            }
        }


        private void GameOver(string message)
        {
            GameTimer.Stop();
            MessageBox.Show(message, "Moo says: ");
            computerScore = 0;
            playerScore = 0;
            ballXSpeed = ballYSpeed = 4;
            computer_speed_change = 50;
            GameTimer.Start();
        }

    }
}
