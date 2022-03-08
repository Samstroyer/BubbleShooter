using Raylib_cs;

class Projectile
{
    //Rörelse är allt en projektil behöver!
    float speed = 1f;
    ushort x;
    float y;

    public Projectile(short startX, short startY)
    {
        //När man skjuter måste man veta vart spelaren står, det är med startX och startY
        x = (ushort)(startX + 23);
        y = startY - 15;
    }

    public void Update()
    {
        //Skjuts alltid uppåt 
        y -= speed;
    }

    public Rectangle Hitbox()
    {
        //Hitboxen av projektilen
        return new Rectangle(x, y, IMGLIB.projectileImg.width, IMGLIB.projectileImg.height);
    }

    public void Show()
    {
        //Rita projektilen
        Raylib.DrawTexture(IMGLIB.projectileImg, x, (int)y, Color.WHITE);
    }

    public bool OutOfScreen()
    {
        //Om den är utanför skärmen ta och radera den
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