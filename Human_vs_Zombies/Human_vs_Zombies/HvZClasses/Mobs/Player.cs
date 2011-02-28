using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.Controls;
using Human_vs_Zombies.Rendering;
using Microsoft.Xna.Framework.Graphics;
using Human_vs_Zombies.HvZClasses.Walls;
using Human_vs_Zombies.GameElements;

namespace Human_vs_Zombies.HvZClasses.Mobs
{
    public class Player : Mob
    {
        private Vector2 m_Target;

        private float m_WeaponTimer;

        private float m_TimerCurrent;

        private float m_WeaponSpeed;

        private Brains m_Brains;

        public Player(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, Vector2 velocity, float weaponTimer, float weaponSpeed)
            : base(hvzWorld, position, rotation, radius, velocity, 400f)
        {
            this.SetBrains(new HumanBrains(hvzWorld));
            this.SetWeaponTimer(weaponTimer);
            m_TimerCurrent = 0;
            this.SetWeaponSpeed(weaponSpeed);
            GameWorld.audio.LoadSound("shoot", @"Sounds/gunshot");
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

        public void SetWeaponTimer(float weaponTimer)
        {
            this.m_WeaponTimer = weaponTimer;
        }

        public float GetWeaponTimer()
        {
            return this.m_WeaponTimer;
        }

        public void SetWeaponSpeed(float weaponSpeed)
        {
            this.m_WeaponSpeed = weaponSpeed;
        }

        public float GetWeaponSpeed()
        {
            return this.m_WeaponSpeed;
        }

        public override void Update(float dTime)
        {
            List<Entity> cols = GetHvZWorld().Collisions(this);

            foreach (Entity c in cols)
            {
                if (c is Zombie)
                {
                    this.SetDead(true);
                }
            }

            this.m_Brains.Update(dTime, this.GetPosition());

            this.SetVelocity(m_Brains.GetWalk());

            if (m_Brains.GetShoot().LengthSquared() > 0f)
            {
                this.SetRotation(m_Brains.GetShoot());
            }
            else if(this.GetVelocity().LengthSquared() > 0f)
            {
                this.SetRotation(this.GetVelocity() / this.GetVelocity().Length());
            }
            //Only fire the gun if the player is aiming and if the weapon can be fired
            if ((m_Brains.GetShoot().LengthSquared() > 0f) && m_TimerCurrent <= 0)
            {
                this.GetHvZWorld().AddEntity(new Projectile(this.GetHvZWorld(), this.GetPosition(), this.GetRotation(), 5f, this.GetVelocity() + this.GetRotation() * this.m_WeaponSpeed));
                m_TimerCurrent = m_WeaponTimer;
                GameWorld.audio.PlaySound("shoot");
            }

            this.m_TimerCurrent -= dTime;

            base.Update(dTime);
        }

        public override void Draw() 
        {
            float ratio = Drawer.GetRatio();
            Drawer.Draw(
                TextureStatic.Get("Human"),
                this.GetPosition(),
                null,
                Color.White,
                (float)Math.Atan2(this.GetRotation().Y, this.GetRotation().X),
                new Vector2(30f),
                1f,
                SpriteEffects.None,
                0.7f);
        }
    }
}
