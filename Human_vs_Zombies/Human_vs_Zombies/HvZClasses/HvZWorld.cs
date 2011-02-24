using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.HvZClasses.Mobs;
using Microsoft.Xna.Framework;
using System.Collections;
using Human_vs_Zombies.Rendering;
using Microsoft.Xna.Framework.Graphics;
using Human_vs_Zombies.GameElements;
using Human_vs_Zombies.Controls;

namespace Human_vs_Zombies.HvZClasses
{
    public class HvZWorld
    {
        private Player m_Player;
        private SortedDictionary<ulong, Entity> m_Entities; // <ID of entity, The Entity>

        public HvZWorld()
        {
            m_Player = new Player(this, new Vector2(100f, 100f), Vector2.Zero, 0f, Vector2.Zero, 5, 5);
        }

        public Player GetPlayer()
        {
            return m_Player;
        }

        public void Update(float dTime)
        {

        }

        public void AddEntity(Entity entity)
        {
            this.m_Entities.Add(entity.GetID(), entity);
        }

        public void Draw()
        {
            Drawer.Draw(
                TextureStatic.Get("background"),
                Drawer.FullScreenRectangle,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f);

            this.m_Player.Draw();
        }

        public void SpawnZombie()
        {
            Random gen = new Random();
            Vector2 position = new Vector2(gen.Next((int)GameWorld.screenDimensions.X-30), gen.Next((int)GameWorld.screenDimensions.Y-30));
            Zombie m_Zombie = new Zombie(this, position, 0f, 0f, Vector2.Zero, 10f, new SimpleAIBrains(this));
            m_Entities.Add(m_Zombie.GetID(), m_Zombie);
        }

    }
}
