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

        private Player m_Player;

        private HvZWorld m_HvZWorld;

        private Vector2 m_Shoot;

        private Vector2 m_Walk;
        
        public HumanBrains(HvZWorld hvzWorld, Player player)
        {
            this.m_HvZWorld = hvzWorld;
            this.m_Player = player;
            m_Shoot = new Vector2();
            m_Walk = new Vector2();
        }

        public override void update(float dTime)
        {
            m_Walk = (new Vector2(this.m_Player.getPosition().X - 10 * GameWorld.controller.ContainsFloat(ActionType.MoveHorizontal), 
                this.m_Player.getPosition().Y - 10 * GameWorld.controller.ContainsFloat(ActionType.MoveVertical)));
            if ((GameWorld.controller.ContainsFloat(ActionType.LookVertical) + GameWorld.controller.ContainsFloat(ActionType.LookHorizontal)) > .5)
            {
                m_Shoot = new Vector2(GameWorld.controller.ContainsFloat(ActionType.LookVertical), GameWorld.controller.ContainsFloat(ActionType.LookHorizontal));
            }

        }

        public override Vector2 getWalk()
        {
            return m_Walk;
        }

        public override Vector2 getShoot()
        {
            return m_Shoot;
        }   
    }
}
