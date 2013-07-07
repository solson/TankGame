using System;

namespace TankGame
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new TankGame())
                game.Run();
        }
    }
#endif
}
