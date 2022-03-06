using Raylib_cs;
using System;

enum States
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
    short x, y;
    States state;
    bool dir;

    public Bubble(short startX, short startY, byte startingState, bool turnRight)
    {
        x = startX;
        y = startY;
        state = (States)startingState;
        dir = turnRight;
    }

    public void WhatSize()
    {
        Console.WriteLine("State: {0}", state);
    }

    public void Show()
    {
        switch ((byte)state)
        {
            case 0b_0000_0001:
                Raylib.DrawTexture(IMGLIB.bubbleImgArr[0], x, y, Color.WHITE);
                break;
            case 0b_0000_0010:
                Raylib.DrawTexture(IMGLIB.bubbleImgArr[1], x, y, Color.WHITE);
                break;
            case 0b_0000_0100:
                Raylib.DrawTexture(IMGLIB.bubbleImgArr[2], x, y, Color.WHITE);
                break;
            case 0b_0000_1000:
                Raylib.DrawTexture(IMGLIB.bubbleImgArr[3], x, y, Color.WHITE);
                break;
            case 0b_0001_0000:
                Raylib.DrawTexture(IMGLIB.bubbleImgArr[4], x, y, Color.WHITE);
                break;
            case 0b_0010_0000:
                Raylib.DrawTexture(IMGLIB.bubbleImgArr[5], x, y, Color.WHITE);
                break;
            case 0b_0100_0000:
                Raylib.DrawTexture(IMGLIB.bubbleImgArr[6], x, y, Color.WHITE);
                break;
            default:
                Console.WriteLine("This should not be happening!");
                break;
        }
    }
}