using System;
using System.Collections.Generic;
using System.Drawing;

namespace BreakoutGameLab001
{
    // 球類別
    class Ball
    {
        // 屬性
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }
        public Color Color { get; set; }
        public int VelocityX { get; set; }
        public int VelocityY { get; set; }

        // 建構子
        public Ball(int x, int y, int radius, int vx, int vy, Color color)
        {
            X = x;
            Y = y;
            Radius = radius;
            Color = Color.Yellow;
            VelocityX = vx;
            VelocityY = vy;
        }

        // 繪製球
        internal void Draw(Graphics gr)
        {
            gr.FillEllipse(new SolidBrush(this.Color), X - Radius, Y - Radius, Radius * 2, Radius * 2);
        }

        // 移動球
        public void Move(int left, int top, int right, int bottom, Action gameOver)
        {
            X += VelocityX;
            Y += VelocityY;

            // 水平方向: 檢查球是否碰到牆壁
            if (X - Radius <= left)
            {
                VelocityX = -VelocityX;
                X = left + Radius;
            }
            else if (X + Radius >= right)
            {
                VelocityX = -VelocityX;
                X = right - Radius;
            }

            // 垂直方向: 檢查球是否碰到牆壁或擋板未擋到球
            if (Y - Radius <= top)
            {
                VelocityY = -VelocityY;
                Y = top + Radius;
            }
            else if (Y + Radius >= bottom)
            {
                gameOver();
            }
        }

        public void CheckCollision(Paddle paddle, List<Brick> bricks, Action allBricksCleared)
        {
            // 檢查球是否與所有的磚塊發生碰撞
            for (int i = bricks.Count - 1; i >= 0; i--)
            {
                Brick brick = bricks[i];
                if (X + Radius >= brick.X && X - Radius <= brick.X + brick.Width &&
                    Y + Radius >= brick.Y && Y - Radius <= brick.Y + brick.Height)
                {
                    VelocityY = -VelocityY;
                    bricks.RemoveAt(i);

                    // 檢查是否所有磚塊都消失
                    if (!bricks.Any())
                    {
                        allBricksCleared();
                    }

                    break;
                }
            }

            // 檢查球是否與擋板發生碰撞
            if (X + Radius >= paddle.X && X - Radius <= paddle.X + paddle.Width &&
                Y + Radius >= paddle.Y && Y - Radius <= paddle.Y + paddle.Height)
            {
                VelocityY = -VelocityY;
                if (Y < paddle.Y)
                    Y = paddle.Y - Radius - 1;
                else if (Y > paddle.Y + paddle.Height)
                    Y = paddle.Y + paddle.Height + Radius + 1;
            }
        }
    }
}
