using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoTest.Base.Graphics;
using MonoTest.Base.Graphics.Animation;
using MonoTest.Base.State;
using MonoTest.Debug.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Debug
{
    public static class DebugManager
    {
        private static readonly HashSet<IDebug> debuggables = new();
        private static readonly Queue<Action> mainThreadDelegates = new Queue<Action>();
        private static readonly Queue<string> messageQueue = new Queue<string>();
        private static SpriteFont font;
        private static GraphicsDevice graphicsDevice;
        private static Game game;
        private static FpsDebugger fps;
        private static Vector2 origin = Vector2.Zero;
        private static Color debugColor = Color.Red;
        private static string originalTitle;
        private static bool forcedFps;
        public static void Initialize(Game game)
        {
            DebugManager.game = game;
            font = game.Content.Load<SpriteFont>("DebugFont");
            graphicsDevice = game.GraphicsDevice;
            originalTitle = game.Window.Title;
            GlobalState.WindowBounds = game.Window.ClientBounds;
            game.Deactivated += (sendder, ev) =>
            {
                game.IsFixedTimeStep = true;
                game.TargetElapsedTime = TimeSpan.FromSeconds(1 / 15.0f);
            };

            game.Activated += (sender, ev) =>
            {
                game.IsFixedTimeStep = forcedFps;
            };
            SetupCommands();
        }

        public static void AddDebuggable(IDebug debuggable)
        {
            debuggables.Add(debuggable);
        }

        private static void SetupCommands()
        {
            DebugTerminal.Commands.Add("help", ShowHelp);
            DebugTerminal.Commands.Add("clear", (_) => Console.Clear());
            DebugTerminal.Commands.Add("fps", SetFPS);
            DebugTerminal.Commands.Add("anim-speed", (args) => SetGlobalFloat(args, ref GlobalState.AnimationScale));
            DebugTerminal.Commands.Add("anim-scale", (args) => SetGlobalFloat(args, ref AnimatedSpriteDebug.GlobalSpriteScale));
            DebugTerminal.Commands.Add("cam-scale", (args) => SetGlobalFloat(args, ref GlobalState.MainCamera.Zoom));
        }
        private static void SetGlobalFloat(string[] args, ref float value)
        {
            if(args.Length >= 1 && float.TryParse(args[0], out var outVal))
            {
                value = outVal;
                Console.WriteLine("Animation speed has been set.");
            }
        }
        private static void ShowHelp(string[] arguments)
        {
            Console.WriteLine("--- Commands ---");
            foreach (var key in DebugTerminal.Commands.Keys)
            {
                Console.WriteLine($"Command: ${key}");
            }
            Console.WriteLine("-- End of help ---");
        }
        private static void SetFPS(string[] arguments)
        {
            if (arguments.Length >= 1 && float.TryParse(arguments[0], out var value))
            {
                GlobalState.GrphDevMngr.SynchronizeWithVerticalRetrace = false;
                ExecuteOnMainThread(GlobalState.GrphDevMngr.ApplyChanges);
                forcedFps = true;
                game.IsFixedTimeStep = true;
                game.TargetElapsedTime = TimeSpan.FromSeconds(1 / value);
            }
        }

        public static void ExecuteOnMainThread(Action action)
        {
            mainThreadDelegates.Enqueue(action);
        }
        // TODO better debugging system should be implemented
        public static void QueueMessage(string msg)
        {
            messageQueue.Enqueue(msg);
        }
        //TODO should call itself
        public static void DebugDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            while(mainThreadDelegates.Count > 0)
            {
                mainThreadDelegates.Dequeue()();
            }
            spriteBatch.Begin();
            var fpsString = $"FPS {Math.Ceiling(fps.FPS())}";
            game.Window.Title = $"{originalTitle} | {fpsString}";
            while(messageQueue.Count > 0)
            {
                fpsString += $"\n{messageQueue.Dequeue()}";
            }
            fps.Add(gameTime.ElapsedGameTime.TotalMilliseconds);
            spriteBatch.DrawString(font, fpsString, origin, debugColor);
            foreach (var debuggable in debuggables)
            {
                debuggable.DebugDraw(spriteBatch, gameTime);
            }
            spriteBatch.End();
        }

        public static void UpdateDebug(GameTime gametime)
        {
            foreach (var debuggable in debuggables)
            {
                debuggable.UpdateDebug(gametime);
            }
        }
    }
}
