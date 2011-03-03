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

        public static int playerAmmo { get { return 128; } }

        //width of game screen
        public static int screenWidth { get { return 960; } }

        //height of game screen
        public static int screenHeight { get { return 540; } }

        //width of game world
        public static int worldWidth { get { return 1920; } }

        //height of game world
        public static int worldHeight { get { return 1080; } }

        public static float wallRadius { get { return 192f; } }

        public static float wallThickness { get { return 64f; } }

        //rate at which new walls spawn
        public static float wallSpawnTimer { get { return 5f; } }

        public static float wallKillTimer { get { return 10f; } }

        //rate at which the zombies spawn, spawns a zombie every 3 seconds
        //zombie spawn rate is also affected by the amount of shadow on the screen
        public static float zombieTimer { get { return .1f; } }

        public static int zombieMax { get { return 30; } }

        public static float zombieMaxVel { get { return 256f; } }

        public static float startClusterAI { get { return 30f; } } 
    }
}
