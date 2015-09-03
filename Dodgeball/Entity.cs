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
        protected Texture2D img;
        public Texture2D Img
        {
            get; set;
        }

        /* Location of the entity */
        protected int x;
        public int X
        {
            get;
        }
        protected int y;
        public int Y
        {
            get;
        }

        /// <summary>
        /// Subclasses implementing this must provide how the state of this
        /// entity changes with each game tick.
        /// </summary>
        public abstract void advanceGameTick(int currentGameTick);

        /// <summary>
        /// Subclasses implementing this must provide how this object is to
        /// be drawn in the game window, to be called each game tick.
        /// </summary>
        public abstract void draw(SpriteBatch sp);

        /// <summary>
        /// Checks to see if this object touches, collides, or intersects with
        /// the given Entity object.
        /// </summary>
        /// <param name="other">The other entity to compare with</param>
        /// <returns>True if the entities intersect, false otherwise</returns>
        public bool intersects(Entity other)
        {
            Rectangle t = this.getRectangle();
            Rectangle r = other.getRectangle();

            return r.Width > 0 && r.Height > 0 && t.Width > 0 && t.Height > 0 && r.X < t.X + t.Width && r.X + r.Width > t.X && r.Y < t.Y + t.Height && r.Y + r.Height > t.Y;
        }

        /// <summary>
        /// Gets a rectangle representation of the sprite denoted by this entity.
        /// This can be overriden to create fine-tuning of rectangle representations.
        /// </summary>
        /// <returns>A Rectangle vector object</returns>
        public virtual Rectangle getRectangle()
        {
            return new Rectangle(x, y, img.Width, img.Height);
        }
    }

}
