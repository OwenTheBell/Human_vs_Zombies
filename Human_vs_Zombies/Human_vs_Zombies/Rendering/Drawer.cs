using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Human_vs_Zombies.GameElements;
using Human_vs_Zombies.Rendering;
using Human_vs_Zombies.Mathematics;

namespace Human_vs_Zombies.Rendering
{
    /// <summary>
    /// This static public class is used for all rendering in the game. It can be called from 
    /// anywhere, but do not attempt to use it until the Initiallize() method has been
    /// called (this only needs to happen once ever).
    /// </summary>
    static public class Drawer
    {
        /// <summary>
        /// Gets the font used for regular text display.
        /// </summary>
        /// <value>The default font.</value>
        public static SpriteFont font { get; private set; }
        
        /// <summary>
        /// Gets the rectangle {0, 0, 1920, 1080}.
        /// </summary>
        /// <value>The full screen rectangle.</value>
        public static Rectangle FullScreenRectangle { get; private set; }

        /// <summary>
        /// Initiallizes the static variables used by this static public class..
        /// </summary>
        public static void Initiallize()
        {
            font = GameWorld.content.Load<SpriteFont>(@"Fonts\defaultFont");
            FullScreenRectangle = new Rectangle(0, 0, 1920, 1080);
        }

        /// <summary>
        /// This returns the ratio of the screen height to 1080, in the event that
        /// the TV is not at a 1080i or 1080p resolution. All scales and positions should
        /// be multiplied by this in order to correctly adjust to any screen resolution,
        /// while always assuming that the screen is 1920 * 1080.
        /// </summary>
        /// <returns>The ratio of the screen height to 1080.</returns>
        public static float GetRatio()
        {
            return GameWorld.graphics.Viewport.Width / 1920f;
        }

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="toDraw">The rectangle to draw.</param>
        public static void DrawRectangle(Rectangle toDraw)
        {
            Vector2 tl = new Vector2(toDraw.Left, toDraw.Top);
            Vector2 tr = new Vector2(toDraw.Right, toDraw.Top);
            Vector2 bl = new Vector2(toDraw.Left, toDraw.Bottom);
            Vector2 br = new Vector2(toDraw.Right, toDraw.Bottom);

            DrawLine(tl, tr);
            DrawLine(tr, br);
            DrawLine(br, bl);
            DrawLine(bl, tl);
        }

        /// <summary>
        /// Draws a black line from point A to point B.
        /// </summary>
        /// <param name="pointA">The point A.</param>
        /// <param name="pointB">The point B.</param>
        public static void DrawLine(Vector2 pointA, Vector2 pointB)
        {
            // Check that all points are valid.
            if (pointA == null || pointB == null ||
                float.IsNaN(pointA.X) || float.IsNaN(pointA.Y) ||
                float.IsNaN(pointB.X) || float.IsNaN(pointB.Y))
            {
                return;
            }

            if (GameWorld.spriteBatch != null)
            {
                Vector2 scale = new Vector2((pointA - pointB).Length() / 20f, 0.25f);
                Vector2 position = (pointA + pointB) / 2f;
                Vector2 origin = Vector2.One * 10f;

                float x = pointA.X - pointB.X;
                float y = pointA.Y - pointB.Y;

                GameWorld.spriteBatch.Draw(
                    TextureStatic.Get("blank"),
                    position * GetRatio(),
                    null,
                    Color.Black, 
                    RotationHelper.Vector2ToAngle(x, y) + MathHelper.PiOver2,
                    origin,
                    scale * GetRatio(), 
                    SpriteEffects.None,
                    1f);
            }
        }

