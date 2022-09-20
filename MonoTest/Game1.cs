using FontStashSharp;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoTest.Base.Effects;
using MonoTest.Base.Scene;
using MonoTest.Base.State;
using MonoTest.Core;
using MonoTest.Core.Physics;
using MonoTest.Scenes;

using System.Collections.Generic;

namespace MonoTest
{
    public class Game1 : Game
    {
        public static Texture2D CurrentTexture { get; private set; }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private BaseScene mainMenu;
        Effect effect;
        HashSet<IUpdate> updateables = new();

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
            updateables.Add(SimulationFactory.GetDefault);

            foreach (var updateable in updateables)
            {
                updateable.Initialize();
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GlobalEffectState.TintShader = Content.Load<Effect>("BasicShader");
            GlobalEffectState.BoxBlurShader = Content.Load<Effect>("BoxBlur");
            GlobalEffectState.BitTex = Content.Load<Effect>("BitTex");
            CurrentTexture = Content.Load<Texture2D>("TextureSample");
            Debug.DebugManager.Initialize(this);
            //mainMenu = new ChipScene();
            //mainMenu = new TestScenes.CircleScene();
            mainMenu = new MainMenu();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
            foreach (var update in updateables)
            {
                update.Update(gameTime);
            }
            mainMenu.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            //_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //effect.CurrentTechnique.Passes[0].Apply();
            mainMenu.Draw(_spriteBatch, gameTime);
            Debug.DebugManager.DebugDraw(_spriteBatch, gameTime);
            //Debug.GraphicsDebugger.DrawDebugRect(_spriteBatch, Vector2.Zero, Vector2.One * 100);
            //_spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
