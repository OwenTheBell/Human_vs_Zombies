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
using Human_vs_Zombies.HvZClasses.Walls;
using Human_vs_Zombies.HvZClasses.Items;

namespace Human_vs_Zombies.HvZClasses
{
    public class HvZWorld
    {
        private Player m_Player;
        private SortedDictionary<ulong, Entity> m_Entities; // <ID of entity, The Entity>
        private SortedDictionary<ulong, List<Entity>> m_ColMatrix;
        private List<ulong> m_WallIndices; //store the indices of walls in the entities dictionary for easy access
        private bool m_ColUpdated;
        private float zombieCountdown; // How often zombies will spawn
        private float wallCountdown; // How often a new wall spawns
        private float wallKillCountdown; // How often to kill existing walls
        private List<GridPoint> m_WallGrid; //List of all the 
        private int numZombies;
        private float m_TimeElapsed;

        private class GridPoint
        {
            public int X, Y;

            public GridPoint(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
        }

        public HvZWorld()
        {
            m_Player = new Player(this, new Vector2(300, 300), new Vector2(1f,0f), 32f, new Vector2(1f,0f), 400f, Settings.playerWeaponTimer, Settings.playerWeaponSpeed, Settings.playerAmmo);
            this.m_Entities = new SortedDictionary<ulong, Entity>();
            this.AddEntity(this.m_Player);
            this.numZombies = 0;

            this.m_WallGrid = new List<GridPoint>();
            this.m_WallIndices = new List<ulong>();

            this.CreateGrid();

            this.SpawnWall();

            //define the boundaries of the play area using walls drawn offscreen that do not cast shadows
            this.AddEntity(new Wall(this, new Vector2(Settings.worldWidth / 2, -16), Vector2.UnitX, Settings.worldWidth, 64, false));
            this.AddEntity(new Wall(this, new Vector2(-16, Settings.worldHeight / 2), Vector2.UnitY, Settings.worldHeight, 64, false));
            this.AddEntity(new Wall(this, new Vector2(Settings.worldWidth / 2, Settings.worldHeight + 16), Vector2.UnitX, Settings.worldWidth, 64, false));
            this.AddEntity(new Wall(this, new Vector2(Settings.worldWidth + 16, Settings.worldHeight / 2), Vector2.UnitY, Settings.worldHeight, 64, false));

            this.m_ColMatrix = null;
            this.zombieCountdown = Settings.zombieTimer;
            this.wallCountdown = Settings.wallSpawnTimer;
            this.wallKillCountdown = Settings.wallKillTimer;
            this.wallCountdown = Settings.wallSpawnTimer;
            m_TimeElapsed = 0;
        }

        public Player GetPlayer()
        {
            return m_Player;
        }

        private void CreateGrid()
        {
            int gridX = (int) Settings.wallRadius;
            int gridY = (int) Settings.wallRadius;

            while (gridX < Settings.worldWidth)
            {
                while (gridY < Settings.worldHeight)
                {
                    this.m_WallGrid.Add(new GridPoint(gridX, gridY));
                    gridY += (int) Settings.wallRadius;
                }
                gridX += (int) Settings.wallRadius;
                gridY = 0; 
            }
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

                    if (one.Collides(two))
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
                        this.numZombies--;
                    }
                    else if (e is Player)
                    {
                        GameWorld.audio.SongPlay("death", false);
                        GameWorld.screens.GameOver();
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
            m_TimeElapsed += dTime;

            this.CheckCols();

            zombieCountdown -= dTime;
            if (zombieCountdown <= 0 && this.numZombies < Settings.zombieMax)
            {
                this.SpawnZombie();
                this.SpawnItem();
                zombieCountdown = Settings.zombieTimer;
            }

            if (m_TimeElapsed >= Settings.startClusterAI)
            {
                ClusterAIBrains.StaticUpdate(dTime);
            }

            wallKillCountdown -= dTime;
            if (wallKillCountdown <= 0)
            {
                this.KillWall();
                wallKillCountdown = Settings.wallKillTimer;
            }

            wallCountdown -= dTime;
            if (wallCountdown <= 0)
            {
                this.SpawnWall();
                wallCountdown = Settings.wallSpawnTimer;
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
                if (e is Wall)
                {
                    Wall wall = (Wall)e; //eeeeeeevaaa!
                    if (wall.CastShadow())
                    {
                        DrawShadow(wall, .9f);
                    }
                }
            }
        }

