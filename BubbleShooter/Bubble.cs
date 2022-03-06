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
    float speed = 0.15f;

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
    }

    private void Show()
    {
        switch ((byte)state)
        {
            case 0b_0000_0001:
                Raylib.DrawTexture(IMGLIB.bubbleImgArr[0], (short)x, (short)y, Color.WHITE);
                break;
            case 0b_0000_0010:
                Raylib.DrawTexture(IMGLIB.bubbleImgArr[1], (short)x, (short)y, Color.WHITE);
                break;
            case 0b_0000_0100:
                Raylib.DrawTexture(IMGLIB.bubbleImgArr[2], (short)x, (short)y, Color.WHITE);
                break;
            case 0b_0000_1000:
                Raylib.DrawTexture(IMGLIB.bubbleImgArr[3], (short)x, (short)y, Color.WHITE);
                break;
            case 0b_0001_0000:
                Raylib.DrawTexture(IMGLIB.bubbleImgArr[4], (short)x, (short)y, Color.WHITE);
                break;
            case 0b_0010_0000:
                Raylib.DrawTexture(IMGLIB.bubbleImgArr[5], (short)x, (short)y, Color.WHITE);
                break;
            case 0b_0100_0000:
                Raylib.DrawTexture(IMGLIB.bubbleImgArr[6], (short)x, (short)y, Color.WHITE);
                break;
            default:
                Console.WriteLine("This should not be happening!");
                break;
        }
    }
}