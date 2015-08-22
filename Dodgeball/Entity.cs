using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dodgeball
{
    abstract class Entity
    {
        /* Image of the entity */
        protected static Texture2D img;
        public static Texture2D Img // img setter property
        {
            set
            {
                img = value;
            }
        }

        /* Location of the entity */
        protected int X;
        protected int Y;

        /// <summary>
        /// Subclasses implementing this must provide how the state of this
        /// entity changes with each game tick.
        /// </summary>
        public abstract void advanceGameTick();
    }

}
