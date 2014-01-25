using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;


class InGame : IGameState
{

    enum GameEventType
    {
        None,
        Win,
        Hourglass,
        X_Ray,
    }

    struct Event
    {
        public GameEventType eventType;
        public Objects objectI;
        public Objects objectJ;
        public bool handleCollision;

        public Event(Objects objectI, Objects objectJ)
        {
            this.objectI = objectI;
            this.objectJ = objectJ;
            this.eventType = getEventType(objectI.getType(), objectJ.getType());
            this.handleCollision = this.eventType == GameEventType.None;
        }

        public static GameEventType getEventType(Objects.BlockType o1, Objects.BlockType o2)
        {
            if (isPair(o1, o2, Objects.BlockType.BALL, Objects.BlockType.HOURGLAS))
            {
                return GameEventType.Hourglass;
            }
            if (isPair(o1, o2, Objects.BlockType.BALL, Objects.BlockType.GOAL))
            {
                return GameEventType.Win;
            }

            return GameEventType.None;
        }

        private static bool isPair(Objects.BlockType o1, Objects.BlockType o2, Objects.BlockType a1, Objects.BlockType a2)
        {
            return o1 == a1 && o2 == a2 || o1 == a2 && o2 == a1;
        }
    }

    List<Objects> worldObjects;
    List<Objects> worldObjectsMovable;
    List<AbstractParticle> particles;

    private Level level;
    Sprite[] floor;


    Texture menubarTexture;
    Sprite menubarSprite;

    Texture[] buttons;
    Sprite[] buttonSprites;
    Text levelName;

    public static bool isPaused = false;
    private int timeFreezeNum = 0;
    private Text timeFrTxt = new Text("", Assets.font);

    public static LevelID levelId;

    RenderStates currentRenderState = ShaderManager.getRenderState(EShader.None);


    private int[,] floorMap;
    public static Random random;

    public static bool isLevelDark = false;
    public static bool isLevelFreezed = false;
    private bool hasWon;

    String[] levelText;


    private Objects selectedObject;

    Text levelDone;




    public InGame()
    {
        hasWon = false;
        random = new Random();
        levelText = setStrings();
        floorMap = new int[Constants.WINDOWWIDTH / 16, Constants.WINDOWHEIGHT / 16];
        for (int x = 0; x < Constants.WINDOWWIDTH / 16; x++)
        {
            for (int y = 0; y < Constants.WINDOWHEIGHT / 16; y++)
            {
                floorMap[x, y] = random.Next(3);
            }
        }
    }

    private String[] setStrings()
    {
        String[] lvT = new String[5];
        lvT[1] = "Da bist du ja quasi schon durch";
        lvT[0] = "Du bist ganz OK";
        lvT[2] = "Aplaus Aplaus";
        lvT[3] = "Du hast doch gecheated";
        lvT[4] = "Absturz in \n3 \n2 \n1";

        return lvT;

    }

