using Microsoft.Xna.Framework.Graphics;

namespace MonoTest.Base.Effects
{
    public interface IShader
    {
        Effect Effect { get; set; }

        void Apply();
    }
}