using System.Collections.Generic;
using Raylib_cs;
using System;

class Engine
{
    List<Levels> content = new List<Levels>();
    sbyte level = 0;

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
        while (!Raylib.WindowShouldClose() && level < content.Count)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.GRAY);

            if (content[level].RunLevel() > 0)
            {
                level++;
            }

            Raylib.EndDrawing();
        }
    }
}