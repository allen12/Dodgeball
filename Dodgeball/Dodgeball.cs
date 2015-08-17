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

        private Texture2D john, 
            monster2, 
            fireball;


        private Rectangle mapRectangle; // Rectangle for the map
        private Texture2D map;

        private SoundEffect backgroundMusic;
        List<int> fireballsX = new List<int>(); //Represents all the x-components of fireballs
        List<int> fireballsY = new List<int>(); //Represents all the y-components of fireballs
        List<Fireball> fireballs;

        Random random = new Random();

        int fireballCounter = 0; //Represents the delay between making more fireballs appear
        int changeX, changeY; //Change in components of the player
        const int monsterX = 50; //Original x-component of monster location
        const int monsterY = 30; //Original y-component of monster location
        int changeXMonster = 0; //Change in x-coordinates of the monster (moves sideways)
        sbyte directionMonster = 1; //1 if moving right, -1 if moving left

        public Dodgeball()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            changeX = 0; changeY = 0;
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //MAKES SHIT APPEAR
            map = Content.Load<Texture2D>("BallGame");
            john = Content.Load<Texture2D>("John front");
            monster2 = Content.Load<Texture2D>("Monster2");
            fireball = Content.Load<Texture2D>("fireball");
            backgroundMusic = Content.Load<SoundEffect>("Paradise Town");
            mapRectangle = new Rectangle(0, 0, map.Width, map.Height);

            DrawStuff.drawMusic(backgroundMusic);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
                Exit();


            //Get user input and store it into <KBS>
            KeyboardState KBS = Keyboard.GetState();

            //Get the location of the player
            int x = map.Width / 3 - 32 - changeX;
            int y = 450 - changeY;

            //Reads user input and checks bounds <-- Somewhat buggy currently
            if (KBS.IsKeyDown(Keys.Down) && !Bounds.BoundedDown(x, y))
                changeY -= 2;
            if (KBS.IsKeyDown(Keys.Up) && !Bounds.BoundedUp(x, y))
                changeY += 2;
            if (KBS.IsKeyDown(Keys.Left) && !Bounds.BoundedLeft(x, y))
                changeX += 2;
            if (KBS.IsKeyDown(Keys.Right) && !Bounds.BoundedRight(x, y))
                changeX -= 2;

            //Monster moves sideways
            if (directionMonster == 1)
                changeXMonster -= 6;
            else if (directionMonster == -1)
                changeXMonster += 6;

            if (changeXMonster >= 20 || changeXMonster <= -350)
                directionMonster = (sbyte)(-1 * directionMonster);

            //Player dies if he touches monster
            if (Math.Abs(monsterX - changeXMonster - x) < 40 && Math.Abs(monsterY - y) < 50)
            {
                Exit();
            }

            //Fireballs
            fireballCounter++;

            if (fireballCounter == 3) //The number represents the delay length before
            {                           //making new fireballs.
                fireballCounter = 0; //Resets the counter
                fireballsX.Add(random.Next(0, 450)); //Adds a random fireball, x-component random between 0 and 449, inclusive
                fireballsY.Add(40); //New fireballs always start at 40 for the y-component
            }

            //For-loop: If any fireballs reach a y-component of 460 (the bottom of the map), they are removed from the arraylist
            for (int i = 0; i < fireballsX.Count; i++)
            {
                fireballsY[i] = (int)fireballsY[i] + 2; //The number here represents the speed of the fireball
                if ((int)fireballsY[i] == 460)
                {
                    fireballsX.RemoveAt(i);
                    fireballsY.RemoveAt(i);
                }
            }

            //Debugging purposes, displays the coordinates of the person
            Console.WriteLine("X: " + x);
            Console.WriteLine("Y: " + y);

            //The player wins if his y-component is below 30
            if (y < 30)
            {
                Exit();
            }

            //For-loop: Quits the game is a fireball touches the player
            for (int i = 0; i < fireballsX.Count; i++)
            {
                if (Math.Abs((int)fireballsX[i] - x) < 10 && Math.Abs((int)fireballsY[i] - y) < 10)
                {
                    Exit();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            DrawStuff.drawMap(spriteBatch, map);
            DrawStuff.drawPerson(spriteBatch, john, map.Width / 3 - 32, 450, changeX, changeY, 0.5f);
            DrawStuff.drawPerson(spriteBatch, monster2, monsterX, monsterY, changeXMonster, 0, 2.0f);

            //Draws all the fireballs
            for (int i = 0; i < fireballsX.Count; i++)
            {
                DrawStuff.draw(spriteBatch, fireball, (int)fireballsX[i], (int)fireballsY[i], 0.03f);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }



    }

    public static class DrawStuff
    {
        public static void drawPerson(SpriteBatch spriteBatch, Texture2D person, int x, int y, int changeX, int changeY, float scale)
        {
            spriteBatch.Draw(person, new Vector2(x - changeX, y - changeY), new Rectangle(0, 0, person.Width, person.Height),
                Color.White, 0.0f, new Vector2(0, 0), scale, SpriteEffects.None, 1.0f);
        }

        public static void drawMap(SpriteBatch spriteBatch, Texture2D map)
        {
            spriteBatch.Draw(map, new Vector2(0, 0), new Rectangle(0, 0, map.Width, map.Height),
                Color.White, 0.0f, new Vector2(0, 0), 0.6f, SpriteEffects.None, 1.0f);
        }

        public static void drawMusic(SoundEffect sound)
        {
            sound.Play();
        }

        public static void stopMusic(SoundEffect sound)
        {
            sound.Dispose();
        }

        public static void draw(SpriteBatch spriteBatch, Texture2D thing, int x, int y, float scale)
        {
            spriteBatch.Draw(thing, new Vector2(x, y), new Rectangle(0, 0, thing.Width, thing.Height), Color.White, 0.0f,
                new Vector2(0, 0), scale, SpriteEffects.None, 1.0f);
        }
    }

    public static class Bounds
    {
        //SEE "BallGame2.png" in the Content folder for what these values represent
        private const int V1 = 19;
        private const int V2 = 58;
        private const int V3 = 115;
        private const int V4 = 173;
        private const int V5 = 230;
        private const int V6 = 250;
        private const int V7 = 307;
        private const int V8 = 365;
        private const int V9 = 412;
        private const int V10 = 451;

        private const int H1 = 0;
        private const int H2 = 115;
        private const int H3 = 230;
        private const int H4 = 307;
        private const int H5 = 384;
        private const int H6 = 442;

        //Returns true if the player is bounded on the left
        public static bool BoundedLeft(int x, int y)
        {
            if (between(y, H1, H2-19) || between(y, H3-19, H4-19) || between(y, H5-19, H6-19))
            {
                if (x <= V1)
                    return true;
            }
         

            else if (between(y, H2-19, H3-19))
            {
                if (x <= V1 || Math.Abs(x-(V9+8)) < 4)
                    return true;
            }

            else if (between(y, H4-19, H5))
            {
                if (x <= V3 || Math.Abs(x - V7) < 4)
                    return true;
            }

            else
            {
                if (x <= V5)
                    return true;
            }

            return false;
        }

        public static bool BoundedRight(int x, int y)
        {
            if (between(y, H1, H2 - 19) || between(y, H3 - 19, H4 - 19) || between(y, H5 - 19, H6 - 19))
            {
                if (x >= V10-6)
                    return true;
            }

            else if (between(y, H2 - 19, H3 - 19))
            {
                if (x >= V10-6 || Math.Abs(x - (V2-10)) < 4)
                    return true;
            }

            else if (between(y, H4 - 19, H5 - 19))
            {
                if (x >= V8-10 || Math.Abs(x - (V4-10)) < 4)
                    return true;
            }

            else
            {
                if (x >= V6-10)
                    return true;
            }

            return false;
        }

        public static bool BoundedUp(int x, int y)
        {
            if (between(x, 0, V2) || between(x, V9, V10))
            {
                if (Math.Abs(y - H5) < 4)
                    return true;
            }

            else if (between(x, V2, V3) || between(x, V4, V5) || between(x, V6, V7) || between(x, V8, V9))
            {
                if (Math.Abs(y - H3) < 4 || Math.Abs(y - H5) < 4)
                    return true;
            }

            else if (between(x, V3, V4) || between(x, V7, V8))
            {
                if (Math.Abs(y - H3) < 4)
                    return true;
            }

            else if (between(x, V5, V6))
            {
                if (Math.Abs(y - H5) < 4 || Math.Abs(y - H3) < 4)
                    return true;
            }

            return false;
        }

        public static bool BoundedDown(int x, int y)
        {
            if (between(x, 0, V2) || between(x, V9, V10))
            {
                if (y >= H6 - 20 || Math.Abs(y - (H4-20)) < 4)
                    return true;
            }

            else if (between(x, V2, V3) || between(x, V4, V5) || between(x, V6, V7) || between(x, V8, V9))
            {
                if (y >= H6 - 20 || Math.Abs(y - (H4 - 20)) < 4 || Math.Abs(y - (H2 - 20)) < 4)
                    return true;
            }

            else if (between(x, V3, V4) || between(x, V7, V8))
            {
                if (y >= H6 - 20 || Math.Abs(y - (H2 - 20)) < 4)
                    return true;
            }

            else if(between(x, V5, V6))
            {
                if (y >= H6 + 19 || Math.Abs(y - (H4 - 20)) < 4 || Math.Abs(y - (H2 - 20)) < 4)
                    return true;
            }

            return false;
        }

        private static bool between(int x, int a, int b)
        {
            if (x >= a && x <= b)
                return true;

            return false;
        }
    }
}
