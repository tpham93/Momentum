using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;


class InGame : IGameState
{

    List<Objects> worldObjects;
    List<Objects> worldObjectsMovable;

    private Level level;
    Sprite[] floor;


    Texture menubarTexture;
    Sprite menubarSprite;

    Texture[] buttons;
    Sprite[] buttonSprites;
    Text levelName;

    private bool isPaused = false;

    public static LevelID levelId;

    RenderStates currentRenderState = ShaderManager.getRenderState(EShader.None);
   // EShader currentShader = EShader.None;

    private int[,] floorMap;
    Random random;

    public static bool isLevelDark= false;
    public static bool isLevelFreezed = true;

    public InGame()
    {
        random = new Random();
        floorMap = new int[Constants.WINDOWWIDTH / 16,Constants.WINDOWHEIGHT/ 16];
        for (int x = 0; x < Constants.WINDOWWIDTH / 16; x++)
        {
            for (int y = 0; y < Constants.WINDOWHEIGHT / 16; y++)
            {
                floorMap[x, y] = random.Next(3);


            }

        }
    }

    public void Initialize()
    {
        worldObjects = new List<Objects>();
        level = new Level();

        Level.Leveldata lvlData = level.generateLevel(levelId);

        worldObjects = lvlData.staticObj;
        worldObjectsMovable = lvlData.movableObj;

        isLevelDark = level.IsLevelDark;
        

        floor = new Sprite[3];
        floor[0] = new Sprite(Objects.objektTextures[0], new IntRect(0, 0, 16, 16));
        floor[1]= new Sprite(Objects.objektTextures[4], new IntRect(0, 0, 16, 16));
        floor[2] = new Sprite(Objects.objektTextures[5], new IntRect(0, 0, 16, 16));

        //Menubar ini
        menubarSprite = new Sprite(menubarTexture, new IntRect(0, 0, 255, 48));
        menubarSprite.Position = new Vector2f(Constants.WINDOWWIDTH - menubarTexture.Size.X, 0);
        menubarSprite.Color = new Color(menubarSprite.Color.R, menubarSprite.Color.G, menubarSprite.Color.B, (byte)150);
        buttonSprites = new Sprite[4];
        buttonSprites[0] = new Sprite(buttons[0], new IntRect(0, 0, 32, 32));
        buttonSprites[0].Position = new Vector2f(Constants.WINDOWWIDTH - 64 - 15, 7);
        buttonSprites[1] = new Sprite(buttons[1], new IntRect(0, 0, 32, 32));
        buttonSprites[1].Position = new Vector2f(Constants.WINDOWWIDTH - 64 - 15, 7);
        buttonSprites[2] = new Sprite(buttons[2], new IntRect(0, 0, 32, 32));
        buttonSprites[2].Position = new Vector2f(Constants.WINDOWWIDTH -32 -5, 7);
        buttonSprites[3] = new Sprite(buttons[2], new IntRect(0, 0, 32, 32));
        buttonSprites[3].Position = new Vector2f(Constants.WINDOWWIDTH - 96 - 5, 7);
        levelName = new Text("Level " + (int)(levelId+1), Assets.font);
        levelName.Position = new Vector2f(Constants.WINDOWWIDTH - menubarTexture.Size.X + 20, 1);
        levelName.Color = Color.White;
        

    }

    

    public void LoadContent(ContentManager manager)
    {
        menubarTexture = new Texture("Content/Ingame/menBar.png");
        buttons = new Texture[3];
        buttons[0] = new Texture("Content/Ingame/pause.png");
        buttons[1] = new Texture("Content/Ingame/play.png");
        buttons[2] = new Texture("Content/Ingame/back.png");

    }

    public EGameState Update(GameTime gameTime, RenderWindow window)
    {
        if (!isPaused)
        {
            level.update(gameTime);

        }


        if (Input.isClicked(Keyboard.Key.P))
            isPaused = !isPaused;



        //Mouse pause Game
        if (Input.leftClicked() && Input.currentMousePos.X  > Constants.WINDOWWIDTH - 64 - 15 && Input.currentMousePos.X  < Constants.WINDOWWIDTH - 32 - 15 && Input.currentMousePos.Y < 69)
            isPaused = !isPaused;
        if (Input.leftClicked() && Input.currentMousePos.X  > Constants.WINDOWWIDTH - 32  && Input.currentMousePos.X  < Constants.WINDOWWIDTH - 5 && Input.currentMousePos.Y < 69)
            Initialize();

        return EGameState.InGame;
    }

    public void Draw(GameTime gameTime, List<RenderTexture> targets)
    {
        
        //draw floor
        for (uint x = 0; x < Constants.WINDOWWIDTH / 16; x++)
        {
            for (uint y = 0; y < Constants.WINDOWHEIGHT / 16; y++)
            {


                if (floorMap[x, y] == 0)
                {
                    floor[0].Position = new SFML.Window.Vector2f(x * 16, y * 16);
                    targets.ElementAt(0).Draw(floor[0], currentRenderState);
                }
                else if (floorMap[x, y] == 1)
                {
                    floor[1].Position = new SFML.Window.Vector2f(x * 16, y * 16);
                    targets.ElementAt(0).Draw(floor[1], currentRenderState);
                }
                else
                {
                    floor[2].Position = new SFML.Window.Vector2f(x * 16, y * 16);
                    targets.ElementAt(0).Draw(floor[2], currentRenderState);
                }
            }
        }



        foreach (Objects obj in worldObjects)
        {
            obj.draw(targets, currentRenderState);
        }
        foreach (Objects obj in worldObjectsMovable)
        {
            obj.draw(targets, currentRenderState);
        }

        targets.ElementAt(0).Draw(menubarSprite);

        if (isPaused)
            targets.ElementAt(0).Draw(buttonSprites[1]);
        else
            targets.ElementAt(0).Draw(buttonSprites[0]);
        targets.ElementAt(0).Draw(buttonSprites[2]);
        targets.ElementAt(0).Draw(levelName);
    }
}