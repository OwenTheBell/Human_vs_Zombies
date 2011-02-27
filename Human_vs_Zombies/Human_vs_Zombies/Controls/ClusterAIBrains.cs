using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.HvZClasses.Mobs;
using Human_vs_Zombies.HvZClasses;
using Human_vs_Zombies.Controls;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.Controls
{
    public class ClusterAIBrains : Brains
    {
        // Zombies will be in "awaiting attack" mode while their position
        // is in the circles of radius minSafeRadius and maxSafeRadius centered
        // on the player.
        // Please ensure at all times that minSafeRadius <= happyRadius <= maxSafeRadius
        public const float MIN_SAFE_RADIUS = 200f; // When a zombie is closer to the player than this, the zombie will attack.
        public const float HAPPY_RADIUS = 300f;   // Zombies will desire to be this distance from the player before the onslaught.
        public const float MAX_SAFE_RADIUS = 400f; // When a zombie is further than this, the zombie will readjust its position.

        private static float s_WaitTimer = 10;
        private static float s_AttackTimer = 3;
        private static bool s_Attack = false;

        private HvZWorld m_HvZWorld;

        private Vector2 m_Shoot;
        private Vector2 m_Walk;
        private Vector2 m_Destination;
        private bool m_Ready, m_Attacking;

        public ClusterAIBrains(HvZWorld hvzWorld)
        {
            this.m_HvZWorld = hvzWorld;
            this.m_Shoot = new Vector2();
            this.m_Walk = new Vector2();
            this.m_Ready = false;
            this.m_Attacking = false;
        }

        public override void Update(float dTime, Vector2 position)
        {
            Player player = m_HvZWorld.GetPlayer();
            Vector2 toPlayer = player.GetPosition() - position;
            float playerDistance = toPlayer.Length();
            toPlayer.Normalize();

            float happyRadius = player.GetWeaponSpeed() * Projectile.LIFE + 128;
            float minSafeRadius = .7f * happyRadius;
            float maxSafeRadius = 1.2f * happyRadius;
            

            if (playerDistance < happyRadius)  // If the zombie is potentially too close to the human.
            {
                if (playerDistance < minSafeRadius)
                {
                    // If the zombie is too close to the human, the zombie should attack!
                    this.m_Attacking = true;
                    this.m_Ready = false;
                    Attack();
                }
                else
                {
                    // The zombie is between minSafeRadius and happyRadius
                    this.m_Attacking = false;
                    this.m_Ready = true;
                }
            }
            else
            {
                if (playerDistance > maxSafeRadius)
                {
                    // If the zombie is too far away from the human, readjust accordingly
                    this.m_Ready = false;
                    this.m_Attacking = false;
                }
                else
                {
                    // The zombie is between happyRadius and maxSafeRadius
                    this.m_Attacking = false;
                    this.m_Ready = true;
                }
            }

            if (this.m_Attacking || (this.m_Ready && ClusterAIBrains.s_Attack))
            {
                this.m_Walk = toPlayer;
                this.m_Attacking = true;
                this.m_Ready = false;
            } else if (!this.m_Ready)
            {
                this.m_Walk = toPlayer;
                // Control the velocity here?
            }
            else
            {
                this.m_Walk = toPlayer * (playerDistance - happyRadius) / dTime / 250f;
            }
            this.m_Shoot = toPlayer;
            //path.Normalize();
            //this.m_Walk = path;
            //this.m_Shoot = this.m_Walk;
        }

        public static void StaticUpdate(float dTime)
        {
            if (s_Attack)
            {
                s_AttackTimer -= dTime;
            }
            else
            {
                s_WaitTimer -= dTime;
            }

            if (s_WaitTimer <= 0)
            {
                Attack();
                s_WaitTimer = 10;
            }
            else if (s_AttackTimer <= 0)
            {
                s_Attack = false;
                s_AttackTimer = 1;
            }
        }

        public static void Attack()
        {
            s_Attack = true;
        }

        public override Vector2 GetWalk()
        {
            return m_Walk;
        }

        public override Vector2 GetShoot()
        {
            return m_Shoot;
        }
    }
}
