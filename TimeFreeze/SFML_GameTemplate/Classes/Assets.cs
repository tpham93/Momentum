using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Audio;


public static class Assets
{
    public static Font font;

    public static Texture lightCircle = new Texture("Content/Lights/lightCircle.png");
    public static Texture sparkle = new Texture("Content/Lights/sparkle.png");
    public static Texture easyArrow = new Texture("Content/Items/basicArrow.png");

    public static Sound hitSound = new Sound(new SoundBuffer("Content/Sounds/HitSound.wav"));
    public static Sound sucessSound = new Sound(new SoundBuffer("Content/Sounds/succes.wav"));
    public static Sound nock = new Sound(new SoundBuffer("Content/Sounds/nock.wav"));

    public static Texture clockBody = new Texture("Content/Ingame/clockBody.png");
    public static Texture watchHandMin = new Texture("Content/Ingame/watchHandRed.png");
    public static Texture watchHandSec =  new Texture("Content/Ingame/watchHandBlack.png");
    public static Texture clockBodyInner = new Texture("Content/Ingame/bodyInner.png");

    public static Color colorFloor = new Color(105, 105, 105);
    public static Color colorWall = new Color(0, 0, 255);
    public static Color colorWall2 = new Color(0, 0, 100);
    public static Color colorGoal = new Color(0, 255, 0);
    public static Color colorLightStone = new Color(100, 0, 100);
    public static Color colorAccelerator = new Color(255, 100, 0);

    public static Vector2f baseBlockSize = new Vector2f(16, 16);
    public static Vector2i worldOffSet = new Vector2i(0, 0);

    public static Color AcaOrange = new Color(255, 144, 0);
    public static Color AcaDarkOrange = new Color(255, 70, 0);
}

