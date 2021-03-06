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
        const float MAXENEMYHEIGHT = 0.5f;
        const float MINENEMYHEIGHT = 0.01f;
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
            for (int i = 0; i < MAXCANNONBALLS; i++)
            {
                CannonBalls[i] = new GameObject(Content.Load<Texture2D>("Sprites\\cannonball"));
                enemies = new GameObject[MAXENEMIES];
                for (int t = 0; t < MAXENEMIES; t++)
                {
                    enemies[t] = new GameObject(
                        Content.Load<Texture2D>("Sprites\\enemy"));
                }
                Viewport = new Rectangle(0, 0,
                    graphics.GraphicsDevice.Viewport.Width,
                    graphics.GraphicsDevice.Viewport.Height);
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

                FireCannonBall();

            if (StateOfTheKeyboard.IsKeyDown(Keys.S))
            {
                Cannon.position.Y = Cannon.position.Y + 1.75f;
            }
            else if (StateOfTheKeyboard.IsKeyDown(Keys.W))
                Cannon.position.Y = Cannon.position.Y - 1.75f;
            else if (StateOfTheKeyboard.IsKeyDown(Keys.A))
                Cannon.position.X = Cannon.position.X - 1.75f;
            else if (StateOfTheKeyboard.IsKeyDown(Keys.D))
                Cannon.position.X = Cannon.position.X + 1.75f;

            UpdateCannonBalls();
            UpdateEnemies();
            previousKeyboardState = StateOfTheKeyboard;
            base.Update(gameTime);
        }

        public void UpdateEnemies()
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy.alive)
                {
                    enemy.position += enemy.velocity;
                    if (!Viewport.Contains(new Point(
                        (int)enemy.position.X,
                        (int)enemy.position.Y)))
                    {
                        enemy.alive = false;
                    }
                }
                else
                {
                    enemy.alive = true;
                    enemy.position = new Vector2(
                        Viewport.Right,
                        MathHelper.Lerp(
                        (float)Viewport.Height * MINENEMYHEIGHT,
                        (float)Viewport.Height * MAXENEMYHEIGHT,
                        (float)random.NextDouble()));
                    enemy.velocity = new Vector2(
                        MathHelper.Lerp(
                        -MINENEMYVELOCITY,
                        -MAXENEMYVELOCITY,
                        (float)random.NextDouble()), 0);
                }


            }


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
                    if (!Viewport.Contains(new Point(
                        (int)ball.position.X,
                        (int)ball.position.Y)))
                    {
                        ball.alive = false;
                        continue;
                    }
               Rectangle cannonBallRect = new Rectangle(
                   (int)ball.position.X,
                   (int)ball.position.Y,
                   ball.sprite.Width,
                   ball.sprite.Height);

                   foreach (GameObject enemy in enemies)
                   {
                       Rectangle enemyRect = new Rectangle(
                           (int)enemy.position.X,
                           (int)enemy.position.Y,
                           enemy.sprite.Width,
                           enemy.sprite.Height);

                       if (cannonBallRect.Intersects(enemyRect))
                       {
                           ball.alive = false;
                           enemy.alive = false;
                           break;
                       }
                   }
                }
            }
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


            foreach (GameObject enemy in enemies)
            {
                if (enemy.alive)
                {
                    spriteBatch.Draw(enemy.sprite,
                        enemy.position, Color.White);
                    
                }
               
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
                