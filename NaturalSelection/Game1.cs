using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NaturalSelection.Models;
using NaturalSelection.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NaturalSelection
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Blob> blobs = new List<Blob>();
        List<Food> food = new List<Food>();
        TimeSpan time = new TimeSpan();

        public const int FOOD_AMOUNT = 6;
        public const int BLOB_AMOUNT = 10;
        public const int ROUND_TIME = 15;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1600;
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
            var blobFactory = new Factory<Blob>();
            var foodFactory = new Factory<Food>();
            Util.Util.Init();
            blobs = blobFactory.CreateRandom(BLOB_AMOUNT);
            food = foodFactory.CreateRandom(FOOD_AMOUNT, new Rectangle(650, 400, 300, 300));
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
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
            Renderer.Init(spriteBatch, GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            time += gameTime.ElapsedGameTime;
            var blobsToAdd = new List<Blob>();
            if (time.TotalSeconds > ROUND_TIME)
            {
                foreach (var blob in blobs.Where(b => b.Alive))
                {
                    if (!blob.HasEaten)
                    {
                        blob.Alive = false;
                    }
                    else
                    {
                        var blobFactory = new Factory<Blob>();
                        var children = blobFactory.CreateRandom(2);
                        blobsToAdd.AddRange(children);
                    }
                }
                blobs.Clear();
                blobs.AddRange(blobsToAdd);
                var foodFactory = new Factory<Food>();
                food = foodFactory.CreateRandom(FOOD_AMOUNT, new Rectangle(650, 400, 500, 500));

                time = new TimeSpan();
            }

            Renderer.Dt = gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            foreach (var blob in blobs.Where(b => b.Alive))
            {
                foreach (var f in food.Where(f => !f.Eaten))
                {
                    if (CollisionDetector.Collides(blob, f))
                    {
                        f.Eaten = true;
                        blob.CurrentState = Blob.state.searching;
                        blob.Energy += f.Energy;
                        blob.HasEaten = true;
                        blob.color = Color.White;
                        blob.Stop();
                    }
                }

                blob.Update(food);

            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            Renderer.BeginDraw();

            foreach (var f in food.Where(f => !f.Eaten))
            {
                Renderer.Draw(f);
            }

            foreach (var b in blobs.Where(b => b.Alive))
            {
                Renderer.Draw(b);
            }

            // TODO: Add your drawing code here
            Renderer.EndDraw();
            base.Draw(gameTime);
        }
    }
}
