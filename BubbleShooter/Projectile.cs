using Raylib_cs;

class Projectile
{
    int x;
    int y = 200;
    Texture2D projectileImg = Raylib.LoadTexture(@"Images\bullet.png");

    public Projectile(int startX)
    {
        x = startX;
    }

    public void Update()
    {

    }

    public void Show()
    {
        Raylib.DrawTexture(projectileImg, x, y, Color.WHITE);
    }

    public void Despawn()
    {
        Raylib.UnloadTexture(projectileImg);
    }
}