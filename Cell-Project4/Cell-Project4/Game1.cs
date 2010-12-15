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

        GameObject[] CannonBalls;
        GamePadState previousGamePadState = GamePad.GetState(PlayerIndex.One);
        KeyboardState previousKeyboardState = Keyboard.GetState();
        GameObject Cannon;

        // It is considered good style that const variables are in all uppercase.
        const int MAXCANNONBALLS = 3;


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
            if(KeyboardState.IsKeyDown(Keys.Space) &&
                previousKeyboardState.IsKeyUp(Keys.Space)
            {
                FireCannonBall();
            }
            UpdateCannonBalls();

            previousKeyboardState = KeyboardState;
#endif
            base.Update(gameTime);
        }

        /// <summary>
        /// We all this when we want to update the screen position of the cannon balls.
        /// </summary>
        public void UpdateCannonBalls()
        {
            foreach (GameObject Ball in CannonBalls)
            {
                if (Ball.alive)
                {
                    Ball.position += Ball.velocity;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, Viewport, Color.White);
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
