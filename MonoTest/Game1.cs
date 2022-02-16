using FontStashSharp;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoTest.Base.Effects;
using MonoTest.Base.Scene;
using MonoTest.Base.State;
using MonoTest.Chip.Engine;
using MonoTest.Scenes;

namespace MonoTest
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private BaseScene mainMenu;
        Effect effect;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            GlobalState.GrphDevMngr = _graphics;
            GlobalState.FontManager = new FontSystem();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GlobalEffectState.TintShader = Content.Load<Effect>("BasicShader");
            GlobalEffectState.BoxBlurShader = Content.Load<Effect>("BoxBlur");
            Debug.Stats.Initialize(this);
            //mainMenu = new ChipScene();
            mainMenu = new TestScenes.CircleScene();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
            mainMenu.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            //_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //effect.CurrentTechnique.Passes[0].Apply();
            mainMenu.Draw(_spriteBatch, gameTime);
            Debug.Stats.ShowStats(_spriteBatch, gameTime);
            //_spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
