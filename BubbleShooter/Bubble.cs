using System.Numerics;
using Raylib_cs;
using System;

//En till lista på alla storlekar
enum States : byte
{
    smallest = 0b_0000_0000, // 0
    smaller = 0b_0000_0001,  // 1
    small = 0b_0000_0010,    // 2
    medium = 0b_0000_0100,   // 4
    large = 0b_0000_1000,    // 8
    larger = 0b_0001_0000,   // 16
    largest = 0b_0010_0000   // 32
}

class Bubble
{
    Random ran = new Random();
    //force, travelingRight, speed, x och y är för rörelse
    float force = 0.25f;
    bool travelingRight;
    float speed = 0.25f;
    public float x, y;
    //States är till igen för att läsa vilken storlek den har (vet att det finns fler och kunde ha använt mig av bara en, mycket enklare för mig såhär dock)
    States state;
    //Boundaries är speciell då jag inte vill säga funktioner hela tiden utan bara spara dem i början av allt
    Vector2 boundaries = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

    public Bubble(short startX, short startY, byte startingState, bool turnRight)
    {
        //När bubblan spawnas: Vart (x, y), storlek (state) och vilket håll den åker
        x = startX;
        y = startY;
        state = (States)startingState;
        travelingRight = turnRight;
    }

    public void WhatSize()
    {
        //Bara en temporär debug sak men kul att ha kvar i koden :p
        Console.WriteLine("State: {0}", state);
    }

    public void Update()
    {
        //Update är enklare att säga istället för Move och Show i "stora loopen" 
        Move();
        Show();
    }

    private void Move()
    {
        //Kolla om den rör sig höger eller vänster och apply till x positionen
        sbyte dir = travelingRight ? (sbyte)1 : (sbyte)-1;
        x += (dir * speed);

        //Force är en konstant ökande kraft!
        y += force;
        force += 0.0005f;

        //När den slår i botten så säts force till ett negativt tal så den "studsar" upp sen tillslut kommer ner igen.
        if (y >= boundaries.Y - CurrentTexture().height)
        {
            force = -0.65f;
        }

        //Studsa på kanter, och eftersom det är en bool så kan man bara "switcha" den till opposite istället för 2 if satser (en per vägg)
        if (x <= 0 || x >= boundaries.X - CurrentTexture().width)
        {
            travelingRight = !travelingRight;
        }
    }

    //Dynamic gör så att man kan returna olika typer och inte behöver bestämma sig för vilken i början
    public dynamic Collision(Projectile p)
    {
        //Kolla om bubblan och projektilen har kolliderat
        Rectangle projectileHitbox = p.Hitbox();
        bool collided = Raylib.CheckCollisionCircleRec(new Vector2(x + (CurrentTexture().width / 2), y), CurrentTexture().width / 2, projectileHitbox);

        if (collided)
        {
            //Om den har kolliderat, kolla om det är minsta storleken - då ta bort bubblan
            //Om det är inte minsta storleken så går man ner ett steg i storlek
            if (state == States.smallest)
            {
                return true;
            }
            else
            {
                States temp = DegradeState();
                return new Bubble[2] { new Bubble((short)x, (short)y, (byte)temp, true), new Bubble((short)x, (short)y, (byte)temp, false) };
            }
        }
        //"return false" är bara "default" så att inget speciellt händer 
        return false;
    }

    private States DegradeState()
    {
        //Här switchar man bubblans status och sen degraderar den ett steg
        switch (state)
        {
            case States.smaller:
                state = States.smallest;
                return States.smallest;
            case States.small:
                state = States.smaller;
                return States.smaller;
            case States.medium:
                state = States.small;
                return States.small;
            case States.large:
                state = States.medium;
                return States.medium;
            case States.larger:
                state = States.large;
                return States.large;
            case States.largest:
                state = States.larger;
                return States.larger;
            //En "error catch", man kan inte gå ner under eller över smallest. Så den tvingas tillbaka 
            default:
                Console.WriteLine("Can not switch to smaller state");
                state = States.smallest;
                return States.smallest;
        }
    }

    public void ResetPos()
    {
        //När man krashar in i spelaren med bubblan så ska den helst inte stanna i spelaren (hp drain)
        y = 200;
    }

    public Texture2D CurrentTexture()
    {
        //Kolla vilken textur bubblan ska ha
        switch ((byte)state)
        {
            case 0b_0000_0000:
                return IMGLIB.bubbleImgArr[0];
            case 0b_0000_0001:
                return IMGLIB.bubbleImgArr[1];
            case 0b_0000_0010:
                return IMGLIB.bubbleImgArr[2];
            case 0b_0000_0100:
                return IMGLIB.bubbleImgArr[3];
            case 0b_0000_1000:
                return IMGLIB.bubbleImgArr[4];
            case 0b_0001_0000:
                return IMGLIB.bubbleImgArr[5];
            case 0b_0010_0000:
                return IMGLIB.bubbleImgArr[6];
            //Här finns inget rätt eller fel i default, man ska inte hamna här men då får man iallafall minsta bubblan tillbaks (states borde ändra det till minsta också i DegradeState()) 
            default:
                Console.WriteLine("This should not be seen!\nYou will instead get the smallest bubble in return...");
                return IMGLIB.bubbleImgArr[0];
        }
    }

    private void Show()
    {
        //Rita ut texturen på x, y 
        Raylib.DrawTexture(CurrentTexture(), (short)x, (short)y, Color.WHITE);
    }
}