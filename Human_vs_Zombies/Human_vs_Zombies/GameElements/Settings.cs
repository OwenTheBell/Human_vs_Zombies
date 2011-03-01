using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Human_vs_Zombies.GameElements
{
    public static class Settings
    {
        //base speed for the darts fired by the player
        public static float playerWeaponSpeed { get { return 500f; } }

        //rate at which the player can fire their weapon
        public static float playerWeaponTimer { get { return .1f; } }

        //width of game screen
        public static int screenWidth { get { return 960; } }

        //height of game screen
        public static int screenHeight { get { return 540; } }

        public static float wallRadius { get { return 256f; } }

        public static float wallThickness { get { return 126f; } }

        public static int wallGridX { get { return (int)((screenWidth * 2) / wallRadius); } }

        public static int wallGridY { get { return (int)((screenHeight * 2) / wallRadius); } }

        //rate at which new walls spawn
        public static float wallTimer { get { return 1f; } }

        //rate at which the zombies spawn, spawns a zombie every 3 seconds
        public static float zombieTimer { get { return 1f; } }
    }
}
