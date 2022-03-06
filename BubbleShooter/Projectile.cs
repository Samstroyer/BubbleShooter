using Raylib_cs;

class Projectile
{
    ushort x;
    float y;
    float speed = 1f;

    public Projectile(short startX, short startY)
    {
        x = (ushort)(startX + 23);
        y = startY - 15;
    }

    public void Update()
    {
        y -= speed;
    }

    public Rectangle Hitbox()
    {
        return new Rectangle(x, y, IMGLIB.projectileImg.width, IMGLIB.projectileImg.height);
    }

    public void Show()
    {
        Raylib.DrawTexture(IMGLIB.projectileImg, x, (int)y, Color.WHITE);
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