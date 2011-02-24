using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.HvZClasses;
using Human_vs_Zombies.GameElements;
using Human_vs_Zombies.Controls;
using Human_vs_Zombies.HvZClasses.Mobs;
using Microsoft.Xna.Framework;


namespace Human_vs_Zombies.Controls
{

    public class HumanBrains:Brains
    {

        private HvZWorld m_HvZWorld;

        private Vector2 m_Shoot;

        private Vector2 m_Walk;
        
        public HumanBrains(HvZWorld hvzWorld)
        {
            this.m_HvZWorld = hvzWorld;
            this.m_Shoot = new Vector2();
            this.m_Walk = new Vector2();
        }

        public override void update(float dTime, Vector2 position)
        {
            this.m_Walk = 
                new Vector2(300 * GameWorld.controller.ContainsFloat(ActionType.MoveHorizontal), 
                -300 * GameWorld.controller.ContainsFloat(ActionType.MoveVertical));
            this.m_Shoot = new Vector2(GameWorld.controller.ContainsFloat(ActionType.LookHorizontal), -GameWorld.controller.ContainsFloat(ActionType.LookVertical));
            this.m_Shoot.Normalize();

        }

        public override Vector2 GetWalk()
        {
            return this.m_Walk;
        }

        public override Vector2 getShoot()
        {
            return m_Shoot;
        }   
    }
}
