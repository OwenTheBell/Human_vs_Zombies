using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Human_vs_Zombies.Mathematics
{
    /// <summary>
    /// This static public class is used to convert from a Vector2
    /// to an angle or vice versa.
    /// </summary>
    static public class RotationHelper
    {
        private static Vector2 returnVector = new Vector2();
        
        /// <summary>
        /// Takes as a paramter a vector and returns
        /// the angle in radians corresponding to the vector.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y cooordinate.</param>
        /// <returns>
        /// The angle that the vector makes with the vertical.
        /// </returns>
        public static float Vector2ToAngle(float x, float y)
        {
            // Check that all points are valid.
            if (float.IsNaN(x) || float.IsNaN(y))
            {
                return 0;
            }

            return (float)Math.Acos(y / Math.Sqrt(x * x + y * y));
        }

        /// <summary>
        /// Converts the specified angle into a vector of magnitude 1.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>A Vector2 of magnitude 1.</returns>
        public static Vector2 AngleToVector2(float angle)
        {
            returnVector.X = (float)Math.Cos(angle + MathHelper.Pi);
            returnVector.Y = (float)Math.Sin(angle + MathHelper.Pi);
            return returnVector;
        }
    }
}
