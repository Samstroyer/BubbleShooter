using System.Collections.Generic;
using Raylib_cs;
using System;

class Engine
{
    List<Levels> content = new List<Levels>();
    sbyte level = 6;

    public Engine()
    {
        //Last bit must be 0 as you cant spawn the smallest size!
        content.Add(new Levels(0b_0000_0010)); // 0 
        content.Add(new Levels(0b_0000_0100)); // 1
        content.Add(new Levels(0b_0000_0110)); // 2
        content.Add(new Levels(0b_0001_0000)); // 3
        content.Add(new Levels(0b_0001_0100)); // 4
        content.Add(new Levels(0b_0010_0100)); // 5
        content.Add(new Levels(0b_0011_1100)); // 6
    }

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.GRAY);

            content[level].RunLevel();

            Raylib.EndDrawing();
        }
    }

    private void Keybinds()
    {
        content[0].RunLevel();
    }

    private void Mousebinds()
    {

    }
}