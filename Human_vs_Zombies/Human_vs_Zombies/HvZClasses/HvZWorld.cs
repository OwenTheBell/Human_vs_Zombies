using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.HvZClasses.Mobs;
using Microsoft.Xna.Framework;
using System.Collections;
using Human_vs_Zombies.Rendering;
using Microsoft.Xna.Framework.Graphics;

namespace Human_vs_Zombies.HvZClasses
{
    public class HvZWorld
    {
        private Player m_Player;
        private SortedDictionary<ulong, Entity> m_Entities;
        private SortedDictionary<ulong, List<Entity>> m_ColMatrix;
        private bool m_ColUpdated;

        public HvZWorld()
        {
            m_Player = new Player(this, new Vector2(100f, 100f), 0f, 0f, Vector2.Zero);
            this.m_Entities = new SortedDictionary<ulong, Entity>();
            this.m_ColMatrix = null;
        }

        public Player GetPlayer()
        {
            return m_Player;
        }

        private void CheckCols()
        {
            m_ColMatrix = new SortedDictionary<ulong, List<Entity>>();
            for (int i = 0; i < m_Entities.Values.Count - 1; i++ )
            {
                for (int j = i + 1; j < m_Entities.Values.Count; j++)
                {
                    Entity one = m_Entities.Values.ElementAt(i);
                    Entity two = m_Entities.Values.ElementAt(j);

                    if ((one.GetPosition() - two.GetPosition()).LengthSquared() <= Math.Pow((one.GetRadius() + two.GetRadius()), 2))
                    {
                        if (m_ColMatrix.ContainsKey(one.GetID()))
                        {
                            List<Entity> cols = new List<Entity>();
                            cols.Add(two);
                            m_ColMatrix.Add(one.GetID(), cols);
                        }
                        else
                        {
                            List<Entity> cols;
                            m_ColMatrix.TryGetValue(one.GetID(), out cols);
                            cols.Add(two);
                        }

                        if (m_ColMatrix.ContainsKey(two.GetID()))
                        {
                            List<Entity> cols = new List<Entity>();
                            cols.Add(one);
                            m_ColMatrix.Add(two.GetID(), cols);
                        }
                        else
                        {
                            List<Entity> cols;
                            m_ColMatrix.TryGetValue(two.GetID(), out cols);
                            cols.Add(one);
                        }
                    }
                }
            }

            m_ColUpdated = true;
        }

        public List<Entity> Collisions(Entity entity)
        {
            if (!m_ColUpdated)
            {
                return new List<Entity>();
            }
            else
            {
                List<Entity> ret = new List<Entity>();
                m_ColMatrix.TryGetValue(entity.GetID(), out ret);
                return ret;
            }
        }

        private void KillDeadEntities()
        {
            m_ColUpdated = false;
            foreach(Entity e in m_Entities.Values)
            {
                if (e.GetDead())
                {
                    m_Entities.Remove(e.GetID());
                }
            }
        }

        public void Update(float dTime)
        {
            this.CheckCols();

            foreach (Entity e in m_Entities.Values)
            {
                e.Update(dTime);
            }

            this.KillDeadEntities();
        }

        public void addEntity(Entity entity)
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

            foreach (Entity e in m_Entities.Values)
            {
                e.Draw();
            }
        }
    }
}
