using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;


class InGame : IGameState
{

    List<Object> worldObjects;

    private Level level;
    Sprite floor;

    Texture menubarTexture;
    Sprite menubarSprite;

    Texture[] buttons;
    Sprite[] buttonSprites;
    Text levelName;

    private bool isPaused = false;

    public static LevelID levelId;

    

    public InGame()
    {
        
    }

    public void Initialize()
    {
        worldObjects = new List<object>();
        level = new Level();
        worldObjects = level.generateLevel(levelId);

        

        
        floor = new Sprite(Objects.objektTextures[0], new IntRect(0, 0, 16, 16));

        //Menubar ini
        menubarSprite = new Sprite(menubarTexture, new IntRect(0, 0, 230, 48));
        menubarSprite.Position = new Vector2f(Constants.WINDOWWIDTH - menubarTexture.Size.X, 0);
        menubarSprite.Color = new Color(menubarSprite.Color.R, menubarSprite.Color.G, menubarSprite.Color.B, (byte)150);
        buttonSprites = new Sprite[3];
        buttonSprites[0] = new Sprite(buttons[0], new IntRect(0, 0, 32, 32));
        buttonSprites[0].Position = new Vector2f(Constants.WINDOWWIDTH - 64 - 15, 7);
        buttonSprites[1] = new Sprite(buttons[1], new IntRect(0, 0, 32, 32));
        buttonSprites[1].Position = new Vector2f(Constants.WINDOWWIDTH - 64 - 15, 7);
        buttonSprites[2] = new Sprite(buttons[2], new IntRect(0, 0, 32, 32));
        buttonSprites[2].Position = new Vector2f(Constants.WINDOWWIDTH -32 -5, 7);
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
        if (Input.leftClicked() && Input.currentMousePos.X - window.Position.X > Constants.WINDOWWIDTH - 64 - 15 && Input.currentMousePos.X - window.Position.X < Constants.WINDOWWIDTH - 32 - 15 && Input.currentMousePos.Y - window.Position.Y < 69)
            isPaused = !isPaused;
        if (Input.leftClicked() && Input.currentMousePos.X - window.Position.X > Constants.WINDOWWIDTH - 32  && Input.currentMousePos.X - window.Position.X < Constants.WINDOWWIDTH - 5 && Input.currentMousePos.Y - window.Position.Y < 69)
            Initialize();

        return EGameState.InGame;
    }

    public void Draw(GameTime gameTime, RenderWindow window)
    {
        
        //draw floor
        for (uint x = 0; x < Constants.WINDOWWIDTH / 16; x++)
        {
            for (uint y = 0; y < Constants.WINDOWHEIGHT / 16; y++)
            {
                floor.Position = new SFML.Window.Vector2f(x * 16, y * 16);
                window.Draw(floor);
            }
        }



        foreach (Objects obj in worldObjects)
        {
            obj.draw(window);
        }

        window.Draw(menubarSprite);

        if (isPaused)
            window.Draw(buttonSprites[1]);
        else
            window.Draw(buttonSprites[0]);
        window.Draw(buttonSprites[2]);
        window.Draw(levelName);
    }
}