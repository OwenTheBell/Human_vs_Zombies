using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.HvZClasses.Walls
{
    public class Wall:Entity
    {
        private float m_Thickness;

        public Wall(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, float thickness)
            : base(hvzWorld, position, rotation, radius)
        {
        }

        public float GetThickness()
        {
            return m_Thickness;
        }

        public void SetThickness(float thickness)
        {
            m_Thickness = thickness;
        }

        public override void Update(float dTime) { }
        public override void Draw() { }
    }
}
