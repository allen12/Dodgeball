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
        private int delay;

        private int X;
        private int Y;

        public Fireball(int delay)
        {
            this.delay = delay;
        }
    }
}
