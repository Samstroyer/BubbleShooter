using System;
using Raylib_cs;

class Engine
{
    Player p = new Player();
    //Levels lvls = new Levels();
    //Bubble b = new Bubble(200, 400, 0b_0000_0100, true);


    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.GRAY);

            p.Run();

            Keybinds();
            Mousebinds();

            Raylib.EndDrawing();
        }
    }

    private void Keybinds()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            p.Shoot();
        }

        bool leftMovement = Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT);
        bool rightMovement = Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT);

        if (leftMovement && !rightMovement)
        {
            p.Move(-1);
        }
        else if (!leftMovement && rightMovement)
        {
            p.Move(1);
        }
    }

    private void Mousebinds()
    {

    }
}