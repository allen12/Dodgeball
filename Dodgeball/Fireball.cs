using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dodgeball
{
    /// <summary>
    /// <p> The fireball class represents a fireball, an enemy object that will
    /// kill the player if this object intersects with the player object.
    /// 
    /// </summary>
    class Fireball
    {
        /* Represents the number of ticks that must pass before a new fireball is generated. 
            "Normal" difficulty is 3. */
        private static int delay;

        private static int lowX = 0;
        private static int highX = 450;
        private static int lowY = 40;
        private static int highY = 40;

        /* A running list of currently active fireballs */
        private static List<Fireball> fireballs = new List<Fireball>();

        private int X;
        private int Y;

        public Fireball(int startX, int startY)
        {
            X = startX;
            Y = startY;
        }

        public static void setFireballDelay(int fireballDelay)
        {
            delay = fireballDelay;
        }

        public static void setBounds(int lX, int hX, int lY, int hY)
        {
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
        public static Fireball generateFireball(int lowX, int highX, int lowY, int highY)
        {
            if (lowX < 0 || lowY < 0)
                throw new ArgumentOutOfRangeException("Low bound is out of bounds!");

            Random random = new Random();

            Fireball fireball = new Fireball(random.Next(lowX, highX), random.Next(lowY, highY));
            fireballs.Add(fireball);
            return fireball;
        }

        public static void advanceGameTick(int currentGameTick)
        {
            if (currentGameTick % 3 == 0)
                
        }
    }
}
