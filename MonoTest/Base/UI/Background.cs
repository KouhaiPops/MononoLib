using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoTest.Base.Effects;
using MonoTest.Base.State;
using MonoTest.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Base.UI
{
    public unsafe class Background : BaseElement, IDrawable
    {
        public Texture2D BackgroundShape { get; set; }
        public Tint Tint { get; set; }
        public DrawActions.PreDrawAction PreDraw { get; set; } = DrawBase.PreDraw;
        public DrawActions.PostDrawAction PostDraw { get; set; } = DrawBase.PostDraw;

        public Background(GraphicsDevice device, Vector2 size, Vector2 position, Color color)
        {
            Transform.Size = size;
            Transform.Scale = new Vector2(0.5f);
            //Transform.Position = size;

            BackgroundShape = new Texture2D(device, (int)Transform.Size.X, (int)Transform.Size.Y);
            Tint = new Tint
            {
                Color = color
            };
            Color[] colors = new Color[(int)(Transform.Size.X * Transform.Size.Y)];
            Array.Fill(colors, Color.White);
            BackgroundShape.SetData(colors);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            //{
            //    Transform.OriginPivot = Component.OriginPivot.BottomRight;
            //}
            PreDraw(spriteBatch, gameTime, Tint);
            spriteBatch.Draw(BackgroundShape, Transform.Position, null, Color.Red, 0, Transform.Origin, Transform.Scale, SpriteEffects.None, 0);
            PostDraw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            //Position.X = 2;
            //Mouse.GetState().X
            //var mouse = Mouse.GetState();
            //var f = Math.Abs((float)Math.Sin(gameTime.TotalGameTime.TotalSeconds));
            //System.Diagnostics.Debug.WriteLine(f);
            //Position.X = f*GlobalState.WindowBounds.Width;
            //Position.Y = mouse.Y-(Size.Y/2);
            //Console.WriteLine("NULL");
            //throw new NotImplementedException();
        }

        public override void Initialize()
        {

        }
    }

    public class NewBG : UIBase
    {

    }
}
