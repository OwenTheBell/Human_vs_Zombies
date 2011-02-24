using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Controls;
using Human_vs_Zombies.Rendering;
using Microsoft.Xna.Framework.Graphics;

namespace Human_vs_Zombies.HvZClasses.Mobs
{
    public class Player : Mob
    {
        private Vector2 m_Target;


        private Brains m_Brains;

        public Player(HvZWorld hvzWorld, Vector2 position, float rotation, float radius, Vector2 velocity)
            : base(hvzWorld, position, rotation, radius, velocity)
        {
            this.SetBrains(new HumanBrains(hvzWorld, this));
            
        }

        public Brains GetBrains()
        {
            return m_Brains;
        }

        public void SetBrains(Brains brains)
        {
            m_Brains = brains;
        }

        public Vector2 GetTarget()
        {
            return m_Target;
        }

        public void SetTarget(Vector2 target)
        {
            this.m_Target = target;
        }

        public override void Update(float dTime)
        {
            base.Update(dTime);
        }

        public void Draw() 
        {
            Drawer.Draw(
                TextureStatic.Get("Human"),
                Vector2.Zero,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                Vector2.Zero,
                SpriteEffects.None,
                0.9f);
        }
    }
}
