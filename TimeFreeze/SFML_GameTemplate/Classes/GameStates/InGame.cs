﻿using SFML.Graphics;
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
    private int timeFreezeNum = 0;
    

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
        timeFreezeNum = lvlData.freezeNum;

        isLevelDark = level.IsLevelDark;
        

        floor = new Sprite[3];
        floor[0] = new Sprite(Objects.objektTextures[0], new IntRect(0, 0, 16, 16));
        floor[1]= new Sprite(Objects.objektTextures[4], new IntRect(0, 0, 16, 16));
        floor[2] = new Sprite(Objects.objektTextures[5], new IntRect(0, 0, 16, 16));

        //Menubar ini
        menubarSprite = new Sprite(menubarTexture, new IntRect(0, 0, 255, 48));
        menubarSprite.Position = new Vector2f(Constants.WINDOWWIDTH - menubarTexture.Size.X, 0);
        menubarSprite.Color = new Color(menubarSprite.Color.R, menubarSprite.Color.G, menubarSprite.Color.B, (byte)150);
        buttonSprites = new Sprite[6];
        buttonSprites[0] = new Sprite(buttons[0], new IntRect(0, 0, 32, 32));
        buttonSprites[0].Position = new Vector2f(Constants.WINDOWWIDTH - 64 - 15, 7);
        buttonSprites[1] = new Sprite(buttons[1], new IntRect(0, 0, 32, 32));
        buttonSprites[1].Position = new Vector2f(Constants.WINDOWWIDTH - 64 - 15, 7);
        buttonSprites[2] = new Sprite(buttons[2], new IntRect(0, 0, 32, 32));
        buttonSprites[2].Position = new Vector2f(Constants.WINDOWWIDTH -32 -5, 7);
        buttonSprites[3] = new Sprite(buttons[3], new IntRect(0, 0, 32, 32));
        buttonSprites[3].Position = new Vector2f(Constants.WINDOWWIDTH - 96 - 30, 7);
        buttonSprites[4] = new Sprite(buttons[4], new IntRect(0, 0, 32, 32));
        buttonSprites[4].Position = new Vector2f(Constants.WINDOWWIDTH - 96 - 30, 7);
        levelName = new Text("Level " + (int)(levelId+1), Assets.font);
        levelName.Position = new Vector2f(Constants.WINDOWWIDTH - menubarTexture.Size.X + 20, 1);
        levelName.Color = Color.White;
        

    }

    

    public void LoadContent(ContentManager manager)
    {
        menubarTexture = new Texture("Content/Ingame/menBar.png");
        buttons = new Texture[5];
        buttons[0] = new Texture("Content/Ingame/pause.png");
        buttons[1] = new Texture("Content/Ingame/play.png");
        buttons[2] = new Texture("Content/Ingame/back.png");
        buttons[3] = new Texture("Content/Ingame/hourButton.png");
        buttons[4] = new Texture("Content/Ingame/hourButtonBW.png");

    }

    public EGameState Update(GameTime gameTime, RenderWindow window)
    {
        if (!isPaused)
        {
            updateGame(gameTime, window);
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

    public void updateGame(GameTime gameTime, RenderWindow window)
    {
        for (int i = 0; i < worldObjectsMovable.Count; ++i)
        {
            worldObjectsMovable[i].update(gameTime);
        }


        for (int i = 0; i < worldObjectsMovable.Count; ++i)
        {
            Shape2DSAT shapeI = worldObjectsMovable[i].Shape;

            for (int j = i + 1; j < worldObjectsMovable.Count; ++j)
            {
                Shape2DSAT shapeJ = worldObjectsMovable[j].Shape;
                IntersectData iData = shapeI.intersects(shapeJ);

                if(iData.Intersects)
                    Shape2DSAT.handleCollision(iData, shapeI, shapeJ);
            }
            for (int j = i + 1; j < worldObjects.Count; ++j)
            {
                Shape2DSAT shapeJ = worldObjects[j].Shape;
                IntersectData iData = shapeI.intersects(shapeJ);

                if (iData.Intersects)
                    Shape2DSAT.handleCollision(iData, shapeI, shapeJ);
            }
        }


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


        //Draw menubar
        targets.ElementAt(0).Draw(menubarSprite);

        if (isPaused)
            targets.ElementAt(0).Draw(buttonSprites[1]);
        else
            targets.ElementAt(0).Draw(buttonSprites[0]);
        targets.ElementAt(0).Draw(buttonSprites[2]);
        targets.ElementAt(0).Draw(levelName);
        if (isPaused)
            targets.ElementAt(0).Draw(buttonSprites[3]);
        else
            targets.ElementAt(0).Draw(buttonSprites[4]);
    }
}