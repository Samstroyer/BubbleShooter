using System.Collections.Generic;
using System.Threading;
using System.Numerics;
using Raylib_cs;
using System;

class Player
{
    //Listan med alla skott man avfyrar
    public List<Projectile> shots = new List<Projectile>();

    //Rörelse och hastighet
    public const int y = 750;
    float speed = 0.2f;
    float x;

    //MISC: Texturer / bilder och hp 
    Texture2D devil;
    byte hp = 3;

    //När man skapar en player så händer allt det här.
    public Player()
    {
        x = (short)(Raylib.GetScreenWidth() / 2);
        Image sizer = Raylib.LoadImage(@"Images/devil.png");
        Raylib.ImageResize(ref sizer, 50, 50);
        devil = Raylib.LoadTextureFromImage(sizer);
        Raylib.UnloadImage(sizer);
    }

    public void Run()
    {
        //Det här är basically en GameLoop för spelaren och dens skott
        Show();
        Projectiles();
        Movement();

        //Det här stänger av spelet om man har lika med eller under 0 liv 
        if (hp <= 0)
        {
            Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), 200, Color.GRAY);
            Raylib.DrawText("You lost!", 100, 50, 100, Color.BLACK);
            Raylib.EndDrawing();
            Thread.Sleep(4000);
            Raylib.CloseWindow();
        }
    }

    //Här gör jag all rörelse i spelet och Keybinds
    private void Movement()
    {
        //Space för att skjuta, skulle kunna ändra till A t.ex om man har en GameBoy
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            //Shoot spawnar en projektil där man står
            Shoot();
        }

        //Jag har 2 bools som kollar om man klickar någon av de två vanligaste movement tangenterna
        //Det är (A || <-) eller (D || ->) och då ger den bools att man ska röra på sig
        bool leftMovement = Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT);
        bool rightMovement = Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT);

        //Här kollar den så att man bara håller in en knapp, man kan inte röra sig åt båda hållen samtidigt
        //Jag gör (ettHåll && inteAndraHållet) så att man inte håller in båda och ändå rör sig
        if (leftMovement && !rightMovement)
        {
            Move(-1);
        }
        else if (!leftMovement && rightMovement)
        {
            Move(1);
        }
    }

    private void Projectiles()
    {
        //För varje projektil, updatera, visa och om den är "utanför" spelet : ta bort den 
        for (int i = shots.Count - 1; i >= 0; i--)
        {
            shots[i].Update();
            shots[i].Show();

            if (shots[i].OutOfScreen())
            {
                shots.RemoveAt(i);
            }
        }
    }

    private void Show()
    {
        //Visa karaktären i "MainLoop", alltså rita bild på x och y
        Raylib.DrawTexture(devil, (int)x, y, Color.WHITE);
        //Skriver också ut ens HP man har kvar
        Raylib.DrawText($"HP: {hp}", 100, 50, 100, Color.BLACK);
    }

    private void Shoot()
    {
        //Skjuta är enkelt då man bara lägger in en ny projektil i projektiler listan
        //Den spawnar på dynamic x (alltså vart spelaren är) och static y vilket kanske kan göras global på ett sätt
        shots.Add(new Projectile((short)x, y));
    }

    private void Move(int dir)
    {
        //Det är enklare göra Move(1) eller Move(-1) än att skriva "left" eller "right"
        //Det fungerar då man antingen rör sig höger (positiv) eller vänster (negativ)
        float movement = dir * speed;
        x += movement;

        //If satsen är gjord så att man inte flyger ut
        if (x < 0)
        {
            x = 0;
        }
        else if (x >= Raylib.GetScreenWidth() - devil.width)
        {
            x = Raylib.GetScreenWidth() - devil.width;
        }
    }

    public void BubbleCollision(Bubble b)
    {
        //Här kollar den om man är död med inbyggd collision
        //Jag vet inte hur "computationally heavy" den är, men är rädd den saktar ner spelet mycket - speciellt senare levlar med många bubblor
        bool dead = Raylib.CheckCollisionCircleRec(new Vector2(b.x + (b.CurrentTexture().width / 2), b.y + b.CurrentTexture().height / 2), b.CurrentTexture().width / 2, Hitbox());

        //Om man dör så ska man förlora HP, man sätter bubblans position på ett annat ställe också. (Annars insta-dör man)
        if (dead)
        {
            hp--;
            b.ResetPos();
            Console.WriteLine("You took damage!");
        }
    }

    private Rectangle Hitbox()
    {
        //Man behöver CircleRecCollision saken, så enklare göra en funktion som kastar tillbaks rektangeln av bilden
        return new Rectangle(x, y, devil.width, devil.height);
    }
}