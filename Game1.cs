using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FNA
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D snezh, background;
        static int n = 700;
        Snow[] snow = new Snow[n];
        Random rnd = new Random();
        private bool pause = true;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = true;
            Add();
            graphics.ApplyChanges();
        }
        protected void Add()
        {
            for (int i = 0; i < n; i++)
            {
                snow[i] = new Snow();
                snow[i].x = rnd.Next(5,graphics.PreferredBackBufferWidth);
                snow[i].y = -rnd.Next(1, graphics.PreferredBackBufferHeight);
                snow[i].size = rnd.Next(5, 40);
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("..\\..\\..\\Pictures/winter_background.jpg");
            snezh = Content.Load<Texture2D>("..\\..\\..\\Pictures/snowflake.png"); 
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            ShutDown();
        }
        
        protected override void Draw(GameTime gameTime)
        {
            if (!pause)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(background,
                        new Rectangle(0, 0,
                        graphics.PreferredBackBufferWidth,
                        graphics.PreferredBackBufferHeight),
                        Color.White);
                for (int i = 0; i < n; i++)
                {
                    snow[i].y += snow[i].size/15;
                    if (snow[i].y > graphics.PreferredBackBufferHeight)
                    {
                        snow[i].x = rnd.Next(5, graphics.PreferredBackBufferWidth);
                        snow[i].y = -snow[i].size;
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    spriteBatch.Draw(snezh,
                    new Rectangle(snow[i].x, snow[i].y, snow[i].size, snow[i].size), Color.White);
                }
                spriteBatch.End();
            }
        }
        private void ShutDown()
        {
            if (Input.KeyPressed(Keys.Enter))
            {
                if (!pause)
                {
                    pause = true;
                }
                else if (pause)
                {
                    pause = false;
                }
            }
        }
    }
}
