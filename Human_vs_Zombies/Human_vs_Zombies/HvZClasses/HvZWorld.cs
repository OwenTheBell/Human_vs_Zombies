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
        private SortedDictionary<ulong, List<Entity>> m_ColMatrix;
        private bool m_ColUpdated;
        private float zombieTimer, zombieCountdown; // How often zombies will spawn.

        public HvZWorld()
        {
            m_Player = new Player(this, new Vector2(100f, 100f), new Vector2(1f,0f), 32f, new Vector2(1f,0f), .1f, 500f);
            this.m_Entities = new SortedDictionary<ulong, Entity>();
            this.AddEntity(this.m_Player);
            this.m_ColMatrix = null;
            zombieTimer = 3; // Spawn a zombie every 3 seconds
            zombieCountdown = zombieTimer;
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
                        if (!m_ColMatrix.ContainsKey(one.GetID()))
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

                        if (!m_ColMatrix.ContainsKey(two.GetID()))
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
                return (ret != null ? ret : new List<Entity>());
            }
        }

        private void KillDeadEntities()
        {
            m_ColUpdated = false;

            List<Entity> dieNow = new List<Entity>();
            foreach(Entity e in m_Entities.Values)
            {
                if (e.GetDead())
                {
                    if (e is Zombie)
                    {
                        //this.zombieTimer *= 0.5f;
                    }
                    dieNow.Add(e);
                }
            }
            foreach (Entity e in dieNow)
            {
                m_Entities.Remove(e.GetID());
            }
        }

        public void Update(float dTime)
        {
            this.CheckCols();

            zombieCountdown -= dTime;
            if (zombieCountdown <= 0)
            {
                this.SpawnZombie();
                zombieCountdown = zombieTimer;
            }

            for (int i = 0; i < m_Entities.Values.Count; i++)
            {
                    m_Entities.Values.ElementAt(i).Update(dTime);
            }

            this.KillDeadEntities();
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

            foreach (Entity e in m_Entities.Values)
            {
                e.Draw();
            }
        }

        public void SpawnZombie()
        {
            Random gen = new Random();
            Vector2 playerPosition = m_Player.GetPosition();
            int spawnDistance = 300;
            Vector2 position= new Vector2(gen.Next((int)GameWorld.screenDimensions.X - 30), gen.Next((int)GameWorld.screenDimensions.Y - 30));
            //ensure that the zombie does not spawn to close to the player
            if ((position - playerPosition).LengthSquared() < spawnDistance * spawnDistance)
            {
                Vector2 shift = position - playerPosition;
                shift.Normalize();
                shift *= spawnDistance;
                position += shift;
            }
            
            Zombie m_Zombie = new Zombie(this, position, Vector2.Zero, 32f, Vector2.Zero, 250f, new SimpleAIBrains(this));
            m_Entities.Add(m_Zombie.GetID(), m_Zombie);
        }
    }
}
