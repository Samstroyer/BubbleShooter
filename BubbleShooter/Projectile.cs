using Raylib_cs;

class Projectile
{
    int x;
    float y;
    float speed = 0.2f;
    Texture2D projectileImg = Raylib.LoadTexture(@"Images\bullet.png");

    public Projectile(int startX, int startY)
    {
        x = startX;
        y = startY;
    }

    public void Update()
    {
        y -= speed;
    }

    public void Show()
    {
        Raylib.DrawTexture(projectileImg, x, (int)y, Color.WHITE);
    }

    public void Despawn()
    {
        Raylib.UnloadTexture(projectileImg);
    }

    public bool OutOfScreen()
    {
        if (y < 200)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}