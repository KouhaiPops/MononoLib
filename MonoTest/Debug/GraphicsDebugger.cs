using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Utils;
using MonoTest.Core.Collision;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Debug
{
    public static class GraphicsDebugger
    {
        private static HashSet<AABB> boxColliders = new HashSet<AABB>();
        private static Dictionary<int, Texture2D> debugLineCache = new Dictionary<int, Texture2D>();
        private static Dictionary<int, Texture2D> debugRectCache = new Dictionary<int, Texture2D>();

        public static bool ShouldDebug { get; set; } = true;

        public static void DebugAABB(AABB aabb)
        {
            boxColliders.Add(aabb);
        }
        public static void Show(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // TODO check boundingbox
            //BoundingBox
            foreach (var aabb in boxColliders)
            {
                
            }
        }


        public static void DrawDebugRect(SpriteBatch sprite, Vector2 position, Vector2 dimensions)
        {
            if (debugRectCache.TryGetValue(dimensions.GetHashCode(), out var tex))
            {
                sprite.DrawDebugTexture(position.X, position.Y, tex);
                return;
            }
            var values = GraphicPrimitives.Primitives.DrawRectangle(0, 0, (int)dimensions.X, (int)dimensions.Y, 3);
            tex = new Texture2D(sprite.GraphicsDevice, (int)dimensions.X+1, (int)dimensions.Y+1);
            var color = new Color[tex.Height * tex.Width];

            foreach(var (x, y) in values)
            {
                color[x + (y * tex.Width)] = Color.White;
            }
            tex.SetData(color);
            debugRectCache.Add(dimensions.GetHashCode(), tex);
            sprite.DrawDebugTexture(position.X, position.Y, tex);
            
        }
        public static void DrawDebugLine(SpriteBatch sprite, int startX, int startY, int endX, int endY)
        {
            var strideX = startX - endX;
            var strideY = startY - endY;
            var hashCode = HashCode.Combine(strideX, strideY);
            if(debugLineCache.TryGetValue(hashCode, out var tex))
            {
                sprite.DrawDebugTexture(startX, startY, tex);
                return;
            }
            var values = GraphicPrimitives.Primitives.DrawLine(startX, startY, endX, endY, 3);
            var x = Math.Abs(endX - startX);
            var y = Math.Abs(endY - startY);
            tex = new Texture2D(sprite.GraphicsDevice, x+1, y+4);
            var colors = new Color[((x+1) * (y+4))];
            foreach(var (_x, _y) in values)
            {
                colors[_x + (_y * tex.Width)] = Color.White;
            }
            tex.SetData(colors);
            debugLineCache.Add(hashCode, tex);
            sprite.DrawDebugTexture(startX, startY, tex);

        }

        private static void DrawDebugTexture(this SpriteBatch sprite, int posX, int posY, Texture2D tex)
        {
            sprite.Begin();
            sprite.Draw(tex,
                    new Vector2(posX, posY),
                    null,
                    Color.Green,
                    0,
                    Vector2.Zero,
                    Vector2.One,
                    SpriteEffects.None,
                    0);
            sprite.End();
        }

        private static void DrawDebugTexture(this SpriteBatch sprite, float posX, float posY, Texture2D tex)
        {
            sprite.Begin();
            sprite.Draw(tex,
                    new Vector2(posX, posY),
                    null,
                    Color.Green,
                    0,
                    Vector2.Zero,
                    Vector2.One,
                    SpriteEffects.None,
                    0);
            sprite.End();
        }
    }
}
