﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dodgeball
{
    /// <summary>
    /// <p> The fireball class represents a fireball, an enemy object that will
    /// kill the player if this object intersects with the player object.
    /// 
    /// </summary>
    class Fireball : Entity
    {
        /* Represents the number of ticks that must pass before a new fireball is generated. 
            "Normal" difficulty is 3. */
        private static int delay;

        /* The Y-coordinate limit at which the fireball is removed */
        private static int remove = 460;

        private static int speed = 2;
        private static int lowX = 0;
        private static int highX = 450;
        private static int lowY = 40;
        private static int highY = 40;

        /* A running list of currently active fireballs */
        private static List<Fireball> fireballs = new List<Fireball>();

        public Fireball(int startX, int startY)
        {
            X = startX;
            Y = startY;
        }

        public Fireball(Texture2D fireball, int startX, int startY)
        {
            X = startX;
            Y = startY;
            img = fireball;
        }

        /// <summary>
        /// Advances this fireball, which moves each fireball down by the value
        /// of speed.
        /// </summary>
        public void advanceGameTick()
        {
            this.Y += speed;

            if (Y >= remove)
                fireballs.Remove(this);
        }

        /// <summary>
        /// Set the number of game ticks to wait before generating a new fireball.
        /// Standard difficulty is 3.
        /// </summary>
        /// <param name="fireballDelay"></param>
        public static void setFireballDelay(int fireballDelay)
        {
            delay = fireballDelay;
        }

        /// <summary>
        /// Set the vertical speed of each fireball
        /// </summary>
        /// <param name="s">Number of pixels to travel every game tick</param>
        public static void setSpeed(int s)
        {
            speed = s;
        }

        /// <summary>
        /// Set the boundaries of where fireballs are allowed to be generated
        /// </summary>
        /// <param name="lX"></param>
        /// <param name="hX"></param>
        /// <param name="lY"></param>
        /// <param name="hY"></param>
        public static void setBounds(int lX, int hX, int lY, int hY)
        {
            if (lowX < 0 || lowY < 0)
                throw new ArgumentOutOfRangeException("Low bound is out of bounds!");

            lowX = lX;
            highX = hX;
            lowY = lY;
            highY = hY;
        }

        /// <summary>
        /// Generate a new fireball with a random starting location and adds it to the running list. 
        /// Bound arguments are inclusive.
        /// </summary>
        /// <param name="lowX">Low x bound</param>
        /// <param name="highX">High x bound</param>
        /// <param name="lowY">Low y bound</param>
        /// <param name="highY">High y bound</param>
        /// <returns>A new fireball</returns>
        public static Fireball generateFireball()
        {
            Random random = new Random();

            Fireball fireball = new Fireball(random.Next(lowX, highX), random.Next(lowY, highY));
            fireballs.Add(fireball);
            return fireball;
        }

        public static void advanceGameTick(int currentGameTick)
        {
            // Each tick advances the Y-component of the fireball by some number of pixels
            foreach (Fireball fb in fireballs)
            {
                fb.advanceGameTick();
            }

            // Generate a new fireball if current game tick reaches the delay threshold
            if (currentGameTick % delay == 0)
                generateFireball();
        }
    }
}