        public void SpawnZombie()
        {
            Random gen = new Random();
            Vector2 playerPosition = m_Player.GetPosition();
            int spawnDistance = 300;
            Vector2 position= new Vector2(gen.Next((int)Settings.worldWidth - 30), gen.Next((int)Settings.worldHeight - 30));
            //ensure that the zombie does not spawn to close to the player
            if ((position - playerPosition).LengthSquared() < spawnDistance * spawnDistance)
            {
                Vector2 shift = position - playerPosition;
                shift.Normalize();
                shift *= spawnDistance;
                position += shift;
            }
            if (InShadow(position, playerPosition)) {
                this.numZombies++;
                Zombie zomblie = new Zombie(this, position, Vector2.Zero, 32f, Vector2.Zero, Settings.zombieMaxVel, m_TimeElapsed < Settings.startClusterAI ? (Brains)new SimpleAIBrains(this) : new ClusterAIBrains(this));
                this.AddEntity(zomblie);
            }
        }
        public void SpawnItem()
        {
            Random gen = new Random();
            Vector2 playerPosition = m_Player.GetPosition();
            Vector2 position = new Vector2(gen.Next((int)Settings.worldWidth - 30), gen.Next((int)Settings.worldHeight - 30));

            if (InShadow(position, playerPosition))
            {
                Item it = Item.NewRandomItem(this, position, Vector2.Zero, 32f, Settings.itemLifespan);
                this.AddEntity(it);
            }
        }
        public bool InShadow(Vector2 point, Vector2 pov)
        {
            foreach(Entity e in m_Entities.Values)
            {
                if (e is Wall)
                {
                    Wall wall = (Wall)e; //wwwaaaaaaallllllleeeee
                    if (wall.CastShadow())
                    {
                        Vector2[] corners = wall.GetPoints();

                        Vector2 center = (corners[0] + corners[1] + corners[2] + corners[3]) / 4;
                        Vector2 ray = center - pov;

                        Vector2 pos1 = corners[0];

                        for (int i = 1; i < 4; i++)
                        {
                            Vector2 ray1 = pos1 - pov;
                            Vector2 ray2 = corners[i] - pov;
                            if (SignedAngle(ray, ray2) < SignedAngle(ray, ray1)) pos1 = corners[i];
                        }

                        Vector2 pos2 = corners[0];

                        for (int i = 1; i < 4; i++)
                        {
                            Vector2 ray1 = pos2 - pov;
                            Vector2 ray2 = corners[i] - pov;
                            if (SignedAngle(ray, ray2) > SignedAngle(ray, ray1)) pos2 = corners[i];
                        }

                        Vector2 raytrace = point - pov;

                        if (ray.Length() <= raytrace.Length() && SignedAngle(ray, raytrace) < SignedAngle(ray, pos2 - pov) && SignedAngle(ray, raytrace) > SignedAngle(ray, pos1 - pov))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void SpawnWall()
        {
            Random random = new Random();
            if (this.m_WallGrid.Count > 0)
            {
                int selectedPoint = random.Next(0, this.m_WallGrid.Count - 1);

                GridPoint selected = this.m_WallGrid.ElementAt(selectedPoint);

                //create floats in the range -1 to 1
                float x = (float)(random.NextDouble() * 2 - 1);
                float y = (float)(random.NextDouble() * 2 - 1);

                Wall wall = new Wall(
                        this,
                        new Vector2(selected.X, selected.Y),
                        new Vector2(x, y),
                        Settings.wallRadius,
                        Settings.wallThickness,
                        true);
                if (!(wall.Collides(this.GetPlayer())))
                {
                    this.m_WallIndices.Add(wall.GetID());
                    this.AddEntity(wall);
                    this.m_WallGrid.Remove(selected);
                }
            }
        }

        private void KillWall()
        {
            Random random = new Random();

            int toRemove = random.Next(0, m_WallIndices.Count - 1);

            this.m_WallIndices.Remove(this.m_Entities.ElementAt(toRemove).Key);

            this.m_Entities.Remove(m_WallIndices.ElementAt(toRemove));
        }

        private void DrawShadow(Wall wall, float layer)
        {
            Vector2[] corners = wall.GetPoints();

            Vector2 p = GetPlayer().GetPosition();

            Vector2 center = (corners[0] + corners[1] + corners[2] + corners[3]) / 4;
            Vector2 ray = center - p;

            Vector2 pos1 = corners[0];

            for (int i = 1; i < 4; i++)
            {
                Vector2 ray1 = pos1 - p;
                Vector2 ray2 = corners[i] - p;
                if (SignedAngle(ray, ray2) < SignedAngle(ray, ray1)) pos1 = corners[i];
            }

            Vector2 pos2 = corners[0];

            for (int i = 1; i < 4; i++)
            {
                Vector2 ray1 = pos2 - p;
                Vector2 ray2 = corners[i] - p;
                if (SignedAngle(ray, ray2) > SignedAngle(ray, ray1)) pos2 = corners[i];
            }

            Vector2 dir1 = pos1 - p;
            Vector2 dir2 = pos2 - p;
            float angle = SignedAngle(dir1, pos2 - p);

            int width = (int)Math.Min(wall.GetRadius(), wall.GetThickness());
            int length = Drawer.FullScreenRectangle.Width;

            Rectangle rect = new Rectangle(0, 0, length, width);

            Vector2 offset = new Vector2(-dir1.Y, dir1.X);
            offset.Normalize();

            float arc = angle * (length + Math.Max(dir1.Length(), dir2.Length()));

            //rect.Location = new Point((int)(pos1.X - width * offset.X), (int)(pos1.Y - width * offset.Y));
            //Drawer.Draw(TextureStatic.Get("Shadow"), rect, null, Color.Black, (float)Math.Atan2(dir1.Y, dir1.X), Vector2.UnitY * TextureStatic.Get("Shadow").Width, SpriteEffects.None, 1f);

            for (float s = 0; s < arc; s += width)
            {
                float frac = s / arc; //fraction of total progress
                float theta = frac * angle;

                //float distMult = (dir2.Length() / dir1.Length()) - (1 - frac) * (dir2.Length() - dir1.Length()) / dir1.Length();

                Vector2 trav = dir2 - dir1;
                Vector2 norm = new Vector2(trav.Y, -trav.X);
                norm.Normalize();
                Vector2 loc = dir1 + trav * frac + norm * width * (1 - frac) * (frac) + p;

                rect.Location = new Point((int)(loc.X + width * offset.X * (1 - frac)), (int)(loc.Y + width * offset.Y * (1 - frac)));
                Drawer.Draw(TextureStatic.Get("Shadow"), rect,
                    null, Color.Black, (float)Math.Atan2(dir1.Y, dir1.X) + theta, Vector2.UnitY * TextureStatic.Get("Shadow").Width, SpriteEffects.None, layer);
            }

            rect.Location = new Point((int)pos2.X, (int)pos2.Y);
            Drawer.Draw(TextureStatic.Get("Shadow"), rect,
                null, Color.Black, (float)Math.Atan2(dir1.Y, dir1.X) + angle, Vector2.UnitY * TextureStatic.Get("Shadow").Width, SpriteEffects.None, layer);
        }

        private float SignedAngle(Vector2 v1, Vector2 v2)
        {
            float angle = (float)Math.Acos(Vector2.Dot(v1, v2) / (v1.Length() * v2.Length()));

            float cross = (v1.X * v2.Y) - (v1.Y * v2.X);

            return angle * cross / Math.Abs(cross);
        }
    }
}
