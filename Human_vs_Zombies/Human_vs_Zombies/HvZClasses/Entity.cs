using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.HvZClasses
{
    /// <summary>
    /// The base class for all objects in the field of play.
    /// </summary>
    public abstract class Entity
    {
        protected static ulong s_ID = 0;

        private Vector2 m_Position;
        private float m_Rotation;
        private float m_Radius;
        public HvZWorld m_HvZWorld;
        private bool m_isDead;
        private ulong m_ID;

        /// <summary>
        /// Constructs a new Entity.
        /// </summary>
        /// <param name="hvzWorld">World the Entity occupies.</param>
        /// <param name="position">Position of the Entity, in pixels.</param>
        /// <param name="rotation">Rotation of the Entity, in radians (right-hand).</param>
        /// <param name="radius">Size of the Entity, in pixels.</param>
        public Entity(HvZWorld hvzWorld, Vector2 position, float rotation, float radius)
        {
            this.m_HvZWorld = hvzWorld;
            this.SetPosition(position);
            this.SetRotation(rotation);
            this.SetRadius(radius);
            this.SetDead(false);

            m_ID = s_ID++;
        }

        /// <summary>
        /// The environment of this.
        /// </summary>
        /// <returns>The world.</returns>
        public HvZWorld GetWorld()
        {
            return this.m_HvZWorld;
        }

        /// <summary>
        /// The position of this.
        /// </summary>
        /// <returns>Vector for position, in pixels</returns>
        public Vector2 GetPosition()
        {
            return this.m_Position;
        }

        /// <summary>
        /// Set the position of this.
        /// </summary>
        /// <param name="position">Vector for position, in pixels</param>
        public void SetPosition(Vector2 position)
        {
            this.m_Position = position;
        }

        /// <summary>
        /// The rotation of this.
        /// </summary>
        /// <returns>Rotation in radians, counterclockwise from positive x axis</returns>
        public float GetRotation()
        {
            return this.m_Rotation;
        }

        /// <summary>
        /// Set the rotation of this.
        /// </summary>
        /// <param name="rotation">Rotation in radians. Right hand rule.</param>
        public void SetRotation(float rotation)
        {
            //the rotation in radians
            this.m_Rotation = rotation % (2.0f * (float)Math.PI);
        }

        /// <summary>
        /// The radius of this, in pixels. Used for collision and drawing.
        /// </summary>
        /// <returns>Radius in pixels.</returns>
        public float GetRadius()
        {
            return this.m_Radius;
        }

        /// <summary>
        /// Set the radius.
        /// </summary>
        /// <param name="radius">Radius in pixels.</param>
        public void SetRadius(float radius)
        {
            this.m_Radius = radius;
        }

        public void SetDead(bool isDead)
        {
            this.m_isDead = isDead;
        }

        public bool GetDead()
        {
            return this.m_isDead;
        }

        public ulong GetID()
        {
            return m_ID;
        }

        /// <summary>
        /// Updates this Entity over an interval in time.
        /// </summary>
        /// <param name="dTime">Interval of elapsed time, in seconds.</param>
        public abstract void Update(float dTime);

        /// <summary>
        /// Draws this Entity.
        /// </summary>
        public abstract void Draw();
    }
}
