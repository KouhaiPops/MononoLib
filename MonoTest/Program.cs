using System;

namespace MonoTest
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Debug.DebugTerminal.Initialize();
            using (var game = new Game1())
                game.Run();
        }
    }
}
