using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Cell_Project4
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D backgroundTexture;
        Rectangle Viewport;
        GameObject Cannon;
        const int MAXCANNONBALLS = 3;
        GameObject[] CannonBalls;
        KeyboardState previousKeyboardState = Keyboard.GetState();
      

        // It is considered good style that const variables are in all uppercase.
        

        const int MAXENEMIES = 3;
        const float MAXENEMYHEIGHT = 0.1f;
        const float MINENEMYHEIGHT = 0.5f;
        const float MAXENEMYVELOCITY = 5.0f;
        const float MINENEMYVELOCITY = 1.0f;
        Random random = new Random();
        GameObject[] enemies;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundTexture = Content.Load<Texture2D>("Sprites\\background");

            Cannon = new GameObject(Content.Load<Texture2D>("Sprites\\cannon"));
            Cannon.position = new Vector2(750, graphics.GraphicsDevice.Viewport.Height - 250);
            CannonBalls = new GameObject[MAXCANNONBALLS];
            for(int i = 0; i < MAXCANNONBALLS; i++)
            {
                CannonBalls[i] = new GameObject(Content.Load<Texture2D>("Sprites\\cannonball"));
                Viewport = new Rectangle(0, 0,
                    graphics.GraphicsDevice.Viewport.Width,
                    graphics.GraphicsDevice.Viewport.Height);
                enemies = new GameObject[MAXENEMIES];
                for (int t = 0; t < MAXENEMIES; t++)
                {
                    enemies[i] = new GameObject(
                        Content.Load<Texture2D>("Sprites\\enemy"));
                }
            }
        }

       
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            KeyboardState StateOfTheKeyboard = Keyboard.GetState();
            if (StateOfTheKeyboard.IsKeyDown(Keys.Left))
            {
                Cannon.rotation -= 0.1f;
            }
            else if (StateOfTheKeyboard.IsKeyDown(Keys.Right))
            {
                Cannon.rotation += 0.1f;
            }
           
             if (StateOfTheKeyboard.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
           
          
             UpdateCannonBalls();

            previousKeyboardState = StateOfTheKeyboard;
            base.Update(gameTime);
        }

        public void FireCannonBall()
        {
            foreach (GameObject ball in CannonBalls)
            {
                if (!ball.alive)
                {
                    ball.alive = true;
                    ball.position = Cannon.position - ball.center;
                    ball.velocity = new Vector2(
                        (float)Math.Cos(Cannon.rotation),
                        (float)Math.Sin(Cannon.rotation))
                        * 5.0f;
                    return;
                }
            }
        }
        
        
        
        public void UpdateCannonBalls()
        {
            foreach (GameObject ball in CannonBalls)
            {
                if (ball.alive)
                {
                    ball.position += ball.velocity;
                    if (!Viewport.Contains (new Point (
                        (int)ball.position.X,
                        (int)ball.position.Y)))
                    {
                        ball.alive = false;
                        continue;
                    }
                }
            }
        }



        private void FireCannonBall()
        {
            // Remove this exception throw and add your code here.

        }

        /// <summary>
        /// We all this when we want to update the screen position of the cannon balls.
       

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, Viewport, Color.White);
            foreach (GameObject ball in CannonBalls)
            {
                if (ball.alive)
                {
                    spriteBatch.Draw(ball.sprite,
                        ball.position, Color.White);
                }

            }
            spriteBatch.Draw(Cannon.sprite,
                Cannon.position,
                null,
                Color.White,
                Cannon.rotation,
                Cannon.center,
                1.0f,
                SpriteEffects.None,
                0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
