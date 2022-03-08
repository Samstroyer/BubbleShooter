using Raylib_cs;

namespace BubbleShooter
{
    class Program
    {
        static void Main(string[] args)
        {
            //init window så att ett fönster finns
            Setup();
            //skapa en ny "GameEngine" 
            Engine e = new Engine();
            //Kör motorn
            e.Run();
        }

        static void Setup()
        {
            //Starta ett 1000x800 fönster som heter "Bubble Struggle (SAMME-VERSION)"
            Raylib.InitWindow(1000, 800, "Bubble Struggle (SAMME-VERSION)");
        }
    }
}
