using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using System;

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

        public override void advanceGameTick()
        {
            throw new NotImplementedException();
        }

        public override void draw(SpriteBatch sp)
        {
            throw new NotImplementedException();
        }
    }
}
