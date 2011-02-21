using System;
using Human_vs_Zombies.GameElements;

namespace Human_vs_Zombies
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameWorld game = new GameWorld())
            {
                game.Run();
            }
        }
    }
#endif
}

