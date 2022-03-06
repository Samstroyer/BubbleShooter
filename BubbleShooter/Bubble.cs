using System.Numerics;
using Raylib_cs;
using System;

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
    float x, y;
    States state;
    bool travelingRight;
    Random ran = new Random();
    float speed = 0.25f;
    float gravity = 0.25f;

    Vector2 boundaries = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

    public Bubble(short startX, short startY, byte startingState, bool turnRight)
    {
        x = startX;
        y = startY;
        state = (States)startingState;
        travelingRight = turnRight;
    }

    public void WhatSize()
    {
        Console.WriteLine("State: {0}", state);
    }

    public void Update()
    {
        Move();
        Show();
    }

    private void Move()
    {
        sbyte dir = travelingRight ? (sbyte)1 : (sbyte)-1;
        x += (dir * speed);

        y += gravity;
        gravity += 0.0005f;

        if (y >= boundaries.Y - CurrentTexture().height)
        {
            gravity = -0.65f;
        }

        if (x <= 0 || x >= boundaries.X - CurrentTexture().width)
        {
            travelingRight = !travelingRight;
        }
    }

    public dynamic Collision(Projectile p)
    {
        Rectangle projectileHitbox = p.Hitbox();
        bool collided = Raylib.CheckCollisionCircleRec(new Vector2(x + (CurrentTexture().width / 2), y), CurrentTexture().width / 2, projectileHitbox);

        if (collided)
        {
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
        return false;
    }

    private States DegradeState()
    {
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
            default:
                Console.WriteLine("Can not switch to smaller state");
                state = States.smallest;
                return States.smallest;
        }
    }

    private Texture2D CurrentTexture()
    {
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
            default:
                Console.WriteLine("This should not be seen!\nYou will instead get the smallest bubble in return...");
                return IMGLIB.bubbleImgArr[0];
        }
    }

    private void Show()
    {
        Raylib.DrawTexture(CurrentTexture(), (short)x, (short)y, Color.WHITE);
    }
}