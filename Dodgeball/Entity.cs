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
        public static Texture2D Img
        {
            get
            {
                return img;
            }

            set
            {
                img = value;
            }
        }

        /* Location of the entity */
        protected int x;
        public int X
        {
            get
            {
                return x;
            }
        }
        protected int y;
        public int Y
        {
            get
            {
                return y;
            }
        }

        /// <summary>
        /// Subclasses implementing this must provide how the state of this
        /// entity changes with each game tick.
        /// </summary>
        public abstract void advanceGameTick();

        /// <summary>
        /// Checks to see if this object touches, collides, or intersects with
        /// the given Entity object.
        /// </summary>
        /// <param name="other">The other entity to compare with</param>
        /// <returns>True if the entities intersect, false otherwise</returns>
        public bool intersects(Entity other)
        {
            return getRectangle().Intersects(other.getRectangle());
        }

        /// <summary>
        /// Gets a rectangle representation of the sprite denoted by this entity
        /// </summary>
        /// <returns>A Rectangle vector object</returns>
        public Rectangle getRectangle()
        {
            return new Rectangle(x, y, img.Width, img.Height);
        }
    }

}
