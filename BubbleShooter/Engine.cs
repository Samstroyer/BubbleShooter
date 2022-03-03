using System;
using Raylib_cs;

class Engine
{
    Player p = new Player();

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

        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && !Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            p.Move(-1);
        }
        else if (!Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
        {
            p.Move(1);
        }
    }

    private void Mousebinds()
    {

    }
}