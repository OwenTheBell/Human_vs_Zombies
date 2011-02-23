using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Human_vs_Zombies.Rendering;
using Human_vs_Zombies.GameElements;

namespace Human_vs_Zombies.Rendering
{
    /// <summary>
    /// This is a static public class for storing and receiving static
    /// Texture2Ds from ImageType enums.
    /// </summary>
    static public class TextureStatic
    {
        /// <summary>
        /// The lookup table of images to match with names.
        /// </summary>
        private static Dictionary<string, Texture2D> images = new Dictionary<string,Texture2D>();
        
        /// <summary>
        /// Loads all of the Texture2Ds for the game, both syncrounously
        /// for the Texture2Ds for the first things displayed, and
        /// asyncrounously for the Texture2Ds for the things not needed
        /// to be loaded right away. Be sure to call this method only once,
        /// and before you use Get(...) for the first time.
        /// </summary>
        public static void Load(string imageName, string directory)
        {
            if (!images.ContainsKey(imageName.ToLower()))
            {
                images.Add(imageName.ToLower(), GameWorld.content.Load<Texture2D>(directory));
            }
        }

        /// <summary>
        /// Gets the specified image type, and returns the corresponding Texture2D.
        /// </summary>
        /// <param name="imageType">Type of the image.</param>
        /// <returns>A Texture2D corresponding to the specified image type.</returns>
        public static Texture2D Get(string imageType)
        {
            if (images.ContainsKey(imageType.ToLower()))
            {
                return images[imageType.ToLower()];
            }

            return null;
        }
    }
}
