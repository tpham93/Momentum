using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using System.IO;


class InGame : IGameState
{

    public enum GameEventType
    {
        None,
        Win,
        Hourglass,
        X_Ray,
        Accelerate,
    }

    public struct LevelShapeData
    {
        public List<ObjectShape> worldObjectShape;
        public List<ObjectShape> worldMoveableObjectShape;
    }

    public struct ObjectShape
    {
        public Objects firstObject;
        public Shape2DSAT shape;
        public Objects.BlockType blockType;

        public ObjectShape(Objects firstObject, Shape2DSAT shape, Objects.BlockType blockType)
        {
            this.firstObject = firstObject;
            this.shape = shape;
            this.blockType = blockType;
        }
    }

    public struct Event
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
            if (isPair(o1, o2, Objects.BlockType.BALL, Objects.BlockType.ACCELERATOR))
            {
                return GameEventType.Accelerate;
            }


            return GameEventType.None;
        }

        private static bool isPair(Objects.BlockType o1, Objects.BlockType o2, Objects.BlockType a1, Objects.BlockType a2)
        {
            return o1 == a1 && o2 == a2 || o1 == a2 && o2 == a1;
        }
    }

    List<Objects> worldObjects;
    List<Objects> worldObjectsMoveable;
    List<ObjectShape> worldShapeObjects;
    List<ObjectShape> worldShapeObjectsMoveable;
    List<AbstractParticle> particles;

    static WallBlock stubWallBlock = new WallBlock(new Vector2f());

    private Level level;
    static int tutState = 0;
    Sprite[] floor;
    //public bool acc;


    Texture menubarTexture;
    Sprite menubarSprite;
    Text popUp = new Text("", Assets.font);
    float popUpTime = 0;
    float popUpBonusTime = 0;

    float tutAniTime = 0;

    bool up = true;

    Texture[] buttons;
    Sprite[] buttonSprites;
    Text levelName;

    public static bool isPaused = false;
    private int timeFreezeNum = 0;
    private Text timeFrTxt = new Text("", Assets.font);

    private bool playWinSound = false;

    public static LevelID levelId;

    Sprite arrowSprite = new Sprite();
    Sprite tutArrowSprite = new Sprite();

    private Text tutText = new Text("Click on the button above or press space", Assets.font);

    UiClock clock;

    bool drawArrow = false;
    bool failTimeStart = false;

    // RenderStates currentRenderState = RenderStates.Default;//ShaderManager.getRenderState(EShader.None);


    private int[,] floorMap;
    public static Random random = new Random();

    public static bool isLevelDark = false;
    public static bool isLevelFreezed = false;
    private bool hasWon;

    float tutFailTime;

    String[] levelText;
    private bool isSelected = false;


    private Objects selectedObject;

    Text levelDone;


    public InGame()
    {

        tutText.Position = new Vector2f(160, 530);
        clock = new UiClock();


        tutText.Position = new Vector2f(160, 530);        failTimeStart = false;

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

        arrowSprite = new Sprite(Assets.easyArrow);
        arrowSprite.Origin = new Vector2f(0, 32);
    }

    private String[] setStrings()
    {
        String[] lvT = new String[10];
        lvT[0] = "You're just quite below average";
        lvT[1] = "We're practically done here";
        lvT[2] = "Aplause aplause";
        lvT[3] = "You cheated, didn't you?";
        lvT[4] = "Computer will crash in \n3 \n\n\n2 \n\n\n1";
        lvT[5] = "Personal data is being uploaded to Acagamics e.V."; 
        lvT[6] = "Your Mother would be proud of you";
        lvT[7] = "Get the Momentum season pass NOW for just 19,99$";
        lvT[8] = "Absturz in \n3 \n2 \n1";
        lvT[9] = "Absturz in \n3 \n2 \n1";

        return lvT;

    }

    public void Initialize()
    {
        failTimeStart = false;
        tutFailTime = 0;
        playWinSound = false;
        if ((int)levelId == 0)
        {
            tutState = 0;
            tutText = new Text("Click on the button above or press space", Assets.font);
            tutText.Position = new Vector2f(160, 530);
            helpTime = 0;

        }
        if ((int)levelId > 0) tutState = 9001;
        tutArrowSprite = new Sprite(new Texture("Content/Items/tutArrow.png"), new IntRect(0, 0, 50, 50));
        tutArrowSprite.Position = new Vector2f(Constants.WINDOWWIDTH - 96 - 40, 65);

        worldObjects = new List<Objects>();
        level = new Level();
        hasWon = false;

        

        Level.Leveldata lvlData = level.generateLevel(levelId);

        worldObjects = lvlData.staticObj;
        worldObjectsMoveable = lvlData.movableObj;

        LevelShapeData levelShapeData = generateShapes(worldObjectsMoveable, worldObjects);

        worldShapeObjects = levelShapeData.worldObjectShape;
        worldShapeObjectsMoveable = levelShapeData.worldObjectShape;

        timeFreezeNum = lvlData.freezeNum;

        //acc = false;

        levelDone = new Text("", Assets.font);
        levelDone.Position = new Vector2f(100, 400);

        isLevelDark = true;//level.IsLevelDark;


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

    private void performPopUp(GameTime gameTime)
    {

        if (failTimeStart) tutFailTime += (float)gameTime.ElapsedTime.TotalMilliseconds;
        if (tutFailTime > 3000 && !hasWon && failTimeStart)
        {
            tutText.Position = new Vector2f(160, 530);
            tutText.DisplayedString = ("Click at the buttons above to reset level");
            tutArrowSprite.Position = new Vector2f(Constants.WINDOWWIDTH - 45, 65);
            failTimeStart = false;

        }
        if (hasWon && !failTimeStart)
        {
            tutText.Position = new Vector2f(400, 530);
            tutText.DisplayedString = ("");
            tutArrowSprite.Position = new Vector2f(Constants.WINDOWWIDTH - 45, 65);
            failTimeStart = true;

        }
        if ((int)levelId == 3 && hasWon && !failTimeStart)
        {

        }
        if (popUpTime < 2 + popUpBonusTime)
        {
            popUpTime += (float)gameTime.ElapsedTime.TotalSeconds;
            popUp.Position += new Vector2f(0, -0.5f);
            popUp.Color = new Color(popUp.Color.R, popUp.Color.G, popUp.Color.B, (byte)(255 - (255 / ((2 + popUpBonusTime - popUpTime) / (2 + popUpBonusTime)))));

        }

        else if (popUpTime >= 2)
        {
            popUpBonusTime = 0;
            popUp.DisplayedString = "";


        }
    }

    private void performTut(GameTime time)
    {


        if (tutAniTime < 400 && up)
        {
            tutAniTime += 10;
            tutArrowSprite.Position += new Vector2f(0, -0.5f);
        }
        else if (tutAniTime >= 400 && up == true)
            up = false;
        else if (tutAniTime < 0)
            up = true;
        else
        {
            tutAniTime -= 10;
            tutArrowSprite.Position -= new Vector2f(0, -0.5f);
        }
        if (helpTime > 5)
        {
            if (tutState == 2)
            {
                levelDone.DisplayedString = "";
                tutText.Position = new Vector2f(50, 530);
                tutText.DisplayedString = ("Click at the buttons above to reset or press enter to join Level 2");
                tutArrowSprite.Position = new Vector2f(Constants.WINDOWWIDTH - 45, 65);
                tutState++;


            }
            if(Input.isClicked(Keyboard.Key.Return))
            {
                tutState =9001;
            }

        }


    }

    public EGameState Update(GameTime gameTime, RenderWindow window)
    {
        performTut(gameTime);
        performPopUp(gameTime);
        if (hasWon)
        {
            if (!playWinSound)
            {
                playWinSound = true;
                Assets.sucessSound.Play();

            }

            helpTime += gameTime.ElapsedTime.TotalSeconds;
            levelDone.Position += new Vector2f(0, -0.5f);
            levelDone.DisplayedString = "Hurray \nLevel complete \n" + levelText[(int)levelId];
        }
        if (helpTime >= 5)
        {
            levelDone.DisplayedString = "";
        }
        if ((helpTime >= 5 ) && tutState >9000)
        {
            hasWon = false;
            levelDone.Position = new Vector2f(100, 400);
            levelId++;

            if (new DirectoryInfo(Constants.LEVELPATH).GetFiles().Length / 2 < (int)levelId + 1)
            {
                return EGameState.MainMenu;
            }

            helpTime = 0;
            Initialize();

        }

        

        if (Input.isClicked(Keyboard.Key.P))
        {
            Assets.nock.Play();
            isPaused = !isPaused;

        }

        if (Input.isClicked(Keyboard.Key.Space))
            if (timeFreezeNum > 0 && !isLevelFreezed)
            {
                if (tutState == 0)
                {
                    tutText.Position = new Vector2f(50, 530);
                    tutText.DisplayedString = ("Click on the ball, drag and release to set the direction and velocity");
                    tutState++;
                    popUpTime = 0;
                    popUp.Position = tutArrowSprite.Position - new Vector2f(70, -10);
                    popUpBonusTime = 2;
                    tutArrowSprite.Position = new Vector2f(69, 305);
                    popUp.DisplayedString = "Yeah,\nyou can\ncontrol time";
                }
                if (tutState == 2)
                {
                    
                    tutState++;
                    tutText.Position = new Vector2f(50, 530);
                    tutText.DisplayedString = ("Click on the ball, drag and release to set the direction and velocity");
                }
                timeFreezeNum--;
                Assets.nock.Play();
                isLevelFreezed = true;
            }
            else if (isLevelFreezed)
            {
                
                failTimeStart = true;
                drawArrow = false;
                isLevelFreezed = false;
                
            }

        //Mouse pause Game
        if (Input.leftClicked() && Input.currentMousePos.X > Constants.WINDOWWIDTH - 64 - 15 && Input.currentMousePos.X < Constants.WINDOWWIDTH - 32 - 15 && Input.currentMousePos.Y < 35)
        {
            isPaused = !isPaused;
            Assets.nock.Play();
        }
        //Mouse reset
        if (Input.leftClicked() && Input.currentMousePos.X > Constants.WINDOWWIDTH - 32 && Input.currentMousePos.X < Constants.WINDOWWIDTH - 5 && Input.currentMousePos.Y < 35)
        {
            Initialize();
            Assets.nock.Play();
        }
        //Mouse timefreeze Game
        if (Input.leftClicked() && Input.currentMousePos.X > Constants.WINDOWWIDTH - 96 - 30 && Input.currentMousePos.X < Constants.WINDOWWIDTH - 64 - 30 && Input.currentMousePos.Y < 35)
        {
            if (timeFreezeNum > 0 && !isLevelFreezed)
            {
                timeFreezeNum--;
                isLevelFreezed = true;
                Assets.nock.Play();
            }
            else if (isLevelFreezed)
            {
                drawArrow = false;
                isLevelFreezed = false;
                Assets.nock.Play();
                if (tutState == 3 && tutState < 9000 && !hasWon)
                {
                    failTimeStart = true;
                }
            }
            if (tutState == 0)
            {
                tutState++;
                tutText.Position = new Vector2f(50, 530);
                tutText.DisplayedString = ("Click on the ball, drag and release to set the direction and velocity");
                popUpTime = 0;
                popUp.Position = tutArrowSprite.Position - new Vector2f(70, -10);
                popUpBonusTime = 2;
                tutArrowSprite.Position = new Vector2f(69, 305);
                popUp.DisplayedString = "Yeah,\nyou can\ncontrol time";

            }

        }
        if (Input.isClicked(Keyboard.Key.Escape))
        {
            isLevelDark = false;
            isLevelFreezed = false;
            Assets.nock.Play();
            return EGameState.MainMenu;
        }
        if (!isPaused)
        {
            updateGame(gameTime, window);
        }


        clock.update(gameTime);





        return EGameState.InGame;
    }

    public EGameState updateGame(GameTime gameTime, RenderWindow window)
    {
        if (!isLevelFreezed)
        {



            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].update(gameTime);

                if (particles[i].lifetime <= 0)
                    particles.Remove(particles[i]);
            }

            for (int i = 0; i < worldObjectsMoveable.Count; ++i)
            {
                worldObjectsMoveable[i].update(gameTime);
            }

            for (int i = 0; i < worldObjectsMoveable.Count; ++i)
            {
                Shape2DSAT shapeI = worldObjectsMoveable[i].Shape;

                for (int j = i + 1; j < worldObjectsMoveable.Count; ++j)
                {
                    Shape2DSAT shapeJ = worldObjectsMoveable[j].Shape;
                    IntersectData iData = shapeI.intersects(shapeJ);

                    if (iData.Intersects)
                    {
                        Event e = new Event(worldObjectsMoveable[i], worldObjectsMoveable[j]);
                        if (e.handleCollision)
                        {
                            handleCollision(worldObjectsMoveable[i], worldObjectsMoveable[j], iData);
                            break;
                        }

                        switch (e.eventType)
                        {
                            case GameEventType.Hourglass:
                                if (worldObjectsMoveable[i].getType() == Objects.BlockType.HOURGLAS)
                                {
                                    Hourglass h = (Hourglass)worldObjectsMoveable[i];
                                    timeFreezeNum += h.getNum();

                                    worldObjectsMoveable.RemoveAt(i);
                                    --i;
                                    --j;
                                }
                                else if (worldObjectsMoveable[j].getType() == Objects.BlockType.HOURGLAS)
                                {
                                    Hourglass h = (Hourglass)worldObjectsMoveable[j];
                                    timeFreezeNum += h.getNum();
                                    worldObjectsMoveable.RemoveAt(j);
                                }
                                break;

                            case GameEventType.Accelerate:
                                if (worldObjectsMoveable[i].getType() == Objects.BlockType.ACCELERATOR)
                                {
                                    //acc = true;
                                    Accelerator a = (Accelerator)worldObjectsMoveable[i];


                                    worldObjectsMoveable.RemoveAt(i);
                                    --i;
                                    --j;
                                }
                                else if (worldObjectsMoveable[j].getType() == Objects.BlockType.HOURGLAS)
                                {
                                    Hourglass h = (Hourglass)worldObjectsMoveable[j];
                                    timeFreezeNum += h.getNum();
                                    worldObjectsMoveable.RemoveAt(j);
                                }
                                break;


                        }
                    }
                }


                for (int j = 0; j < worldShapeObjects.Count; ++j)
                {
                    Shape2DSAT shapeJ = worldShapeObjects[j].shape;
                    IntersectData iData = shapeI.intersects(shapeJ);

                    if (iData.Intersects)
                    {
                        Event e = new Event(worldObjectsMoveable[i], worldShapeObjects[j].firstObject);
                        if (e.handleCollision)
                        {
                            handleCollision(worldObjectsMoveable[i], worldShapeObjects[j].firstObject, iData);
                            break;
                        }

                        switch (e.eventType)
                        {
                            case GameEventType.Win:
                                if (!hasWon)
                                    for (int k = 0; k < 25; k++)
                                        particles.Add(new SparkleParticle(shapeI.Position));

                                hasWon = true;
                                break;
                        }
                    }
                }
            }
        }
        else if (isLevelFreezed)
        {
            //if (Input.leftPressed())
            {
                if (Input.leftClicked() && selectedObject == null)
                {
                    for (int i = 0; i < worldObjectsMoveable.Count; ++i)
                    {
                        if (worldObjectsMoveable[i].Shape.contains(Input.currentMousePos))
                        {
                            selectedObject = worldObjectsMoveable[i];


                            drawArrow = true;

                            //Console.Out.WriteLine("Selected");
                            popUp.DisplayedString = ("Ball selected");
                            popUp.Position = selectedObject.Position;
                            popUpTime = 0;
                            Assets.nock.Play();
                            isSelected = true;
                            if (tutState == 1)
                            {

                                tutState++;
                                tutText.Position = new Vector2f(80, 530);
                                tutText.DisplayedString = ("Click on the ball, drag and release to set the direction and velocity");
                                tutArrowSprite.Position = new Vector2f(639, 345);
                            }
                        }
                    }
                }
                else if (Input.leftReleased() && selectedObject != null)
                {
                    Vector2 velocity = new Vector2(Input.currentMousePos.X - selectedObject.Position.X, Input.currentMousePos.Y - selectedObject.Position.Y);
                    float length = velocity.Length();
                    velocity /= length;

                    selectedObject.Velocity = new Vector2f(velocity.X, velocity.Y) * Math.Min(length, Constants.MAXVELOCITY) / 30;
                    tutAniTime = 0;
                    tutText.DisplayedString = ("Click on the button above or press space to continue");

                    Console.Out.WriteLine("velocity set to " + length / 30);

                    Vector2 v = new Vector2(selectedObject.Velocity);
                    if (v != Vector2.Zero && v.LengthSquared() < (Constants.MINVELOCITY * Constants.MINVELOCITY / 90))
                    {
                        v = Vector2.Normalize(v);
                        v *= Constants.MINVELOCITY / 30;
                        selectedObject.Velocity = new Vector2f(v.X, v.Y);
                    }

                    //Console.Out.WriteLine("velocity set to " + new Vector2(selectedObject.Velocity).Length());
                    selectedObject = null;
                   // drawArrow = false;
                }
            }
            }

        if (selectedObject != null)
        {
            arrowSprite.Position = selectedObject.Position;
            Vector2 direction = new Vector2(Input.currentMousePos.X - selectedObject.Position.X, Input.currentMousePos.Y - selectedObject.Position.Y);
            arrowSprite.Scale = new Vector2f(Math.Min(direction.Length() / (99 + arrowSprite.Origin.X), Constants.MAXVELOCITY / (99 + arrowSprite.Origin.X)), 1);
            arrowSprite.Rotation = Help.toDegree((float)(Math.Atan2(selectedObject.Position.Y - Input.currentMousePos.Y, selectedObject.Position.X - Input.currentMousePos.X)));
            arrowSprite.Rotation += Help.toDegree((float)Math.PI);
        }

        return EGameState.InGame;
    }

    public void handleCollision(Objects objectsI, Objects objectsJ, IntersectData iData)
    {
        Shape2DSAT shapeI = objectsI.Shape;
        Shape2DSAT shapeJ = objectsJ.Shape;

        //kollision
        Shape2DSAT.handleCollision(iData, shapeI, shapeJ);


        //if (acc)
        //{
        //    Vector2f dir = new Vector2f(iData.Mtv.X, iData.Mtv.Y);
        //    Vector2 speedI = new Vector2(objectsI.Velocity);

        //    Vector2f speedHelp = new Vector2f(speedI.X, speedI.Y);
        //    float speedValueI = speedI.Length();

        //    speedI /= speedValueI;
        //    Vector2 newSpeedI = speedI - 2 * Vector2.Dot(speedI, iData.Mtv) * iData.Mtv;
        //    objectsI.Velocity = speedValueI * 2f * new Vector2f(newSpeedI.X, newSpeedI.Y);


        //    Vector2 speedJ = new Vector2(objectsJ.Velocity);
        //    float speedValueJ = speedJ.Length();
        //    speedI /= speedValueJ;
        //    Vector2 newSpeedJ = speedI - 2 * Vector2.Dot(speedJ, -iData.Mtv) * -iData.Mtv;
        //    objectsJ.Velocity = speedValueJ * 2f * new Vector2f(newSpeedJ.X, newSpeedJ.Y);
        //}
        //else
        {
            Vector2f dir = new Vector2f(iData.Mtv.X, iData.Mtv.Y);
            Vector2 speedI = new Vector2(objectsI.Velocity);
            Assets.hitSound.Play();
            Vector2f speedHelp = new Vector2f(speedI.X, speedI.Y);
            float speedValueI = speedI.Length();


            for (int i = 0; i < InGame.random.Next(5, 10); i++)
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
                    targets.ElementAt(0).Draw(floor[0]);
                }
                else if (floorMap[x, y] == 1)
                {
                    floor[1].Position = new SFML.Window.Vector2f(x * 16, y * 16);
                    targets.ElementAt(0).Draw(floor[1]);
                }
                else
                {
                    floor[2].Position = new SFML.Window.Vector2f(x * 16, y * 16);
                    targets.ElementAt(0).Draw(floor[2]);
                }
            }
        }



        foreach (Objects obj in worldObjects)
        {
            obj.draw(targets, gameTime);
        }
        foreach (Objects obj in worldObjectsMoveable)
        {
            obj.draw(targets, gameTime);
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

        if (drawArrow)
            targets.ElementAt(2).Draw(arrowSprite);


        targets.ElementAt(2).Draw(levelDone);
        targets.ElementAt(2).Draw(popUp);

        clock.draw(targets, gameTime);

        //Console.WriteLine(tutArrowSprite.Position);

        if (!(tutState > 9000))
        {
            targets.ElementAt(2).Draw(tutArrowSprite);

            if (tutState == 0 || tutState == 1 || tutState == 2 || tutState ==3)
            {
                targets.ElementAt(2).Draw(tutText);

            }

        }
    }

    public static LevelShapeData generateShapes(List<Objects> worldObjectsMoveable, List<Objects> worldObjects)
    {
        LevelShapeData output = new LevelShapeData();

        bool[,] blocks = new bool[Constants.WINDOWWIDTH / Constants.TILESIZE, Constants.WINDOWHEIGHT / Constants.TILESIZE];

        output.worldMoveableObjectShape = new List<ObjectShape>();
        output.worldObjectShape = new List<ObjectShape>();

        for (int i = 0; i < worldObjectsMoveable.Count; ++i)
        {
            output.worldMoveableObjectShape.Add(new ObjectShape(worldObjectsMoveable[i],worldObjectsMoveable[i].Shape, worldObjectsMoveable[i].getType()));
        }
        for (int i = 0; i < worldObjects.Count; ++i)
        {
            
            Vector2f pos = (worldObjects[i].Position - (new Vector2f(Constants.TILESIZE,Constants.TILESIZE)/2f)) / Constants.TILESIZE;
            if (worldObjects[i].getType() != Objects.BlockType.WALL && worldObjects[i].getType() != Objects.BlockType.LIGHTBLOCK)
            {
                output.worldObjectShape.Add(new ObjectShape(worldObjects[i],worldObjects[i].Shape, worldObjects[i].getType()));
                blocks[(int)pos.X, (int)pos.Y] = false;
            }
            else
            {
                blocks[(int)pos.X, (int)pos.Y] = true;
            }
        }

        int counter = 0;
        int[,] shapesMatrix = new int[Constants.WINDOWWIDTH / Constants.TILESIZE, Constants.WINDOWHEIGHT / Constants.TILESIZE];

        for (int y = 0; y <= blocks.GetUpperBound(1); ++y)
        {
            for (int x = 0; x <= blocks.GetUpperBound(0); ++x)
            {

                if (blocks[x, y])
                {
                    ++counter;
                    Shape2DSAT shape = null;

                    int size_x = 0;
                    int size_y = 0;

                    for (int i = x; i <= blocks.GetUpperBound(0) && blocks[i, y]; ++i)
                    {
                        ++size_x;
                    }
                    for (int i = y; i <= blocks.GetUpperBound(1) && blocks[x, i];  ++i)
                    {
                        ++size_y;
                    }

                    size_x = Math.Max(size_x - 1, 1);
                    size_y = Math.Max(size_y - 1, 1);

                    if (size_x > size_y)
                    {
                        for (int i = 0; i < size_x; ++i)
                        {
                            blocks[x+i, y] = false;
                            shapesMatrix[x+i, y] = 1;
                        }
                        Vector2 size = new Vector2(size_x * Constants.TILESIZE, Constants.TILESIZE);
                        shape = new PolygonShapeSAT( new Point((int)size.X, (int)size.Y), (new Vector2(x, y) * Constants.TILESIZE + size/2));

                    }
                    else
                    {
                        for (int i = 0; i < size_y; ++i)
                        {
                            blocks[x, y+i] = false;
                            shapesMatrix[x, y+i] = 1;
                        }
                        Vector2 size = new Vector2(Constants.TILESIZE, size_y * Constants.TILESIZE);
                        shape = new PolygonShapeSAT(new Point((int)size.X,(int)size.Y), (new Vector2(x, y) * Constants.TILESIZE + size/2));
                    }

                    output.worldObjectShape.Add(new ObjectShape(stubWallBlock,shape, Objects.BlockType.WALL));

                }



            }
        }

        return output;
    }
}