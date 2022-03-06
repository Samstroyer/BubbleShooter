using System.Collections.Generic;
using Raylib_cs;
using System;

class Player
{
    float speed = 0.2f;
    float x;
    public const int y = 750;
    public List<Projectile> shots = new List<Projectile>();
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
        Movement();
    }

    private void Movement()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            Shoot();
        }

        bool leftMovement = Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT);
        bool rightMovement = Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT);

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

    private void Shoot()
    {
        shots.Add(new Projectile((short)x, y));
    }

    private void Move(int dir)
    {
        float movement = dir * speed;
        x += movement;
        if (x < 0)
        {
            x = 0;
        }
        else if (x >= Raylib.GetScreenWidth() - devil.width)
        {
            x = Raylib.GetScreenWidth() - devil.width;
        }
    }

    public Rectangle Hitbox()
    {
        return new Rectangle(x, y, devil.width, devil.height);
    }
}