using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Dodgeball
{
    /// <summary>
    /// An Entity class representing the enemy non-player character.
    /// </summary>
    class Monster : Entity
    {
        /* Direction that the monster is moving in horizontally. 1 = right, -1 = left. */
        private int direction = 1;

        public Monster(Texture2D i)
        {
            img = i;
        }

        public override void advanceGameTick(int currentGameTick)
        {
            // Monster moves sideways
            if (direction == 1)
                x += 6;
            else
                x -= 6;

            if (x >= 20 || x <= -350)
                direction *= -1;
        }

        public override void draw(SpriteBatch sp)
        {
            const float SCALE = 2.0f;

            sp.Draw(img, new Vector2(x, y), new Rectangle(0, 0, img.Width, img.Height),
                Color.White, 0.0f, new Vector2(0, 0), SCALE, SpriteEffects.None, 1.0f);
        }
    }
}
