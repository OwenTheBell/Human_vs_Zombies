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

        private int m_WeaponTimer;

        private int m_WeaponSpeed;

        private Brains m_Brains;

        public Player(HvZWorld hvzWorld, Vector2 position, Vector2 rotation, float radius, Vector2 velocity, int weaponTimer, int weaponSpeed)
            : base(hvzWorld, position, rotation, radius, velocity, 10f)
        {
            this.SetBrains(new HumanBrains(hvzWorld, this));
            this.SetWeaponTimer(weaponTimer);
            this.SetWeaponSpeed(weaponSpeed);
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

        public void SetWeaponTimer(int weaponTimer)
        {
            this.m_WeaponTimer = weaponTimer;
        }

        public int GetWeaponTimer()
        {
            return this.m_WeaponTimer;
        }

        public void SetWeaponSpeed(int weaponSpeed)
        {
            this.m_WeaponSpeed = weaponSpeed;
        }

        public int GetWeaponSpeed()
        {
            return this.m_WeaponSpeed;
        }

        public override void Update(float dTime)
        {
            //no collision handling at this time
            this.m_Brains.update(dTime);

            this.SetVelocity(m_Brains.GetWalk());
            this.SetRotation(m_Brains.getShoot());
            //Only fire the gun if the player is aiming and if the weapon can be fired
            if ((m_Brains.getShoot().Y != 0 || m_Brains.getShoot().X != 0) && m_WeaponTimer == 0)
            {
                this.m_HvZWorld.addEntity(new Projectile(this.m_HvZWorld, this.GetPosition(), this.GetRotation(), 10f, this.GetRotation() * this.m_WeaponSpeed));
            }
            else
            {
                this.m_WeaponTimer--;
            }

            base.Update(dTime);
        }

        public override void Draw() 
        {
            Drawer.Draw(
                TextureStatic.Get("Human"),
                this.GetPosition(),
                null,
                Color.White,
                0f,
                new Vector2(30f),
                1f,
                SpriteEffects.None,
                0.9f);
        }
    }
}
