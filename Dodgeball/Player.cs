using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using System;
using Microsoft.Xna.Framework.Input;

namespace Dodgeball
{
    /// <summary>
    /// An entity class representing the player character (PC).
    /// </summary>
    class Player : Entity
    {
        /// <summary>
        /// Default Person constructor
        /// </summary>
        /// <param name="txt2D">Sprite image of the player</param>
        public Player(Texture2D txt2D)
        {
            img = txt2D;
        }

        public override void advanceGameTick(int currentGameTick)
        {
            // Obtain the user's input
            KeyboardState KBS = Keyboard.GetState();

            // Reads user input and checks bounds
            if (KBS.IsKeyDown(Keys.Down) && !Bounds.BoundedDown(x, y))
                y -= 2;
            if (KBS.IsKeyDown(Keys.Up) && !Bounds.BoundedUp(x, y))
                y += 2;
            if (KBS.IsKeyDown(Keys.Left) && !Bounds.BoundedLeft(x, y))
                x += 2;
            if (KBS.IsKeyDown(Keys.Right) && !Bounds.BoundedRight(x, y))
                x -= 2;

            // Player wins if his y-component is below 30
            if (y < 30)
            {
                Environment.Exit(0);
            }

            // Quits the game is a fireball touches the player
            foreach (Fireball fb in Fireball.Fireballs)
            {
                if (this.intersects(fb))
                    Environment.Exit(0);
            }

        }

        public override void draw(SpriteBatch sp)
        {
            throw new NotImplementedException();
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
                if (between(y, H1, H2 - 19) || between(y, H3 - 19, H4 - 19) || between(y, H5 - 19, H6 - 19))
                {
                    if (x <= V1)
                        return true;
                }


                else if (between(y, H2 - 19, H3 - 19))
                {
                    if (x <= V1 || Math.Abs(x - (V9 + 8)) < 4)
                        return true;
                }

                else if (between(y, H4 - 19, H5))
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
                    if (x >= V10 - 6)
                        return true;
                }

                else if (between(y, H2 - 19, H3 - 19))
                {
                    if (x >= V10 - 6 || Math.Abs(x - (V2 - 10)) < 4)
                        return true;
                }

                else if (between(y, H4 - 19, H5 - 19))
                {
                    if (x >= V8 - 10 || Math.Abs(x - (V4 - 10)) < 4)
                        return true;
                }

                else
                {
                    if (x >= V6 - 10)
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
                    if (y >= H6 - 20 || Math.Abs(y - (H4 - 20)) < 4)
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

                else if (between(x, V5, V6))
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
}
