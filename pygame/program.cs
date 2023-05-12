using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FirstGame
{
    public partial class Form1 : Form
    {
        private const int WIDTH = 900;
        private const int HEIGHT = 500;

        private readonly Color WHITE = Color.FromArgb(255, 255, 255);
        private readonly Color BLACK = Color.FromArgb(0, 0, 0);
        private readonly Color RED = Color.FromArgb(255, 0, 0);
        private readonly Color YELLOW = Color.FromArgb(255, 255, 0);
        private readonly int FPS = 100;
        private readonly int VEL = 5;
        private readonly int BULLET_VEL = 7;
        private readonly int MAX_BULLETS = 5;

        private readonly int SPACESHIP_WIDTH = 55;
        private readonly int SPACESHIP_HEIGHT = 40;

        private Rectangle BORDER;

        private readonly Font HEALTH_FONT = new Font("comicsans", 40);
        private readonly Font WINNER_FONT = new Font("comicsans", 100);

        private readonly Image YELLOW_SPACESHIP_IMAGE = Image.FromFile(@"Assets\spaceship_yellow.png");
        private readonly Image RED_SPACESHIP_IMAGE = Image.FromFile(@"Assets\spaceship_red.png");
        private readonly Image SPACE = Image.FromFile(@"Assets\space.png");

        private Rectangle yellow;
        private Rectangle red;
        private List<Rectangle> yellow_bullets = new List<Rectangle>();
        private List<Rectangle> red_bullets = new List<Rectangle>();
        private int yellow_health = 10;
        private int red_health = 10;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            BORDER = new Rectangle(WIDTH / 2 - 5, 0, 10, HEIGHT);
            yellow = new Rectangle(100, 300, SPACESHIP_WIDTH, SPACESHIP_HEIGHT);
            red = new Rectangle(700, 300, SPACESHIP_WIDTH, SPACESHIP_HEIGHT);
        }

        private void DrawWindow()
        {
            using (var g = CreateGraphics())
            {
                g.DrawImage(SPACE, 0, 0, WIDTH, HEIGHT);

                g.FillRectangle(Brushes.Black, BORDER);

                var redHealthText = new SolidBrush(WHITE);
                g.DrawString("Health: " + red_health, HEALTH_FONT, redHealthText, WIDTH - HEALTH_FONT.Size * 10, 10);
                var yellowHealthText = new SolidBrush(WHITE);
                g.DrawString("Health: " + yellow_health, HEALTH_FONT, yellowHealthText, 10, 10);

                g.DrawImage(YELLOW_SPACESHIP_IMAGE, yellow);
                g.DrawImage(RED_SPACESHIP_IMAGE, red);

                foreach (var bullet in yellow_bullets)
                    g.FillRectangle(Brushes.Yellow, bullet);

                foreach (var bullet in red_bullets)
                    g.FillRectangle(Brushes.Red, bullet);

                g.Dispose();
            }
        }

        private void YellowHandleMovement()
        {
            if (Keyboard.IsKeyDown(Keys.A) && yellow.X - VEL > 0) //LEFT
                yellow.X -= VEL;
            if (Keyboard.IsKeyDown(Keys.D) && yellow.X + VEL + yellow.Width < BORDER.X) //right
                yellow.X += VEL;
            if (Keyboard.IsKeyDown(Keys.W) && yellow.Y - VEL > 0) //up
                yellow.Y -= VEL;
            if (Keyboard.IsKeyDown(Keys.S
