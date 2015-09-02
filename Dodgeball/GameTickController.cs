using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dodgeball
{
    /// <summary>
    /// Aggregate/helper class to organize each game tick of the game.
    /// </summary>
    static class GameTickController
    {
        private static int gameTick;
        private static List<Entity> entities;
        public static List<Entity> Entities { get { return entities; } }

        private static SpriteBatch sb;
        
        static GameTickController()
        {
            gameTick = 0;

            entities = new List<Entity>();
        }

        public static void setSpriteBatch(SpriteBatch s)
        {
            sb = s;
        }

        public static void addEntity(Entity e)
        {
            entities.Add(e);
        }

        public static void advanceGameTick()
        {
            if (sb == null)
                throw new ArgumentNullException("SpriteBatch is null! Please set the sprite batch first.");

            foreach (Entity e in entities)
            {
                e.advanceGameTick(++gameTick);

                if (e is Fireball)
                {
                    if (e.Y > Fireball.Remove)
                        entities.Remove(e);
                }
            }

            Fireball newFireball = Fireball.generateFireball(gameTick);
            if (newFireball != null)
                entities.Add(newFireball);
        }

        public static void drawEntities()
        {
            

            foreach (Entity e in entities)
                e.draw(sb);

           
        }


    }
}
