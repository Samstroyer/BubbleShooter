using System.Collections.Generic;
using Raylib_cs;
using System;

class Player
{
    float speed = 0.2f;
    float x;
    public const int y = 750;
    List<Projectile> shots = new List<Projectile>();
    Texture2D devil;


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
        Show();
        Projectiles();
    }

    private void Projectiles()
    {
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
        Raylib.DrawTexture(devil, (int)x, y, Color.WHITE);
    }

    public void Shoot()
    {
        shots.Add(new Projectile((short)x, y));
    }

    public void Move(int dir)
    {
        float movement = dir * speed;
        x += movement;
    }
}