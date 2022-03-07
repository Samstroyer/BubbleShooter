using System.Collections.Generic;
using System.Threading;
using System.Numerics;
using Raylib_cs;
using System;

class Player
{
    public List<Projectile> shots = new List<Projectile>();
    public const int y = 750;
    float speed = 0.2f;
    Texture2D devil;
    byte hp = 3;
    float x;


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
        if (hp <= 0)
        {
            Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), 200, Color.GRAY);
            Raylib.DrawText("You lost!", 100, 50, 100, Color.BLACK);
            Raylib.EndDrawing();
            Thread.Sleep(4000);
            Raylib.CloseWindow();
        }
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
        Raylib.DrawText($"HP: {hp}", 100, 50, 100, Color.BLACK);
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

    public void BubbleCollision(Bubble b)
    {
        bool dead = Raylib.CheckCollisionCircleRec(new Vector2(b.x + (b.CurrentTexture().width / 2), b.y + b.CurrentTexture().height / 2), b.CurrentTexture().width / 2, Hitbox());
        if (dead)
        {
            hp--;
            b.ResetPos();
            Console.WriteLine("You took damage!");
        }
    }

    private Rectangle Hitbox()
    {
        return new Rectangle(x, y, devil.width, devil.height);
    }
}