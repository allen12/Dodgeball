#region Using Statements

using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

#endregion



namespace Dodgeball
{
    
    public class Dodgeball : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        private Texture2D map;

        private SoundEffect backgroundMusic;
        
        public Dodgeball()
            : base()
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
            base.Initialize();

            GameTickController.setSpriteBatch(spriteBatch);

            Fireball.setFireballDelay(10);
            Fireball.setSpeed(5);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all content.
        /// </summary>
        protected override void LoadContent()
        { 
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            map = Content.Load<Texture2D>("BallGame");
            Entity player = new Player(Content.Load<Texture2D>("John front"));
            Entity monster = new Monster(Content.Load<Texture2D>("Monster2"));
            GameTickController.addEntity(player);
            GameTickController.addEntity(monster);
            Fireball.FireballImg = Content.Load<Texture2D>("fireball");

            backgroundMusic = Content.Load<SoundEffect>("Paradise Town");
            backgroundMusic.Play();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Nothing to unload!
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // ESC exits the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
                Exit();

            GameTickController.advanceGameTick();
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(map, new Vector2(0, 0), new Rectangle(0, 0, map.Width, map.Height),
                Color.White, 0.0f, new Vector2(0, 0), 0.6f, SpriteEffects.None, 1.0f);
            GameTickController.drawEntities();
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
