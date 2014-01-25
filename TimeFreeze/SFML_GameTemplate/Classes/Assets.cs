using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;


public static class Assets
{
    public static Font font;

    public static Texture lightCircle = new Texture("Content/Lights/lightCircle.png");
    public static Texture sparkle = new Texture("Content/Lights/sparkle.png");
    public static Texture lightMask = new Texture("Content/Lights/lightMap.png");
    public static Texture easyArrow = new Texture("Content/Items/basicArrow.png");

    public static Color colorFloor = new Color(105, 105, 105);
    public static Color colorWall = new Color(0, 0, 255);
    public static Color colorWall2 = new Color(0, 0, 100);
    public static Color colorGoal = new Color(0, 255, 0);
    public static Color colorLightStone = new Color(100, 0, 100);

    public static Vector2f baseBlockSize = new Vector2f(16, 16);
    public static Vector2i worldOffSet = new Vector2i(0, 0);

    public static Color AcaOrange = new Color(255, 144, 0);
    public static Color AcaDarkOrange = new Color(255, 70, 0);
}

