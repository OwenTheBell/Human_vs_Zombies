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
            this.m_Shoot = new Vector2();
            this.m_Walk = new Vector2();
        }

        public override void update(float dTime)
        {
            this.m_Walk = (
                new Vector2(this.m_Player.GetPosition().X - 10 * GameWorld.controller.ContainsFloat(ActionType.MoveHorizontal), 
                this.m_Player.GetPosition().Y - 10 * GameWorld.controller.ContainsFloat(ActionType.MoveVertical))
                );
            this.m_Shoot = new Vector2(GameWorld.controller.ContainsFloat(ActionType.LookVertical), GameWorld.controller.ContainsFloat(ActionType.LookHorizontal));
            float length = (float) Math.Sqrt(Math.Pow(this.m_Shoot.X, 2) + Math.Pow(this.m_Shoot.Y, 2));
            this.m_Shoot.X = this.m_Shoot.X / length;
            this.m_Shoot.Y = this.m_Shoot.Y / length;

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
