using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BreakoutGameLab001
{
    internal class BrickGamePanel : Panel
    {
        // 定義遊戲元件
        private List<Ball> balls = new List<Ball>();
        private Paddle paddle;
        private List<Brick> bricks = new List<Brick>();
        // 定義 Timer 控制項
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public BrickGamePanel(int width, int height)
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Gray;
            this.Size = new Size(width, height);
        }

        public void Initialize()
        {
            // 初始化遊戲元件
            balls.Add(new Ball(Width / 2, Height / 2, 15, 3, -3, Color.Red));
            paddle = new Paddle(Width / 2 - 50, Height - 50, 120, 20, Color.Blue);

            bricks = new List<Brick>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    bricks.Add(new Brick(25 + j * 80, 25 + this.Location.Y + i * 30, 80, 30, Color.Green));
                }
            }

            // 設定遊戲的背景控制流程
            timer.Interval = 20;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (Ball ball in balls)
            {
                ball.Move(0, 61, Width, Height, GameOver);
                ball.CheckCollision(paddle, bricks, AllBricksCleared);

                // 检查球是否未被挡板阻挡
                if (ball.Y + ball.Radius >= paddle.Y + paddle.Height)
                {
                    GameOver();
                }
            }

            // 重绘游戏画面
            Invalidate(); // --> 触发 OnPaint 事件
        }


        private void GameOver()
        {
            timer.Stop();
            MessageBox.Show("Game Over!");
        }

        private void AllBricksCleared()
        {
            timer.Stop();
            MessageBox.Show("Congratulations! You've cleared all the bricks!");
        }

        // 處理畫面的重繪事件
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics gr = e.Graphics;

            // 繪製球、擋板
            foreach (Ball ball in balls)
            {
                ball.Draw(gr);
            }

            paddle.Draw(gr);

            // 繪製磚塊
            foreach (Brick brick in bricks)
            {
                brick.Draw(gr);
            }
        }

        public void paddleMoveLeft()
        {
            paddle.Move(-30);
        }

        public void paddleMoveRight()
        {
            paddle.Move(30);
        }
    }
}
