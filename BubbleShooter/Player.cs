using System;
using Raylib_cs;
using System.Collections.Generic;

class Player
{
    float speed = 0.2f;
    float x;
    public const int y = 750;
    List<Projectile> shots = new List<Projectile>();
    Texture2D devil;


    public Player()
    {
        x = Raylib.GetScreenWidth() / 2;
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
        foreach (Projectile projectile in shots)
        {
            projectile.Update();
            projectile.Show();
        }
    }

    private void Show()
    {
        Raylib.DrawTexture(devil, (int)x, y, Color.WHITE);
    }

    public void Shoot()
    {
        shots.Add(new Projectile(Convert.ToInt32(x)));
    }

    public void Move(int dir)
    {
        float movement = dir * speed;
        x += movement;
    }
}