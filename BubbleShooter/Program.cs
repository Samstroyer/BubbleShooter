using Raylib_cs;
using System;

namespace BubbleShooter
{
    class Program
    {
        static void Main(string[] args)
        {
            Setup();
            Engine e = new Engine();
            e.Run();
        }

        static void Setup()
        {
            Raylib.InitWindow(1000, 800, "Bubble Struggle (SAMME-VERSION)");
        }
    }
}