        /// <summary>
        /// Draws the specified texture2D, assuming that it was drawn assuming the screen is
        /// exactly 1920 * 1080. Be sure to call spriteBatch.Begin() before this and spriteBatch.End()
        /// at the end of the series of Drawing calls.
        /// </summary>
        /// <param name="texture2D">The texture2D.</param>
        /// <param name="position">The position.</param>
        /// <param name="sourceRectangle">The source rectangle.</param>
        /// <param name="color">The color.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="effects">The effects.</param>
        /// <param name="layerDepth">The layer depth.</param>
        public static void Draw(
            Texture2D texture2D, 
            Vector2 position, 
            Rectangle? sourceRectangle, 
            Color color,
            float rotation, 
            Vector2 origin,
            Vector2 scale, 
            SpriteEffects effects, 
            float layerDepth)
        {
            if (GameWorld.spriteBatch != null)
            {
                GameWorld.spriteBatch.Draw(
                    texture2D, 
                    position * GetRatio(),
                    sourceRectangle, 
                    color, 
                    rotation, 
                    origin,
                    scale * GetRatio(),
                    effects,
                    layerDepth);
            }
        }

        /// <summary>
        /// Draws the specified texture2D, assuming that it was drawn assuming the screen is
        /// exactly 1920 * 1080. Be sure to call spriteBatch.Begin() before this and spriteBatch.End()
        /// at the end of the series of Drawing calls.
        /// </summary>
        /// <param name="texture2D">The texture2 D.</param>
        /// <param name="position">The position.</param>
        /// <param name="sourceRectangle">The source rectangle.</param>
        /// <param name="color">The color.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="effects">The effects.</param>
        /// <param name="layerDepth">The layer depth.</param>
        public static void Draw(
            Texture2D texture2D, 
            Vector2 position, 
            Rectangle? sourceRectangle, 
            Color color,
            float rotation, 
            Vector2 origin, 
            float scale, 
            SpriteEffects effects,
            float layerDepth)
        {
            Draw(
                texture2D, 
                position,
                sourceRectangle,
                color, 
                rotation, 
                origin, 
                new Vector2(scale),
                effects, 
                layerDepth);
        }

        /// <summary>
        /// Draws the specified texture2D, assuming that it was drawn assuming the screen is
        /// exactly 1920 * 1080. Be sure to call spriteBatch.Begin() before this and spriteBatch.End()
        /// at the end of the series of Drawing calls.
        /// </summary>
        /// <param name="texture2D">The texture2 D.</param>
        /// <param name="destinationRectangle">The destination rectangle.</param>
        /// <param name="sourceRectangle">The source rectangle.</param>
        /// <param name="color">The color.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="effects">The effects.</param>
        /// <param name="layerDepth">The layer depth.</param>
        public static void Draw(
            Texture2D texture2D, 
            Rectangle destinationRectangle,
            Rectangle? sourceRectangle,
            Color color, 
            float rotation, 
            Vector2 origin,
            SpriteEffects effects, 
            float layerDepth)
        {
            if (GameWorld.spriteBatch != null && texture2D != null)
            {
                GameWorld.spriteBatch.Draw(
                    texture2D,
                    new Rectangle(
                        (int)(destinationRectangle.X * GetRatio()),
                        (int)(destinationRectangle.Y * GetRatio()),
                        (int)(destinationRectangle.Width * GetRatio()),
                        (int)(destinationRectangle.Height * GetRatio())),
                    sourceRectangle, 
                    color,
                    rotation,
                    origin,
                    effects, 
                    layerDepth);
            }
        }
                
        /// <summary>
        /// Draws the specified string in the regular font, assuming that it was drawn assuming the screen is
        /// exactly 1920 * 1080. Be sure to call spriteBatch.Begin() before this and spriteBatch.End()
        /// at the end of the series of Drawing calls.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color of the string.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="spriteEffects">The sprite effects.</param>
        /// <param name="layerDepth">The layer depth.</param>
        public static void DrawString(
            string text, 
            Vector2 position, 
            Color color,
            float rotation, 
            Vector2 origin,
            float scale,
            SpriteEffects spriteEffects, 
            float layerDepth)
        {
            if (GameWorld.spriteBatch != null)
            {
                GameWorld.spriteBatch.DrawString(
                    font, 
                    text, 
                    position * GetRatio(),
                    color,
                    rotation,
                    origin, 
                    scale * GetRatio(), 
                    spriteEffects,
                    layerDepth);
            }
        }
    }
}
