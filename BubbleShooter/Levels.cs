using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using System;

//Flags gör så att man kan select'a flera samtidigt, så jag kan spawna smaller och small med 0011 istället för att behöva säga 0001 && 0010
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
    //Minsta bubblan man ska kunna spawna in i en bana är "smaller".
    //"smallest" ska inte kunna spawnas som en egen i start!

    //currLevelBubbles är en lista på banans bubblor
    List<Bubble> currLevelBubbles = new List<Bubble>();
    Random randomGen = new Random();

    // Player skapas tyvärr varje level så man kan inte spara poäng på ett bra sätt
    Player p = new Player();

    //Spawns och diff är för storleken på alla bubblor i banan
    //Spawns är enum på alla storlekar
    //Diff är argumentet som skickas in i banans skapelse som sparas 
    Spawns spawns;
    byte diff;


    public Levels(byte difficulty)
    {
        //När man skapar banan så sparas difficulty argumentet
        diff = difficulty;

        //Spawns är så att den vet allt som ska spawnas, det görs med flags så man kan spawna flera olika typer
        spawns = (Spawns)difficulty;

        //SpawnBubbles() är spawna alla bubblor i banan från difficulty argumentet
        SpawnBubbles();
    }

    public int RunLevel()
    {
        //Kör spelaren i banan först
        p.Run();

        //För varje bubbla i banan...
        for (int i = currLevelBubbles.Count - 1; i >= 0; i--)
        {
            //... updatera varje bubbla och kolla om spelaren är i en bubbla...
            currLevelBubbles[i].Update();
            p.BubbleCollision(currLevelBubbles[i]);

            for (int j = p.shots.Count - 1; j >= 0; j--)
            {
                //... för varje bubbla kolla med varje projektil ...
                //... (om det finns mer än 0 bubblor) ...
                if (currLevelBubbles.Count > 0)
                {
                    //Får en konstig error men det går att hoppa över de random frames den uppstår med "try-catch"
                    try
                    {
                        //... kolliderar bubblan med en projektil?...
                        dynamic result = currLevelBubbles[i].Collision(p.shots[j]);
                        if (result is true)
                        {
                            //... om resultatet kommer tillbaka som true, ta bort bubblan och skottet (spawnar INTE 2 till bubblor) ...
                            currLevelBubbles.RemoveAt(i);
                            p.shots.RemoveAt(j);
                        }
                        else if (result is Bubble[])
                        {
                            //... om man istället får tillbaka 2 nya bubblor för att man spräckte en större, lägg in dem i levelns bubblor och ta bort skottet.
                            currLevelBubbles.RemoveAt(i);
                            currLevelBubbles.Add(result[0]);
                            currLevelBubbles.Add(result[1]);
                            p.shots.RemoveAt(j);
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        //Om den strular, hoppa bara över allt. Koden fungerar bra ändå :p
                        break;
                    }
                }
            }
        }
        //Här så ger den tillbaka en siffra beroende på om det finns bubblor kvar eller inte i banan
        return currLevelBubbles.Count > 0 ? 0 : 1;
    }

    private void SpawnBubbles()
    {
        //Medans man har bubblor kvar att spawna (skulle kunna sättas till >= 2 för ska ändå inte spawna 0b_0000_0000)
        while ((int)diff > 0)
        {
            //SpawnPositionen är random i sidan
            Vector2 spawnPos = new Vector2(randomGen.Next(100, Raylib.GetScreenWidth() - 100));

            //Hållet den flyger är också random
            bool spawnDir = randomGen.NextDouble() > 0.5 ? true : false;

            //Eftersom det är bytes och varje är en "power of 2" så kan man kolla på det sättet också!
            switch ((int)diff)
            {
                //Om det står största bubblan, spawna in en stor bubbla och ta bort dens värde
                case >= 128:
                    currLevelBubbles.Add(new Bubble((short)spawnPos.X, (short)spawnPos.Y, 0b_0100_0000, spawnDir));
                    diff -= 128;
                    break;
                //Om det står större bubbla, spawna in en stor bubbla och ta bort dens värde
                case >= 64:
                    currLevelBubbles.Add(new Bubble((short)spawnPos.X, (short)spawnPos.Y, 0b_0010_0000, spawnDir));
                    diff -= 64;
                    break;
                //ETC
                case >= 32:
                    currLevelBubbles.Add(new Bubble((short)spawnPos.X, (short)spawnPos.Y, 0b_0001_0000, spawnDir));
                    diff -= 32;
                    break;
                //ETC
                case >= 16:
                    currLevelBubbles.Add(new Bubble((short)spawnPos.X, (short)spawnPos.Y, 0b_0000_1000, spawnDir));
                    diff -= 16;
                    break;
                //ETC
                case >= 8:
                    currLevelBubbles.Add(new Bubble((short)spawnPos.X, (short)spawnPos.Y, 0b_0000_0100, spawnDir));
                    diff -= 8;
                    break;
                //ETC
                case >= 4:
                    currLevelBubbles.Add(new Bubble((short)spawnPos.X, (short)spawnPos.Y, 0b_0000_0010, spawnDir));
                    diff -= 4;
                    break;
                //ETC
                case >= 2:
                    currLevelBubbles.Add(new Bubble((short)spawnPos.X, (short)spawnPos.Y, 0b_0000_0001, spawnDir));
                    diff -= 2;
                    break;

                //0b_0000_0000 (int: 0) är inte med då man inte ska kunna spawn minsta (gula) bubblan

                //Om man lyckas strula till det så "void"as banan och allt (står iallafall varför "diff" dog med console.writeline())
                default:
                    Console.WriteLine("Error has occured, you should not be seeing this message!");
                    Console.WriteLine("Setting 'diff' from {0} to {1}", diff, 0);
                    diff = 0;
                    break;
            }
        }
    }
}