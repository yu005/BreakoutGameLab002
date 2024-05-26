
using System.Reflection.Metadata;

namespace BreakoutGameLab001
{
    public partial class Form1 : Form
    {
        // �C�����O���
        BrickGamePanel gamePanel;
        //
        public Form1()
        {
            InitializeComponent();
            //
            InitializeGame();
        }

        private void InitializeGame()
        {
            // ���� ���ե� panel2 ���
            Controls.Remove(panel2);
            //
            gamePanel = new BrickGamePanel(panel2.Width, panel2.Height);
            gamePanel.Dock = DockStyle.Fill;
            gamePanel.Location = new Point(0, 61);
            gamePanel.Name = "BrickoutGamePanel";
            gamePanel.Size = new Size(panel2.Width, panel2.Height);
            gamePanel.TabIndex = 1;
            //
            gamePanel.Initialize();
            //
            Controls.Add(gamePanel);
            //
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // if (e.KeyCode == Keys.Left) { ... }
            switch (e.KeyCode)
            {
                case Keys.Left:
                    gamePanel.paddleMoveLeft();
                    break;
                case Keys.Right:
                    gamePanel.paddleMoveRight();
                    break;
                case Keys.Up:
                    //
                    break;
                default:
                    //
                    break;
            }
        }
    }
}
