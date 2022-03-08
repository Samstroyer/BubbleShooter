using System.Collections.Generic;
using Raylib_cs;

//Engine klassen skapas i Program.cs och är enbart här för att ta bort kod från Program.cs
class Engine
{
    //En lista av banor - byte version på leveln används då man inte behöver mer än 2^8 banor... (256)
    List<Levels> content = new List<Levels>();
    sbyte level = 0;

    public Engine()
    {
        //Jag vill inte att man ska kunna spawna den minsta storleken så det slutar alltid på 0
        //"level" som bara är en byte bevisas här vara effektiv då vi bara har 7 banor. En int32 skulle ta 4 bytes. Efficiency!
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
        //Run loopen som slutar om man blir klar med alla levlar (kommer inte på något bättre) (eller stänger fönstret)
        //Hur det här fungerar är att i If() argumenten har jag "GameLoop", så den ger körs samtidigt som den kollar om man är klar med leveln
        //(Den vet om den är klar då den ger tillbaka en variabel som kollas)
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