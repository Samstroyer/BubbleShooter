using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using System;

[Flags]
enum Spawns : byte
{
    smallest = 0b_0000_0000, // 0
    smaller = 0b_0000_0001,  // 1
    small = 0b_0000_0010,    // 2
    medium = 0b_0000_0100,   // 4
    large = 0b_0000_1000,    // 8
    larger = 0b_0001_0000,   // 16
    largest = 0b_0010_0000   // 32

}

class Levels
{
    //Minsta bubblan man ska kunna spawna in i en bana är smaller.
    //Smallest ska inte kunna spawnas som en egen i start!
    Spawns spawns;
    List<Bubble> currLevelBubbles;

    public Levels(byte difficulty)
    {
        var a = (Spawns)Convert.ToInt16(difficulty);
        Console.WriteLine(a);
    }
}