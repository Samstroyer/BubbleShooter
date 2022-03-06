
using Raylib_cs;

class IMGLIB
{
    public static Texture2D projectileImg = Raylib.LoadTexture(@"Images\bullet.png");

    public static Texture2D[] bubbleImgArr = new Texture2D[7]
    {
        Raylib.LoadTexture(@"Images\Bubbles\20x20.png"),
        Raylib.LoadTexture(@"Images\Bubbles\24x24.png"),
        Raylib.LoadTexture(@"Images\Bubbles\32x32.png"),
        Raylib.LoadTexture(@"Images\Bubbles\40x40.png"),
        Raylib.LoadTexture(@"Images\Bubbles\48x48.png"),
        Raylib.LoadTexture(@"Images\Bubbles\56x56.png"),
        Raylib.LoadTexture(@"Images\Bubbles\64x64.png")
    };
}
