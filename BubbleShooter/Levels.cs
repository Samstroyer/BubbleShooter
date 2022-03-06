using System.Collections.Generic;
using System.Collections;
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

    Random randomGen = new Random();
    List<Bubble> currLevelBubbles = new List<Bubble>();
    Spawns spawns;
    byte diff;
    Player p = new Player();


    public Levels(byte difficulty)
    {
        diff = difficulty;
        spawns = (Spawns)difficulty;

        SpawnBubbles();
    }

    public void RunLevel()
    {
        p.Run();


        for (int i = currLevelBubbles.Count - 1; i >= 0; i--)
        {
            currLevelBubbles[i].Update();

            for (int j = p.shots.Count - 1; j >= 0; j--)
            {
                if (i >= 0 && j >= 0 && i < p.shots.Count && j < p.shots.Count)
                {
                    dynamic result = currLevelBubbles[i].Collision(p.shots[j]);

                    if (result is true)
                    {
                        currLevelBubbles.RemoveAt(i);
                        p.shots.RemoveAt(j);
                    }
                    else if (result is Bubble[])
                    {
                        currLevelBubbles.RemoveAt(i);
                        currLevelBubbles.Add(result[0]);
                        currLevelBubbles.Add(result[1]);
                        p.shots.RemoveAt(j);
                    }
                }
            }
        }
    }

    private void SpawnBubbles()
    {
        while ((int)diff > 0)
        {
            Vector2 spawnPos = new Vector2(randomGen.Next(100, Raylib.GetScreenWidth() - 100));
            bool spawnDir = randomGen.NextDouble() > 0.5 ? true : false;

            switch ((int)diff)
            {
                case >= 128:
                    currLevelBubbles.Add(new Bubble((short)spawnPos.X, (short)spawnPos.Y, 0b_0100_0000, spawnDir));
                    diff -= 128;
                    break;
                case >= 64:
                    currLevelBubbles.Add(new Bubble((short)spawnPos.X, (short)spawnPos.Y, 0b_0010_0000, spawnDir));
                    diff -= 64;
                    break;
                case >= 32:
                    currLevelBubbles.Add(new Bubble((short)spawnPos.X, (short)spawnPos.Y, 0b_0001_0000, spawnDir));
                    diff -= 32;
                    break;
                case >= 16:
                    currLevelBubbles.Add(new Bubble((short)spawnPos.X, (short)spawnPos.Y, 0b_0000_1000, spawnDir));
                    diff -= 16;
                    break;
                case >= 8:
                    currLevelBubbles.Add(new Bubble((short)spawnPos.X, (short)spawnPos.Y, 0b_0000_0100, spawnDir));
                    diff -= 8;
                    break;
                case >= 4:
                    currLevelBubbles.Add(new Bubble((short)spawnPos.X, (short)spawnPos.Y, 0b_0000_0010, spawnDir));
                    diff -= 4;
                    break;
                case >= 2:
                    currLevelBubbles.Add(new Bubble((short)spawnPos.X, (short)spawnPos.Y, 0b_0000_0001, spawnDir));
                    diff -= 2;
                    break;

                //0b_0000_0001 (int: 1) is not in the switch as you cant spawn the smallest bubble!le!

                default:
                    Console.WriteLine("Error has occured, you should not be seeing this message!");
                    Console.WriteLine("You CAN NOT spawn the smallest bubble size (can be the problem)");
                    Console.WriteLine("Setting 'diff' from {0} to {1}", diff, 0);
                    diff = 0;
                    break;
            }
        }
    }
}