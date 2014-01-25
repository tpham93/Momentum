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

    public static Color colorFloor = new Color(105, 105, 105);
    public static Color colorWall = new Color(0, 0, 255);
    public static Color colorGoal = new Color(0, 255, 0);

    public static Vector2f baseBlockSize = new Vector2f(16, 16);
    public static Vector2i worldOffSet = new Vector2i(0, 0);
}

