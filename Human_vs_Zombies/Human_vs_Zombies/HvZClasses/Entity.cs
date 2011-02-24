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
        private Vector2 m_Position;
        private float m_Rotation;
        private float m_Radius;
        public HvZWorld m_HvZWorld;

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
            this.setPosition(position);
            this.setRotation(rotation);
            this.setRadius(radius);
        }

        /// <summary>
        /// The environment of this.
        /// </summary>
        /// <returns>The world.</returns>
        public HvZWorld getWorld()
        {
            return this.m_HvZWorld;
        }

        /// <summary>
        /// The position of this.
        /// </summary>
        /// <returns>Vector for position, in pixels</returns>
        public Vector2 getPosition()
        {
            return this.m_Position;
        }

        /// <summary>
        /// Set the position of this.
        /// </summary>
        /// <param name="position">Vector for position, in pixels</param>
        public void setPosition(Vector2 position)
        {
            this.m_Position = position;
        }

        /// <summary>
        /// The rotation of this.
        /// </summary>
        /// <returns>Rotation in radians, counterclockwise from positive x axis</returns>
        public float getRotation()
        {
            return this.m_Rotation;
        }

        /// <summary>
        /// Set the rotation of this.
        /// </summary>
        /// <param name="rotation">Rotation in radians. Right hand rule.</param>
        public void setRotation(float rotation)
        {
            //the rotation in radians
            this.m_Rotation = rotation % (2.0f * (float)Math.PI);
        }

        /// <summary>
        /// The radius of this, in pixels. Used for collision and drawing.
        /// </summary>
        /// <returns>Radius in pixels.</returns>
        public float getRadius()
        {
            return this.m_Radius;
        }

        /// <summary>
        /// Set the radius.
        /// </summary>
        /// <param name="radius">Radius in pixels.</param>
        public void setRadius(float radius)
        {
            this.m_Radius = radius;
        }

        /// <summary>
        /// Updates this Entity over an interval in time.
        /// </summary>
        /// <param name="dTime">Interval of elapsed time, in seconds.</param>
        public abstract void update(float dTime);

        /// <summary>
        /// Draws this Entity.
        /// </summary>
        public abstract void Draw();
    }
}