    public void Initialize()
    {
        worldObjects = new List<Objects>();
        level = new Level();

        Level.Leveldata lvlData = level.generateLevel(levelId);

        worldObjects = lvlData.staticObj;
        worldObjectsMovable = lvlData.movableObj;
        timeFreezeNum = lvlData.freezeNum;

        levelDone = new Text("", Assets.font);
        levelDone.Position = new Vector2f(100, 400);

        isLevelDark = false;//level.IsLevelDark;


        floor = new Sprite[3];
        floor[0] = new Sprite(Objects.objektTextures[0], new IntRect(0, 0, 16, 16));
        floor[1] = new Sprite(Objects.objektTextures[4], new IntRect(0, 0, 16, 16));
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
        buttonSprites[2].Position = new Vector2f(Constants.WINDOWWIDTH - 32 - 5, 7);
        buttonSprites[3] = new Sprite(buttons[3], new IntRect(0, 0, 32, 32));
        buttonSprites[3].Position = new Vector2f(Constants.WINDOWWIDTH - 96 - 30, 7);
        buttonSprites[4] = new Sprite(buttons[4], new IntRect(0, 0, 32, 32));
        buttonSprites[4].Position = new Vector2f(Constants.WINDOWWIDTH - 96 - 30, 7);
        levelName = new Text("Level " + (int)(levelId + 1), Assets.font);
        levelName.Position = new Vector2f(Constants.WINDOWWIDTH - menubarTexture.Size.X + 20, 1);
        levelName.Color = Color.White;
        timeFrTxt.Color = Color.White;
        timeFrTxt.Position = new Vector2f(Constants.WINDOWWIDTH - 75 - 27, 15);
        timeFrTxt.Scale = new Vector2f(0.7f, 0.7f);


        particles = new List<AbstractParticle>();

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

    double helpTime = 0;

    public EGameState Update(GameTime gameTime, RenderWindow window)


    {
        if (hasWon)
        {
            helpTime += gameTime.ElapsedTime.TotalSeconds;
            levelDone.Position += new Vector2f(0, -0.5f);
            levelDone.DisplayedString = " Hurray \n Level Geschafft \n" + levelText[(int)levelId] ;
        }

        if (helpTime >= 5)
        {
            hasWon = false;
            levelDone.Position = new Vector2f(100, 400);
            levelId++;
            helpTime = 0;
            Initialize();
            

        }
        
        if (Input.isClicked(Keyboard.Key.G))
        {
            timeFreezeNum++;
        }

        if (Input.isClicked(Keyboard.Key.P))
            isPaused = !isPaused;

        if (Input.isClicked(Keyboard.Key.Space))
            if (timeFreezeNum > 0 && !isLevelFreezed)
            {
                timeFreezeNum--;
                isLevelFreezed = true;
            }
            else if (isLevelFreezed)
            {
                isLevelFreezed = false;
            }

        //Mouse pause Game
        if (Input.leftClicked() && Input.currentMousePos.X > Constants.WINDOWWIDTH - 64 - 15 && Input.currentMousePos.X < Constants.WINDOWWIDTH - 32 - 15 && Input.currentMousePos.Y < 35)
            isPaused = !isPaused;
        //Mouse reset
        if (Input.leftClicked() && Input.currentMousePos.X > Constants.WINDOWWIDTH - 32 && Input.currentMousePos.X < Constants.WINDOWWIDTH - 5 && Input.currentMousePos.Y < 35)
            Initialize();
        //Mouse timefreeze Game
        if (Input.leftClicked() && Input.currentMousePos.X > Constants.WINDOWWIDTH - 96 - 30 && Input.currentMousePos.X < Constants.WINDOWWIDTH - 64 - 30 && Input.currentMousePos.Y < 35)
            if (timeFreezeNum > 0 && !isLevelFreezed)
            {
                timeFreezeNum--;
                isLevelFreezed = true;
            }
            else if (isLevelFreezed)
            {
                isLevelFreezed = false;
            }

        if (Input.isClicked(Keyboard.Key.Escape))
            return EGameState.MainMenu;

        if (!isPaused)
        {
            updateGame(gameTime, window);
        }


        return EGameState.InGame;
    }

    public EGameState updateGame(GameTime gameTime, RenderWindow window)
    {
        if (!isLevelFreezed)
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

                    if (iData.Intersects)
                    {
                        Event e = new Event(worldObjectsMovable[i], worldObjectsMovable[j]);
                        if (e.handleCollision)
                            handleCollision(worldObjectsMovable[i], worldObjectsMovable[j], iData);

                        switch (e.eventType)
                        {
                            case GameEventType.Hourglass:
                                if (worldObjectsMovable[i].getType() == Objects.BlockType.HOURGLAS)
                                {
                                    Hourglass h = (Hourglass)worldObjectsMovable[i];
                                    timeFreezeNum += h.getNum();

                                    worldObjectsMovable.RemoveAt(i);
                                    --i;
                                    --j;
                                }
                                else if (worldObjectsMovable[j].getType() == Objects.BlockType.HOURGLAS)
                                {
                                    Hourglass h = (Hourglass)worldObjectsMovable[j];
                                    timeFreezeNum += h.getNum();
                                    worldObjectsMovable.RemoveAt(j);
                                }
                                break;

                        }
                    }
                }


                for (int j = i+1; j < worldObjects.Count; ++j)
                {
                    Shape2DSAT shapeJ = worldObjects[j].Shape;
                    IntersectData iData = shapeI.intersects(shapeJ);

                    if (iData.Intersects)
                    {
                        Event e = new Event(worldObjectsMovable[i], worldObjects[j]);
                        if (e.handleCollision)
                            handleCollision(worldObjectsMovable[i], worldObjects[j], iData);

                        switch(e.eventType)
                        {
                            case GameEventType.Win:
                                hasWon = true;
                                break;
                        }
                    }
                }
            }
        }
        else if (isLevelFreezed && Input.leftClicked())
        {
            if (selectedObject == null)
            {
                for (int i = 0; i < worldObjectsMovable.Count; ++i)
                {
                    if (worldObjectsMovable[i].Shape.contains(Input.currentMousePos))
                    {
                        selectedObject = worldObjectsMovable[i];
                        Console.Out.WriteLine("Selected");
                        break;
                    }
                }
            }
            else
            {
                Vector2 velocity = new Vector2(Input.currentMousePos.X - selectedObject.Position.X, Input.currentMousePos.Y - selectedObject.Position.Y);
                float length = velocity.Length();
                velocity /= length;

                Console.Out.WriteLine("velocity set");
                selectedObject.Velocity = new Vector2f(velocity.X * 5, velocity.Y * 5);
                selectedObject = null;
            }
        }

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].update(gameTime);

            if (particles[i].lifetime <= 0)
                particles.Remove(particles[i]);
        }

        return EGameState.InGame;
    }

    public void handleCollision(Objects objectsI, Objects objectsJ, IntersectData iData)
    {
        Shape2DSAT shapeI = objectsI.Shape;
        Shape2DSAT shapeJ = objectsJ.Shape;

        //kollision
        Shape2DSAT.handleCollision(iData, shapeI, shapeJ);

        Vector2f dir = new Vector2f(iData.Mtv.X, iData.Mtv.Y);
        Vector2 speedI = new Vector2(objectsI.Velocity); 

        Vector2f speedHelp = new Vector2f(speedI.X, speedI.Y);
        float speedValueI = speedI.Length();


        for (int i = 0; i < InGame.random.Next(5,10); i++)
        {
            particles.Add(new SparkleParticle(shapeI.Position, -dir, Help.toVec2f(speedI), speedValueI));
        }



        speedI /= speedValueI;
        Vector2 newSpeedI = speedI - 2 * Vector2.Dot(speedI, iData.Mtv) * iData.Mtv;
        objectsI.Velocity = speedValueI * 0.6f * new Vector2f(newSpeedI.X, newSpeedI.Y);


        Vector2 speedJ = new Vector2(objectsJ.Velocity);
        float speedValueJ = speedJ.Length();
        speedI /= speedValueJ;
        Vector2 newSpeedJ = speedI - 2 * Vector2.Dot(speedJ, -iData.Mtv) * -iData.Mtv;
        objectsJ.Velocity = speedValueJ * 0.6f * new Vector2f(newSpeedJ.X, newSpeedJ.Y);
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
            obj.draw(targets, currentRenderState, gameTime);
        }
        foreach (Objects obj in worldObjectsMovable)
        {
            obj.draw(targets, currentRenderState, gameTime);
        }

        foreach (AbstractParticle p in particles)
            p.draw(gameTime, targets);

        //Draw menubar
        targets.ElementAt(2).Draw(menubarSprite);

        if (isPaused)
            targets.ElementAt(2).Draw(buttonSprites[1]);
        else
            targets.ElementAt(2).Draw(buttonSprites[0]);
        targets.ElementAt(2).Draw(buttonSprites[2]);
        targets.ElementAt(2).Draw(levelName);
        if (timeFreezeNum != 0)
            targets.ElementAt(2).Draw(buttonSprites[3]);
        else
            targets.ElementAt(2).Draw(buttonSprites[4]);
        timeFrTxt.DisplayedString = timeFreezeNum.ToString();
        targets.ElementAt(2).Draw(timeFrTxt);

        targets.ElementAt(0).Draw(levelDone, currentRenderState);
    }
}